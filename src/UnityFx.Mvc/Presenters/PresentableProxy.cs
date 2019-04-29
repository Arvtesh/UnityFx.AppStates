﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Defines a wrapper data/services for a <see cref="IViewController"/>.
	/// </summary>
	/// <remarks>
	/// We want <see cref="IViewController"/> interface to be as minimalistic as possible. That's why we need to store
	/// controller context outside of actual controller. This class manages the controller created, provides its context
	/// (via <see cref="IPresentContext"/> interface) and serves as a proxy between the controller and
	/// <see cref="IPresentService"/> implementation.
	/// </remarks>
	internal class PresentableProxy : TreeListNode<PresentableProxy>, IPresentContext, IPresentResult, ICommandTarget
	{
		#region data

		private enum State
		{
			Initialized,
			Presented,
			Active,
			Dismissed,
			Disposed
		}

		private readonly PresentService _presenter;
		private readonly IServiceProvider _serviceProvider;
		private readonly IDisposable _scope;
		private readonly IPresentable _controller;
		private readonly PresentOptions _presentOptions;
		private readonly string _name;
		private readonly int _id;

		private State _state;

		#endregion

		#region interface

		internal PresentableProxy(PresentService presentManager, PresentableProxy parent, Type controllerType, PresentArgs args, int id)
			: base(parent)
		{
			Debug.Assert(presentManager != null);
			Debug.Assert(controllerType != null);
			Debug.Assert(args != null);

			_presenter = presentManager;
			_serviceProvider = presentManager.ServiceProvider;
			_presentOptions = args.Options;
			_name = Utility.GetControllerTypeId(controllerType);
			_id = id;

			// Controller should be created after the proxy has been initialized.
			try
			{
				if (_serviceProvider.GetService(typeof(IViewControllerFactory)) is IViewControllerFactory controllerFactory)
				{
					_scope = controllerFactory.CreateControllerScope(ref _serviceProvider);
					_controller = (IPresentable)controllerFactory.CreateController(controllerType, this, args);
				}
				else
				{
					_controller = (IPresentable)ActivatorUtilities.CreateInstance(_serviceProvider, controllerType, this, args);
				}
			}
			catch
			{
				_scope?.Dispose();
				throw;
			}
		}

		internal void Present()
		{
			_controller.LoadViewAsync();
			_controller.Dismissed += OnDismissed;

			if (_controller.IsViewLoaded)
			{
				OnPresented();
			}
			else
			{
				_controller.Presented += OnPresented;
			}
		}

		internal bool TryActivate()
		{
			if (_state == State.Presented)
			{
				_state = State.Active;

				if (_controller is IPresentableEvents controllerEvents)
				{
					controllerEvents.OnActivate();
				}

				return true;
			}

			return false;
		}

		internal bool TryDeactivate()
		{
			if (_state == State.Active)
			{
				_state = State.Presented;

				if (_controller is IPresentableEvents controllerEvents)
				{
					controllerEvents.OnDeactivate();
				}

				return true;
			}

			return false;
		}

		internal bool TryDismiss()
		{
			if (_state == State.Presented)
			{
				_controller.Dismiss();
				return true;
			}

			return false;
		}

		internal void DismissChildControllers()
		{
			var children = GetChildControllers();

			if (children != null)
			{
				foreach (var controller in children)
				{
					controller.Dismiss();
				}
			}
		}

		#endregion

		#region IPresentableContext

		public int Id => _id;

		public bool IsActive => _state == State.Active;

		public bool IsModal => (_presentOptions & PresentOptions.Modal) != 0;

		#endregion

		#region IPresenter

		public IPresentResult Present(Type controllerType)
		{
			Debug.Assert(!IsDismissed);
			return _presenter.Present(this, controllerType, PresentArgs.Default);
		}

		public IPresentResult Present(Type controllerType, PresentArgs args)
		{
			Debug.Assert(!IsDismissed);
			return _presenter.Present(this, controllerType, args);
		}

		public IPresentResult<TController> Present<TController>() where TController : class, IPresentable
		{
			Debug.Assert(!IsDismissed);
			return _presenter.Present<TController>(this, PresentArgs.Default);
		}

		public IPresentResult<TController> Present<TController>(PresentArgs args) where TController : class, IPresentable
		{
			Debug.Assert(!IsDismissed);
			return _presenter.Present<TController>(this, args);
		}

		#endregion

		#region IPresentResult

		public event EventHandler<AsyncCompletedEventArgs> Presented { add => _controller.Presented += value; remove => _controller.Presented -= value; }

		public bool IsPresented => _controller.IsPresented;

		public IPresentable Controller => _controller;

		#endregion

		#region IDismissable

		public event EventHandler Dismissed { add => _controller.Dismissed += value; remove => _controller.Dismissed -= value; }

		public bool IsDismissed => _controller.IsDismissed;

		public void Dismiss() => _controller.Dismiss();

		#endregion

		#region ICommandTarget

		public bool InvokeCommand(string commandName, object args)
		{
			Debug.Assert(_state == State.Presented || _state == State.Active);

			if (_controller is ICommandTarget cmdTarget)
			{
				return cmdTarget.InvokeCommand(commandName, args);
			}

			return false;
		}

		#endregion

		#region IServiceProvider

		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(IPresentContext) || serviceType == typeof(IPresenter) || serviceType == typeof(IServiceProvider))
			{
				return this;
			}

			return _serviceProvider.GetService(serviceType);
		}

		#endregion

		#region IDisposable

		public void Dispose()
		{
			if (_state != State.Disposed)
			{
				_state = State.Disposed;

				try
				{
					_controller.Dispose();
				}
				finally
				{
					_scope?.Dispose();
				}
			}
		}

		#endregion

		#region implementation

		private void OnPresented(object sender, AsyncCompletedEventArgs args)
		{
			_controller.Presented -= OnPresented;

			if (_state == State.Initialized)
			{
				if (args.Error == null && !args.Cancelled)
				{
					OnPresented();
				}
			}
		}

		private void OnDismissed(object sender, EventArgs e)
		{
			_controller.Dismissed -= OnDismissed;

			if (_state != State.Dismissed && _state != State.Disposed)
			{
				_state = State.Dismissed;
				_presenter.Dismissed(this);
			}
		}

		private void OnPresented()
		{
			Debug.Assert(_state == State.Initialized);

			_state = State.Presented;
			_presenter.Presented(this, _presentOptions);
		}

		private Stack<PresentableProxy> GetChildControllers()
		{
			var result = default(Stack<PresentableProxy>);
			var nextState = Next;

			while (nextState != null)
			{
				if (nextState.Parent == this)
				{
					if (result == null)
					{
						result = new Stack<PresentableProxy>();
					}

					result.Push(nextState);
				}

				nextState = nextState.Next;
			}

			return result;
		}

		#endregion
	}
}

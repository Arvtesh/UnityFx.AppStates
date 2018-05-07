﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	/// <summary>
	/// Enumerates state presentation modes.
	/// </summary>
	public enum AppStatePresentationMode
	{
		/// <summary>
		/// Default (non-modal popup).
		/// </summary>
		Default,

		/// <summary>
		/// Exclusive (covers whole screen).
		/// </summary>
		Exclusive,

		/// <summary>
		/// Modal popup (appears on top of non-modal views).
		/// </summary>
		Modal,
	}

	/// <summary>
	/// A generic application state.
	/// </summary>
	/// <remarks>
	/// By design an application flow is a sequence of state switches. A state may represent a single screen,
	/// a dialog or a menu. States are supposed to be as independent as possible. Only one state may be active
	/// (i.e. process user input) at time, but unlimited number of states may exist (be rendered on the screen
	/// and execute their code) at the same time.
	/// </remarks>
	/// <seealso href="http://gameprogrammingpatterns.com/state.html"/>
	/// <seealso href="https://en.wikipedia.org/wiki/State_pattern"/>
	public class AppState : IDisposable
	{
		#region data

		private enum AppStateState
		{
			Created,
			Pushed,
			Popped,
			Disposed
		}

		private readonly AppStateService _stateManager;
		private readonly AppViewController _controller;
		private readonly AppState _parentState;

		private readonly TraceSource _console;
		private readonly PresentArgs _args;

		private AppStateState _state;
		private bool _isActive;

		#endregion

		#region interface

		internal bool IsPushed => _state == AppStateState.Pushed;
		internal AppStateService StateManager => _stateManager;
		internal IAppViewManager ViewManager => _stateManager.Shared.ViewManager;
		internal IAppControllerFactory ControllerFactory => _stateManager.Shared.ControllerFactory;

		internal AppViewController TmpController { get; set; }
		internal PresentOptions TmpControllerOptions { get; set; }
		internal object TmpControllerArgs { get; set; }

		internal AppState(AppStateService stateManager, AppState parentState, Type controllerType, PresentArgs args)
		{
			Debug.Assert(stateManager != null);
			Debug.Assert(controllerType != null);

			_stateManager = stateManager;
			_parentState = parentState;
			_args = args;
			_console = stateManager.TraceSource;
			_controller = stateManager.Shared.ControllerFactory.CreateController(controllerType, this);
		}

		internal IAsyncOperation Push(IAppStateOperationInfo op)
		{
			Debug.Assert(_state == AppStateState.Created);

			_console.TraceData(TraceEventType.Verbose, op.OperationId, "PushState " + _controller.Id);
			_stateManager.States.Add(this);
			_state = AppStateState.Pushed;

			return _controller.View.Load();
		}

		internal void Pop(IAppStateOperationInfo op)
		{
			if (_state == AppStateState.Pushed)
			{
				_console.TraceData(TraceEventType.Verbose, op.OperationId, "PopState " + _controller.Id);
				_stateManager.States.Remove(this);
				_state = AppStateState.Popped;
			}

			Dispose();
		}

		internal void Activate(IAppStateOperationInfo op)
		{
			Debug.Assert(_state == AppStateState.Pushed);

			if (!_isActive && (_parentState == null || _parentState.IsActive))
			{
				_console.TraceEvent(TraceEventType.Verbose, op.OperationId, "ActivateState " + _controller.Id);

				_isActive = true;
				_controller.InvokeOnActivate();
			}
		}

		internal void Deactivate(IAppStateOperationInfo op)
		{
			Debug.Assert(_state == AppStateState.Pushed);

			if (_isActive)
			{
				_console.TraceData(TraceEventType.Verbose, op.OperationId, "DeactivateState " + _controller.Id);

				try
				{
					_controller.InvokeOnDeactivate();
				}
				finally
				{
					_isActive = false;
				}
			}
		}

		internal AppView GetPrevView()
		{
			var i = _stateManager.States.Count - 1;

			while (i >= 0)
			{
				if (_stateManager.States[i] != this)
				{
					--i;
				}
				else
				{
					break;
				}
			}

			if (i >= 0)
			{
				return _stateManager.States[i].View;
			}

			return _parentState?.GetPrevView();
		}

		#endregion

		#region IAppState

		/// <summary>
		/// Gets the state type identifier.
		/// </summary>
		public string Id => _controller.Id;

		/// <summary>
		/// Gets the state path.
		/// </summary>
		public string Path
		{
			get
			{
				var localPath = '/' + _controller.Id;

				if (_parentState == null)
				{
					return _parentState?.Path + localPath ?? localPath;
				}

				return _parentState.Path;
			}
		}

		/// <summary>
		/// Gets the state creation arguments.
		/// </summary>
		public PresentArgs CreationArgs => _args;

		/// <summary>
		/// Gets a deeplink representing this state.
		/// </summary>
		public Uri Deeplink
		{
			get
			{
				var uriBuilder = new UriBuilder(_stateManager.Shared.DeeplinkScheme, _stateManager.Shared.DeeplinkDomain)
				{
					Path = Path
				};

				if (_parentState != null)
				{
					uriBuilder.Fragment = _controller.Id;
				}

				return uriBuilder.Uri;
			}
		}

		/// <summary>
		/// Gets a view instance attached to the state.
		/// </summary>
		public AppView View => _controller.View;

		/// <summary>
		/// Gets a controller attached to the state.
		/// </summary>
		public AppViewController Controller => _controller;

		/// <summary>
		/// Gets a value indicating whether the state is active.
		/// </summary>
		public bool IsActive => _isActive;

		#endregion

		#region tt

		/// <summary>
		/// Gets a collection of the state's children.
		/// </summary>
		public IReadOnlyCollection<AppState> Substates
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Gets a parent state.
		/// </summary>
		public AppState Parent => _parentState;

		/// <summary>
		/// Enumerates child states.
		/// </summary>
		/// <param name="states">A collection to store results to.</param>
		/// <exception cref="ObjectDisposedException">Thrown if the state is disposed.</exception>
		public void GetSubstates(ICollection<AppState> states)
		{
			// TODO
		}

		/// <summary>
		/// Enumerates child states recursively.
		/// </summary>
		/// <param name="states">A collection to store results to.</param>
		public void GetSubstatesRecursive(ICollection<AppState> states)
		{
			// TODO
		}

		/// <summary>
		/// Removes the state from the stack.
		/// </summary>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ObjectDisposedException">Thrown if the state is disposed.</exception>
		public IAsyncOperation DismissAsync()
		{
			ThrowIfDisposed();
			return _stateManager.PopStateAsync(this);
		}

		/// <summary>
		/// Throws <see cref="ObjectDisposedException"/> if the instance is disposed.
		/// </summary>
		protected void ThrowIfDisposed()
		{
			if (_state == AppStateState.Disposed)
			{
				throw new ObjectDisposedException(_controller.Id);
			}
		}

		#endregion

		#region IDisposable

		/// <inheritdoc/>
		public void Dispose()
		{
			if (_state != AppStateState.Disposed)
			{
				_state = AppStateState.Disposed;
				_stateManager.States.Remove(this);
				_controller.Dispose();
			}
		}

		#endregion

		#region implementation
		#endregion
	}
}

﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.ComponentModel;
using UnityEngine;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	/// <summary>
	/// Prefab view manager.
	/// </summary>
	public abstract class ViewManagerBehaviour<TView> : ContainerBehaviour, IViewFactory, IServiceProvider where TView : ViewBehaviour
	{
		#region data

		private IViewLoader _viewLoader;

		#endregion

		#region interface

		/// <summary>
		/// Adds a new view to the container.
		/// </summary>
		/// <param name="view">A view to add.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is <see langword="null"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the container is disposed.</exception>
		public void Add(TView view)
		{
			base.Add(view);
		}

		/// <summary>
		/// Adds a new view to the container.
		/// </summary>
		/// <param name="view">A view to add.</param>
		/// <param name="index">Insert index.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is invalid.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the container is disposed.</exception>
		public void Add(TView view, int index)
		{
			Add(view, name, index);
		}

		/// <summary>
		/// Adds a new view to the container.
		/// </summary>
		/// <param name="view">A view to add.</param>
		/// <param name="name">Name of the view.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is <see langword="null"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the container is disposed.</exception>
		public void Add(TView view, string name)
		{
			base.Add(view, name);
		}

		/// <summary>
		/// Adds a new view to the container.
		/// </summary>
		/// <param name="view">A view to add.</param>
		/// <param name="name">Name of the view.</param>
		/// <param name="index">Insert index.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="view"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is invalid.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the container is disposed.</exception>
		public void Add(TView view, string name, int index)
		{
			if (index < 0 || index > ComponentCount)
			{
				throw new ArgumentOutOfRangeException("index", index, "Invalid insert index.");
			}

			base.Add(view, name, index);
		}

		/// <summary>
		/// Updates a view at the specific index. This is called each time a view is added or removed. Default implementation
		/// updates view position.
		/// </summary>
		/// <param name="viewTransform">Transform of the view to update.</param>
		/// <param name="index">Index of the view.</param>
		protected virtual void UpdateView(Transform viewTransform, int index)
		{
			viewTransform.localPosition = new Vector3(0, 0, 10 + 1 * index);
		}

		#endregion

		#region MonoBehaviour

		private void Awake()
		{
			_viewLoader = GetComponent<IViewLoader>();
		}

		#endregion

		#region ContainerBehaviour

		/// <inheritdoc/>
		protected override bool OnAddComponent(IComponent component, int index)
		{
			if (base.OnAddComponent(component, index) && component is TView)
			{
				var view = component as TView;

				view.transform.SetParent(transform, false);
				view.transform.SetSiblingIndex(index);

				UpdateViews(index);
				return true;
			}

			return false;
		}

		/// <inheritdoc/>
		protected override void OnRemoveComponent(IComponent component)
		{
			((TView)component).transform.SetParent(null);
			base.OnRemoveComponent(component);
		}

		#endregion

		#region IViewFactory

		/// <summary>
		/// Asynchronously loads the specified view.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="viewId"/> is <see langword="null"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the container is disposed.</exception>
		public IAsyncOperation<IView> LoadViewAsync(string viewId, IView insertAfter)
		{
			ThrowIfDisposed();

			if (viewId == null)
			{
				throw new ArgumentNullException("viewId");
			}

			var insertAfterBehaviour = insertAfter as ViewBehaviour;

			if (insertAfterBehaviour)
			{
				if (insertAfterBehaviour.transform.parent == transform)
				{
					return LoadViewInternal(viewId, insertAfterBehaviour.transform.GetSiblingIndex() + 1);
				}
				else
				{
					return LoadViewInternal(viewId, 0);
				}
			}
			else
			{
				return LoadViewInternal(viewId, 0);
			}
		}

		#endregion

		#region IServiceProvider

		/// <summary>
		/// Gets the service object of the specified type.
		/// </summary>
		public override object GetService(Type serviceType)
		{
			if (serviceType == typeof(IViewFactory))
			{
				return this;
			}
			else if (serviceType == typeof(IViewLoader))
			{
				return _viewLoader;
			}

			return base.GetService(serviceType);
		}

		#endregion

		#region implementation

		private IAsyncOperation<IView> LoadViewInternal(string viewId, int index)
		{
			return _viewLoader.LoadViewAsync(viewId, transform).ContinueWith(
				op =>
				{
					var view = op.Result;

					if (!Add(view, viewId, index))
					{
						throw new InvalidOperationException();
					}

					return view;
				},
				AsyncContinuationOptions.OnlyOnRanToCompletion);
		}

		private void UpdateViews(int startIndex)
		{
			for (var i = startIndex; i < transform.childCount; ++i)
			{
				var siteTransform = transform.GetChild(i);
				UpdateView(siteTransform, i);
			}
		}

		#endregion
	}
}
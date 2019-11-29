﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.ComponentModel;
using UnityEngine;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Extensions of <see cref="IPresenter"/> interface.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class IPresenterExtensions
	{
		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <param name="presenter">The presenter.</param>
		/// <param name="controllerType">Type of the view controller to present.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="controllerType"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="controllerType"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		/// <seealso cref="Present(Type)"/>
		public static IPresentResult PresentAsync(this IPresenter presenter, Type controllerType)
		{
			return presenter.PresentAsync(controllerType, PresentOptions.None, null, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <param name="presenter">The presenter.</param>
		/// <param name="controllerType">Type of the view controller to present.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="controllerType"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="controllerType"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		/// <seealso cref="Present(Type)"/>
		public static IPresentResult PresentAsync(this IPresenter presenter, Type controllerType, Transform transform)
		{
			return presenter.PresentAsync(controllerType, PresentOptions.Popup, transform, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <param name="presenter">The presenter.</param>
		/// <param name="controllerType">Type of the view controller to present.</param>
		/// <param name="options">Present options.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="controllerType"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="controllerType"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		/// <seealso cref="Present(Type)"/>
		public static IPresentResult PresentAsync(this IPresenter presenter, Type controllerType, PresentOptions options)
		{
			return presenter.PresentAsync(controllerType, options, null, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <param name="presenter">The presenter.</param>
		/// <param name="controllerType">Type of the view controller to present.</param>
		/// <param name="options">Present options.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="controllerType"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="controllerType"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		/// <seealso cref="Present(Type)"/>
		public static IPresentResult PresentAsync(this IPresenter presenter, Type controllerType, PresentOptions options, PresentArgs args)
		{
			return presenter.PresentAsync(controllerType, options, null, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <param name="presenter">The presenter.</param>
		/// <param name="controllerType">Type of the view controller to present.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="controllerType"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="controllerType"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		/// <seealso cref="Present(Type)"/>
		public static IPresentResult PresentAsync(this IPresenter presenter, Type controllerType, PresentArgs args)
		{
			return presenter.PresentAsync(controllerType, PresentOptions.None, null, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), PresentOptions.None, null, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter, Transform transform) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), PresentOptions.Popup, transform, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="options">Present options.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter, PresentOptions options, Transform transform) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), options, transform, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), PresentOptions.None, null, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="options">Present options.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter, PresentOptions options, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), options, null, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter, Transform transform, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), PresentOptions.Popup, transform, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="options">Present options.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController> PresentAsync<TController>(this IPresenter presenter, PresentOptions options, Transform transform, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController>)presenter.PresentAsync(typeof(TController), options, transform, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <typeparam name="TResult">Type of the controller result value.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="InvalidCastException">Thrown if the <typeparamref name="TResult"/> does not match result type of the <typeparamref name="TController"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController, TResult> PresentAsync<TController, TResult>(this IPresenter presenter) where TController : IViewController
		{
			return (IPresentResult<TController, TResult>)presenter.PresentAsync(typeof(TController), PresentOptions.None, null, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <typeparam name="TResult">Type of the controller result value.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="InvalidCastException">Thrown if the <typeparamref name="TResult"/> does not match result type of the <typeparamref name="TController"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController, TResult> PresentAsync<TController, TResult>(this IPresenter presenter, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController, TResult>)presenter.PresentAsync(typeof(TController), PresentOptions.None, null, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <typeparam name="TResult">Type of the controller result value.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="InvalidCastException">Thrown if the <typeparamref name="TResult"/> does not match result type of the <typeparamref name="TController"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController, TResult> PresentAsync<TController, TResult>(this IPresenter presenter, Transform transform) where TController : IViewController
		{
			return (IPresentResult<TController, TResult>)presenter.PresentAsync(typeof(TController), PresentOptions.Popup, transform, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <typeparam name="TResult">Type of the controller result value.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="options">Present options.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="InvalidCastException">Thrown if the <typeparamref name="TResult"/> does not match result type of the <typeparamref name="TController"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController, TResult> PresentAsync<TController, TResult>(this IPresenter presenter, PresentOptions options, Transform transform) where TController : IViewController
		{
			return (IPresentResult<TController, TResult>)presenter.PresentAsync(typeof(TController), options, transform, null);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <typeparam name="TResult">Type of the controller result value.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="InvalidCastException">Thrown if the <typeparamref name="TResult"/> does not match result type of the <typeparamref name="TController"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController, TResult> PresentAsync<TController, TResult>(this IPresenter presenter, Transform transform, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController, TResult>)presenter.PresentAsync(typeof(TController), PresentOptions.Popup, transform, args);
		}

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		/// <typeparam name="TController">Type of the controller to instantiate.</typeparam>
		/// <typeparam name="TResult">Type of the controller result value.</typeparam>
		/// <param name="presenter">The presenter.</param>
		/// <param name="options">Present options.</param>
		/// <param name="transform">Parent transform of the controller view.</param>
		/// <param name="args">Controller arguments.</param>
		/// <returns>An object that can be used to track the operation progress.</returns>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TController"/> cannot be used to instantiate the controller (for instance it is abstract type).</exception>
		/// <exception cref="InvalidCastException">Thrown if the <typeparamref name="TResult"/> does not match result type of the <typeparamref name="TController"/>.</exception>
		/// <exception cref="ObjectDisposedException">Thrown if the presenter is disposed.</exception>
		public static IPresentResult<TController, TResult> PresentAsync<TController, TResult>(this IPresenter presenter, PresentOptions options, Transform transform, PresentArgs args) where TController : IViewController
		{
			return (IPresentResult<TController, TResult>)presenter.PresentAsync(typeof(TController), options, transform, args);
		}
	}
}
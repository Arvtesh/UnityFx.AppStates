﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	/// <summary>
	/// Context data for an <see cref="IViewController"/> instance. The class is basically a link between <see cref="IAppState"/> and child controllers.
	/// It is here for the sake of testability/explicit dependencies for <see cref="IViewController"/> implementations.
	/// </summary>
	/// <seealso cref="IViewController"/>
	public interface IPresentContext
	{
		/// <summary>
		/// Gets the controller creation arguments.
		/// </summary>
		PresentArgs PresentArgs { get; }

		/// <summary>
		/// Gets parent state.
		/// </summary>
		IAppState ParentState { get; }

		/// <summary>
		/// Gets parent controller (if any).
		/// </summary>
		IViewController ParentController { get; }

		/// <summary>
		/// Gets the view.
		/// </summary>
		IAppView View { get; }

		/// <summary>
		/// Gets a <see cref="IServiceProvider"/> that can be used to resolve controller dependencies.
		/// </summary>
		IServiceProvider ServiceProvider { get; }

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		IAsyncOperation<IViewController> PresentAsync(Type controllerType, PresentArgs args);

		/// <summary>
		/// Presents a controller of the specified type.
		/// </summary>
		IAsyncOperation<TController> PresentAsync<TController>(PresentArgs args) where TController : class, IViewController;

		/// <summary>
		/// Dismisses the specified controller.
		/// </summary>
		IAsyncOperation DismissAsync(IViewController controller);
	}
}

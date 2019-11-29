﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Result of a present operation. Can be used very much like <see cref="DismissTask"/>.
	/// </summary>
	/// <seealso cref="IViewController"/>
	/// <seealso cref="IPresenter"/>
	public interface IPresentResult : IViewControllerInfo, ICommandTarget, IDisposable
	{
		/// <summary>
		/// Gets the view controller.
		/// </summary>
		/// <seealso cref="PresentCompleted"/>
		/// <seealso cref="Dismissed"/>
		IViewController Controller { get; }

		/// <summary>
		/// Gets the view.
		/// </summary>
		IView View { get; }

		/// <summary>
		/// Gets a <see cref="Task"/> instance that can be used to await the operation completion (i.e. until the <see cref="Controller"/> is dismissed).
		/// </summary>
		Task DismissTask { get; }

		/// <summary>
		/// Gets a <see cref="Task"/> instance that can be used to await the operation completion (i.e. until the <see cref="Controller"/> is presented).
		/// </summary>
		Task PresentTask { get; }
	}
}
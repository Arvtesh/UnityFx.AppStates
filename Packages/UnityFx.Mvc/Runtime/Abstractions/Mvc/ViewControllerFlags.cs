﻿// Copyright (c) 2018-2020 Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.Mvc
{
	/// <summary>
	/// View controller creation flags.
	/// </summary>
	/// <seealso cref="ViewControllerAttribute"/>
	public enum ViewControllerFlags
	{
		/// <summary>
		/// No flags.
		/// </summary>
		None = 0,

		/// <summary>
		/// Marks the controller as exclusive. Exclusive controllers do not forward unprocessed commands to controllers below them is the stack
		/// and their views are assumed to be full-screen. Cannot be combined with <see cref="Popup"/> and <see cref="ModalPopup"/>.
		/// </summary>
		Exclusive = 0x1,

		/// <summary>
		/// Marks the controller as popup. Cannot be combined with <see cref="Exclusive"/>.
		/// </summary>
		Popup = 0x2,

		/// <summary>
		/// Marks the controller as modal. Modal controllers do not forward unprocessed commands to controllers below them is the stack.
		/// Cannot be combined with <see cref="Exclusive"/>.
		/// </summary>
		Modal = 0x4,

		/// <summary>
		/// Marks the controller as modal. Modal controllers do not forward unprocessed commands to controllers below them is the stack.
		/// Cannot be combined with <see cref="Exclusive"/>.
		/// </summary>
		ModalPopup = Modal | Popup,

		/// <summary>
		/// If set, presenting the controller would dismiss all other controllers of the same type.
		/// </summary>
		Singleton = 0x10,
	}
}
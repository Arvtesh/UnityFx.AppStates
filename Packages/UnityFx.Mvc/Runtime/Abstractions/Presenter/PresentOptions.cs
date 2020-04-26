﻿// Copyright (c) 2018-2020 Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Enumerates state controller push options.
	/// </summary>
	/// <seealso cref="IPresenter"/>
	/// <seealso cref="IViewController"/>
	[Flags]
	public enum PresentOptions
	{
		/// <summary>
		/// Default options. The new state is pushed onto the stack.
		/// </summary>
		None = 0,

		/// <summary>
		/// Parents the presented controller to the caller. Child controllers are dismissed with their parent.
		/// </summary>
		Child = 0x100,

		/// <summary>
		/// If set the caller presenter is dismissed.
		/// </summary>
		DismissCurrent = 0x1000,

		/// <summary>
		/// If set all controllers are dismissed before presenting the new one.
		/// </summary>
		DismissAll = 0x2000
	}
}

﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.App
{
	/// <summary>
	/// Arguments for state-related events.
	/// </summary>
	public class AppStateEventArgs : EventArgs
	{
		/// <summary>
		/// Returns state instance event refers to (if any). Read only.
		/// </summary>
		public IAppState State { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AppStateEventArgs"/> class.
		/// </summary>
		internal AppStateEventArgs(IAppState state) => State = state;
	}
}
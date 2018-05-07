﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	/// <summary>
	/// A factory for <see cref="AppView"/> instances.
	/// </summary>
	/// <seealso cref="AppView"/>
	public interface IAppViewManager
	{
		/// <summary>
		/// Creates an empty view with the specified <paramref name="id"/> on top of the <paramref name="insertAfter"/> one.
		/// If <paramref name="insertAfter"/> is <see langword="null"/> the view is created below all others.
		/// </summary>
		AppView CreateView(string id, AppView insertAfter, AppViewOptions options);

		/// <summary>
		/// Creates an empty view with the specified <paramref name="id"/> on top of the <paramref name="parent"/> one.
		/// If <paramref name="parent"/> is <see langword="null"/> the view is created below all others.
		/// </summary>
		AppView CreateChildView(string id, AppView parent, AppViewOptions options);
	}
}

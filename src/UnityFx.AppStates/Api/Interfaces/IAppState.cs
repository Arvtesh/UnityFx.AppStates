﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	/// <summary>
	/// A generic application state.
	/// </summary>
	public interface IAppState : ITreeListNode<IAppState>, IPresenter, IPresentable, IDeeplinkable
	{
		/// <summary>
		/// Gets the view root view controller attached to the state.
		/// </summary>
		IPresentable Controller { get; }
	}
}

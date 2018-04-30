﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	internal class AppStateTransitionManager : IAppStateTransitionManager
	{
		public void CancelTransition(IAsyncOperation transition)
		{
		}

		public IAsyncOperation PlayPopTransition(AppStateView view)
		{
			return AsyncResult.CompletedOperation;
		}

		public IAsyncOperation PlayPushTransition(AppStateView view)
		{
			return AsyncResult.CompletedOperation;
		}

		public IAsyncOperation PlayTransition(AppStateView fromView, AppStateView toView)
		{
			return AsyncResult.CompletedOperation;
		}
	}
}

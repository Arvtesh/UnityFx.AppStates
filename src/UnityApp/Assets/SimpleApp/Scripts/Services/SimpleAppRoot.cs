﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using UnityEngine;

namespace UnityFx.AppStates.Samples
{
	/// <summary>
	/// Application entry point.
	/// </summary>
	public class SimpleAppRoot : MonoBehaviour
	{
		#region data

		private IAppStateService _stateManager;
		private IAppViewService _viewManager;

		#endregion

		#region MonoBehaviour

		private void Awake()
		{
			// TODO
			_viewManager = null;
			_stateManager = null;
		}

		private void Start()
		{
			_stateManager.PresentAsync<MainMenuController>();
		}

		private void OnDestroy()
		{
			if (_stateManager != null)
			{
				_stateManager.Dispose();
				_stateManager = null;
			}

			if (_viewManager != null)
			{
				_viewManager.Dispose();
				_viewManager = null;
			}
		}

		#endregion
	}
}
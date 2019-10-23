﻿// Copyright (C) 2019 Alexander Bogarsukov. All rights reserved.
// See the LICENSE.md file in the project root for more information.

using System;
using UnityEditor;
using UnityEngine;

namespace UnityFx.Mvc
{
	[CustomEditor(typeof(Presenter))]
	public class PresenterEditor : Editor
	{
		private Presenter _presenter;

		private void OnEnable()
		{
			_presenter = (Presenter)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var controllers = _presenter.Controllers;

			if (controllers != null && controllers.Count > 0)
			{
				var controllerId = 0;

				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.LabelField("Controllers");
				EditorGUI.indentLevel += 1;

				foreach (var c in _presenter.Controllers)
				{
					EditorGUILayout.LabelField("#" + controllerId.ToString(), c.GetType().Name);
					controllerId++;
				}

				EditorGUI.indentLevel -= 1;
				EditorGUI.EndDisabledGroup();
			}
		}
	}
}

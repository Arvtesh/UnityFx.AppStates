﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityFx.Async;

namespace UnityFx.AppStates
{
	/// <summary>
	/// A generic prefab-based view.
	/// </summary>
	public sealed class PrefabView : AppView
	{
		#region data

		private readonly IPrefabLoader _prefabLoader;
		private GameObject _go;

		#endregion

		#region interface

		/// <summary>
		/// Initializes a new instance of the <see cref="PrefabView"/> class.
		/// </summary>
		public PrefabView(string id, PresentOptions options, IPrefabLoader prefabLoader)
			: base(id, options)
		{
			_prefabLoader = prefabLoader;
		}

		#endregion

		#region AppView

		/// <inheritdoc/>
		protected override void SetVisible(bool visible)
		{
			if (_go)
			{
				_go.SetActive(visible);
			}
		}

		/// <inheritdoc/>
		protected override void SetEnabled(bool enabled)
		{
			if (_go)
			{
				foreach (var raycaster in _go.GetComponentsInChildren<GraphicRaycaster>(true))
				{
					raycaster.enabled = enabled;
				}
			}
		}

		/// <inheritdoc/>
		protected override IAsyncOperation LoadContent(string resourceId)
		{
			return _prefabLoader.LoadPrefab(resourceId).ContinueWith(op => _go = op.Result, AsyncContinuationOptions.OnlyOnRanToCompletion);
		}

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing && _go)
			{
				GameObject.Destroy(_go);
				_go = null;
			}
		}

		#endregion

		#region IComponentContainer

		/// <inheritdoc/>
		public override TComponent GetComponent<TComponent>()
		{
			if (_go)
			{
				var c = _go.GetComponent<TComponent>();

				if (c != null)
				{
					return c;
				}
			}

			return default(TComponent);
		}

		/// <inheritdoc/>
		public override TComponent GetComponentRecursive<TComponent>()
		{
			if (_go)
			{
				var c = _go.GetComponentInChildren<TComponent>(true);

				if (c != null)
				{
					return c;
				}
			}

			return default(TComponent);
		}

		/// <inheritdoc/>
		public override TComponent[] GetComponents<TComponent>()
		{
			if (_go)
			{
				var result = new List<TComponent>();
				_go.GetComponents(result);
				return result.ToArray();
			}

			return null;
		}

		/// <inheritdoc/>
		public override TComponent[] GetComponentsRecursive<TComponent>()
		{
			if (_go)
			{
				var result = new List<TComponent>();
				_go.GetComponentsInChildren(true, result);
				return result.ToArray();
			}

			return null;
		}

		#endregion

		#region implementation
		#endregion
	}
}
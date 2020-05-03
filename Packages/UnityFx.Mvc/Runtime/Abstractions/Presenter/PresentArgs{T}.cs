﻿// Copyright (c) 2018-2020 Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Arguments for <see cref="IPresenter"/> methods.
	/// </summary>
	/// <typeparam name="T">Type of the nested value.</typeparam>
	public class PresentArgs<T> : PresentArgs
	{
		#region interface

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public T Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentArgs{T}"/> class.
		/// </summary>
		public PresentArgs()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PresentArgs{T}"/> class.
		/// </summary>
		public PresentArgs(T value)
		{
			Value = value;
		}

		#endregion

		#region Object
		#endregion

		#region implementation
		#endregion
	}
}

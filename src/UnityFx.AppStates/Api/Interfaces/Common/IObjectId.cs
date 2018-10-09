﻿// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.AppStates
{
	/// <summary>
	/// An object with instance identifier.
	/// </summary>
	public interface IObjectId
	{
		/// <summary>
		/// Gets the instance identifier. The identifier returned is supposed to be used for informational/debugging purposes only. Uniqueness is not required.
		/// </summary>
		string Id { get; }
	}
}

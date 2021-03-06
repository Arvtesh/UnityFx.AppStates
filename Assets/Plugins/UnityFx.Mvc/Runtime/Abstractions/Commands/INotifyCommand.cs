﻿// Copyright (c) 2018-2020 Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Notifies clients of a command.
	/// </summary>
	/// <seealso cref="ICommandTarget"/>
	public interface INotifyCommand
	{
		/// <summary>
		/// Raised when a user issues a command.
		/// </summary>
		event EventHandler<CommandEventArgs> Command;
	}
}

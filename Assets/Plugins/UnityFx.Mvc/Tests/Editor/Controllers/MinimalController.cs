﻿// Copyright (c) 2018-2020 Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.Mvc
{
	public class MinimalController : IViewController
	{
		public IView View { get; }

		public MinimalController(IView view)
		{
			View = view;
		}

		public bool InvokeCommand<TCommand>(TCommand command)
		{
			return false;
		}
	}
}

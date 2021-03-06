﻿// Copyright (c) 2018-2020 Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace UnityFx.Mvc
{
	[ViewController(PresentOptions = PresentOptions.Singleton)]
	public class SingletonController : IViewController
	{
		public IView View { get; }

		public SingletonController(IView view)
		{
			View = view;
		}
	}
}

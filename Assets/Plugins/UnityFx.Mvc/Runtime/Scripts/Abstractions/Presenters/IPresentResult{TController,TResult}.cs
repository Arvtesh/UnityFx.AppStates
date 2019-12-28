// Copyright (c) Alexander Bogarsukov.
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UnityFx.Mvc
{
	/// <summary>
	/// Result of a present operation.
	/// </summary>
	/// <seealso cref="IPresentResult"/>
	public interface IPresentResult<out TController, TResult> : IPresentResult<TController> where TController : IViewController
	{
		/// <summary>
		/// Gets the operation result value.
		/// </summary>
		/// <remarks>
		/// Once the result of an operation is available, it is stored and is returned immediately on subsequent calls to the <see cref="Result"/> property.
		/// Note that, if an exception occurred during the operation, or if the operation has been cancelled, the <see cref="Result"/> property does not return a value.
		/// Instead, attempting to access the property value throws an exception.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown if the property is accessed before operation is completed or the operation completed with errors.</exception>
		TResult Result { get; }

		/// <summary>
		/// Gets a <see cref="Task"/> instance that can be used to await the operatino completion.
		/// </summary>
		new Task<TResult> Task { get; }
	}
}

/* 
    Copyright (c) 2020 - 2021 Redux Engine. All Rights Reserved. https://github.com/Redux-Engine
    Copyright (c) Piero Castillo. All Rights Reserved. https://github.com/PieroCastillo
    This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Redux
{
    /// <summary>
    /// Resolves dependecies.
    /// </summary>
    public interface IReduxDependencyResolver
    {
        /// <summary>
        /// Gets a Service.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The initialized service, if does not exist, returns null</returns>
        public T? GetService<T>() where T : class;

        /// <summary>
        /// Register a service.
        /// </summary>
        /// <typeparam name="T">The type of the service to register.</typeparam>
        /// <param name="t">The service to register.</param>
        /// <returns>Returns itself.</returns>
        public IReduxDependencyResolver RegisterService<T>(T t) where T : class;
    }
}

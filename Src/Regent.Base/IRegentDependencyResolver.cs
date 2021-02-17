using System;
using System.Collections.Generic;
using System.Text;

namespace Regent.Base
{
    /// <summary>
    /// Resolves dependecies.
    /// </summary>
    public interface IRegentDependencyResolver
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
        public IRegentDependencyResolver RegisterService<T>(T t) where T : class;
    }
}

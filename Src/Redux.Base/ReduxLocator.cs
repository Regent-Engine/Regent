using System;
using System.Collections.Generic;
using System.Text;

namespace Redux
{
    /// <inheritdoc/>
    public class ReduxLocator : IReduxDependencyResolver
    {
        /// <summary>
        /// Gets the <see cref="ReduxLocator"/> current instance.
        /// </summary>
        public static IReduxDependencyResolver Current
        {
            get;
            private set;
        }

        static ReduxLocator()
        {
            Current = new ReduxLocator();
        }

        private Dictionary<object, object> cache = new();

        private ReduxLocator() { }

        /// <inheritdoc/>
        public T? GetService<T>() where T : class
        {
            if (cache.ContainsKey(typeof(T)))
            {
                return (T)cache[typeof(T)];
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public IReduxDependencyResolver RegisterService<T>(T t) where T : class
        {
            if (!cache.ContainsKey(typeof(T)))
            {
                cache.Add(typeof(T), t);
                return this;
            }
            else
            {
                throw new NotImplementedException("The Service is already registred.");
            }
        }
    }
}

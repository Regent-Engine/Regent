using System;
using System.Collections.Generic;
using System.Text;

namespace Redux.Base
{
    /// <inheritdoc/>
    public class RegentLocator : IRegentDependencyResolver
    {
        /// <summary>
        /// Gets the <see cref="RegentLocator"/> current instance.
        /// </summary>
        public static IRegentDependencyResolver Current
        {
            get;
            private set;
        }

        static RegentLocator()
        {
            Current = new RegentLocator();
        }

        private Dictionary<object, object> cache = new();

        private RegentLocator() { }

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
        public IRegentDependencyResolver RegisterService<T>(T t) where T : class
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

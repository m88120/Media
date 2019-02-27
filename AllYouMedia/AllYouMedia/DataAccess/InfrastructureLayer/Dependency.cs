namespace AllYouMedia.DataAccess.InfrastructureLayer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Autofac;

    public class Dependency : IDependencyResolver
    {
        private readonly IContainer container;

        public Dependency(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return
                this.container.IsRegistered(serviceType)
                    ? this.container.Resolve(serviceType)
                    : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            Type enumerableServiceType =
                typeof(IEnumerable<>).MakeGenericType(serviceType);

            object instance =
                this.container.Resolve(enumerableServiceType);

            return ((IEnumerable)instance).Cast<object>();
        }
    }
}
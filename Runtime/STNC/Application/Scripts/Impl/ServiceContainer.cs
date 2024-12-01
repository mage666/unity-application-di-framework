using System;
using System.Collections.Generic;

namespace STNC.Application
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Dictionary<Type, Func<IServiceContainer, object>> _factories = new();

        public void Register<TService>(Func<IServiceContainer, TService> factory)
        {
            _factories[typeof(TService)] = container => factory(container);
        }

        public void RegisterFactory<TService, TFactory>() where TFactory : IServiceFactory<TService>, new()
        {
            _factories[typeof(TService)] = container =>
                                           {
                                               var factory = new TFactory();
                                               return factory.Create(container);
                                           };
        }

        public void RegisterInstance<TService>(TService instance)
        {
            _factories[typeof(TService)] = _ => instance;
        }

        public TService Resolve<TService>()
        {
            if (_factories.TryGetValue(typeof(TService), out var factory))
            {
                return (TService)factory(this);
            }

            throw new InvalidOperationException($"Service of type {typeof(TService)} not registered.");
        }
    }
}
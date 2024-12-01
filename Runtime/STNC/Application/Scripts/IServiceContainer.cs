using System;

namespace STNC.Application
{
    public interface IServiceContainer
    {
        TService Resolve<TService>();
        void     Register<TService>(Func<IServiceContainer, TService> factory);
        void     RegisterFactory<TService, TFactory>() where TFactory : IServiceFactory<TService>, new();
        void     RegisterInstance<TService>(TService instance);
    }
}
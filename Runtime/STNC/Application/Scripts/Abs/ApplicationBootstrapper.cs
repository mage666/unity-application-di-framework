using System;
using STNC.UnityUtilities.Serialization;
using UnityEngine;

namespace STNC.Application
{
    [Serializable]
    public abstract class ApplicationBootstrapper : IApplicationBootstrapper
    {
        [SerializeReference, SerializeInterface] protected IServiceContainer _serviceContainer;

        protected ApplicationBootstrapper(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public abstract void Initialize();

        public abstract void Run();

        public IServiceContainer ServiceContainer { get => _serviceContainer; }
    }
}
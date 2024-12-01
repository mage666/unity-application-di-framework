using System;
using STNC.UnityUtilities.Serialization;
using UnityEngine;

namespace STNC.Application
{
    [Serializable]
    public abstract class SceneBootstrapper : ISceneBootstrapper
    {
        [SerializeReference, SerializeInterface] protected IServiceContainer _serviceContainer;

        protected SceneBootstrapper(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public virtual void Initialize(IApplicationBootstrapper application)
        {
            Application = application;
        }

        public abstract void Run();

        public IServiceContainer        ServiceContainer { get => _serviceContainer; }
        public IApplicationBootstrapper Application      { get; private set; }
    }
}
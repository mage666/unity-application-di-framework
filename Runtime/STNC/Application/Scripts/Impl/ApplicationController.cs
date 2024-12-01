using STNC.UnityUtilities.Serialization;
using UnityEngine;

namespace STNC.Application
{
    public sealed class ApplicationController : MonoBehaviour, IApplicationController
    {
        [SerializeReference, SerializeInterface]
        private IApplicationBootstrapper _application;

        public IApplicationBootstrapper Application
        {
            get => _application;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _application.Initialize();
            _application.Run();
        }
    }
}
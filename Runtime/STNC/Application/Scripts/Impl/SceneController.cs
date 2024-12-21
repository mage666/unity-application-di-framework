using System.Linq;
using STNC.UnityUtilities.Serialization;
using UnityEngine;

namespace STNC.Application
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        [SerializeReference, SerializeInterface] protected ISceneBootstrapper _sceneBootstrapper;
        [SerializeReference, SerializeInterface] protected IDebugApplicationFactory _debugApplicationFactory;
        
        public IApplicationBootstrapper Application { get; private set; }
        public ISceneBootstrapper       Scene       => _sceneBootstrapper;

        protected virtual void Awake()
        {
            var application = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                          .OfType<IApplicationController>()
                          .FirstOrDefault();

            Application = application == null ? _debugApplicationFactory.GetApplication() : application.Application;

            _sceneBootstrapper.Initialize(Application);
        }
        
        protected virtual void Start()
        {
            _sceneBootstrapper.Run();
        }
    }
}
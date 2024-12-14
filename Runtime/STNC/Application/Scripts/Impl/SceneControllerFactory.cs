using System;
using System.Reflection;
using UnityEngine;

namespace STNC.Application
{
    public class SceneControllerFactory
    {
        public static SceneController CreateSceneController<TSceneBootstrapper, TDebugApplicationFactory, TServiceContainer>()
            where TSceneBootstrapper : SceneBootstrapper
            where TDebugApplicationFactory : IDebugApplicationFactory, new()
            where TServiceContainer : IServiceContainer, new()
        {
            var gameObject = new GameObject("SceneController");
            var sceneController = gameObject.AddComponent<SceneController>();
            
            var sceneBootstrapper = (TSceneBootstrapper)Activator.CreateInstance(typeof(TSceneBootstrapper), new TServiceContainer());
            var debugApplicationFactory = new TDebugApplicationFactory();
            
            InjectSerializedReference(sceneController, "_sceneBootstrapper", sceneBootstrapper);
            InjectSerializedReference(sceneController, "_debugApplicationFactory", debugApplicationFactory);
            
            return sceneController;
        }
        
        private static void InjectSerializedReference(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (field == null)
            {
                Debug.LogError($"Field '{fieldName}' not found on type '{target.GetType().Name}'.");
                return;
            }

            if (!field.IsDefined(typeof(SerializeReference), inherit: true))
            {
                Debug.LogError($"Field '{fieldName}' on type '{target.GetType().Name}' is not marked with [SerializeReference].");
                return;
            }

            if (!field.FieldType.IsAssignableFrom(value.GetType()))
            {
                Debug.LogError($"Cannot assign value of type '{value.GetType().Name}' to field '{fieldName}' of type '{field.FieldType.Name}'.");
                return;
            }

            field.SetValue(target, value);
        }
    }
}
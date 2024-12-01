# Service Container Documentation

Service container can be used to register and resolve dependencies for Interface segregation.

## Application Bootstrapper Service Container
Application Bootstrapper's Service Container is applicable on global application context. <br>
General usage is for global services which would be required throughout the application.

### Fetching Application Service Container
- You can pass the service container dependency in the Application Bootstrapper where required.
    ```csharp
        public class ExampleApplicationBootstrapper : ApplicationBootstrapper
        {
            public ExampleApplicationBootstrapper(IServiceContainer serviceContainer) : base(serviceContainer)
            {
            }
    
            public override void Initialize()
            {
                _serviceContainer.Register<ILoggerService>(_ => new LoggerService(_serviceContainer));
            }
    
            public override void Run()
            {
                var logger = _serviceContainer.Resolve<ILoggerService>();
                logger.Log("Bootstrapper initialized");
            }
        }
    ```
- In MonoBehaviours you can fetch from `ApplicationController` or interface `IApplicationController`
    ```csharp
    var application = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                              .OfType<IApplicationController>()
                              .FirstOrDefault();
    application.ServiceContainer;
    ```
- In Scene Bootstrapper or related flow
    ```csharp
        public class ExampleScene1Bootstrapper : SceneBootstrapper
        {
            [SerializeField] private Scene1Manager _scene1Manager;
            public ExampleScene1Bootstrapper(IServiceContainer serviceContainer) : base(serviceContainer)
            {
                
            }
            
            public override void Initialize(IApplicationBootstrapper application)
            {
                base.Initialize(application);
            }
    
            public override void Run()
            {
                var loggerService = Application.ServiceContainer.Resolve<ILoggerService>();
                loggerService.Log("ExampleScene1Loaded");
            }
        }
    ```

### Fetching Scene Service Container
- You can pass the service container dependency in the Scene Bootstrapper where required.
    ```csharp
        public class ExampleScene1Bootstrapper : SceneBootstrapper
        {
            [SerializeField] private Scene1Manager _scene1Manager;
            public ExampleScene1Bootstrapper(IServiceContainer serviceContainer) : base(serviceContainer)
            {
                
            }
            
            public override void Initialize(IApplicationBootstrapper application)
            {
                base.Initialize(application);
                _serviceContainer.Register<IUIManager>(container => new Scene1UIManager(_serviceContainer));
            }
        }
    ```
  
- In MonoBehaviours you can fetch from `SceneController` or interface `ISceneController`
    ```csharp
        var sceneController = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                                  .OfType<ISceneController>()
                                  .FirstOrDefault();
        sceneController.Scene.ServiceContainer;
    ```
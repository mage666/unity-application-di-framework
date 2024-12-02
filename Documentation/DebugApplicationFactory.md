# Debug Application Factory

You can use the Debug Application Factory to run scenes directly during development while resolving Global dependencies using the Debug Application Factory to mock behaviour of global services.

## Example
Let's say you have a global dependency on a scene specific script like this.

`ExampleSceneBootstrapper`
```csharp
    [Serializable]
    public class ExampleSceneBootstrapper : SceneBootstrapper
    {
        [SerializeField] private ExampleSceneUIManager _sceneManager;
        public ExampleSceneBootstrapper(IServiceContainer serviceContainer) : base(serviceContainer)
        {
            
        }
        
        public override void Initialize(IApplicationBootstrapper application)
        {
            base.Initialize(application);
            _serviceContainer.RegisterInstance<IUIManager>(_sceneManager);
        }

        public override void Run()
        {
            _serviceContainer.Resolve<IUIManager>().Initialize(this);

        }
    }
```

`ExampleSceneUIManager`
```csharp
    public class ExampleSceneUIManager : IUIManager
    {
        public void Initialize(ISceneBootstrapper scene)
        {
            var logger = scene.Application.ServiceContainer.Resolve<ILogger>();
            logger.Log("ExampleScene Initialized");
        }
    }
```

Now because ExampleScene script has a dependency on the `ILogger` being registered in the Application context, you will get `NullReferenceException` in `ExampleSceneUIManager`

However, you can utilize the DebugApplicationFactory to inject required dependencies like so.

`ExampleSceneApplicationFactory`
```csharp
    [Serializable]
    public class ExampleSceneApplicationFactory : IDebugApplicationFactory
    {
        public IApplicationBootstrapper GetApplication()
        {
            IServiceContainer serviceContainer = new ServiceContainer();
            serviceContainer.Register<ILoggerService>(container => new LoggerService());
            IApplicationBootstrapper application = new ExampleApplicationBootstrapper(serviceContainer);
            return application;
        }
    }
```

Now in the `SceneController` you can select `ExampleSceneApplicationFactory` from the dropdown and run the scene directly.
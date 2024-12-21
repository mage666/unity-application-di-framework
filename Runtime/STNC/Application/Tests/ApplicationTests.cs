using Moq;
using NUnit.Framework;

namespace STNC.Application.Tests
{
    public class ApplicationTests
    {
        [Test]
        public void ApplicationBootstrapper_ServiceContainerInitialization_Test()
        {
            var serviceContainer            = Mock.Of<IServiceContainer>();
            var mockApplicationBootstrapper = new Mock<ApplicationBootstrapper>(serviceContainer);
            var applicationBootstrapper     = mockApplicationBootstrapper.Object;
        
            Assert.That(applicationBootstrapper.ServiceContainer, Is.Not.Null);
        }

        [Test]
        public void SceneBootstrapper_ServiceContainerInitialization_Test()
        {
            var serviceContainer      = Mock.Of<IServiceContainer>();
            var mockSceneBootstrapper = new Mock<SceneBootstrapper>(serviceContainer);
            var sceneBootstrapper     = mockSceneBootstrapper.Object;
            Assert.That(sceneBootstrapper.ServiceContainer, Is.Not.Null);
        }

        [Test]
        public void SceneBootstrapper_ApplicationInitialization_Test()
        {
            var serviceContainer      = Mock.Of<IServiceContainer>();
            var application           = Mock.Of<IApplicationBootstrapper>();
            var mockSceneBootstrapper = new Mock<SceneBootstrapper>(serviceContainer){CallBase = true};
            var sceneBootstrapper     = mockSceneBootstrapper.Object;
            sceneBootstrapper.Initialize(application);
            Assert.That(sceneBootstrapper.Application, Is.Not.Null);
        }

        [Test]
        public void ServiceContainer_CanRegisterService_WithLambdaFactory_Test()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<ITestInterface>(_ => new TestImpl());
            var service = serviceContainer.Resolve<ITestInterface>();
        
            Assert.That(service, Is.Not.Null);
            Assert.That(service, Is.InstanceOf<TestImpl>());
        }

        [Test]
        public void ServiceContainer_CanRegisterService_WithFactory_Test()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterFactory<ITestInterface, TestFactory>();
            var service = serviceContainer.Resolve<ITestInterface>();
            
            Assert.That(service, Is.Not.Null);
            Assert.That(service, Is.InstanceOf<TestImpl>());
        }

        [Test]
        public void ServiceContainer_CanRegisterService_WithInstance_Test()
        {
            var serviceContainer = new ServiceContainer();
            var testImpl         = new TestImpl();
            serviceContainer.RegisterInstance<ITestInterface>(testImpl);
            var service = serviceContainer.Resolve<ITestInterface>();
        
            Assert.That(service, Is.Not.Null);
            Assert.That(service, Is.InstanceOf<TestImpl>());
            Assert.That(service, Is.EqualTo(testImpl));
        }

        private interface ITestInterface
        {

        }

        private class TestImpl : ITestInterface
        {

        }

        private class TestFactory : IServiceFactory<ITestInterface>
        {
            public ITestInterface Create(IServiceContainer container)
            {
                return new TestImpl();
            }
        }
    }
}
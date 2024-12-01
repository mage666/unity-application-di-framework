namespace STNC.Application
{
    public interface ISceneBootstrapper
    {
        void                     Initialize(IApplicationBootstrapper application);
        void                     Run();
        IServiceContainer        ServiceContainer { get; }
        IApplicationBootstrapper Application      { get; }
    }
}
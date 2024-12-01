namespace STNC.Application
{
    public interface IApplicationBootstrapper
    {
        void              Initialize();
        void              Run();
        IServiceContainer ServiceContainer { get; }
    }
}
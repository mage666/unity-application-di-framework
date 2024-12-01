namespace STNC.Application
{
    public interface IDebugApplicationFactory
    {
        IApplicationBootstrapper GetApplication();
    }
}
namespace STNC.Application
{
    public interface IServiceFactory<out TService>
    {
        TService Create(IServiceContainer container);
    }
}
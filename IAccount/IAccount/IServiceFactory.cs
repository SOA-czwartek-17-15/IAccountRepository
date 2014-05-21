using Contracts;

namespace AccountRepository
{
    internal interface IServiceFactory
    {
        IServiceRepository GetServiceRepository();

        IClientRepository GetClientRepository();
    }
}
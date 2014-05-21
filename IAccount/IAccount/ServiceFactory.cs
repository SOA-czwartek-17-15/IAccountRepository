using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace AccountRepository
{
    class ServiceFactory : IServiceFactory
    {
        public IServiceRepository GetServiceRepository()
        {
            string serviceRepositoryAddress = Settings.ServiceRepositoryAddress;

            return GetServiceChannel<IServiceRepository>(serviceRepositoryAddress);
        }

        public IClientRepository GetClientRepository()
        {
            var repo = GetServiceRepository();

            string serviceRepositoryAddress = repo.GetServiceLocation("IClientRepository");
            return GetServiceChannel<IClientRepository>(serviceRepositoryAddress);

        }

        private static T GetServiceChannel<T>(string serviceAddress)
        {
            var binding = new NetTcpBinding(SecurityMode.None);
            var cf = new ChannelFactory<T>(binding, new EndpointAddress(serviceAddress));
            return cf.CreateChannel();
        }
    }
}

using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Castle.DynamicProxy;
using System;
using System.Threading;
using log4net;

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

        private static T GetServiceChannel<T>(string serviceAddress) where T : class
        {
            var binding = new NetTcpBinding(SecurityMode.None);
            var cf = new ChannelFactory<T>(binding, new EndpointAddress(serviceAddress));
            cf.Open();

            ProxyGenerator generator = new ProxyGenerator();

            return generator.CreateInterfaceProxyWithoutTarget<T>(new WcfInterceptor<T>(cf));
        }
    }

    public class WcfInterceptor<T> : IInterceptor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(WcfInterceptor<T>));
        private const int MaxTryCount = 5;
        private readonly TimeSpan waitTime = TimeSpan.FromSeconds(2);
        private Exception lastExeption;
        private ChannelFactory<T> channelFactory;

        public WcfInterceptor(ChannelFactory<T> channelFactory)
        {
            this.channelFactory = channelFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("try to invoke methode {0} from {1}", invocation.Method.Name, typeof(T));
            int tryCount = 0;
            var result = Execute(invocation);
            while (result == false && tryCount < MaxTryCount && invocation.Method.Name!="Alive")
            {

                Console.WriteLine("Próba połączenia: {0}", tryCount+1);
                Thread.Sleep(waitTime);
                result = Execute(invocation);
                tryCount++;
            }

            if (result == false)
                throw new Exception("Communication exception");
        }

        private bool Execute(IInvocation invocation)
        {
            try
            {
                if (channelFactory.State != CommunicationState.Opened)
                    channelFactory.Open();

                var instance = channelFactory.CreateChannel();
                invocation.Method.Invoke(instance, invocation.Arguments);
                return true;
            }
            catch (Exception e)
            {
                lastExeption = e;
                log.Error("Exception: {0}", lastExeption); 
                return false;
            }
        }
    }
}

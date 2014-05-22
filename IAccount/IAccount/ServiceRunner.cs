using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Timers;
using AccountRepository.DataAccess;
using Castle.MicroKernel.Registration;
using Contracts;
using FluentNHibernate.Utils;
using log4net;

namespace AccountRepository
{
    class ServiceRunner
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceRunner));

        private const string AccountServiceName = "IAccountService";

        private IServiceRepository serviceRepository;
        private ServiceHost accountServiceHost;
        private Timer timer;

        public ServiceRunner()
        {
            
        }

        public void RunService()
        {
            serviceRepository = ServiceLocator.Instance.Resolve<IServiceFactory>().GetServiceRepository();
            log.InfoFormat("Chennel ServiceRepository created. Address: {0}", serviceRepository);
            
            //accountServiceHost = GetAccountServiceHost();
          //  log.Info("Host for Account Service created");

            serviceRepository.RegisterService(AccountServiceName, Settings.AccountServiceAddress);
            log.Info("Account Service registered");

            KeepAlive();

            Console.WriteLine("Kliknij Enter, aby wyłączyć serwis...");
            Console.ReadLine();

           // serviceRepository.Unregister(AccountServiceName);

            accountServiceHost.Close();
            log.Info("Account Service closed");
        }

        private void KeepAlive()
        {
            timer = new Timer {Interval = Settings.KeepAliveInterval};
            timer.Elapsed += (s, e) =>
            {
            serviceRepository = ServiceLocator.Instance.Resolve<IServiceFactory>().GetServiceRepository();
                serviceRepository.Alive(AccountServiceName);
            }; 
            timer.Start();
        }

        private ServiceHost GetAccountServiceHost()
        {
            var account = new AccountRepository();

            string serviceAdress = Settings.AccountServiceAddress;

            var sh = new ServiceHost(account, new[] { new Uri(serviceAdress) });
            var bindingOut = new NetTcpBinding(SecurityMode.None);
            sh.AddServiceEndpoint(typeof(IAccountRepository), bindingOut, serviceAdress);
            sh.Open();

            return sh;
        }
    }
}

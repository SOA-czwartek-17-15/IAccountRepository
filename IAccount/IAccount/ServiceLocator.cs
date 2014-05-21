using System;
using System.Collections.Generic;
using AccountRepository.DataAccess;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AccountRepository
{
    internal class ServiceLocator
    {
        private static WindsorContainer instance;
        public static WindsorContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindsorContainer();
                    RegisterServices();
                }
                return instance;
            }
        }

        private static void RegisterServices()
        {
            Instance.Register(Component.For<IAccountDataAccess>().ImplementedBy<NhibernateAccountDataAccess>());
            //ServiceLocator.Instance.Register(Component.For<IAccountDataAccess>().ImplementedBy<MockAccountDataAccess>());

            Instance.Register(Component.For<IServiceFactory>().ImplementedBy<ServiceFactory>());
            //Instance.Register(Component.For<IServiceFactory>().ImplementedBy<MockServiceFactory>());
        }
    }
}
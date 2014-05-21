using System;
using System.Collections.Generic;
using Contracts;

namespace AccountRepository
{
    class MockServiceFactory : IServiceFactory
    {
        public IServiceRepository GetServiceRepository()
        {
            throw new NotImplementedException();
        }

        public IClientRepository GetClientRepository()
        {
           return new MockClientRepository();
        }

        class MockClientRepository : IClientRepository
        {

            public Guid CreateClient(ClientInformation clientInfo)
            {
                throw new NotImplementedException();
            }

            public ClientInformation GetClientInformation(Guid clientID)
            {
                return new ClientInformation();
            }

            public IEnumerable<Guid> SearchForClientsBy(ClientInformation someClientInfo)
            {
                throw new NotImplementedException();
            }
        }
    }
}
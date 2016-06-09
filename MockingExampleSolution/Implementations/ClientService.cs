using DAL.Interfaces;
using MockingExampleSolution.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace MockingExampleSolution.Implementations
{
    public class ClientService : IClientService
    {
        protected IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
    }
}

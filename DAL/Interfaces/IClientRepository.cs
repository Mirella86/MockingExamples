﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        IEnumerable<Client> GetAllClients();
    }
}

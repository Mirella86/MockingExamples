using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL.Interfaces;
using DAL.Implementations;
using MockingExampleSolution.Interfaces;
using MockingExampleSolution.Implementations;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Models;

namespace UIApplication.Infrastructure
{
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
            .BasedOn<IController>()
            .LifestyleTransient());
            container.Register(Component.For<DbContext>()
                                  .ImplementedBy<BankContext>()
                                  .LifestyleTransient()
                        );
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(RepositoryBase<>)));
            container.Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>());
            container.Register(Component.For<IClientRepository>().ImplementedBy<ClientRepository>());
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>());
            container.Register(Component.For<IClientService>().ImplementedBy<ClientService>());


        }

    }
}
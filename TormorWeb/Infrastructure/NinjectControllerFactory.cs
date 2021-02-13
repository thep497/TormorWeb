using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Tormor.DomainModel;
using Tormor.Web.Models;

namespace NNS.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        // A Ninject "kernel" is the thing that can supply object instances
        private IKernel kernel = new StandardKernel(new NeoFLServices());

        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }

        // Configures how abstract service types are mapped to concrete implementations
        private class NeoFLServices : NinjectModule
        {
            public override void Load()
            {
                Bind<IReferenceRepository>().To<EFReferenceRepository>();
                Bind<IAlienRepository>().To<EFAlienRepository>();
                Bind<IVisaRepository>().To<EFVisaRepository>();
                Bind<IStayRepository>().To<EFStayRepository>();
                Bind<IReEntryRepository>().To<EFReEntryRepository>();
                Bind<IEndorseRepository>().To<EFEndorseRepository>();
                Bind<IEndorseStampRepository>().To<EFEndorseStampRepository>();
                Bind<IAlienTransactionRepository>().To<EFAlienTransactionRepository>();
                Bind<ISearchRepository>().To<SSSearchRepository>();
                Bind<IConveyanceRepository>().To<EFConveyanceRepository>();
                Bind<IConveyanceInOutRepository>().To<EFConveyanceInOutRepository>();
                Bind<ICrewRepository>().To<EFCrewRepository>();
                Bind<IAddRemoveCrewRepository>().To<EFAddRemoveCrewRepository>();
            }
        }
    }
}
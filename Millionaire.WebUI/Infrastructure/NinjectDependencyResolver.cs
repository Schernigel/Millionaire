using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Millionaire.Domain.Abstract;
using Millionaire.Domain.Concrete;
using Millionaire.WebUI.Infrastructure.Abstuct;
using Millionaire.WebUI.Infrastructure.Concrete;

namespace Millionaire.WebUI.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUserRepository>().To<EfUserRepository>();
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
            kernel.Bind<UnitOfWork>().To<UnitOfWork>();
        }
    }
}
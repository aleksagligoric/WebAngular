using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.Repository;
using WebApp.Persistence.UnitOfWork;
using WebApp.Providers;
using WebApp.Hubs;

namespace WebApp.App_Start
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void RegisterTypes()
        {
         
            container.RegisterType<DbContext, ApplicationDbContext>(new PerResolveLifetimeManager());
            container.RegisterType<IRepositoryVozlio, VoziloRepository>();
            container.RegisterType<IRepositoryLinija, LinijaRepository>();
            container.RegisterType<IRepositoryCenaKarte, CenaKarteRepository>();
            container.RegisterType<IRepositoryKarta, KartaRepository>();
            container.RegisterType<IRepositoryStanica, StanicaRepository>();
            container.RegisterType<IRepositoryRedVoznje, RedVoznjeRepository>();
            container.RegisterType<IRepositoryCenovnik, CenovnikRepository>();
            container.RegisterType<IRepositorySlika, SlikaRepository>();


            //container.RegisterType<IProductRepositry, ProductRepository>();
            container.RegisterType<IUnitOfWork, DemoUnitOfWork>();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}
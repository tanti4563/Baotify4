using Sample.api.Data;
using Repository.Implements;
using Repository.Interfaces;
using Service.Implements;
using Service.Interfaces;
using System;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;

namespace Service
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // Register Entity Framework DbContext
            container.RegisterType<DbContext, ShipManageEntities>(new HierarchicalLifetimeManager());
            container.RegisterType<ShipManageEntities>(new HierarchicalLifetimeManager());

            // Register your repositories and services
            container.RegisterType<IRepository<Users>, Repository<Users>>();
            container.RegisterType<IUserService, UserService>();

            // Register Ferry Booking Service
            container.RegisterType<IFerryBookingService, FerryBookingService>();
        }
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register Entity Framework DbContext
            container.RegisterType<DbContext, ShipManageEntities>(new HierarchicalLifetimeManager());
            container.RegisterType<ShipManageEntities>(new HierarchicalLifetimeManager());

            // Register your repositories and services
            container.RegisterType<IRepository<Users>, Repository<Users>>();
            container.RegisterType<IUserService, UserService>();

            // Register Ferry Booking Service
            container.RegisterType<IFerryBookingService, FerryBookingService>();

            // Set Unity as the dependency resolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
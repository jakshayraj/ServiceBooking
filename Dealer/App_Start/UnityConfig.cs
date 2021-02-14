using Business;
using Business.Interface;
using Business.RepositoryHelper;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Dealer
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
           
            container.RegisterType<ICustomerManager, CustomerManager>();
            container.RegisterType<IVehicleManager, VehicleManager>();
            container.RegisterType<IMechanicManager, MechanicManager>();
            container.RegisterType<IServiceManager, ServiceManager>();
            container.RegisterType<IBookingManager, BookingManager>();

            container.AddNewExtension<RepositoryHelper>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
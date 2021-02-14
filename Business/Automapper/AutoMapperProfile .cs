using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities;
using Data.Model;

namespace Business.Automapper
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
        {
            //CreateMap<Dealer, DealerViewModel>();
            //CreateMap<DealerViewModel, Dealer>();

            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();

            CreateMap<Service, ServiceViewModel>();
            CreateMap<ServiceViewModel, Service>();

            CreateMap<Mechanic, MechanicViewModel>();
            CreateMap<MechanicViewModel, Mechanic>();

            CreateMap<Booking, BookingViewModel>();
            CreateMap<BookingViewModel, Booking>();

            CreateMap<Vehicle, VehicleViewModel>();
            CreateMap<VehicleViewModel, Vehicle>();

            CreateMap<StautsOfBooking, StautsOfBookingViewModel>();
            CreateMap<StautsOfBookingViewModel, StautsOfBooking>();
        }
    }
}

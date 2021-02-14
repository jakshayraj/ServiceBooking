using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using Data.Model;
using AutoMapper;
using System.Data.Entity;
using System.Linq;

namespace Data.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehiclesEntities _dbContext;

        public VehicleRepository()
        {
            _dbContext = new VehiclesEntities();
        }
        public string CreateVehicle(VehicleViewModel model)
        {
            if (model != null)
            {
                int vehicle = _dbContext.Vehicles.Where(a => a.LicencePlate == model.LicencePlate).Count();
                if (vehicle != 0)
                {
                    return "Exist";
                }
                else {
                    Vehicle entity = Mapper.Map<VehicleViewModel, Vehicle>(model);
                    _dbContext.Vehicles.Add(entity);
                    _dbContext.SaveChanges();
                    return "Vehicle added";
                }
            }

            return "Model is null";
        }

        public string DeleteVehicle(int? Id)
        {
            Vehicle entity = _dbContext.Vehicles.Find(Id);
            Booking booking;
            int count = _dbContext.Bookings.Where(a => a.VehicleId == entity.VehicleId).Count();
            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    booking = _dbContext.Bookings.Where(a => a.VehicleId == entity.VehicleId).FirstOrDefault();
                    _dbContext.Bookings.Remove(booking);
                    _dbContext.SaveChanges();
                }

            }
            _dbContext.Vehicles.Remove(entity);
            _dbContext.SaveChanges();
            return "Deleted";
        }

        public IEnumerable<VehicleViewModel> GetAllVehicle()
        {
            IEnumerable<VehicleViewModel> vehiclesEntities =
                Mapper.Map<IEnumerable<VehicleViewModel>>(_dbContext.Vehicles);
            return vehiclesEntities;
        }

        public IEnumerable<VehicleViewModel> GetAllVehicleByCustomer(int id)
        {
            IEnumerable<Vehicle> vehicle = _dbContext.Vehicles.Where(a => a.CustomerId == id);
            IEnumerable<VehicleViewModel> vehiclesEntities =
                Mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);
            return vehiclesEntities;
        }

        public VehicleViewModel GetVehicle(int? Id)
        {
            VehicleViewModel vehiclesEntities = Mapper.Map<VehicleViewModel>(_dbContext.Vehicles.Find(Id));
            return vehiclesEntities;
        }

       
        public string UpdateVehicle(VehicleViewModel model)
        {
            if (model != null)
            {
                Vehicle entity = _dbContext.Vehicles.Find(model.VehicleId);
                Mapper.Map(model, entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return "Vehicle updated";
            }

            return "Model is null";
        }
    }
}

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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly VehiclesEntities _dbContext;

        public CustomerRepository()
        {
            _dbContext = new VehiclesEntities();
        }
        public string CreateCustomer(CustomerViewModel model)
        {
            
            if (model != null)
            {
                int customer = _dbContext.Customers.Where(a => a.EmailId == model.EmailId).Count();
                if(customer > 0)
                {
                    return "Exist";
                }
                Customer entity = Mapper.Map<CustomerViewModel,Customer>(model);
                _dbContext.Customers.Add(entity);
                _dbContext.SaveChanges();
                return "Customer added";
            }

            return "Model is null";
        }

        public string DeleteCustomer(int? Id)
        {
            Customer entity = _dbContext.Customers.Find(Id);
            Vehicle vehicle;
            Booking booking;
            int count = _dbContext.Vehicles.Where(a=> a.CustomerId == entity.CustomerId).Count();
            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    vehicle = _dbContext.Vehicles.Where(a => a.CustomerId == entity.CustomerId).FirstOrDefault();
                    int countvehicle = _dbContext.Bookings.Where(a => a.VehicleId == vehicle.VehicleId).Count();
                    if (countvehicle != 0)
                    {
                        for (int j = 0; j < countvehicle; j++)
                        {
                            booking = _dbContext.Bookings.Where(a => a.VehicleId == vehicle.VehicleId).FirstOrDefault();
                            _dbContext.Bookings.Remove(booking);
                            _dbContext.SaveChanges();
                        }

                    }
                    _dbContext.Vehicles.Remove(vehicle);
                    _dbContext.SaveChanges();
                }
                
            }
            _dbContext.Customers.Remove(entity);
            _dbContext.SaveChanges();
            return "Deleted";
        }

        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {
            IEnumerable<CustomerViewModel> customersEntities =
                Mapper.Map<IEnumerable<CustomerViewModel>>(_dbContext.Customers);
            return customersEntities;
        }

        public CustomerViewModel GetCustomer(int? Id)
        {
            CustomerViewModel customersEntitie = Mapper.Map<CustomerViewModel>(_dbContext.Customers.Find(Id));
            return customersEntitie;
        }

        public string UpdateCustomer(CustomerViewModel model)
        {
            if (model != null)
            {

                Customer entity = _dbContext.Customers.Find(model.CustomerId);
                Mapper.Map(model, entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return "Customer updated";
            }

            return "Model is null";
        }
    }
}

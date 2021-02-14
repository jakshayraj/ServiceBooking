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
    public class MechanicRepository : IMechanicRepository
    {
        private readonly VehiclesEntities _dbContext;

        public MechanicRepository()
        {
            _dbContext = new VehiclesEntities();
        }
        public string CreateMechanic(MechanicViewModel model)
        {
            if (model != null)
            {
                Mechanic entity = Mapper.Map<MechanicViewModel, Mechanic>(model);
                _dbContext.Mechanics.Add(entity);
                _dbContext.SaveChanges();
                return "Mechanic added";
            }

            return "Model is null";
        }

        public string DeleteMechanic(int? Id)
        {
            Booking booking;
            Mechanic entity = _dbContext.Mechanics.Find(Id);
            int count = _dbContext.Bookings.Where(a => a.MechanicId == entity.MechanicId).Count();
            if (count != 0)
            {
                for (int j = 0; j < count; j++)
                {
                    booking = _dbContext.Bookings.Where(a => a.MechanicId == entity.MechanicId).FirstOrDefault();
                    _dbContext.Bookings.Remove(booking);
                    _dbContext.SaveChanges();
                }

            }
            _dbContext.Mechanics.Remove(entity);
            _dbContext.SaveChanges();
            return "Deleted";
        }

        public IEnumerable<MechanicViewModel> GetAllMechanic()
        {
            IEnumerable<MechanicViewModel> mechanicsEntities =
                Mapper.Map<IEnumerable<MechanicViewModel>>(_dbContext.Mechanics);
            return mechanicsEntities;
        }

        public MechanicViewModel GetMechanic(int? Id)
        {
            MechanicViewModel mechanicsEntities = Mapper.Map<MechanicViewModel>(_dbContext.Mechanics.Find(Id));
            return mechanicsEntities;
        }

        public string UpdateMechanic(MechanicViewModel model)
        {
            if (model != null)
            {
                Mechanic entity = _dbContext.Mechanics.Find(model.MechanicId);
                Mapper.Map(model, entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return "Mechanic updated";
            }

            return "Model is null";
        }
    }
}

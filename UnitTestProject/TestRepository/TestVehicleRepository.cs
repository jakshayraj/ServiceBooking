using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTests.TestRepository
{
    public class TestVehicleRepository : IVehicleRepository
    {
        private List<VehicleViewModel> _dbContext = new List<VehicleViewModel>
        {
            new VehicleViewModel{
                VehicleId = 1,
                LicencePlate = "GJ03DE3941",
                Model = "Alto",
                Brand = "Maruti suzuki",
                RegistraionDate = DateTime.Now,
                ChassiNo = "MA48590385M875389",
                CustomerId = 1
            }
        };

        public string CreateVehicle(VehicleViewModel model)
        {
            if (model != null)
            {
                int Vehicle = _dbContext.Where(a => a.VehicleId == model.VehicleId).Count();
                if (Vehicle > 0)
                {
                    return "Exist";
                }
                _dbContext.Add(model);
                return "Vehicle added";
            }

            return "Model is null";
        }

        public string DeleteVehicle(int? Id)
        {
            int id = (int)Id;
            VehicleViewModel VehicleEntitie = _dbContext.Where(s => s.VehicleId == id).First();
            _dbContext.Remove(VehicleEntitie);
            return "Deleted";
        }

        public IEnumerable<VehicleViewModel> GetAllVehicle()
        {
            IEnumerable<VehicleViewModel> VehicleEntities = _dbContext;
            return VehicleEntities;
        }

        public IEnumerable<VehicleViewModel> GetAllVehicleByCustomer(int id)
        {
            IEnumerable<VehicleViewModel> Vehicle = _dbContext.Where(a => a.CustomerId == id);
            return Vehicle;
        }

        public VehicleViewModel GetVehicle(int? Id)
        {
            int id = (int)Id;
            VehicleViewModel VehiclesEntities = _dbContext.Where(s => s.VehicleId == id).First();
            return VehiclesEntities;
        }

        public string UpdateVehicle(VehicleViewModel model)
        {
            if (model != null)
            {
                _dbContext.Add(model);
                return "Vehicle updated";
            }
            return "Model is null";
        }
    }
}

using Business.Interface;
using BusinessEntities;
using Data.Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public class VehicleManager : IVehicleManager
    {
        private readonly IVehicleRepository _VehicleRepository;

        public VehicleManager(IVehicleRepository VehicleRepository)
        {
            _VehicleRepository = VehicleRepository;
        }

        public IEnumerable<VehicleViewModel> GetAllVehicle()
        {
            return _VehicleRepository.GetAllVehicle();
        }
        public VehicleViewModel GetVehicle(int? Id)
        {
            return _VehicleRepository.GetVehicle(Id);
        }
        public string CreateVehicle(VehicleViewModel model)
        {
            return _VehicleRepository.CreateVehicle(model);
        }
        public string UpdateVehicle(VehicleViewModel model)
        {
            return _VehicleRepository.UpdateVehicle(model);
        }
        public string DeleteVehicle(int? Id)
        {
            return _VehicleRepository.DeleteVehicle(Id);
        }
        public IEnumerable<VehicleViewModel> GetAllVehicleByCustomer(int id)
        {
            return _VehicleRepository.GetAllVehicleByCustomer(id);
        }
    }
}

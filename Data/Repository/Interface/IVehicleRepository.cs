using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface IVehicleRepository
    {
        VehicleViewModel GetVehicle(int? Id);
        IEnumerable<VehicleViewModel> GetAllVehicle();
        IEnumerable<VehicleViewModel> GetAllVehicleByCustomer(int id);
        string CreateVehicle(VehicleViewModel model);
        string UpdateVehicle(VehicleViewModel model);
        string DeleteVehicle(int? Id);
    }
}

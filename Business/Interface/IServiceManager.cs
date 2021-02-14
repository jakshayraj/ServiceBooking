using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IServiceManager
    {
        ServiceViewModel GetService(int? Id);
        IEnumerable<ServiceViewModel> GetAllService();
        string CreateService(ServiceViewModel model);
        string UpdateService(ServiceViewModel model);
        string DeleteService(int? Id);
        IEnumerable<StautsOfBookingViewModel> GetAllServiceStatus();
    }
}

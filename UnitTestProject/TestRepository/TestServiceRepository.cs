using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTests.TestRepository
{
    public class TestServiceRepository : IServiceRepository
    {
        private List<ServiceViewModel> _dbContext = new List<ServiceViewModel>
        {
            new ServiceViewModel{
                ServiceId=1,
                ServiceName= "Washing",
                Price=1000,
                Duration="1 hour",
                Active = true
            }
        };
        private List<StautsOfBookingViewModel> _dbStatus = new List<StautsOfBookingViewModel>
        {
            new StautsOfBookingViewModel{
                Id=1,
                Status="Pending"
            }
        };

        public string CreateService(ServiceViewModel model)
        {
            if (model != null)
            {
                _dbContext.Add(model);
                return "Service added";
            }

            return "Model is null";
        }

        public string DeleteService(int? Id)
        {
            int id = (int)Id;
            ServiceViewModel ServiceEntitie = _dbContext.Where(s => s.ServiceId == id).First();
            _dbContext.Remove(ServiceEntitie);
            return "Deleted";
        }

        public IEnumerable<ServiceViewModel> GetAllService()
        {
            IEnumerable<ServiceViewModel> ServiceEntities = _dbContext;
            return ServiceEntities;
        }

        public IEnumerable<StautsOfBookingViewModel> GetAllServiceStatus()
        {
            IEnumerable<StautsOfBookingViewModel> ServicesStatusEntities =_dbStatus;
            return ServicesStatusEntities;
        }

        public ServiceViewModel GetService(int? Id)
        {
            int id = (int)Id;
            ServiceViewModel ServicesEntities = _dbContext.Where(s => s.ServiceId == id).First();
            return ServicesEntities;
        }

        public string UpdateService(ServiceViewModel model)
        {
            if (model != null)
            {
                _dbContext.Add(model);
                return "Service updated";
            }
            return "Model is null";
        }
    }
}

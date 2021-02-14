using Business.Interface;
using BusinessEntities;
using Data.Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public class ServiceManager : IServiceManager
    {
        private readonly IServiceRepository _ServiceRepository;

        public ServiceManager(IServiceRepository ServiceRepository)
        {
            _ServiceRepository = ServiceRepository;
        }

        public IEnumerable<ServiceViewModel> GetAllService()
        {
            return _ServiceRepository.GetAllService();
        }
        public ServiceViewModel GetService(int? Id)
        {
            return _ServiceRepository.GetService(Id);
        }
        public string CreateService(ServiceViewModel model)
        {
            return _ServiceRepository.CreateService(model);
        }
        public string UpdateService(ServiceViewModel model)
        {
            return _ServiceRepository.UpdateService(model);
        }
        public string DeleteService(int? Id)
        {
            return _ServiceRepository.DeleteService(Id);
        }

        public IEnumerable<StautsOfBookingViewModel> GetAllServiceStatus()
        {
            return _ServiceRepository.GetAllServiceStatus();
        }
    }
}

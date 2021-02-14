using Business.Interface;
using BusinessEntities;
using Data.Repository.Interface;
using System.Collections.Generic;

namespace Business
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository _CustomerRepository;

        public CustomerManager(ICustomerRepository CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }

        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {
            return _CustomerRepository.GetAllCustomer();
        }
        public CustomerViewModel GetCustomer(int? Id)
        {
            return _CustomerRepository.GetCustomer(Id);
        }
        public string CreateCustomer(CustomerViewModel model)
        {
            return _CustomerRepository.CreateCustomer(model);
        }
        public string UpdateCustomer(CustomerViewModel model)
        {
            return _CustomerRepository.UpdateCustomer(model);
        }
        public string DeleteCustomer(int? Id)
        {
            return _CustomerRepository.DeleteCustomer(Id);
        }
    }
}

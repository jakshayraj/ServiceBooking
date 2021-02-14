using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface ICustomerRepository
    {
        CustomerViewModel GetCustomer(int? Id);
        IEnumerable<CustomerViewModel> GetAllCustomer();
        string CreateCustomer(CustomerViewModel model);
        string UpdateCustomer(CustomerViewModel model);
        string DeleteCustomer(int? Id);
    }
}

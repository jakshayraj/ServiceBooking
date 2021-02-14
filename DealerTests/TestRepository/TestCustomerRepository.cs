using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTests.TestRepository
{
    public class TestCustomerRepository : ICustomerRepository
    {
        private List<CustomerViewModel> _dbContext = new List<CustomerViewModel>
        {
            new CustomerViewModel{
                CustomerId = 1,
                Name = "Akshayraj",
                Address1 = "Rajkot",
                Address2 = "Rajkot",
                Zipcode = 360003,
                PhoneNo = "8780088346",
                HomePhone = "1234",
                EmailId = "akshayraj123@gmail.com",
                Password = "123456", }
        };
        public string CreateCustomer(CustomerViewModel model)
        {
            if (model != null)
            {
                int customer = _dbContext.Where(a => a.EmailId == model.EmailId).Count();
                if (customer > 0)
                {
                    return "Exist";
                }
                _dbContext.Add(model);
                return "Customer added";
            }

            return "Model is null";
        }

        public string DeleteCustomer(int? Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        public CustomerViewModel GetCustomer(int? Id)
        {
            throw new NotImplementedException();
        }

        public string UpdateCustomer(CustomerViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

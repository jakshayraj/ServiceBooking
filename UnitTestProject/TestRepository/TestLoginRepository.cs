using BusinessEntities;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerTests.TestRepository
{
    public class TestLoginRepository : ILoginRepository
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
        public int validUser(LoginViewModel objUser)
        {
            var obj = _dbContext.Where(a => a.EmailId.Equals(objUser.emailid) && a.Password.Equals(objUser.password)).FirstOrDefault();
            if (obj != null)
            {
                return obj.CustomerId;
            }
            return 0;
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Users.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using BusinessEntities;
using System.Web.Mvc;
using Business;
using DealerTests.TestRepository;

namespace Dealer.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            var logincontroller = new LoginController(new LoginManager(new TestLoginRepository()), new CustomerManager(new TestCustomerRepository()));
            LoginViewModel login = new LoginViewModel() { emailid= "akshayraj123@gmail.com",password = "123456" };
            ViewResult result = logincontroller.Login(login) as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void SignupTest()
        {
            var logincontroller = new LoginController(new LoginManager(new TestLoginRepository()), new CustomerManager(new TestCustomerRepository()));
            CustomerViewModel customer = new CustomerViewModel()
            {
                CustomerId = 1,
                Name = "Akshayraj",
                Address1 = "Rajkot",
                Address2 = "Rajkot",
                Zipcode = 360003,
                PhoneNo = "8780088346",
                HomePhone = "1234",
                EmailId = "akshayraj123@gmail.com",
                Password = "123456"
            };
            ViewResult result = logincontroller.Signup(customer) as ViewResult;
            Assert.IsNotNull(result);
        }
    }

}
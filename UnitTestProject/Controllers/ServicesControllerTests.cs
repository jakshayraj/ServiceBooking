using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dealer.Controllers;
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
    public class ServicesControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            CustomersController controller = new CustomersController(new CustomerManager(new TestCustomerRepository()));
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
                Password = "123456",
            };
            // Act
            ViewResult result = controller.Index(customer) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void EditTest()
        {
            // Arrange
            CustomersController controller = new CustomersController(new CustomerManager(new TestCustomerRepository()));

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void DetailsTest()
        {
            // Arrange
            CustomersController controller = new CustomersController(new CustomerManager(new TestCustomerRepository()));

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            CustomersController controller = new CustomersController(new CustomerManager(new TestCustomerRepository()));

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
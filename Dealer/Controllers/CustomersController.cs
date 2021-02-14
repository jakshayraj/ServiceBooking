using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Business.Interface;
using BusinessEntities;
using log4net;

namespace Dealer.Controllers
{
    public class CustomersController : Controller
    {
        // For the loging
        private static readonly ILog Log = LogManager.GetLogger(typeof(CustomersController));
        // Customer Manager Instance
        private readonly ICustomerManager _customerManager;
        // Customer Controller constructor
        public CustomersController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }
        // GET: Index and create Customers in one controller
        // GET: Customers
        [Route("Customers/Index")]
        public ActionResult Index()
        {
            var customer = new CustomerViewModel()
            {
                customerlist = _customerManager.GetAllCustomer()
            };
            return View(customer);
        }
        // Post: Index and create Customers in one controller
        // Post: Customers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CustomerViewModel customer)
        {
            // Exception Handling
            try
            {
                if (ModelState.IsValid)
                {
                    string add = _customerManager.CreateCustomer(customer);
                    if (add == "Exist")
                    {
                        ModelState.AddModelError("", "Email id is already register");
                        Log.Error("Email id is already register");
                        var Customers = new CustomerViewModel()
                        {
                            customerlist = _customerManager.GetAllCustomer()
                        };
                        return View(Customers);
                    }
                    else
                    {
                        Log.Info("Customer added Successffuly");
                        return RedirectToAction("Index");
                    }
                }
            }
            // Catch the Exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in customer adding");
            }
            var customers = new CustomerViewModel()
            {
                customerlist = _customerManager.GetAllCustomer()
            };
            return View(customers);
        }

        // GET: Customers/Details/5
        [Route("Customers/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerViewModel customer = _customerManager.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }



        // GET: Customers/Edit/5
        [Route("Customers/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerViewModel customer = _customerManager.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Name,Address1,Address2,Zipcode,PhoneNo,HomePhone,EmailId,Password")] CustomerViewModel customer)
        {
            // Exception handling
            try
            {
                if (ModelState.IsValid)
                {
                    string update = _customerManager.UpdateCustomer(customer);
                    Log.Info("Customer edited Successffuly");
                    return RedirectToAction("Index");
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in customer updating");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Route("Customers/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerViewModel customer = _customerManager.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string delete = _customerManager.DeleteCustomer(id);
            Log.Info("Customer deleted Successffuly");
            return RedirectToAction("Index");
        }
    }
}

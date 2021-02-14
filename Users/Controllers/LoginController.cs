using Business.Interface;
using BusinessEntities;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Users.Controllers
{
    public class LoginController : Controller
    {
        // For the loging
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoginController));
        // Login and customer Manager Instance
        private readonly ILoginManager _LoginManager;
        private readonly ICustomerManager _customerManager;
        // Login Controller constructor
        public LoginController(ILoginManager LoginManager, ICustomerManager customerManager)
        {
            _LoginManager = LoginManager;
            _customerManager = customerManager;
        }
        

        // GET: Login
        [Route("Login/Login")]
        public ActionResult Login()
        {
            return View();
        }
        //POST: Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int valid = _LoginManager.validUser(objUser);
                    if (valid != 0) 
                    {
                        FormsAuthentication.SetAuthCookie(valid.ToString(), false);
                        Log.Info("Login Successffuly");
                        Session["Id"] = valid;
                        ViewBag.Message = "Login";
                        return this.RedirectToAction("Index", "Booking");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Email Id or Password");
                        ViewBag.Message = "Login";
                        Log.Error("Invalid Email id or password");
                        return View("Login");
                    }
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Model Error");
            }
            //Return view with user object
            return View(objUser);
        }
        //POST: Login/Signup
        [Route("Login/Signup")]
        public ActionResult Signup()
        {
            return View();
        }
        //POST: Booking/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(CustomerViewModel customer)
        {
            //Exception Handling
            try
            {
                if (ModelState.IsValid)
                {
                    string add = _customerManager.CreateCustomer(customer);
                    if (add == "Exist")
                    {
                        ModelState.AddModelError("", "Email id is already register");
                        return View(customer);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            //Catch the exception
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in customer signup");
            }
            return View(customer);
        }
        //GET: Loing/Logout
        //For user logout
        public ActionResult Logout()
        {
            //Logout Logic
            Session.Abandon();
            FormsAuthentication.SignOut();
            ViewBag.Message= "Logout";
            //Return to the Login Page
            return RedirectToAction("Login", "Login");
        }
    }
}
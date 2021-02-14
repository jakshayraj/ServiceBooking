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
    public class ServicesController : Controller
    {

        // For the loging
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServicesController));
        // Service Manager Instance
        private readonly IServiceManager _serviceManager;
        // Service Controller constructor
        public ServicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: Index and create services in one controller
        // GET: Service
        [Route("Services/Index")]
        public ActionResult Index()
        {
            var service = new ServiceViewModel()
            {
                servicelist = _serviceManager.GetAllService()
            };
            return View(service);
        }
        // Post: Index and create services in one controller
        // Post: Service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ServiceViewModel service)
        {
            // Exception Handling
            try
            {
                if (ModelState.IsValid)
                {
                    string add = _serviceManager.CreateService(service);
                    Log.Info("Service listing Successffuly");
                    return RedirectToAction("Index");
                }
            }
            // Catch the Exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in service listing");
            }
            var services = new ServiceViewModel()
            {
                servicelist = _serviceManager.GetAllService()
            };
            return View(services);
        }
        // GET: services/Details/5
        [Route("Services/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceViewModel service = _serviceManager.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: services/Edit/5
        [Route("Services/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceViewModel service = _serviceManager.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceViewModel service)
        {
            // Exception handling
            try
            { 
                if (ModelState.IsValid)
            {
                string update = _serviceManager.UpdateService(service);
                    Log.Info("Service edited Successffuly");
                    return RedirectToAction("Index");
            }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in product uploading");
            }
            return View(service);
        }

        // GET: services/Delete/5
        [Route("Services/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceViewModel service = _serviceManager.GetService(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Log.Info("Service deleted Successffuly");
            string delete = _serviceManager.DeleteService(id);
            return RedirectToAction("Index");
        }

    }
}

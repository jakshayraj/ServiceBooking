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
namespace Users.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        // For the loging
        private static readonly ILog Log = LogManager.GetLogger(typeof(VehiclesController));
        // Vehicle and customer manager instance
        private readonly IVehicleManager _vehicleManager;
        private readonly ICustomerManager _customerManager;
        // Vehicle Controller constructor
        public VehiclesController(IVehicleManager vehicleManager, ICustomerManager customerManager)
        {
            _vehicleManager = vehicleManager;
            _customerManager = customerManager;
        }
        // GET: Index and create vehicle in one controller
        // GET: Vehicles
        [Route("Vehicles/Index")]
        public ActionResult Index()
        {
            int id = (int)Session["Id"];
            ViewBag.CustomerId = new SelectList(_customerManager.GetAllCustomer(), "CustomerId", "Name");
            var vehicle = new VehicleViewModel()
            {
                vehiclelist = _vehicleManager.GetAllVehicleByCustomer(id)
            };
            return View(vehicle);
        }
        // Post: Index and create vehicle in one controller
        // Post: Vehicles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VehicleViewModel vehicle)
        {
            ModelState.Remove("CustomerId");
            vehicle.CustomerId = (int)Session["Id"];
            int id = (int)Session["Id"];
            // Exception Handling
            try
            {
                if (ModelState.IsValid)
                {
                    string add = _vehicleManager.CreateVehicle(vehicle);
                    // If vehicle is exist in database
                    if (add == "Exist")
                    {
                        ViewBag.CustomerId = new SelectList(_customerManager.GetAllCustomer(), 
                            "CustomerId", "Name");
                        Log.Error("Vehicle already register");
                        var Vehicles = new VehicleViewModel()
                        {
                            vehiclelist = _vehicleManager.GetAllVehicleByCustomer(id)
                        };
                        return View(Vehicles);
                    }
                    // If vehicle is not exist in database then added successfully
                    else
                    {
                        Log.Info("Vehicle added Successffuly");
                        return RedirectToAction("Index");
                    }
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in vehicle uploading");
            }
            ViewBag.CustomerId = new SelectList(_customerManager.GetAllCustomer(), "CustomerId", "Name");
            var vehicles = new VehicleViewModel()
            {
                vehiclelist = _vehicleManager.GetAllVehicleByCustomer(id)
            };
            return View(vehicles);
        }
        // GET: Vehicles/Details/5
        [Route("Vehicles/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleViewModel vehicle = _vehicleManager.GetVehicle(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }



        // GET: Vehicles/Edit/5
        [Route("Vehicles/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleViewModel vehicle = _vehicleManager.GetVehicle(id);
            ViewBag.CustomerId = new SelectList(_customerManager.GetAllCustomer(), "CustomerId", "Name");
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehicleViewModel vehicle)
        {
            // Exception handling
            try
            {
                if (ModelState.IsValid)
            {
                string update = _vehicleManager.UpdateVehicle(vehicle);
                    Log.Info("Product edit Successffuly");
                    return RedirectToAction("Index");
            }
                 }
           // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in product edition");
            }
            ViewBag.CustomerId = new SelectList(_customerManager.GetAllCustomer(), "CustomerId", "Name");
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int id)
        {
            
            VehicleViewModel vehicle = _vehicleManager.GetVehicle(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string delete = _vehicleManager.DeleteVehicle(id);
            return RedirectToAction("Index");
        }

       
    }
}

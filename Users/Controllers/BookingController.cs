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
    public class BookingController : Controller
    {

        // For the loging
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookingController));
        // Booking Manager Instance
        private readonly IBookingManager _bookingManager;
        // Service Manager Instance
        private readonly IServiceManager _serviceManager;
        // Vehicle Manager Instance
        private readonly IVehicleManager _vehicleManager;
        // Booking Controller constructor

        public BookingController(IBookingManager bookingManager, IServiceManager serviceManager, IVehicleManager vehicleManager)
        {
            _bookingManager = bookingManager;
            _serviceManager = serviceManager;
            _vehicleManager = vehicleManager;
        }
        // GET: Booking
        // GET: Index and create Booking in one controller
        [Route("Booking/Index")]
        public ActionResult Index()
        {
            int custiId = (int)Session["Id"];
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicleByCustomer(custiId), "VehicleId", "LicencePlate");
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName");
            var booking = new BookingViewModel()
            {
                bookinglist = _bookingManager.GetAllBookingByCustomer(custiId)
            };
            return View(booking);
        }
        // Post: Index and create Bookings in one controller
        // Post: Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BookingViewModel booking)
        {
            try
            {
                booking.CustomerId = (int)Session["Id"];
                if (ModelState.IsValid)
                {
                    if (booking.StartBookingDate < DateTime.Now)
                    {
                        throw new Exception("Start Booking date must be greater than today's date");
                    }
                    if (booking.StartBookingDate > booking.EndBookingDate)
                    {
                        throw new Exception("End Booking date must be greater than start booking date");
                    }
                    string add = _bookingManager.CreateBooking(booking);
                    if (add == "Exist")
                    {
                        int custId = (int)Session["Id"];
                        ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicleByCustomer(custId), "VehicleId", "LicencePlate");
                        ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName");
                        ModelState.AddModelError("", "Booking already register");
                        var Bookings = new BookingViewModel()
                        {
                            bookinglist = _bookingManager.GetAllBooking()
                        };
                        return View(Bookings);
                    }
                    else
                    {
                        Log.Info("Booking added Successffuly");
                        return RedirectToAction("Index");
                    }
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in booking uploading");
            }
            int custiId = (int)Session["Id"];
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicleByCustomer(custiId), "VehicleId", "LicencePlate");
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName");
            var bookings = new BookingViewModel()
            {
                bookinglist = _bookingManager.GetAllBooking()
            };
            return View(bookings);
        }
        // GET: Booking/Details/5
        [Route("Booking/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingViewModel booking = _bookingManager.GetBooking(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Edit/5
        [Route("Booking/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingViewModel booking = _bookingManager.GetBooking(id);
            int custiId = (int)Session["Id"];
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicleByCustomer(custiId).Where(a => a.VehicleId == booking.VehicleId), "VehicleId", "LicencePlate", selectedValue: booking.VehicleId);
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName", selectedValue: booking.ServiceId);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingViewModel booking)
        {
            // Exception handlling
            try
            {
                if (ModelState.IsValid)
                {
                    if (booking.StartBookingDate < DateTime.Now)
                    {
                        throw new Exception("Start Booking date must be greater than today's date");
                    }
                    if (booking.StartBookingDate > booking.EndBookingDate)
                    {
                        throw new Exception("End Booking date must be greater than start booking date");
                    }
                    Log.Info("Booking updated Successffuly");
                    string update = _bookingManager.UpdateBooking(booking);
                    return RedirectToAction("Index");
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", ex.Message);
            }
            int custiId = (int)Session["Id"];
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicleByCustomer(custiId).Where(a => a.VehicleId == booking.VehicleId), "VehicleId", "LicencePlate", selectedValue: booking.VehicleId);
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName", selectedValue: booking.ServiceId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        [Route("Booking/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            
            BookingViewModel booking = _bookingManager.GetBooking(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string delete = _bookingManager.DeleteBooking(id);
            return RedirectToAction("Index");
        }
    }
}

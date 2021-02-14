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
using ClosedXML.Excel;
using System.IO;
using log4net;

namespace Dealer.Controllers
{
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
        // GET: Index and create Booking in one controller
        // GET: Booking
        [Route("Booking/Index")]
        public ActionResult Index(DateTime? StartDate, DateTime? EndDate)
        {
            // For the filtering
            if (StartDate != null && EndDate != null)
            {
                var booking = new BookingViewModel()
                {
                    bookinglist = _bookingManager.GetAllBooking().Where(s => s.StartBookingDate >= StartDate).Where(s => s.StartBookingDate <= StartDate)
                };
                ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle(), "VehicleId", "LicencePlate");
                ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName");
                return View(booking);
            }
            // For the normal listing
            else
            {
                var booking = new BookingViewModel()
                {
                    bookinglist = _bookingManager.GetAllBooking()
                };
                ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle(), "VehicleId", "LicencePlate");
                ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName");
                return View(booking);
            }
        }
        // Post: Index and create Bookings in one controller
        // Post: Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BookingViewModel booking)
        {
            // Exception Handling
            try
            {
                VehicleViewModel vehicle = _vehicleManager.GetAllVehicle().Where(a => a.VehicleId == booking.VehicleId).First();
                booking.CustomerId = (int)vehicle.CustomerId;
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
                        ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle(), "VehicleId", "LicencePlate");
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
            var bookings = new BookingViewModel()
            {
                bookinglist = _bookingManager.GetAllBooking()
            };
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle(), "VehicleId", "LicencePlate");
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService(), "ServiceId", "ServiceName");
            return View(bookings);
        }


        // GET: Booking/ChangeStatus/5
        [Route("Booking/ChangeStatus/{id}")]
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingViewModel booking = _bookingManager.GetBooking(id);
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle().Where(a => a.VehicleId == booking.VehicleId), "VehicleId", "LicencePlate", selectedValue: booking.VehicleId);
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService().Where(a => a.ServiceId == booking.ServiceId), "ServiceId", "ServiceName", selectedValue: booking.ServiceId);
            ViewBag.Status =  new SelectList(_serviceManager.GetAllServiceStatus(), "Id", "Status", selectedValue: booking.Status);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/ChangeStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(BookingViewModel booking)
        {
            // Exception handling
            try
            {
                if (ModelState.IsValid) 
                {
                    string update = _bookingManager.UpdateBooking(booking);
                    Log.Info("Booking status updated Successffuly");
                    return RedirectToAction("Index");
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in change status");
            }
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle().Where(a => a.VehicleId == booking.VehicleId), "VehicleId", "LicencePlate", selectedValue: booking.VehicleId);
            ViewBag.ServiceId = new SelectList(_serviceManager.GetAllService().Where(a => a.ServiceId == booking.ServiceId), "ServiceId", "ServiceName", selectedValue: booking.ServiceId);
            ViewBag.Status = new SelectList(_serviceManager.GetAllServiceStatus(), "Id", "Status", selectedValue: booking.Status);
            return View(booking);
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
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle().Where(a => a.VehicleId == booking.VehicleId), "VehicleId", "LicencePlate", selectedValue: booking.VehicleId);
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
            ViewBag.VehicleId = new SelectList(_vehicleManager.GetAllVehicle().Where(a=> a.VehicleId == booking.VehicleId), "VehicleId", "LicencePlate", selectedValue: booking.VehicleId);
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
            Log.Info("Booking deleted Successffuly");
            return RedirectToAction("Index");
        }

        // POST: For export the data in excel sheet
        [HttpPost]
        public FileResult Export()
        {
            
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { 
                                            new DataColumn("BookingId"),
                                            new DataColumn("LicencePlate"),
                                            new DataColumn("ServiceName"),
                                            new DataColumn("StartBookingDate"),
                                            new DataColumn("EndBookingDate"),
                                            new DataColumn("Mechanic"),
                                            new DataColumn("Customer"),
                                            new DataColumn("Status"),});

            var bookings = from booking in _bookingManager.GetAllBooking()
                            select booking;

            foreach (var booking in bookings)
            {
                dt.Rows.Add(booking.BookingId, booking.Vehicle.LicencePlate, booking.Service.ServiceName, booking.StartBookingDate, booking.EndBookingDate, booking.Mechanic.Name, booking.Vehicle.Customer, booking.StautsOfBooking.Status);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Booking.xlsx");
                }
            }
        }
    }
}

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
    public class MechanicsController : Controller
    {
        // For the loging
        private static readonly ILog Log = LogManager.GetLogger(typeof(MechanicsController));
        // Mechanic Manager Instance
        private readonly IMechanicManager _mechanicManager;
        // Mechanic Controller constructor
        public MechanicsController(IMechanicManager mechanicManager)
        {
            _mechanicManager = mechanicManager;
        }
        // GET: Index and create Mechanics in one controller
        // GET: Mechanics
        [Route("Mechanics/Index")]
        public ActionResult Index()
        {
            var mechanic = new MechanicViewModel()
            {
                mechaniclist = _mechanicManager.GetAllMechanic()
            };
            Log.Info("Mechanic listing Successffuly");
            return View(mechanic);
        }
        // Post: Index and create Mechanics in one controller
        // Post: Mechanics
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MechanicViewModel mechanic)
        {
            // Exception Handling
            try
            {
                if (ModelState.IsValid)
                {
                    string add = _mechanicManager.CreateMechanic(mechanic);
                    Log.Info("Mechanic created Successffuly");
                    return RedirectToAction("Index");
                }
            }
            // Catch the Exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in mechanic created");
            }
            var mechanics = new MechanicViewModel()
            {
                mechaniclist = _mechanicManager.GetAllMechanic()
            };
            return View(mechanics);
        }
        // GET: Mechanics/Details/5
        [Route("Mechanics/Details/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MechanicViewModel mechanic = _mechanicManager.GetMechanic(id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            return View(mechanic);
        }
        // GET: Mechanics/Edit/5
        [Route("Mechanics/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MechanicViewModel mechanic = _mechanicManager.GetMechanic(id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            return View(mechanic);
        }

        // POST: Mechanic/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MechanicViewModel mechanic)
        {
            // Exception handling
            try
            {
                if (ModelState.IsValid)
                {
                    string update = _mechanicManager.UpdateMechanic(mechanic);
                    Log.Info("Mechanic update Successffuly");
                    return RedirectToAction("Index");
                }
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in mechanic updating");
            }
            return View(mechanic);
        }

        // GET: Mechanics/Delete/5
        [Route("Mechanics/Delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MechanicViewModel mechanic = _mechanicManager.GetMechanic(id);
            if (mechanic == null)
            {
                return HttpNotFound();
            }
            return View(mechanic);
        }

        // POST: Mechanics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Exception handling
            try
            {
                string delete = _mechanicManager.DeleteMechanic(id);
                Log.Info("Mechanic deleted Successffuly");
                return RedirectToAction("Index");
            }
            // Catch the exception
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                ModelState.AddModelError("", "Error in product deleting");
            }
            return RedirectToAction("Index");
        }
    }
}

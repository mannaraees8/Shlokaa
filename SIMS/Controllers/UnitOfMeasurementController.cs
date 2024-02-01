using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.BL;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]

    public class UnitOfMeasurementController : Controller
    {
        // GET: UnitOfMeasurement

        public ActionResult Index(int? pageIndex, string search = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("UnitOfMeasurement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetunitOfMeasurementList(search).ToPagedList(pageIndex ?? 1, 10));

                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }

        }


        private IEnumerable<UnitOfMeasurementModel> GetunitOfMeasurementList(string search = "")
        {
            List<UnitOfMeasurementModel> unitOfMeasurementModelList = new List<UnitOfMeasurementModel>();
            List<UnitOfMeasurement> unitOfMeasurementList = UnitOfMeasurement.RetrieveAll();

            if (search.Length > 0)
            {
                unitOfMeasurementList = unitOfMeasurementList.OfType<UnitOfMeasurement>().Where(s => s.UnitOfMeasurementName.ToLower().Contains(search.ToLower()) /*|| s.StaffName.Contains(search)*/).ToList();
            }

            foreach (UnitOfMeasurement a in unitOfMeasurementList)
            {
                unitOfMeasurementModelList.Add(new UnitOfMeasurementModel(a));
            }

            return unitOfMeasurementModelList;
        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("UnitOfMeasurement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost]
        public ActionResult Create(UnitOfMeasurementModel unitOfMeasurementModel)
        {
            bool inValidState = false;


            if (unitOfMeasurementModel.UnitOfMeasurementName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Name";
            }



            if (inValidState == false)
            {

                if (ModelState.IsValid)
                {
                    int count = UnitOfMeasurement.Create(unitOfMeasurementModel.UnitOfMeasurementName.Trim());
                    if (count != 0)
                    {
                        return RedirectToAction("Index", "UnitOfMeasurement");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Unit already exists.";
                    }
                }


            }

            return View(unitOfMeasurementModel);
        }


        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("UnitOfMeasurement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new UnitOfMeasurementModel(UnitOfMeasurement.RetrieveById(id)));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost]
        public ActionResult Edit(UnitOfMeasurementModel unitOfMeasurementModel)
        {
            bool inValidState = false;


            if (unitOfMeasurementModel.UnitOfMeasurementName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Name";
            }



            if (inValidState == false)
            {

                if (ModelState.IsValid)
                {
                    bool status = unitOfMeasurementModel.UnitOfMeasurement.Update();
                    if (status == true)
                    {
                        return RedirectToAction("Index", "UnitOfMeasurement");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Unit already exists.";
                    }
                }


            }

            return View(unitOfMeasurementModel);
        }




        public ActionResult Delete(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("UnitOfMeasurement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        UnitOfMeasurement unitOfMeasurement = UnitOfMeasurement.RetrieveById(id);
                        if (unitOfMeasurement == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new UnitOfMeasurementModel(unitOfMeasurement));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            bool status = false;
            UnitOfMeasurement unitOfMeasurement = UnitOfMeasurement.RetrieveById(id);
            status = unitOfMeasurement.Delete();

            if (status == false)
            {
                return RedirectToAction("Index", "UnitOfMeasurement");
            }
            ViewBag.count = true;
            return View("Delete", new UnitOfMeasurementModel(unitOfMeasurement));
        }

        public ActionResult Details(int id = 0)
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("UnitOfMeasurement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsDetails == true)
                    {
                        return View(new UnitOfMeasurementModel(UnitOfMeasurement.RetrieveById(id)));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

    }
}
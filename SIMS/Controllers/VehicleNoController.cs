using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.VehicleNoModel;
using SIMS.BL;
using PagedList;

namespace SIMS.Controllers
{
    public class VehicleNoController : Controller
    {
        // GET: VehicleNo
        public ActionResult Index(int? pageIndex)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleNo");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetDepartmentModelList().ToPagedList(pageIndex ?? 1, 10));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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

        private IEnumerable<VehicleNoModel> GetDepartmentModelList()
        {
            List<VehicleNoModel> vehicleNoModelList = new List<VehicleNoModel>();
            foreach (VehicleNo a in VehicleNo.RetrieveAll())
            {
                vehicleNoModelList.Add(new VehicleNoModel(a));
            }


            return vehicleNoModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleNo");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new VehicleNoModel(VehicleNo.RetrieveById(id)));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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
        public ActionResult Edit(VehicleNoModel vehicleNoModel)
        {
            VehicleNo vehicleNo = VehicleNo.RetrieveById(vehicleNoModel.Id);
            if (vehicleNo != null)
            {
                vehicleNoModel.VehicleNoObj.Isdeleted = vehicleNoModel.Isdeleted;
            }
            if (ModelState.IsValid)
            {
                vehicleNoModel.VehicleNoObj.Update();
                return RedirectToAction("Index");
            }
            return View(vehicleNoModel);
        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleNo");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new VehicleNoModel());
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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
        public ActionResult Create(VehicleNoModel vehicleNoModel)
        {
            if (ModelState.IsValid)
            {
                VehicleNo.Create(vehicleNoModel.VehicleNum, vehicleNoModel.Isdeleted);
                return RedirectToAction("Index");
            }

            return View(vehicleNoModel);
        }

        public ActionResult Delete(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleNo");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        VehicleNo vehicleNo = VehicleNo.RetrieveById(id);
                        if (vehicleNo == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new VehicleNoModel(vehicleNo));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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
            VehicleNo vehicleNo = VehicleNo.RetrieveById(id);
            vehicleNo.Delete();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new VehicleNoModel(VehicleNo.RetrieveById(id)));
        }
    }
}
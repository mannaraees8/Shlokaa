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
    public class PlaceController : Controller
    {
        // GET: Place
        public ActionResult Index(int? pageIndex, string search = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Place");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetPlaceModelList(search).ToPagedList(pageIndex ?? 1, 10)); ;

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

        private IEnumerable<PlaceModel> GetPlaceModelList(string search = "")
        {
            List<PlaceModel> placeModelList = new List<PlaceModel>();
            List<Place> placeList = Place.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Place> g = new GenericList<Place>();
                placeList = g.SerachFun(placeList, search);
                placeList = placeList.Distinct().ToList();
            }

            foreach (Place a in placeList)
            {
                placeModelList.Add(new PlaceModel(a));
            }

            return placeModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Place");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new PlaceModel(Place.RetrieveById(id)));
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
        public ActionResult Edit(PlaceModel placeModel)
        {
            bool inValidState = false;

            if (placeModel.PlaceName.Trim() == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Transport Vehicle";
            }

            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    placeModel.Place.Update();
                    return RedirectToAction("Index", "Place");
                }
            }

            return View();
        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Place");
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
        public ActionResult Create(PlaceModel placeModel)
        {
            bool inValidState = false;

            if (placeModel.PlaceName.Trim() == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Transport Vehicle";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {

                    Place.Create(placeModel.PlaceName, placeModel.Isdeleted);
                    return RedirectToAction("Index", "Place");
                }
            }

            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            Place place = Place.RetrieveById(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Place");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new PlaceModel(place));
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
            Place place = Place.RetrieveById(id);

            place.Delete();
            return RedirectToAction("Index", "Place");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new PlaceModel(Place.RetrieveById(id)));
        }
    }
}
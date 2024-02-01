using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using SIMS.ViewModels.AccessMatrixModel;
using SIMS.BL;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class AccessMatrixController : Controller
    {
        //
        // GET: /AccessMatrix/

        public ActionResult Index(int? pageIndex)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(GetAccessMatrixList().ToPagedList(pageIndex ?? 1, 10));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }


        private IEnumerable<AccessMatrixModel> GetAccessMatrixList()
        {
            List<AccessMatrixModel> accessMatrixModelList = new List<AccessMatrixModel>();
            foreach (AccessMatrix a in AccessMatrix.RetrieveAll())
            {
                accessMatrixModelList.Add(new AccessMatrixModel(a));
            }

            return accessMatrixModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            //List<Users> lstUsers = Users.RetrieveAll();
            //Users users = new Users();
            //users.Name = "Select";
            //lstUsers.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(new AccessMatrixModel(AccessMatrix.RetrieveById(id)));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(AccessMatrixModel accessMatrixModel)
        {
            AccessMatrix accessMatrix = AccessMatrix.RetrieveById(accessMatrixModel.Id);
            if (accessMatrix != null)
            {
                accessMatrixModel.AccessMatrix.Isdeleted = accessMatrix.Isdeleted;
            }
            if (ModelState.IsValid)
            {
                accessMatrixModel.AccessMatrix.Update();
                return RedirectToAction("Index");
            }
            //List<Users> lstUsers = Users.RetrieveAll();
            //Users users = new Users();
            //users.Name = "Select";
            //lstUsers.Insert(0, users);
            return View(accessMatrixModel);
        }

        public ActionResult Create()
        {
            //List<Users> lstUsers = Users.RetrieveAll();
            //Users users = new Users();
            //users.Name = "Select";
            //lstUsers.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View();
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(AccessMatrixModel accessMatrixModel)
        {
            if (ModelState.IsValid)
            {
                AccessMatrix.Create(accessMatrixModel.ModuleName,accessMatrixModel.IsDeleted);
                return RedirectToAction("Index");
            }

            //List<Users> lstUsers = Users.RetrieveAll();
            //Users users = new Users();
            //users.Name = "Select";
            //lstUsers.Insert(0, users);
            return View(accessMatrixModel);
        }

        public ActionResult Delete(int id = 0)
        {
            AccessMatrix accessMatrix = AccessMatrix.RetrieveById(id);
            if (accessMatrix == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(new AccessMatrixModel(accessMatrix));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            AccessMatrix accessMatrix = AccessMatrix.RetrieveById(id);
            accessMatrix.Delete();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new AccessMatrixModel(AccessMatrix.RetrieveById(id)));
        }
    }
}


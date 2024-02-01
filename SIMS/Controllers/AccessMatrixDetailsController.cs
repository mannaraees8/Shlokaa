using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using SIMS.ViewModels.AccessMatrixDetailsModel;
using SIMS.BL;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class AccessMatrixDetailsController : Controller
    {
        //
        // GET: /AccessMatrixDetails/

        public ActionResult Index(int? pageIndex, string search = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            List<Users> lstUserType = Users.RetrieveUniqueUserType();
            List<AccessMatrix> accessMatrixlst = AccessMatrix.RetrieveAll();

            for(int i=0;i< accessMatrixlst.Count; i++)
            {
                AccessMatrixDetails accessMatrixDetailslst = AccessMatrixDetails.RetrieveAccessById(accessMatrixlst[i].Id);
                if (accessMatrixDetailslst == null)
                {
                    foreach(var type in lstUserType)
                    {
                        AccessMatrixDetails.Create(accessMatrixlst[i].Id, type.Usertype, true, true, true, true, true, true, true, true, true);

                    }

                }
            }


            //for (int i = 0; i < lstUsers.Count; i++)
            //{
            //    for (int j = 0; j < accessMatrixlst.Count; j++)
            //    {
            //        AccessMatrixDetails accessMatrixDetailslst = AccessMatrixDetails.RetrieveAccessByModuleId(lstUsers[i].Id, accessMatrixlst[j].Id);
            //        if (accessMatrixDetailslst == null)
            //        {
            //            AccessMatrixDetails.Create(accessMatrixlst[j].Id, lstUsers[i].Id, false, false, false, false, false, false, false, false, false);

            //            //if (lstUsers[i].Usertype == "Admin" || lstUsers[i].Usertype == "Group Head")
            //            //{
            //            //    AccessMatrixDetails.Create(accessMatrixlst[j].Id, lstUsers[i].Id, true, true, true, true, true, true, true, true, true);
            //            //}
            //            //else
            //            //{
            //            //    AccessMatrixDetails.Create(accessMatrixlst[j].Id, lstUsers[i].Id, false, false, false, false, false, false, false, false, false);
            //            //}

            //        }
            //    }
            //}

            if (userType == "Admin" || userType == "Group Head")
            {
                return View(GetAccessMatrixList(search).ToPagedList(pageIndex ?? 1, 10));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }


        private IEnumerable<AccessMatrixDetailsModel> GetAccessMatrixList(string search = "")
        {
            List<AccessMatrixDetailsModel> accessMatrixModelList = new List<AccessMatrixDetailsModel>();
            List<AccessMatrixDetails> accessMatrixDetailslst = AccessMatrixDetails.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<AccessMatrixDetails> g = new GenericList<AccessMatrixDetails>();
                accessMatrixDetailslst = g.SerachFun(accessMatrixDetailslst, search);
                accessMatrixDetailslst = accessMatrixDetailslst.Distinct().ToList();
            }

            foreach (AccessMatrixDetails a in accessMatrixDetailslst)
            {
                accessMatrixModelList.Add(new AccessMatrixDetailsModel(a));
            }

            return accessMatrixModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            //List<AccessMatrix> accessMatrixList = AccessMatrix.RetrieveAll();
            //AccessMatrix accessMatrix = new AccessMatrix();
            //accessMatrix.ModuleName = "Select";
            //accessMatrixList.Insert(0, accessMatrix);


            //List<Users> usersList = Users.RetrieveAll();
            //Users users = new Users();
            //users.Name = "Select";
            //usersList.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(new AccessMatrixDetailsModel(AccessMatrixDetails.RetrieveById(id)));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(AccessMatrixDetailsModel accessMatrixModel)
        {
            AccessMatrixDetails accessMatrix = AccessMatrixDetails.RetrieveById(accessMatrixModel.Id);
            bool inValidState = false;

         
            if (accessMatrixModel.AccessMatrixId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Module Name";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    accessMatrixModel.AccessMatrixDetails.Update();
                    return RedirectToAction("Index");
                }
            }
            //List<AccessMatrix> accessMatrixList = AccessMatrix.RetrieveAll();
            //AccessMatrix accessMatrix1 = new AccessMatrix();
            //accessMatrix1.ModuleName = "Select";
            //accessMatrixList.Insert(0, accessMatrix1);


            //List<Users> usersList = Users.RetrieveAll();
            //Users users = new Users();
            //users.Name = "Select";
            //usersList.Insert(0, users);
            return View(new AccessMatrixDetailsModel(accessMatrix));
        }

        public ActionResult Create()
        {
            List<AccessMatrix> accessMatrixList = AccessMatrix.RetrieveAll();
            AccessMatrix accessMatrix1 = new AccessMatrix();
            accessMatrix1.ModuleName = "Select";
            accessMatrixList.Insert(0, accessMatrix1);
            List<Users> usersList = Users.RetrieveUniqueUserType();
            Users users = new Users();
            users.Name = "Select";
            usersList.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(new AccessMatrixDetailsModel(accessMatrixList, usersList));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(AccessMatrixDetailsModel accessMatrixModel)
        {
            bool inValidState = false;
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);

            if (accessMatrixModel.AccessMatrixId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Module Name";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    AccessMatrixDetails.Create(accessMatrixModel.AccessMatrixId, accessMatrixModel.UserType, accessMatrixModel.IsIndex, accessMatrixModel.IsCreate, accessMatrixModel.IsEdit, accessMatrixModel.IsDetails, accessMatrixModel.IsTally, accessMatrixModel.IsUpdate, accessMatrixModel.IsSearch, accessMatrixModel.IsExport, accessMatrixModel.IsDeleted);
                    return RedirectToAction("Index");
                }
            }
            List<AccessMatrix> accessMatrixList = AccessMatrix.RetrieveAll();
            AccessMatrix accessMatrix1 = new AccessMatrix();
            accessMatrix1.ModuleName = "Select";
            accessMatrixList.Insert(0, accessMatrix1);

            List<Users> usersList = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            usersList.Insert(0, users);
            return View(new AccessMatrixDetailsModel(accessMatrixList, usersList));
        }

        public ActionResult Delete(int id = 0)
        {
            AccessMatrixDetails accessMatrix = AccessMatrixDetails.RetrieveById(id);
            if (accessMatrix == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(new AccessMatrixDetailsModel(accessMatrix));
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
            AccessMatrixDetails accessMatrix = AccessMatrixDetails.RetrieveById(id);
            accessMatrix.Delete();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new AccessMatrixDetailsModel(AccessMatrixDetails.RetrieveById(id)));
        }
    }
}


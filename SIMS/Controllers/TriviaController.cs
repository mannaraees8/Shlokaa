using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SIMS.Models;
using SIMS.BL;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class TriviaController : Controller
    {
        // GET: Trivia

        public ActionResult Index(int? pageIndex)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Group Head"||userType=="Admin")
            {
                return View(GetTriviaModelList().ToPagedList(pageIndex ?? 1, 10));
            }            
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }
        }


        private IEnumerable<TriviaModel> GetTriviaModelList()
        {
            List<TriviaModel> triviaModelList = new List<TriviaModel>();
            foreach (Trivia a in Trivia.RetrieveAll())
            {
                triviaModelList.Add(new TriviaModel(a));
            }

            return triviaModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);

            if (userType == "Group Head" || userType == "Admin")
            {
                return View(new TriviaModel(Trivia.RetrieveById(id)));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }

        }

        [HttpPost]
        public ActionResult Edit(TriviaModel triviaModel, HttpPostedFileBase Photo)
        {
            byte[] imgByte = null;
            bool inValidState = false;

            if (Photo != null && Photo.FileName != "")
            {
                string docName = Path.GetFileName(Photo.FileName);
                imgByte = new byte[Photo.ContentLength];
                Photo.InputStream.Read(imgByte, 0, (int)Photo.ContentLength);
                if (triviaModel.TabName == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Tab Name";
                }
                if (triviaModel.Type == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Type";
                }
                if (triviaModel.Type == "Paragrapgh")
                {
                    if (triviaModel.SubContents == "")
                    {
                        inValidState = true;
                        TempData["ErrorMsg"] = "Please Enter SubContents";
                    }

                }
                if (triviaModel.Type == "Point")
                {
                    if (triviaModel.Point1 == "")
                    {
                        inValidState = true;
                        TempData["ErrorMsg"] = "Please Enter Points atleast one";
                    }

                }
                if (inValidState == false)
                {
                    if (ModelState.IsValid)
                    {
                        //int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                        //	materialMovementModel.Staffid = userId;
                        triviaModel.Trivia.Update(imgByte);
                        return RedirectToAction("Index", "Trivia");
                    }
                }
            }
            else
            {
                if (triviaModel.TabName == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Tab Name";
                }
                if (triviaModel.Type == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Type";
                }
                if (triviaModel.Type == "Paragrapgh")
                {
                    if (triviaModel.SubContents == "")
                    {
                        inValidState = true;
                        TempData["ErrorMsg"] = "Please Enter SubContents";
                    }

                }
                if (triviaModel.Type == "Point")
                {
                    if (triviaModel.Point1 == "")
                    {
                        inValidState = true;
                        TempData["ErrorMsg"] = "Please Enter Points atleast one";
                    }

                }
                if (inValidState == false)
                {
                    if (ModelState.IsValid)
                    {
                        //int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                        //	materialMovementModel.Staffid = userId;
                        triviaModel.Trivia.Update();
                        return RedirectToAction("Index", "Trivia");
                    }
                }
            }
          
            return View(triviaModel);
        }

        public ActionResult Create()
        {
            //int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            //	materialMovementModel.Staffid = userId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(TriviaModel triviaModel, HttpPostedFileBase Photo)
        {
            byte[] imgByte = null;
            bool inValidState = false;
            if (Photo != null && Photo.FileName != "")
            {
                string docName = Path.GetFileName(Photo.FileName);
                imgByte = new byte[Photo.ContentLength];
                Photo.InputStream.Read(imgByte, 0, (int)Photo.ContentLength);

            }
            else
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select  Photo";
            }
            if (triviaModel.TabName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Tab Name";
            }
            if (triviaModel.Type == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Type";
            }
            if (triviaModel.Type == "Paragrapgh")
            {
                if (triviaModel.SubContents == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter SubContents";
                }

            }
            if (triviaModel.Type == "Point")
            {
                if (triviaModel.Point1 == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Points atleast one";
                }

            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    //int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    //	materialMovementModel.Staffid = userId;
                    Trivia.Create(triviaModel.TabName.Trim(), triviaModel.Contents.Trim(), triviaModel.Title.Trim(), triviaModel.SubContents.Trim(), triviaModel.Point1.Trim(), triviaModel.Point2.Trim(), triviaModel.Point3.Trim(), triviaModel.Point4.Trim(), triviaModel.Point5.Trim(), triviaModel.Point6.Trim(), triviaModel.Point7.Trim(), triviaModel.Point8.Trim(), triviaModel.Point9.Trim(), triviaModel.Point10.Trim(), triviaModel.Type.Trim(), imgByte);
                    return RedirectToAction("Index", "Trivia");
                }
            }

            return View(triviaModel);
        }

        public ActionResult Delete(int id = 0)
        {
            Trivia trivia = Trivia.RetrieveById(id);
            if (trivia == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);

            if (userType == "Group Head" || userType == "Admin")
            {
                return View(new TriviaModel(trivia));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Trivia trivia = Trivia.RetrieveById(id);
            trivia.Delete();
            return RedirectToAction("Index", "Trivia");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new TriviaModel(Trivia.RetrieveById(id)));
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.ViewModels.UsersModel;
using SIMS.BL;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index(int? pageIndex)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Gallery");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetGalleryModelList().ToPagedList(pageIndex ?? 1, 10));
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



        private IEnumerable<GalleryModel> GetGalleryModelList()
        {
            List<GalleryModel> galleryModelList = new List<GalleryModel>();
            foreach (GalleryBl a in GalleryBl.RetrieveAll())
            {
                galleryModelList.Add(new GalleryModel(a));
            }


            return galleryModelList;
        }


        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Gallery");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new GalleryModel(GalleryBl.RetrieveById(id)));
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

        //[HttpPost]
        //public ActionResult Edit(GalleryModel galleryModel, HttpPostedFileBase GalleryPhoto, HttpPostedFileBase ThumbnailPhoto)
        //{
        //    if (GalleryPhoto != null && GalleryPhoto.FileName != "")
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                string OldFile = Request.MapPath(galleryModel.FileDataOldData);
        //                if (System.IO.File.Exists(OldFile))
        //                {
        //                    System.IO.File.Delete(OldFile);
        //                }
        //                string galleryPhoto = "";
        //                galleryPhoto = Guid.NewGuid() + Path.GetExtension(GalleryPhoto.FileName);
        //                GalleryPhoto.SaveAs(Server.MapPath("~/GalleryImages/" + galleryPhoto));
        //                string fullpath = "~/GalleryImages/" + galleryPhoto;
        //                Gallery.RetrieveById(id);
        //                galleryModel.Gallery.Update();
        //                return RedirectToAction("Index", "Gallery");
        //            }
        //        }
        //    }

        //    return View(galleryModel);
        //}
        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Gallery");
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
        public ActionResult Create(GalleryModel galleryModel, HttpPostedFileBase GalleryPhoto, HttpPostedFileBase ThumbnailPhoto)
        {
            string fileName = "";
            bool inValidState = false;
            string galleryImage = "";
            string thumbnail = "";
            string fullpathGalleryPhoto = "", fullpathThumbnailPhoto = "";
            if (galleryModel.FileType == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select FileType";
            }
            if (galleryModel.FileType == "Brochure" || galleryModel.FileType == "Trivia")
            {
                if (GalleryPhoto != null && GalleryPhoto.FileName != "")
                {
                    fileName = GalleryPhoto.FileName;
                    galleryImage = Guid.NewGuid() + Path.GetExtension(GalleryPhoto.FileName);
                    GalleryPhoto.SaveAs(Server.MapPath("~/GalleryImages/PdfData/" + galleryImage));
                    fullpathGalleryPhoto = "~/GalleryImages/PdfData/" + galleryImage;


                }
                else
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Image";
                }
                if (ThumbnailPhoto != null && ThumbnailPhoto.FileName != "")
                {
                    //fileName = Path.GetFileName(Thumbnail.FileName);
                    thumbnail = Guid.NewGuid() + Path.GetExtension(ThumbnailPhoto.FileName);
                    ThumbnailPhoto.SaveAs(Server.MapPath("~/GalleryImages/Photo/" + thumbnail));
                    fullpathThumbnailPhoto = "~/GalleryImages/Photo/" + thumbnail;

                    if (ThumbnailPhoto.ContentLength >= (1024 * 1024) * 2)
                    {
                        inValidState = true;
                        TempData["ErrorMsg"] = "Thumbnail Size Should be less than 2MB";
                    }

                }
                else
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Image";
                }
            }
            if (galleryModel.FileType == "Image")
            {
                if (GalleryPhoto != null && GalleryPhoto.FileName != "")
                {
                    fileName = GalleryPhoto.FileName;
                    galleryImage = Guid.NewGuid() + Path.GetExtension(GalleryPhoto.FileName);
                    GalleryPhoto.SaveAs(Server.MapPath("~/GalleryImages/Photo/" + galleryImage));
                    fullpathGalleryPhoto = "~/GalleryImages/Photo/" + galleryImage;


                }
                else
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Image";
                }
            }
            if (galleryModel.FileType == "Brochure" || galleryModel.FileType == "Trivia")
            {
                if (galleryModel.Title.Trim() == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Title";
                }

            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    GalleryBl.Create(fileName, fullpathGalleryPhoto, fullpathThumbnailPhoto, galleryModel.FileType, galleryModel.Title.Trim());
                    return RedirectToAction("Index", "Gallery");
                }
            }
            return View(galleryModel);
        }

        public ActionResult Delete(int id = 0)
        {
            GalleryBl gallery = GalleryBl.RetrieveById(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Gallery");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new GalleryModel(gallery));
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
            GalleryBl gallery = GalleryBl.RetrieveById(id);
            gallery.Delete();

            string FileDataOldData = Request.MapPath(gallery.FileDataOldData);
            string ThumbnailOldData = Request.MapPath(gallery.ThumbnailOldData);

            if (System.IO.File.Exists(FileDataOldData))
            {
                System.IO.File.Delete(FileDataOldData);
            }
            if (System.IO.File.Exists(ThumbnailOldData))
            {
                System.IO.File.Delete(ThumbnailOldData);
            }
            return RedirectToAction("Index", "Gallery");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new UsersModel(Users.RetrieveById(id)));
        }

    }
}
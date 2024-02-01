using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.CategoryModel;
using SIMS.Models;
using SIMS.BL;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]

    public class CategoryController : Controller
    {
        // GET: Category

        public ActionResult Index(int? pageIndex, string search = "")
       {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Category");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {

                        return View(GetCategoryModelList(search).ToPagedList(pageIndex ?? 1, 10));

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

        private IEnumerable<CategoryModel> GetCategoryModelList(string search = "")
        {
            List<CategoryModel> categoryModelList = new List<CategoryModel>();
            List<Category> categoryList = Category.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Category> g = new GenericList<Category>();
                categoryList = g.SerachFun(categoryList, search);
                categoryList = categoryList.Distinct().ToList();
            }

            foreach (Category a in categoryList)
            {
                categoryModelList.Add(new CategoryModel(a));
            }

            return categoryModelList;
        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Category");
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
        public ActionResult Create(CategoryModel CategoryModel, HttpPostedFileBase ImageFile)
        {

            bool inValidState = false;


            if (CategoryModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Category";

            }
            else if (CategoryModel.Discription == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Description";

            }
            else if (ImageFile == null || ImageFile.FileName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Upload Image";

            }
            else if (ImageFile != null && ImageFile.FileName != "")
            {
                if (ImageFile.ContentLength >= (1024 * 1024) * 2)
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "File size must be less than 2MB";
                }
            }

            if (inValidState == false)
            {

                if (ModelState.IsValid)
                {
                    //byte[] byteDocContents = null;
                    string categoryImage = "";


                    categoryImage = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    ImageFile.SaveAs(Server.MapPath("~/CategoryImages/" + categoryImage));
                    string fullpath = "~/CategoryImages/" + categoryImage;

                    if (ModelState.IsValid)
                    {
                        int count = Category.Create(CategoryModel.Name.Trim(), CategoryModel.Discription.Trim(), fullpath);
                        if (count != 0)
                        {
                            return RedirectToAction("Index", "Category");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Category already exists.";
                        }
                    }
                }


            }


            return View(CategoryModel);


        }


        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Category");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {

                        return View(new CategoryModel(Category.RetrieveById(id)));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Category");
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
        public ActionResult Edit(CategoryModel categoryModel, HttpPostedFileBase ImageFile)
        {
            bool inValidState = false;

            if (categoryModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Category";

            }
            else if (categoryModel.Discription == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Description";

            }
            else if (ImageFile != null && ImageFile.FileName != "")
            {
                if (ImageFile.ContentLength >= (1024 * 1024) * 2)
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "File size must be less than 2MB";
                }
            }

            if (inValidState == false)
            {
                if (ImageFile != null && ImageFile.FileName != "")
                {


                    if (ModelState.IsValid)
                    {
                        string OldFile = Request.MapPath(categoryModel.OldFile);
                        if (System.IO.File.Exists(OldFile))
                        {
                            System.IO.File.Delete(OldFile);
                        }

                        string categoryImage = "";
                        categoryImage = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                        ImageFile.SaveAs(Server.MapPath("~/CategoryImages/" + categoryImage));
                        string fullpath = "~/CategoryImages/" + categoryImage;

                        if (ModelState.IsValid)
                        {
                            bool status = categoryModel.Category.Update(fullpath);
                            if (status == true)
                            {
                                return RedirectToAction("Index", "Category");
                            }
                            else
                            {
                                TempData["ErrorMsg"] = "Category already exists.";
                            }
                        }
                    }

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        bool status = categoryModel.Category.Update();
                        if (status == true)
                        {
                            return RedirectToAction("Index", "Category");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Category already exists.";
                        }
                    }
                }
            }

            return View(new CategoryModel(Category.RetrieveById(categoryModel.Id)));


        }




        public ActionResult Delete(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Category");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        Category category = Category.RetrieveById(id);
                        if (category == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new CategoryModel(category));


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
            bool category = false;
            Category subcategory = Category.RetrieveById(id);
            category = subcategory.Delete();

            if (category == false)
            {
                return RedirectToAction("Index", "Category");
            }

            ViewBag.count = true;
            return View("Delete", new CategoryModel(subcategory));
        }

        public ActionResult Details(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Category");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsDetails == true)
                    {
                        return View(new CategoryModel(Category.RetrieveById(id)));
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
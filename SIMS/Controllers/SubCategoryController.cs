using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        public ActionResult Index(int? pageIndex, string search = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SubCategory");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetSubCategoryModelList(search).ToPagedList(pageIndex ?? 1, 10));

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


        private IEnumerable<SubCategoryModel> GetSubCategoryModelList(string search = "")
        {
            List<SubCategoryModel> SubCategoryModelList = new List<SubCategoryModel>();
            List<SubCategory> categoryList = SubCategory.RetrieveAll();


            if (search.Length > 0)
            {
                GenericList<SubCategory> g = new GenericList<SubCategory>();
                categoryList = g.SerachFun(categoryList, search);
                categoryList = categoryList.Distinct().ToList();
            }

            foreach (SubCategory a in categoryList)
            {
                SubCategoryModelList.Add(new SubCategoryModel(a));
            }


            return SubCategoryModelList;
        }
        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SubCategory");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        List<Category> lstCenter = Category.RetrieveAll();
                        Category category = new Category();
                        category.Name = "Select";
                        lstCenter.Insert(0, category);
                        SubCategoryModel CategoryModel = new SubCategoryModel(lstCenter, SubCategory.RetrieveById(id));
                        return View(CategoryModel);
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
        public ActionResult Edit(SubCategoryModel SubCategoryModel)
        {
            bool inValidState = false;


            if (SubCategoryModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Sub Category";
            }
            else if (SubCategoryModel.CategoryID == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Category";
            }


            if (inValidState == false)
            {
                bool status = SubCategoryModel.SubCategory.Update();
                if (status == true)
                {
                    return RedirectToAction("Index", "SubCategory");
                }
                else
                {
                    TempData["ErrorMsg"] = "Sub Category already exists.";

                }

            }
            List<Category> lstCenter = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            lstCenter.Insert(0, category);
            SubCategoryModel CategoryModel = new SubCategoryModel(lstCenter, SubCategory.RetrieveById(SubCategoryModel.Id));
            return View(CategoryModel);


        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SubCategory");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        List<Category> lstCenter = Category.RetrieveAll();
                        Category category = new Category();
                        category.Name = "Select";
                        lstCenter.Insert(0, category);
                        SubCategoryModel CategoryModel = new SubCategoryModel(lstCenter);
                        return View(CategoryModel);
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
        public ActionResult Create(SubCategoryModel SubCategoryModel)
        {
            bool inValidState = false;


            if (SubCategoryModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Sub Category";
            }
            else if (SubCategoryModel.CategoryID == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Category";
            }

            if (inValidState == false)
            {

                if (ModelState.IsValid)
                {
                    int count = SubCategory.Create(SubCategoryModel.CategoryID, SubCategoryModel.Name.Trim(), SubCategoryModel.Discription.Trim());
                    if (count == 0)
                    {
                        TempData["ErrorMsg"] = "Sub Category already exists.";
                        return View(SubCategoryModel);
                    }
                    else
                    {
                        return RedirectToAction("Index", "SubCategory");
                    }

                }


            }
            List<Category> lstCenter = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            lstCenter.Insert(0, category);
            SubCategoryModel CategoryModel = new SubCategoryModel(lstCenter);
            return View(CategoryModel);

        }

        public ActionResult Delete(int id = 0)
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SubCategory");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        SubCategory category = SubCategory.RetrieveById(id);
                        if (category == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new SubCategoryModel(category));
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
            bool subcategory = false;
            SubCategory category = SubCategory.RetrieveById(id);
            subcategory = category.Delete();
            if (subcategory == false)
            {
                return RedirectToAction("Index", "SubCategory");
            }
            ViewBag.count = true;
            return View("Delete", new SubCategoryModel(category));
        }

        public ActionResult Details(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SubCategory");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsDetails == true)
                    {
                        return View(new SubCategoryModel(SubCategory.RetrieveById(id)));
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

        public ActionResult Excel(string search = "")
        {
            List<SubCategoryModel> SubCategoryModelList = new List<SubCategoryModel>();
            List<SubCategory> subCategoryList = SubCategory.RetrieveAll();


            if (search.Length > 0)
            {
                GenericList<SubCategory> g = new GenericList<SubCategory>();
                subCategoryList = g.SerachFun(subCategoryList, search);
                subCategoryList = subCategoryList.Distinct().ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SubCategory List");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Category";
                worksheet.Cell(currentrow, 2).Value = "Sub Category";


                foreach (SubCategory a in subCategoryList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Category;
                    worksheet.Cell(currentrow, 2).Value = a.Name;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SubCategoryList.xlsx");
                }
            }

        }
    }
}
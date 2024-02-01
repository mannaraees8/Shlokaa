using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.SalesModel;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using System.Globalization;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        //
        // GET: /Sales/
        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Sales");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetSalesModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
                    }
                    else
                    {
                        return View(GetSalesRetreiveByIdList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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

        private IEnumerable<SalesModel> GetSalesModelList(string search = "", string startdate = "", string enddate = "")
        {
            List<SalesModel> salesModelList = new List<SalesModel>();
            List<Sales> salesList = Sales.RetrieveAll();
            //int year = DateTime.Now.Year;
            //salesList = salesList.OfType<Sales>().Where(s => s.Date.Year == year).ToList();
            if (search.Length > 0)
            {
                GenericList<Sales> g = new GenericList<Sales>();
                salesList = g.SerachFun(salesList, search);
                salesList = salesList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesList = salesList.OfType<Sales>().Where(s => s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesList = salesList.OfType<Sales>().Where(s => s.Date <= endDate).ToList();
            }

            foreach (Sales a in salesList)
            {
                salesModelList.Add(new SalesModel(a));
            }

            return salesModelList;
        }

        private IEnumerable<SalesModel> GetSalesRetreiveByIdList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            List<SalesModel> salesModelList = new List<SalesModel>();
            List<Sales> salesList = Sales.RetrieveAllByUserId(userId);
            int year = DateTime.Now.Year;
            //salesList = salesList.OfType<Sales>().Where(s => s.Date.Year == year).ToList();
            if (search.Length > 0)
            {
                GenericList<Sales> g = new GenericList<Sales>();
                salesList = g.SerachFun(salesList, search);
                salesList = salesList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesList = salesList.OfType<Sales>().Where(s => (s.ProductCategory.ToLower().Contains(search.ToLower()) || s.MarketingExecutive.ToLower().Contains(search.ToLower()) || s.SalesReturnAmount.ToString().Contains(search.ToLower()) || s.SalesAmount.ToString().Contains(search.ToLower())) && s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesList = salesList.OfType<Sales>().Where(s => (s.ProductCategory.ToLower().Contains(search.ToLower()) || s.MarketingExecutive.ToLower().Contains(search.ToLower()) || s.SalesReturnAmount.ToString().Contains(search.ToLower()) || s.SalesAmount.ToString().Contains(search.ToLower())) && s.Date <= endDate).ToList();
            }

            foreach (Sales a in salesList)
            {
                salesModelList.Add(new SalesModel(a));
            }

            return salesModelList;
        }
        //public JsonResult SalesDateSearch(DateTime startDate, DateTime endDate)
        //{
        //    List<Sales> salesList = Sales.RetrieveAllDataByDate(startDate, endDate).ToList();
        //    return new JsonResult { Data = salesList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        //}
        public ActionResult Edit(int id = 0)
        {

            List<Users> userList = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);

            List<Category> categoryList = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            categoryList.Insert(0, category);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Sales");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new SalesModel(Sales.RetrieveById(id), userList, categoryList));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Home", "Staff");
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
        public ActionResult Edit(SalesModel salesModel)
        {

            if (ModelState.IsValid)
            {
                if (salesModel.StaffID != 0)
                {
                    Users users1 = Users.RetrieveById(salesModel.StaffID);
                    salesModel.MarketingExecutive = users1.Name;
                    salesModel.Sales.Update();
                }
                else
                {
                    salesModel.Sales.Update();
                }
                return RedirectToAction("Index", "Sales");

            }

            List<Users> userList = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);

            List<Category> categoryList = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            categoryList.Insert(0, category);
            return View(new SalesModel(userList, categoryList));
        }

        public ActionResult Create()
        {
            List<Users> userList = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);

            List<Category> categoryList = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            categoryList.Insert(0, category);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Sales");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new SalesModel(userList, categoryList));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Home", "Staff");
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
        public ActionResult Create(SalesModel salesModel)
        {

            if (ModelState.IsValid)
            {
                if (salesModel.StaffID != 0)
                {
                    Users users1 = Users.RetrieveById(salesModel.StaffID);
                    Sales.Create(salesModel.Date, salesModel.StaffID, users1.Name, salesModel.ProductCategory, salesModel.SalesAmount, salesModel.SalesReturnAmount);
                }
                else
                {
                    Sales.Create(salesModel.Date, salesModel.StaffID, salesModel.MarketingExecutive, salesModel.ProductCategory, salesModel.SalesAmount, salesModel.SalesReturnAmount);
                }
                return RedirectToAction("Index", "Sales");

            }

            List<Users> userList = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);

            List<Category> categoryList = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            categoryList.Insert(0, category);
            return View(new SalesModel(userList, categoryList));
        }

        public ActionResult Delete(int id = 0)
        {
            Sales sales = Sales.RetrieveById(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Sales");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new SalesModel(sales));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Home", "Staff");
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
            Sales sales = Sales.RetrieveById(id);
            bool status = sales.Delete();
            if (status == true)
            {
                return RedirectToAction("Index", "Sales");
            }
            return View("Delete", new SalesModel(Sales.RetrieveById(id)));
        }

        public ActionResult Details(int id = 0)
        {
            return View(new SalesModel(Sales.RetrieveById(id)));
        }

        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<SalesModel> salesModelList = new List<SalesModel>();
            List<Sales> salesList = Sales.RetrieveAll();
            int year = DateTime.Now.Year;
            //salesList = salesList.OfType<Sales>().Where(s => s.Date.Year == year).ToList();
            if (search.Length > 0)
            {
                GenericList<Sales> g = new GenericList<Sales>();
                salesList = g.SerachFun(salesList, search);
                salesList = salesList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesList = salesList.OfType<Sales>().Where(s => (s.ProductCategory.ToLower().Contains(search.ToLower()) || s.MarketingExecutive.ToLower().Contains(search.ToLower()) || s.SalesReturnAmount.ToString().Contains(search.ToLower()) || s.SalesAmount.ToString().Contains(search.ToLower())) && s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesList = salesList.OfType<Sales>().Where(s => (s.ProductCategory.ToLower().Contains(search.ToLower()) || s.MarketingExecutive.ToLower().Contains(search.ToLower()) || s.SalesReturnAmount.ToString().Contains(search.ToLower()) || s.SalesAmount.ToString().Contains(search.ToLower())) && s.Date <= endDate).ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SalesList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Date";
                worksheet.Cell(currentrow, 2).Value = "Employee Name";
                worksheet.Cell(currentrow, 3).Value = "Product Category";
                worksheet.Cell(currentrow, 4).Value = "Sales Amount";
                worksheet.Cell(currentrow, 5).Value = "Sales Return Amount ";

                foreach (Sales a in salesList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Date;
                    worksheet.Cell(currentrow, 2).Value = a.MarketingExecutive;
                    worksheet.Cell(currentrow, 3).Value = a.ProductCategory;
                    worksheet.Cell(currentrow, 4).Value = a.SalesAmount;
                    worksheet.Cell(currentrow, 5).Value = a.SalesReturnAmount;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sales.xlsx");
                }
            }

        }
    }
}


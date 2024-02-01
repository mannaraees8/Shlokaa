using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels;
using SIMS.Models;
using SIMS.BL;
using System.IO;
using SIMS.ViewModels.SalesOrderModel;
using SIMS.ViewModels.UsersModel;
using ClosedXML.Excel;
using System.Drawing;
using System.Globalization;
using PagedList;

namespace SIMS.Views.Staff
{
    [Authorize]
    public class StaffController : Controller
    {

        //  GET: Admin

        //public JsonResult getSalesOrderStaffId()
        //{
        //    List<SalesOrder> salesOrderStaffId = SalesOrder.RetrieveAllChartTallyData();

        //    return new JsonResult { Data = salesOrderStaffId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public ActionResult Index()
        {
            GetAttendanceModelList();
            return View();
        }


        public ActionResult Home()
        {

            return View();
        }
        public ActionResult Master()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);

            if (userType == "Group Head" || userType == "Admin")
            {
                return View();
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }
        }

        private void GetAttendanceModelList()
        {
            DateTime today = DateTime.Today;
            string status = "Absent";
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            List<Attendance> attendanceList = new List<Attendance>();
            while (startDate != today)
            {
               attendanceList = Attendance.RetrieveByDate(startDate);

                if (attendanceList.Count == 0)
                {
                    foreach (Users b in Users.RetrieveAllWithoutAdmin())
                    {
                        Attendance.Create(b.Id, startDate, b.EnteringTime, b.LeavingTime, b.EnteringTime, b.LeavingTime, b.Department, status);

                    }
                }
                startDate = startDate.AddDays(1);

            }
            attendanceList = Attendance.RetrieveByDate(today);

            if (attendanceList.Count == 0)
            {
                foreach (Users b in Users.RetrieveAllWithoutAdmin())
                {
                    Attendance.Create(b.Id, startDate, b.EnteringTime, b.LeavingTime, b.EnteringTime, b.LeavingTime, b.Department, status);

                }
            }
        }

        //public ActionResult Salesorder(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SalesOrder");
        //    if (accessMatrix != null)
        //    {
        //        AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
        //        if (accessMatrixDetails != null)
        //        {
        //            if (accessMatrixDetails.IsIndex == true)
        //            {
        //                return View(GetSalesOrderModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "SalesOrder");

        //            }
        //        }
        //        else
        //        {
        //            TempData["ErrorMsg"] = "Access permission not defined";
        //            return RedirectToAction("Index", "Staff");

        //        }
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Module Not defined";
        //        return RedirectToAction("Index", "Staff");
        //    }
        //}


        //private IEnumerable<SalesOrderModel> GetSalesOrderModelList(string search = "", string startdate = "", string enddate = "")
        //{
        //    List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
        //    List<SalesOrder> salesorderList = SalesOrder.RetrieveAll();
        //    salesorderList = salesorderList.OrderByDescending(a => a.Timestamp).ToList();

        //    if (search.Length > 0)
        //    {
        //        GenericList<SalesOrder> g = new GenericList<SalesOrder>();
        //        salesorderList = g.SerachFun(salesorderList, search);
        //        salesorderList = salesorderList.Distinct().ToList();
        //    }
        //    if (startdate.Length > 0)
        //    {
        //        DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

        //        salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp >= startDate).ToList();
        //    }
        //    if (enddate.Length > 0)
        //    {

        //        DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
        //        salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp <= endDate).ToList();
        //    }
        //    salesorderList = salesorderList.OrderByDescending(s => s.Timestamp).ToList();
        //    foreach (SalesOrder a in salesorderList)
        //    {
        //        salesOrderModelList.Add(new SalesOrderModel(a));
        //    }

        //    return salesOrderModelList;
        //}

        //public ActionResult Salesorderdetails(int? pageIndex, int id = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head" || userType == "Admin" || userType == "Executive Admin" || userType == "Executive Accounts" || userType == "Executive Operations" || userType == "Executive Marketing" || userType == "Associate Operations" || userType == "Associate Admin" || userType == "Associate Accounts" || userType == "Associate Marketing")
        //    {
        //        return View(GetSalesOrderDetailsList(id).ToPagedList(pageIndex ?? 1, 10));
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Your not authorised to access this page";
        //        return RedirectToAction("Index", "Staff");
        //    }

        //}
        //public JsonResult getTotalOrderValue(int id)
        //{
        //    List<SalesOrder> salesOrderlist = SalesOrder.RetrieveAllDetails(id).ToList();

        //    return new JsonResult { Data = salesOrderlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
        //public ActionResult Edit(int subCategoryId=0,int id = 0, int itemid = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head" || userType == "Admin" || userType == "Executive Marketing" || userType == "Associate Marketing")
        //    {

        //        List<Item> lstItem = Item.RetrieveBySubCategoryId(subCategoryId);
        //        Item item = new Item();
        //        item.Name = "Select";
        //        lstItem.Insert(0, item);


        //        List<ProductSize> lstItemSizeList = ProductSize.RetrieveById(itemid);
        //        ProductSize itemSize = new ProductSize();
        //        itemSize.Size = "Select";
        //        lstItemSizeList.Insert(0, itemSize);


        //        List<Category> lstCategoryList = Category.RetrieveAll();
        //        Category category = new Category();
        //        category.Name = "Select";
        //        lstCategoryList.Insert(0, category);



        //        List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
        //        SubCategory subCategory = new SubCategory();
        //        subCategory.Name = "Select";
        //        lstSubCategoryList.Insert(0, subCategory);


        //        List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
        //        UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
        //        uniOfMeasurement.UnitOfMeasurementName = "Select";
        //        lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

        //        return View(new SalesOrderModel(lstItem, lstItemSizeList, lstCategoryList, lstSubCategoryList, lstUnitOfMeasurementList, SalesOrder.RetrieveById(id)));

        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Your not authorised to access this page";
        //        return RedirectToAction("Index", "Staff");
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(SalesOrderModel salesOrderModel)
        //{
        //    bool inValidState = false;


        //    if (salesOrderModel.CategoryId == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Category";
        //    }
        //    else if (salesOrderModel.SubCategoryId == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Sub Category";
        //    }
        //    else if (salesOrderModel.ItemId == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Item";
        //    }

        //    else if (salesOrderModel.Quantity == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Enter Quantity";
        //    }
        //    else if (salesOrderModel.SizeId == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Size";
        //    }


        //    if (inValidState == false)
        //    {
        //        int pageIndex = 0;
        //        if (ModelState.IsValid)
        //        {
        //            int Id = salesOrderModel.Id;
        //            salesOrderModel.SalesOrder.Update(Id);
        //            return View("Salesorderdetails", GetSalesOrderDetailsList(salesOrderModel.Id, pageIndex));
        //        }


        //    }
        //    List<Item> lstItem = Item.RetrieveAll();
        //    Item item = new Item();
        //    item.Name = "Select";
        //    lstItem.Insert(0, item);


        //    List<ProductSize> lstItemSizeList = ProductSize.RetrieveById(salesOrderModel.ItemId);
        //    ProductSize itemSize = new ProductSize();
        //    itemSize.Size = "Select";
        //    lstItemSizeList.Insert(0, itemSize);


        //    List<Category> lstCategoryList = Category.RetrieveAll();
        //    Category category = new Category();
        //    category.Name = "Select";
        //    lstCategoryList.Insert(0, category);



        //    List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
        //    SubCategory subCategory = new SubCategory();
        //    subCategory.Name = "Select";
        //    lstSubCategoryList.Insert(0, subCategory);


        //    List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
        //    UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
        //    uniOfMeasurement.UnitOfMeasurementName = "Select";
        //    lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

        //    return View(new SalesOrderModel(lstItem, lstItemSizeList, lstCategoryList, lstSubCategoryList, lstUnitOfMeasurementList, SalesOrder.RetrieveById(salesOrderModel.Id))); ;
        //}


        //public ActionResult Delete(int id = 0, int pageIndex = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head" || userType == "Admin" || userType == "Executive Accounts")
        //    {
        //        return View(GetSalesOrderDetailsList(id, pageIndex));
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Your not authorised to access this page";
        //        return RedirectToAction("Index", "Staff");
        //    }

        //}
        //private IEnumerable<SalesOrderModel> GetSalesOrderDetailsList(int id, int pageIndex = 0)
        //{
        //    List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
        //    foreach (SalesOrder a in SalesOrder.RetrieveAllDetails(id))
        //    {
        //        salesOrderModelList.Add(new SalesOrderModel(a));
        //    }

        //    return salesOrderModelList;
        //}
        //[HttpPost]
        //public ActionResult Delete(SalesOrderModel salesOrderModel)
        //{
        //    int id = salesOrderModel.Id;
        //    SalesOrder salesOrder = SalesOrder.RetrieveOrderDetailsById(id);
        //    bool status = salesOrder.Delete();
        //    return RedirectToAction("Salesorder", "Staff");


        //}
        //[HttpPost]
        //public JsonResult DeleteItem(int id, int salesorderdetailsid)
        //{

        //    SalesOrder salesOrder = new SalesOrder();
        //    int count = salesOrder.DeleteOrderDetails(id, salesorderdetailsid);

        //    if (count != 1)
        //    {

        //        return new JsonResult { Data = count, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    return new JsonResult { Data = count, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        //}
        public ActionResult ChangePassword()
        {
            return View();
        }

        public FileContentResult UserPhoto()
        {

            int userId = 0;
            userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            Users users = Users.RetrieveById(userId);
            if (users.Photo == null)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Photo/default_user.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/png");

            }
            else
            {
                return new FileContentResult(users.Photo, "image/jpeg");

            }

        }


        public ActionResult UserName()
        {
            int userId = 0;
            userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            Users users = Users.RetrieveById(userId);
            //   var Name = users.Name;
            return View(users.Name);
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            bool success = false;
            int userId = 0;
            userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            Users users = new Users();
            changePasswordModel.Id = userId;
            success = users.Update(changePasswordModel.Id, changePasswordModel.Password.Trim());
            if (success == true)
            {
                return RedirectToAction("Index", "Staff");
            }
            else
            {
                TempData["ErrorMsg"] = "Failed Update Password";
                return View();

            }
        }

        //[ChildActionOnly]
        //public ActionResult RenderOrderMaster(SalesOrderModel salesOrderModel)
        //{
        //    SalesOrderModel model = new SalesOrderModel(SalesOrder.RetrieveById(salesOrderModel.Id));
        //    return PartialView("_OrderMaster", model);
        //}

        //public ActionResult Excel(int id)
        //{
        //    List<Item> ItemList = Item.RetrieveAll();

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("ItemList");
        //        var currentrow = 1;
        //        worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 4).Style.Font.Bold = true;

        //        worksheet.Cell(currentrow, 1).Value = "Order No ";
        //        worksheet.Cell(currentrow, 2).Value = "Order Date ";
        //        worksheet.Cell(currentrow, 3).Value = "Party name ";
        //        worksheet.Cell(currentrow, 4).Value = "Employee Name ";

        //        SalesOrderModel model = new SalesOrderModel(SalesOrder.RetrieveById(id));
        //        currentrow++;
        //        worksheet.Cell(currentrow, 1).Value = model.Id;
        //        worksheet.Cell(currentrow, 2).Value = model.Timestamp;
        //        worksheet.Cell(currentrow, 3).Value = model.CustomerName;
        //        worksheet.Cell(currentrow, 4).Value = model.StaffName;
        //        currentrow += 3;

        //        worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentrow, 5).Style.Font.Bold = true;

        //        worksheet.Cell(currentrow, 1).Value = "Item Name";
        //        worksheet.Cell(currentrow, 2).Value = "Category";
        //        worksheet.Cell(currentrow, 3).Value = "Sub Category";
        //        worksheet.Cell(currentrow, 4).Value = "Size";
        //        worksheet.Cell(currentrow, 5).Value = "Qty";
        //        foreach (SalesOrder a in SalesOrder.RetrieveAllDetails(id))
        //        {
        //            currentrow++;
        //            worksheet.Cell(currentrow, 1).Value = a.ItemName;
        //            worksheet.Cell(currentrow, 2).Value = a.CategoryName;
        //            worksheet.Cell(currentrow, 3).Value = a.SubCategoryName;
        //            worksheet.Cell(currentrow, 4).Value = a.Size;
        //            worksheet.Cell(currentrow, 5).Value = a.Quantity;
        //        }


        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sales Order Details.xlsx");
        //        }
        //    }

        //}



        //public ActionResult ExcelSalesOrder(string search = "", string startdate = "", string enddate = "")
        //{

        //    List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
        //    List<SalesOrder> salesorderList = SalesOrder.RetrieveAll();
        //    salesorderList = salesorderList.OrderByDescending(a => a.Timestamp).ToList();

        //    if (search.Length > 0)
        //    {
        //        GenericList<SalesOrder> g = new GenericList<SalesOrder>();
        //        salesorderList = g.SerachFun(salesorderList, search);
        //        salesorderList = salesorderList.Distinct().ToList();
        //    }
        //    if (startdate.Length > 0)
        //    {
        //        DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

        //        salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp >= startDate).ToList();
        //    }
        //    if (enddate.Length > 0)
        //    {

        //        DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
        //        salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp <= endDate).ToList();
        //    }
        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("SalesOrder");
        //        var currentrow = 1;
        //        worksheet.Cell(currentrow, 1).Value = "Order No";
        //        worksheet.Cell(currentrow, 2).Value = "Order Date";
        //        worksheet.Cell(currentrow, 3).Value = "Party Name";
        //        worksheet.Cell(currentrow, 4).Value = "Marketing Executive";
        //        worksheet.Cell(currentrow, 5).Value = "Total Order Value";

        //        foreach (SalesOrder a in salesorderList)
        //        {
        //            currentrow++;
        //            worksheet.Cell(currentrow, 1).Value = a.Id;
        //            worksheet.Cell(currentrow, 2).Value = a.Timestamp;
        //            worksheet.Cell(currentrow, 3).Value = a.CustomerName;
        //            worksheet.Cell(currentrow, 4).Value = a.StaffName;
        //            worksheet.Cell(currentrow, 5).Value = a.TotalOrderValue;


        //        }
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOrder.xlsx");
        //        }
        //    }

        //}
    }

}
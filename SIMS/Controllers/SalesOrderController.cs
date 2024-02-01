using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SIMS.ViewModels.SalesOrderModel;
using SIMS.BL;
using System.Globalization;
using PagedList;
using System.IO;
using ClosedXML.Excel;

namespace SIMS.Controllers
{
    [Authorize]
    public class SalesOrderController : Controller
    {
        //
        // GET: /SalesOrder/

        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SalesOrder");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetSalesOrderModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
                       
                    }
                    else
                    {
                        return View(GetSalesOrderModelIndividualList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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




        private IEnumerable<SalesOrderModel> GetSalesOrderModelList(string search = "", string startdate = "", string enddate = "")
        {
            List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
            List<SalesOrder> salesorderList = SalesOrder.RetrieveAll();
            salesorderList = salesorderList.OrderByDescending(a => a.Timestamp).ToList();

            if (search.Length > 0)
            {
                GenericList<SalesOrder> g = new GenericList<SalesOrder>();
                salesorderList = g.SerachFun(salesorderList, search);
                salesorderList = salesorderList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

                salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp >= startDate).ToList();
            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp <= endDate).ToList();
            }
            salesorderList = salesorderList.OrderByDescending(s => s.Timestamp).ToList();
            foreach (SalesOrder a in salesorderList)
            {
                salesOrderModelList.Add(new SalesOrderModel(a));
            }

            return salesOrderModelList;
        }

        private IEnumerable<SalesOrderModel> GetSalesOrderModelIndividualList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
            List<SalesOrder> salesorderList = SalesOrder.RetrieveOnlyStaff(userId);

            if (search.Length > 0)
            {
                GenericList<SalesOrder> g = new GenericList<SalesOrder>();
                salesorderList = g.SerachFun(salesorderList, search);
                salesorderList = salesorderList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

                salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp >= startDate).ToList();
            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp <= endDate).ToList();
            }
            foreach (SalesOrder a in salesorderList)
            {
                salesOrderModelList.Add(new SalesOrderModel(a));
            }

            return salesOrderModelList;
        }


        public ActionResult OrderDetails(int? pageIndex, int id = 0)
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SalesOrder");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsDetails == true)
                    {
                        return View(GetSalesOrderDetailsList(id).ToPagedList(pageIndex ?? 1, 10));
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
        private IEnumerable<SalesOrderModel> GetSalesOrderDetailsList(int id)
        {
            List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
            foreach (SalesOrder a in SalesOrder.RetrieveAllDetails(id))
            {
                salesOrderModelList.Add(new SalesOrderModel(a));
            }

            return salesOrderModelList;
        }

        public JsonResult getCustomerList()
        {
            List<Customer> customer = Customer.RetrieveAll().ToList();

            return new JsonResult { Data = customer, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getCategoryList()
        {
            List<Category> category = Category.RetrieveAll().ToList();

            var jasonResult = Json(category, JsonRequestBehavior.AllowGet);
            jasonResult.MaxJsonLength = int.MaxValue;
            return jasonResult;
        }

        public JsonResult getUnitOfMeasurementList()
        {
            List<UnitOfMeasurement> unitOfMeasurement = UnitOfMeasurement.RetrieveAll().ToList();


            return new JsonResult { Data = unitOfMeasurement, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getSubCategoryList(int CategoryID)
        {
            List<SubCategory> subCategory = SubCategory.RetrieveByCategoryId(CategoryID).ToList();

            var jasonResult = Json(subCategory, JsonRequestBehavior.AllowGet);
            jasonResult.MaxJsonLength = int.MaxValue;
            return jasonResult;
        }

        public JsonResult getItemList(int SubCategoryID)
        {
            List<Item> itemlist = Item.RetrieveBySubCategoryId(SubCategoryID).ToList();

            return new JsonResult { Data = itemlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getSizeList(int ItemId)
        {
            List<ItemSize> itemSize = new List<ItemSize>();
            itemSize = ItemSize.RetrieveByItemId(ItemId);

            return new JsonResult { Data = itemSize, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult Save(SalesOrderModel salesOrder, List<SalesOrderModel> OrderDetails)
        {
            int Count = 0;
            int CustomerId = salesOrder.CustomerId;
            DateTime OrderDate = salesOrder.Timestamp;
            string PaymentMode = salesOrder.Paymentmode.Trim();
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            salesOrder.StaffId = userId;
            int SalesOrderId = SalesOrder.Create(userId, OrderDate, CustomerId, PaymentMode, salesOrder.TotalOrderValue);

            foreach (SalesOrderModel a in OrderDetails)
            {
                Count = SalesOrderDetails.Create(SalesOrderId, a.CategoryId, a.SubCategoryId, a.ItemId, a.SizeId, a.Quantity, a.OrderValue);
            }

            return new JsonResult { Data = SalesOrderId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SalesOrder");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        SalesOrderModel salesOrderModel = new SalesOrderModel();
                        salesOrderModel.Timestamp = DateTime.Now;
                        return View(salesOrderModel);
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
        public ActionResult UpdateIsAddedToTally(int id, int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SalesOrder");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsTally == true)
                    {
                        bool Isaddedtotally = true;
                        SalesOrder.UpdateIsAddedToTally(id, Isaddedtotally);
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Access permission not defined";
                    }

                    return RedirectToAction("Index", "SalesOrder", new { pageIndex, search, startdate, enddate });

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
        public JsonResult getTotalOrderValue(int id)
        {
            List<SalesOrder> salesOrderlist = SalesOrder.RetrieveAllDetails(id).ToList();

            return new JsonResult { Data = salesOrderlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Edit(int subCategoryId = 0, int id = 0, int itemid = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Group Head" || userType == "Admin" || userType == "Executive Marketing" || userType == "Associate Marketing")
            {

                List<Item> lstItem = Item.RetrieveBySubCategoryId(subCategoryId);
                Item item = new Item();
                item.Name = "Select";
                lstItem.Insert(0, item);


                List<ProductSize> lstItemSizeList = ProductSize.RetrieveById(itemid);
                ProductSize itemSize = new ProductSize();
                itemSize.Size = "Select";
                lstItemSizeList.Insert(0, itemSize);


                List<Category> lstCategoryList = Category.RetrieveAll();
                Category category = new Category();
                category.Name = "Select";
                lstCategoryList.Insert(0, category);



                List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
                SubCategory subCategory = new SubCategory();
                subCategory.Name = "Select";
                lstSubCategoryList.Insert(0, subCategory);


                List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
                uniOfMeasurement.UnitOfMeasurementName = "Select";
                lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

                return View(new SalesOrderModel(lstItem, lstItemSizeList, lstCategoryList, lstSubCategoryList, lstUnitOfMeasurementList, SalesOrder.RetrieveById(id)));

            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost]
        public ActionResult Edit(int? pageIndex,SalesOrderModel salesOrderModel)
        {
            bool inValidState = false;

            if (salesOrderModel.CategoryId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Category";
            }
            else if (salesOrderModel.SubCategoryId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Sub Category";
            }
            else if (salesOrderModel.ItemId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Item";
            }

            else if (salesOrderModel.Quantity == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Quantity";
            }
            else if (salesOrderModel.SizeId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Size";
            }


            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int Id = salesOrderModel.Id;
                    salesOrderModel.SalesOrder.Update(Id);
                    return View("OrderDetails", GetSalesOrderDetailsList(salesOrderModel.Id).ToPagedList(pageIndex ?? 1, 10)); 
                }


            }
            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);


            List<ProductSize> lstItemSizeList = ProductSize.RetrieveById(salesOrderModel.ItemId);
            ProductSize itemSize = new ProductSize();
            itemSize.Size = "Select";
            lstItemSizeList.Insert(0, itemSize);


            List<Category> lstCategoryList = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            lstCategoryList.Insert(0, category);



            List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
            SubCategory subCategory = new SubCategory();
            subCategory.Name = "Select";
            lstSubCategoryList.Insert(0, subCategory);


            List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
            uniOfMeasurement.UnitOfMeasurementName = "Select";
            lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

            return View(new SalesOrderModel(lstItem, lstItemSizeList, lstCategoryList, lstSubCategoryList, lstUnitOfMeasurementList, SalesOrder.RetrieveById(salesOrderModel.Id)));
        }

        public ActionResult Delete(int? pageIndex,int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Group Head" || userType == "Admin" || userType == "Executive Accounts")
            {
                return View(GetSalesOrderDetailsList(id).ToPagedList(pageIndex ?? 1, 10));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }

        }

        [HttpPost]
        public ActionResult Delete(SalesOrderModel salesOrderModel)
        {
            int id = salesOrderModel.Id;
            SalesOrder salesOrder = SalesOrder.RetrieveOrderDetailsById(id);
            bool status = salesOrder.Delete();
            return RedirectToAction("Index", "SalesOrder");


        }

        [HttpPost]
        public JsonResult DeleteItem(int id, int salesorderdetailsid)
        {

            SalesOrder salesOrder = new SalesOrder();
            int count = salesOrder.DeleteOrderDetails(id, salesorderdetailsid);

            return new JsonResult { Data = count, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //public ActionResult Details(int id = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SalesOrder");
        //    if (accessMatrix != null)
        //    {
        //        AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
        //        if (accessMatrixDetails != null)
        //        {
        //            if (accessMatrixDetails.IsDetails == true)
        //            {
        //                return View(new SalesOrderModel(SalesOrder.RetrieveById(id)));
        //            }
        //            else
        //            {
        //                TempData["ErrorMsg"] = "Your not authorised to access this page";
        //                return RedirectToAction("Index", "Staff");
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



        [ChildActionOnly]
        public ActionResult RenderOrderMaster(SalesOrderModel salesOrderModel)
        {
            SalesOrderModel model = new SalesOrderModel(SalesOrder.RetrieveById(salesOrderModel.Id));
            return PartialView("_OrderMaster", model);
        }

        public ActionResult Excel(int id)
        {
            List<Item> ItemList = Item.RetrieveAll();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ItemList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 4).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Order No ";
                worksheet.Cell(currentrow, 2).Value = "Order Date ";
                worksheet.Cell(currentrow, 3).Value = "Party name ";
                worksheet.Cell(currentrow, 4).Value = "Employee Name ";

                SalesOrderModel model = new SalesOrderModel(SalesOrder.RetrieveById(id));
                currentrow++;
                worksheet.Cell(currentrow, 1).Value = model.Id;
                worksheet.Cell(currentrow, 2).Value = model.Timestamp;
                worksheet.Cell(currentrow, 3).Value = model.CustomerName;
                worksheet.Cell(currentrow, 4).Value = model.StaffName;
                currentrow += 3;

                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 5).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Item Name";
                worksheet.Cell(currentrow, 2).Value = "Category";
                worksheet.Cell(currentrow, 3).Value = "Sub Category";
                worksheet.Cell(currentrow, 4).Value = "Size";
                worksheet.Cell(currentrow, 5).Value = "Qty";
                foreach (SalesOrder a in SalesOrder.RetrieveAllDetails(id))
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.ItemName;
                    worksheet.Cell(currentrow, 2).Value = a.CategoryName;
                    worksheet.Cell(currentrow, 3).Value = a.SubCategoryName;
                    worksheet.Cell(currentrow, 4).Value = a.Size;
                    worksheet.Cell(currentrow, 5).Value = a.Quantity;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sales Order Details.xlsx");
                }
            }

        }



        public ActionResult ExcelSalesOrder(string search = "", string startdate = "", string enddate = "")
        {

            List<SalesOrderModel> salesOrderModelList = new List<SalesOrderModel>();
            List<SalesOrder> salesorderList = SalesOrder.RetrieveAll();
            salesorderList = salesorderList.OrderByDescending(a => a.Timestamp).ToList();

            if (search.Length > 0)
            {
                GenericList<SalesOrder> g = new GenericList<SalesOrder>();
                salesorderList = g.SerachFun(salesorderList, search);
                salesorderList = salesorderList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

                salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp >= startDate).ToList();
            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                salesorderList = salesorderList.OfType<SalesOrder>().Where(s => s.Timestamp <= endDate).ToList();
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SalesOrder");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Order No";
                worksheet.Cell(currentrow, 2).Value = "Order Date";
                worksheet.Cell(currentrow, 3).Value = "Party Name";
                worksheet.Cell(currentrow, 4).Value = "Marketing Executive";
                worksheet.Cell(currentrow, 5).Value = "Total Order Value";

                foreach (SalesOrder a in salesorderList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Id;
                    worksheet.Cell(currentrow, 2).Value = a.Timestamp;
                    worksheet.Cell(currentrow, 3).Value = a.CustomerName;
                    worksheet.Cell(currentrow, 4).Value = a.StaffName;
                    worksheet.Cell(currentrow, 5).Value = a.TotalOrderValue;


                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOrder.xlsx");
                }
            }

        }
    }
}


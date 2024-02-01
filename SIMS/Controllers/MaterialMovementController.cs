using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.ViewModels.MaterialMovementModel;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using System.Globalization;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class MaterialMovementController : Controller
    {
        //
        // GET: /MaterialMovement/

        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("MaterialMovement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetMaterialMovementModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

                    }
                    else
                    {
                        //return View(GetMaterialMovementRetreiveByUserIdModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
                        return View(GetMaterialMovementRetreiveByUserIdModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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


        public JsonResult GetItemSize(int Itemid)
        {

            List<ProductSize> ItemSizes = ProductSize.RetrieveById(Itemid).ToList();
            return new JsonResult { Data = ItemSizes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getProductSizeList(int Id)
        {

            List<ProductSize> productSizes = ProductSize.RetrievebymaterialMovementId(Id).ToList();

            return new JsonResult { Data = productSizes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private IEnumerable<MaterialMovementModel> GetMaterialMovementModelList(string search = "", string startdate = "", string enddate = "")
        {
            List<MaterialMovementModel> materialMovementModelList = new List<MaterialMovementModel>();
            List<MaterialMovement> materialMovementList = MaterialMovement.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<MaterialMovement> g = new GenericList<MaterialMovement>();
                materialMovementList = g.SerachFun(materialMovementList, search);
                materialMovementList = materialMovementList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                materialMovementList = materialMovementList.OfType<MaterialMovement>().Where(s => s.Timestamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                materialMovementList = materialMovementList.OfType<MaterialMovement>().Where(s => s.Timestamp <= endDate).ToList();
            }

            foreach (MaterialMovement a in materialMovementList)
            {
                materialMovementModelList.Add(new MaterialMovementModel(a));
            }

            return materialMovementModelList;
        }

        private IEnumerable<MaterialMovementModel> GetMaterialMovementRetreiveByUserIdModelList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            List<MaterialMovementModel> materialMovementModelList = new List<MaterialMovementModel>();
            List<MaterialMovement> materialMovementList = MaterialMovement.RetrieveAllByUserId(userId);

            if (search.Length > 0)
            {
                GenericList<MaterialMovement> g = new GenericList<MaterialMovement>();
                materialMovementList = g.SerachFun(materialMovementList, search);
                materialMovementList = materialMovementList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                materialMovementList = materialMovementList.OfType<MaterialMovement>().Where(s => s.Timestamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                materialMovementList = materialMovementList.OfType<MaterialMovement>().Where(s => s.Timestamp <= endDate).ToList();
            }

            foreach (MaterialMovement a in materialMovementList)
            {
                materialMovementModelList.Add(new MaterialMovementModel(a));
            }

            return materialMovementModelList;
        }




        public ActionResult Edit(int id = 0)
        {
            List<Customer> lstCustomer = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomer.Insert(0, customer);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);

            List<UnitOfMeasurement> lstunitOfMeasurements = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
            unitOfMeasurement.UnitOfMeasurementName = "Select";
            lstunitOfMeasurements.Insert(0, unitOfMeasurement);

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("MaterialMovement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new MaterialMovementModel(MaterialMovement.RetrieveById(id), lstCustomer, lstItem, lstunitOfMeasurements));
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
        public ActionResult Edit(MaterialMovementModel materialMovementModel, List<string> Size)
        {
            bool inValidState = false;

            if (materialMovementModel.Customerid == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Customer Name";
            }
            else if (materialMovementModel.Itemid == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Item Name";
            }
            else if (materialMovementModel.Staffid == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select staff";
            }
            else if (Size == null)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Size";
            }
            else if (materialMovementModel.Movementtype == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Movementtype";
            }
            else if (materialMovementModel.Invoiceno == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Invoiceno";
            }
            else if (materialMovementModel.UnitId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Unit Of Measurement";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {

                    bool status = materialMovementModel.MaterialMovement.Update();
                    if (status)
                    {
                        ProductSize.DeleteMaterialMovementItemSize(materialMovementModel.Id);
                        foreach (string a in Size)
                        {
                            MaterialMovement.AddItemSize(materialMovementModel.Id, materialMovementModel.Itemid, a);
                        }
                    }

                    return RedirectToAction("Index", "MaterialMovement");
                }
            }
            List<Customer> lstCustomer = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomer.Insert(0, customer);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);

            List<UnitOfMeasurement> lstunitOfMeasurements = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
            unitOfMeasurement.UnitOfMeasurementName = "Select";
            lstunitOfMeasurements.Insert(0, unitOfMeasurement);
            return View(new MaterialMovementModel(lstCustomer, lstItem, lstunitOfMeasurements));
        }

        public ActionResult Create()
        {

            List<Customer> lstCustomer = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomer.Insert(0, customer);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);

            List<UnitOfMeasurement> lstunitOfMeasurements = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
            unitOfMeasurement.UnitOfMeasurementName = "Select";
            lstunitOfMeasurements.Insert(0, unitOfMeasurement);

            MaterialMovementModel materialMovementModel = new MaterialMovementModel(lstCustomer, lstItem, lstunitOfMeasurements);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("MaterialMovement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(materialMovementModel);
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
        public JsonResult Save(List<MaterialMovementModel> materialMovementModel)
        {
            foreach (var item in materialMovementModel)
            {
                int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                item.Staffid = userId;
                int Id = MaterialMovement.Create(item.Timestamp, item.Staffid, item.Customerid, item.Itemid, item.Invoiceno.Trim(), item.Invoicedate, item.UnitId, item.Quantity.Trim(), item.Invoiceamount.Trim(), item.Remarks.Trim(), item.Movementtype.Trim(), item.Isdeleted);

                MaterialMovement.AddItemSize(Id, item.Itemid, item.ItemSize);
            }

            return new JsonResult { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //[HttpPost]
        //public ActionResult Create(MaterialMovementModel materialMovementModel, List<string> Size)
        //{
        //    bool inValidState = false;
        //    // materialMovementModel.

        //    if (materialMovementModel.Customerid == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Customer Name";
        //    }
        //    else if (materialMovementModel.Itemid == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Item Name";
        //    }
        //    else if (Size == null)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Size";
        //    }
        //    else if (materialMovementModel.Movementtype == "Select")
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Movementtype";
        //    }
        //    else if (materialMovementModel.Invoiceno == "")
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Enter Invoiceno";
        //    }
        //    else if (materialMovementModel.UnitId == 0)
        //    {
        //        inValidState = true;
        //        TempData["ErrorMsg"] = "Please Select Unit Of Measurement";
        //    }
        //    if (inValidState == false)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
        //            materialMovementModel.Staffid = userId;
        //            int Id = MaterialMovement.Create(materialMovementModel.Timestamp, materialMovementModel.Staffid, materialMovementModel.Customerid, materialMovementModel.Itemid, materialMovementModel.Invoiceno.Trim(), materialMovementModel.Invoicedate, materialMovementModel.UnitId, materialMovementModel.Quantity.Trim(), materialMovementModel.Invoiceamount.Trim(), materialMovementModel.Remarks.Trim(), materialMovementModel.Movementtype.Trim(), materialMovementModel.Isdeleted);
        //            foreach (string a in Size)
        //            {
        //                MaterialMovement.AddItemSize(Id, materialMovementModel.Itemid, a);
        //            }
        //            return RedirectToAction("Index", "MaterialMovement");
        //        }
        //    }
        //    List<Customer> lstCustomer = Customer.RetrieveAll();
        //    Customer customer = new Customer();
        //    customer.Name = "Select";
        //    lstCustomer.Insert(0, customer);

        //    List<Item> lstItem = Item.RetrieveAll();
        //    Item item = new Item();
        //    item.Name = "Select";
        //    lstItem.Insert(0, item);

        //    List<UnitOfMeasurement> lstunitOfMeasurements = UnitOfMeasurement.RetrieveAll();
        //    UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
        //    unitOfMeasurement.UnitOfMeasurementName = "Select";
        //    lstunitOfMeasurements.Insert(0, unitOfMeasurement);
        //    return View(new MaterialMovementModel(lstCustomer, lstItem, lstunitOfMeasurements));
        //}

        public ActionResult Delete(int id = 0)
        {
            MaterialMovement materialMovement = MaterialMovement.RetrieveById(id);
            if (materialMovement == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("MaterialMovement");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new MaterialMovementModel(materialMovement));
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
            MaterialMovement materialMovement = MaterialMovement.RetrieveById(id);
            materialMovement.Delete();
            ProductSize.DeleteMaterialMovementItemSize(id);
            return RedirectToAction("Index", "MaterialMovement");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new MaterialMovementModel(MaterialMovement.RetrieveById(id)));
        }


        [ChildActionOnly]
        public ActionResult RenderItemISize(MaterialMovementModel materialMovementModel)
        {
            List<ProductSizeModel> ProductSizeModelList = new List<ProductSizeModel>();
            foreach (ProductSize a in ProductSize.RetrievebymaterialMovementId(materialMovementModel.Id))
            {
                ProductSizeModelList.Add(new ProductSizeModel(a));
            }
            return PartialView("_ItemSizes", ProductSizeModelList);
        }
        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<MaterialMovementModel> materialMovementModelList = new List<MaterialMovementModel>();
            List<MaterialMovement> materialMovementList = MaterialMovement.RetrieveAll();

            if (search.Length > 0)
            {

                GenericList<MaterialMovement> g = new GenericList<MaterialMovement>();
                materialMovementList = g.SerachFun(materialMovementList, search);
                materialMovementList = materialMovementList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                materialMovementList = materialMovementList.OfType<MaterialMovement>().Where(s => s.Timestamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                materialMovementList = materialMovementList.OfType<MaterialMovement>().Where(s => s.Timestamp <= endDate).ToList();
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MaterialMovementList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 9).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 10).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 11).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Employee Name ";
                worksheet.Cell(currentrow, 2).Value = "Date";
                worksheet.Cell(currentrow, 3).Value = "Party Name";
                worksheet.Cell(currentrow, 4).Value = "Item Name";
                worksheet.Cell(currentrow, 5).Value = "Invoice No";
                worksheet.Cell(currentrow, 6).Value = "Invoice Date";
                worksheet.Cell(currentrow, 7).Value = "Unit Name";
                worksheet.Cell(currentrow, 8).Value = "Quantity";
                worksheet.Cell(currentrow, 9).Value = "Invoice Amount";
                worksheet.Cell(currentrow, 10).Value = "Movementtype";
                worksheet.Cell(currentrow, 11).Value = "Remarks";



                foreach (MaterialMovement a in materialMovementList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.UserName;
                    worksheet.Cell(currentrow, 2).Value = a.Timestamp;
                    worksheet.Cell(currentrow, 3).Value = a.CustomerName;
                    worksheet.Cell(currentrow, 4).Value = a.ItemName;
                    worksheet.Cell(currentrow, 5).Value = a.Invoiceno;
                    worksheet.Cell(currentrow, 6).Value = a.Invoicedate;
                    worksheet.Cell(currentrow, 7).Value = a.UnitName;
                    worksheet.Cell(currentrow, 8).Value = a.Quantity;
                    worksheet.Cell(currentrow, 9).Value = a.Invoiceamount;
                    worksheet.Cell(currentrow, 10).Value = a.Movementtype;
                    worksheet.Cell(currentrow, 11).Value = a.Remarks;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MaterialMovementList.xlsx");
                }
            }

        }
    }
}


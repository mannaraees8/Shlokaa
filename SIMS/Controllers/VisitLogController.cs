using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.VisitLogModel;
using SIMS.BL;
using System.Web.UI;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using PagedList.Mvc;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class VisitLogController : Controller
    {
        //
        // GET: /VisitLog/
        [HttpPost]
        public JsonResult CreateForReceived(VisitLogModel visitLogModel)
        {
            int Count = 0;
            bool status = false;
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            Count = VisitLog.Create(visitLogModel.Datetime, userId, visitLogModel.Customerid, visitLogModel.Orderstatus.Trim(), visitLogModel.Ordervalue.Trim(), visitLogModel.Paymentmode.Trim(), visitLogModel.Amount.Trim(), visitLogModel.Reasonfornopayment.Trim(), visitLogModel.Remarks.Trim(), visitLogModel.Reasonfornoorder.Trim(), visitLogModel.Isdeleted);

            if (Count != 0)
            {
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VisitLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetVisitLogModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

                    }
                    else
                    {
                        return View(GetVisitLogRetreiveByIdModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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


        private IEnumerable<VisitLogModel> GetVisitLogRetreiveByIdModelList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            List<VisitLogModel> visitLogModelList = new List<VisitLogModel>();
            List<VisitLog> visitLogList = VisitLog.RetrieveAllByUserId(userId);
            int year = DateTime.Now.Year;
            //visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime.Year == year).ToList();

            if (search.Length > 0)
            {
                GenericList<VisitLog> g = new GenericList<VisitLog>();
                visitLogList = g.SerachFun(visitLogList, search);
                visitLogList = visitLogList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime <= endDate).ToList();
            }


            foreach (VisitLog a in visitLogList)
            {
                visitLogModelList.Add(new VisitLogModel(a));
            }

            return visitLogModelList;
            //ViewBag.PageIndex = pageIndex;
            //ViewBag.PageCount = (visitLogModelList.Count + pageSize - 1) / pageSize;

            //return visitLogModelList.Skip(pageIndex * pageSize).Take(pageSize);
        }

        private IEnumerable<VisitLogModel> GetVisitLogModelList(string search = "", string startdate = "", string enddate = "")
        {
            List<VisitLogModel> visitLogModelList = new List<VisitLogModel>();
            List<VisitLog> visitLogList = VisitLog.RetrieveAll();
            int year = DateTime.Now.Year;
            //visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime.Year == year).ToList();

            if (search.Length > 0)
            {
                GenericList<VisitLog> g = new GenericList<VisitLog>();
                visitLogList = g.SerachFun(visitLogList, search);
                visitLogList = visitLogList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime <= endDate).ToList();
            }


            foreach (VisitLog a in visitLogList)
            {
                visitLogModelList.Add(new VisitLogModel(a));
            }

            return visitLogModelList;
            //ViewBag.PageIndex = pageIndex;
            //ViewBag.PageCount = (visitLogModelList.Count + pageSize - 1) / pageSize;

            //return visitLogModelList.Skip(pageIndex * pageSize).Take(pageSize);
        }



        public ActionResult Edit(int id = 0)
        {
            List<Customer> lstCustomers = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomers.Insert(0, customer);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VisitLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new VisitLogModel(lstCustomers, VisitLog.RetrieveById(id)));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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
        public ActionResult Edit(VisitLogModel visitLogModel)
        {

            bool inValidState = false;

            VisitLog visitLog = new VisitLog();

            if (visitLogModel.Customerid == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Customer Name";

            }
            if (visitLogModel.Orderstatus == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Orderstatus";
            }
            if (visitLogModel.Orderstatus == "Not Received")
            {
                if (visitLogModel.Reasonfornoorder == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Reason Fornoorder";
                }
            }
            if (visitLogModel.Paymentmode == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Payment Mode";
            }
            if (visitLogModel.Paymentmode == "Cash" || visitLogModel.Paymentmode == "NEFT" || visitLogModel.Paymentmode == "Cheque")
            {
                if (visitLogModel.Amount.Trim() == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Amount";
                }
            }
            if (visitLogModel.Paymentmode.Trim() == "No Payment")
            {
                if (visitLogModel.Reasonfornopayment == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Reason For No Payment";
                }
            }
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            visitLogModel.Staffid = userId;

            if (inValidState == false)
            {

                visitLogModel.VisitLog.Update();
                return RedirectToAction("Index", "VisitLog");
            }


            List<Customer> lstCustomers = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomers.Insert(0, customer);
            return View(new VisitLogModel(lstCustomers));
        }


        public ActionResult Create()
        {
            List<Customer> lstCustomers = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomers.Insert(0, customer);
            VisitLogModel visitLogModel = new VisitLogModel(lstCustomers);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VisitLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View("Create", visitLogModel);
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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
        public ActionResult Create(VisitLogModel visitLogModel)
        {
            bool inValidState = false;
            if (visitLogModel.Customerid == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Customer Name";

            }
            if (visitLogModel.Orderstatus == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Orderstatus";
            }
            if (visitLogModel.Orderstatus == "Not Received")
            {
                if (visitLogModel.Reasonfornoorder == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Reason Fornoorder";
                }
            }
            if (visitLogModel.Paymentmode == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Payment Mode";
            }
            if (visitLogModel.Paymentmode == "Cash" || visitLogModel.Paymentmode == "NEFT" || visitLogModel.Paymentmode == "Cheque")
            {
                if (visitLogModel.Amount.Trim() == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Amount";
                }
            }
            if (visitLogModel.Paymentmode.Trim() == "No Payment")
            {
                if (visitLogModel.Reasonfornopayment == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Reason For No Payment";
                }
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    if (visitLogModel.Orderstatus == "Received")
                    {

                        int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                        visitLogModel.Staffid = userId;
                        VisitLog.Create(visitLogModel.Datetime, visitLogModel.Staffid, visitLogModel.Customerid, visitLogModel.Orderstatus.Trim(), visitLogModel.Ordervalue.Trim(), visitLogModel.Paymentmode.Trim(), visitLogModel.Amount.Trim(), visitLogModel.Reasonfornopayment.Trim(), visitLogModel.Remarks.Trim(), visitLogModel.Reasonfornoorder.Trim(), visitLogModel.Isdeleted);
                        TempData["SuccessMsg"] = "Successfully Added";
                        return RedirectToAction("Index", "VisitLog");

                    }
                    else
                    {
                        int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                        visitLogModel.Staffid = userId;
                        VisitLog.Create(visitLogModel.Datetime, visitLogModel.Staffid, visitLogModel.Customerid, visitLogModel.Orderstatus.Trim(), visitLogModel.Ordervalue.Trim(), visitLogModel.Paymentmode.Trim(), visitLogModel.Amount.Trim(), visitLogModel.Reasonfornopayment.Trim(), visitLogModel.Remarks.Trim(), visitLogModel.Reasonfornoorder.Trim(), visitLogModel.Isdeleted);
                        TempData["SuccessMsg"] = "Successfully Added";
                        return RedirectToAction("Index", "VisitLog");
                    }
                }
            }
            List<Customer> lstCustomers = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            lstCustomers.Insert(0, customer);
            return View(new VisitLogModel(lstCustomers));
        }

        public ActionResult Delete(int id = 0)
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VisitLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        VisitLog visitLog = VisitLog.RetrieveById(id);
                        if (visitLog == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new VisitLogModel(visitLog));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Home");
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
            VisitLog visitLog = VisitLog.RetrieveById(id);
            visitLog.Delete();
            return RedirectToAction("Index", "VisitLog");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new VisitLogModel(VisitLog.RetrieveById(id)));
        }
        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<VisitLogModel> visitLogModelList = new List<VisitLogModel>();
            List<VisitLog> visitLogList = VisitLog.RetrieveAll();
            int year = DateTime.Now.Year;
            //visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime.Year == year).ToList();

            if (search.Length > 0)
            {
                GenericList<VisitLog> g = new GenericList<VisitLog>();
                visitLogList = g.SerachFun(visitLogList, search.Trim());
                visitLogList = visitLogList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                visitLogList = visitLogList.OfType<VisitLog>().Where(s => s.Datetime <= endDate).ToList();
            }


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("VisitLogList");
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

                worksheet.Cell(currentrow, 1).Value = "Employee Name ";
                worksheet.Cell(currentrow, 2).Value = "Date";
                worksheet.Cell(currentrow, 3).Value = "Party Name";
                worksheet.Cell(currentrow, 4).Value = "Orderstatus";
                worksheet.Cell(currentrow, 5).Value = "Ordervalue";
                worksheet.Cell(currentrow, 6).Value = "Reasonfornoorder";
                worksheet.Cell(currentrow, 7).Value = "Paymentmode";
                worksheet.Cell(currentrow, 8).Value = "Amount";
                worksheet.Cell(currentrow, 9).Value = "Reasonfornopayment";
                worksheet.Cell(currentrow, 10).Value = "Remarks";



                foreach (VisitLog a in visitLogList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.UserName;
                    worksheet.Cell(currentrow, 2).Value = a.Datetime;
                    worksheet.Cell(currentrow, 3).Value = a.CustomerName;
                    worksheet.Cell(currentrow, 4).Value = a.Orderstatus;
                    worksheet.Cell(currentrow, 5).Value = a.Ordervalue;
                    worksheet.Cell(currentrow, 6).Value = a.Reasonfornoorder;
                    worksheet.Cell(currentrow, 7).Value = a.Paymentmode;
                    worksheet.Cell(currentrow, 8).Value = a.Amount;
                    worksheet.Cell(currentrow, 9).Value = a.Reasonfornopayment;
                    worksheet.Cell(currentrow, 10).Value = a.Remarks;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisitLogList.xlsx");
                }
            }

        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.ExpenseReportModel;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using PagedList;
using System.Globalization;

namespace SIMS.Controllers
{
    [Authorize]
    public class ExpenseReportController : Controller
    {
        // GET: ExpenseReport
        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ExpenseReport");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {

                        return View(GetExpenseReportModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

                    }
                    else
                    {
                        return View(GetExpenseReportRetreiveByUserIdModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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

        private IEnumerable<ExpenseReportModel> GetExpenseReportModelList(string search = "", string startdate = "", string enddate = "")
        {

            List<ExpenseReportModel> expenseReportModelList = new List<ExpenseReportModel>();
            List<ExpenseReport> expenseReportList = ExpenseReport.RetrieveAll();


            if (search.Length > 0)
            {
                GenericList<ExpenseReport> g = new GenericList<ExpenseReport>();
                expenseReportList = g.SerachFun(expenseReportList, search);
                expenseReportList = expenseReportList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                expenseReportList = expenseReportList.OfType<ExpenseReport>().Where(s => s.TimeStamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                expenseReportList = expenseReportList.OfType<ExpenseReport>().Where(s => s.TimeStamp <= endDate).ToList();
            }

            foreach (ExpenseReport a in expenseReportList)
            {
                expenseReportModelList.Add(new ExpenseReportModel(a));
            }

            return expenseReportModelList;
        }
        private IEnumerable<ExpenseReportModel> GetExpenseReportRetreiveByUserIdModelList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            List<ExpenseReportModel> expenseReportModelList = new List<ExpenseReportModel>();
            List<ExpenseReport> expenseReportList = ExpenseReport.RetrieveAllByUserId(userId);


            if (search.Length > 0)
            {
                GenericList<ExpenseReport> g = new GenericList<ExpenseReport>();
                expenseReportList = g.SerachFun(expenseReportList, search);
                expenseReportList = expenseReportList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                expenseReportList = expenseReportList.OfType<ExpenseReport>().Where(s => s.TimeStamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                expenseReportList = expenseReportList.OfType<ExpenseReport>().Where(s => s.TimeStamp <= endDate).ToList();
            }

            foreach (ExpenseReport a in expenseReportList)
            {
                expenseReportModelList.Add(new ExpenseReportModel(a));
            }

            return expenseReportModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ExpenseReport");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new ExpenseReportModel(ExpenseReport.RetrieveById(id), lstCustomer));
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
        public ActionResult Edit(ExpenseReportModel expenseReportModel)
        {
            bool inValidState = false;

            if (expenseReportModel.TransportVehicle == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Transport Vehicle";
            }
            else if (expenseReportModel.Amount == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Amount";
            }
            else if (expenseReportModel.StartingPoint == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select From";
            }
            else if (expenseReportModel.Destination == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select To";
            }
            else if (expenseReportModel.TransportVehicle == "Bike")
            {
                if (expenseReportModel.Distance.Trim() == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Distance";
                }
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    expenseReportModel.Staffid = userId;
                    expenseReportModel.ExpenseReport.Update();
                    return RedirectToAction("Index", "ExpenseReport");
                }
            }
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);
            return View(new ExpenseReportModel(lstCustomer));
        }

        public ActionResult Create()
        {
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ExpenseReport");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new ExpenseReportModel(lstCustomer));
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
        public ActionResult Create(ExpenseReportModel expenseReportModel)
        {
            bool inValidState = false;

            if (expenseReportModel.TransportVehicle == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Transport Vehicle";
            }
            else if (expenseReportModel.Amount == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Amount";
            }
            else if (expenseReportModel.StartingPoint == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select From";
            }
            else if (expenseReportModel.Destination == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select To";
            }
            else if (expenseReportModel.TransportVehicle == "Bike")
            {
                if (expenseReportModel.Distance.Trim() == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Distance";
                }
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    expenseReportModel.Staffid = userId;
                    ExpenseReport.Create(expenseReportModel.Staffid, expenseReportModel.TimeStamp, expenseReportModel.TransportVehicle.Trim(), expenseReportModel.Amount.Trim(), expenseReportModel.StartingPoint.Trim(), expenseReportModel.Destination.Trim(), expenseReportModel.Distance.Trim(), expenseReportModel.FoodCharge.Trim(), expenseReportModel.ParkingCharge.Trim(), expenseReportModel.TollOrFineCharge.Trim(), expenseReportModel.TollOrFineDetails.Trim(), expenseReportModel.Remarks.Trim());
                    return RedirectToAction("Index", "ExpenseReport");
                }
            }
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);
            return View(new ExpenseReportModel(lstCustomer));
        }

        public ActionResult Delete(int id = 0)
        {
            ExpenseReport expenseReport = ExpenseReport.RetrieveById(id);
            if (expenseReport == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ExpenseReport");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new ExpenseReportModel(expenseReport));
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
            ExpenseReport expenseReport = ExpenseReport.RetrieveById(id);
            expenseReport.Delete();
            return RedirectToAction("Index", "ExpenseReport");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new ExpenseReportModel(ExpenseReport.RetrieveById(id)));
        }

        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {

            List<ExpenseReportModel> expenseReportModelList = new List<ExpenseReportModel>();
            List<ExpenseReport> expenseReportList = ExpenseReport.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<ExpenseReport> g = new GenericList<ExpenseReport>();
                expenseReportList = g.SerachFun(expenseReportList, search);
                expenseReportList = expenseReportList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                expenseReportList = expenseReportList.OfType<ExpenseReport>().Where(s => s.TimeStamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                expenseReportList = expenseReportList.OfType<ExpenseReport>().Where(s => s.TimeStamp <= endDate).ToList();
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ExpenseReportList");
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
                worksheet.Cell(currentrow, 12).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Employee Name ";
                worksheet.Cell(currentrow, 2).Value = "Date";
                worksheet.Cell(currentrow, 3).Value = "Transport Vehicle";
                worksheet.Cell(currentrow, 4).Value = "From";
                worksheet.Cell(currentrow, 5).Value = "To";
                worksheet.Cell(currentrow, 6).Value = "Distance";
                worksheet.Cell(currentrow, 7).Value = "Transport Charge";
                worksheet.Cell(currentrow, 8).Value = "Food Charge";
                worksheet.Cell(currentrow, 9).Value = "Parking Charge";
                worksheet.Cell(currentrow, 10).Value = "Toll/Fine Charge";
                worksheet.Cell(currentrow, 11).Value = "Toll/Fine Details";
                worksheet.Cell(currentrow, 12).Value = "Remarks";



                foreach (ExpenseReport a in expenseReportList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.UserName;
                    worksheet.Cell(currentrow, 2).Value = a.TimeStamp;
                    worksheet.Cell(currentrow, 3).Value = a.TransportVehicle;
                    worksheet.Cell(currentrow, 4).Value = a.StartingPoint;
                    worksheet.Cell(currentrow, 5).Value = a.Destination;
                    worksheet.Cell(currentrow, 6).Value = a.Distance;
                    worksheet.Cell(currentrow, 7).Value = a.Amount;
                    worksheet.Cell(currentrow, 8).Value = a.FoodCharge;
                    worksheet.Cell(currentrow, 9).Value = a.ParkingCharge;
                    worksheet.Cell(currentrow, 10).Value = a.TollOrFineCharge;
                    worksheet.Cell(currentrow, 11).Value = a.TollOrFineDetails;
                    worksheet.Cell(currentrow, 12).Value = a.Remarks;


                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExpenseReportList.xlsx");
                }
            }

        }
    }
}
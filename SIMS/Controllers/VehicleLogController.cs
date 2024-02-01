using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.VehicleLogModel;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using PagedList;
using System.Globalization;

namespace SIMS.Controllers
{
    [Authorize]
    public class VehicleLogController : Controller
    {
        // GET: VehicleLog
        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetVehicleLogModelModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

                    }
                    else
                    {
                        return View(GetVehicleLogRetreiveByUserIdModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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


        private IEnumerable<VehicleLogModel> GetVehicleLogModelModelList(string search = "", string startdate = "", string enddate = "")
        {
            List<VehicleLogModel> vehicleLogModelList = new List<VehicleLogModel>();
            List<VehicleLog> vehicleLogList = VehicleLog.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<VehicleLog> g = new GenericList<VehicleLog>();
                vehicleLogList = g.SerachFun(vehicleLogList, search);
                vehicleLogList = vehicleLogList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                vehicleLogList = vehicleLogList.OfType<VehicleLog>().Where(s => s.TimeStamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                vehicleLogList = vehicleLogList.OfType<VehicleLog>().Where(s => s.TimeStamp <= endDate).ToList();
            }

            foreach (VehicleLog a in vehicleLogList)
            {
                vehicleLogModelList.Add(new VehicleLogModel(a));
            }

            return vehicleLogModelList;
        }

        private IEnumerable<VehicleLogModel> GetVehicleLogRetreiveByUserIdModelList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            List<VehicleLogModel> vehicleLogModelList = new List<VehicleLogModel>();
            List<VehicleLog> vehicleLogList = VehicleLog.RetrieveAllByUserId(userId);

            if (search.Length > 0)
            {
                GenericList<VehicleLog> g = new GenericList<VehicleLog>();
                vehicleLogList = g.SerachFun(vehicleLogList, search);
                vehicleLogList = vehicleLogList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                vehicleLogList = vehicleLogList.OfType<VehicleLog>().Where(s => s.TimeStamp >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                vehicleLogList = vehicleLogList.OfType<VehicleLog>().Where(s => s.TimeStamp <= endDate).ToList();
            }

            foreach (VehicleLog a in vehicleLogList)
            {
                vehicleLogModelList.Add(new VehicleLogModel(a));
            }

            return vehicleLogModelList;
        }
        public ActionResult Edit(int id = 0)
        {
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);

            List<VehicleNo> lstVehicleNo = VehicleNo.RetrieveAll();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            VehicleNo vehicleNo = new VehicleNo();
            vehicleNo.VehicleNum = "Select";
            lstVehicleNo.Insert(0, vehicleNo);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new VehicleLogModel(lstCustomer, lstVehicleNo, VehicleLog.RetrieveById(id)));
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
        public ActionResult Edit(VehicleLogModel vehicleLogModel)
        {
            bool inValidState = false;

            if (vehicleLogModel.StartingPoint == "Select")
            {
                TempData["ErrorMsg"] = "Please Select From";
                inValidState = true;
            }
            if (vehicleLogModel.Destination == "Select")
            {
                TempData["ErrorMsg"] = "Please Select Destination";
                inValidState = true;

            }
            if (vehicleLogModel.VehicleNo.Trim() == "")
            {
                TempData["ErrorMsg"] = "Please Enter Vehicle No";
                inValidState = true;

            }
            if (vehicleLogModel.Amount.Trim() == "")
            {
                TempData["ErrorMsg"] = "Please Enter Amount";
                inValidState = true;

            }
            if (vehicleLogModel.Purpose.Trim() == "")
            {
                TempData["ErrorMsg"] = "Please Enter Purpose";
                inValidState = true;

            }
            if (vehicleLogModel.FuelFilled == "Select")
            {
                TempData["ErrorMsg"] = "Please Select FuelFilled";
                inValidState = true;

            }
            if (vehicleLogModel.FuelFilled == "Yes")
            {
                if (vehicleLogModel.FuelQuantity.Trim() == "")
                {
                    TempData["ErrorMsg"] = "Please Enter fuel quantity";
                    inValidState = true;
                }
            }
            if (inValidState == false)
            {

                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    vehicleLogModel.Staffid = userId;
                    vehicleLogModel.VehicleLog.Update();
                    return RedirectToAction("Index", "VehicleLog");
                }
            }
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);

            List<VehicleNo> lstVehicleNo = VehicleNo.RetrieveAll();

            VehicleNo vehicleNo = new VehicleNo();
            vehicleNo.VehicleNum = "Select";
            lstVehicleNo.Insert(0, vehicleNo);
            return View(new VehicleLogModel(lstCustomer, lstVehicleNo));
        }

        public ActionResult Create()
        {
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);
            List<VehicleNo> lstVehicleNo = VehicleNo.RetrieveAll();

            VehicleNo vehicleNo = new VehicleNo();
            vehicleNo.VehicleNum = "Select";
            lstVehicleNo.Insert(0, vehicleNo);

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new VehicleLogModel(lstCustomer, lstVehicleNo));
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
        public ActionResult Create(VehicleLogModel vehicleLogModel)
        {
            bool inValidState = false;

            if (vehicleLogModel.StartingPoint == "Select")
            {
                TempData["ErrorMsg"] = "Please Select From";
                inValidState = true;
            }
            if (vehicleLogModel.Destination == "Select")
            {
                TempData["ErrorMsg"] = "Please Select Destination";
                inValidState = true;

            }
            if (vehicleLogModel.VehicleNo.Trim() == "")
            {
                TempData["ErrorMsg"] = "Please Enter Vehicle No";
                inValidState = true;

            }
            if (vehicleLogModel.Amount.Trim() == "")
            {
                TempData["ErrorMsg"] = "Please Enter Amount";
                inValidState = true;

            }
            if (vehicleLogModel.Purpose.Trim() == "")
            {
                TempData["ErrorMsg"] = "Please Enter Purpose";
                inValidState = true;

            }
            if (vehicleLogModel.FuelFilled == "Select")
            {
                TempData["ErrorMsg"] = "Please Select FuelFilled";
                inValidState = true;

            }
            if (vehicleLogModel.FuelFilled == "Yes")
            {
                if (vehicleLogModel.FuelQuantity.Trim() == "")
                {
                    TempData["ErrorMsg"] = "Please Enter fuel quantity";
                    inValidState = true;
                }
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    vehicleLogModel.Staffid = userId;
                    VehicleLog.Create(vehicleLogModel.Staffid, vehicleLogModel.TimeStamp, vehicleLogModel.VehicleNo.Trim(), vehicleLogModel.StartingPoint.Trim(), vehicleLogModel.Destination.Trim(), vehicleLogModel.Purpose.Trim(), vehicleLogModel.StartReading.Trim(), vehicleLogModel.EndReading.Trim(), vehicleLogModel.FuelFilled.Trim(), vehicleLogModel.FuelQuantity.Trim(), vehicleLogModel.Amount.Trim(), vehicleLogModel.Voucher.Trim(), vehicleLogModel.Remarks.Trim());
                    return RedirectToAction("Index", "VehicleLog");
                }
            }
            List<Customer> lstCustomer = Customer.RetrieveAllLocation();
            //lstCustomer = lstCustomer.GroupBy(o => o.Location).Select(g => g.First()).ToList();

            Customer customer = new Customer();
            customer.Place = "Select";
            lstCustomer.Insert(0, customer);

            List<VehicleNo> lstVehicleNo = VehicleNo.RetrieveAll();

            VehicleNo vehicleNo = new VehicleNo();
            vehicleNo.VehicleNum = "Select";
            lstVehicleNo.Insert(0, vehicleNo);
            return View(new VehicleLogModel(lstCustomer, lstVehicleNo));

        }

        public ActionResult Delete(int id = 0)
        {
            VehicleLog vehicleLog = VehicleLog.RetrieveById(id);
            if (vehicleLog == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("VehicleLog");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new VehicleLogModel(vehicleLog));
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
            VehicleLog vehicleLog = VehicleLog.RetrieveById(id);
            vehicleLog.Delete();
            return RedirectToAction("Index", "VehicleLog");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new VehicleLogModel(VehicleLog.RetrieveById(id)));
        }


        public ActionResult Excel(string search = "")
        {
            List<VehicleLog> vehicleLogList = VehicleLog.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<VehicleLog> g = new GenericList<VehicleLog>();
                vehicleLogList = g.SerachFun(vehicleLogList, search);
                vehicleLogList = vehicleLogList.Distinct().ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("VehicleLogList");
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
                worksheet.Cell(currentrow, 13).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Employee Name ";
                worksheet.Cell(currentrow, 2).Value = "Date";
                worksheet.Cell(currentrow, 3).Value = "VehicleNo";
                worksheet.Cell(currentrow, 4).Value = "Amount";
                worksheet.Cell(currentrow, 5).Value = "StartingPoint";
                worksheet.Cell(currentrow, 6).Value = "Destination";
                worksheet.Cell(currentrow, 7).Value = "Purpose";
                worksheet.Cell(currentrow, 8).Value = "StartReading";
                worksheet.Cell(currentrow, 9).Value = "EndReading";
                worksheet.Cell(currentrow, 10).Value = "FuelFilled";
                worksheet.Cell(currentrow, 11).Value = "FuelQuantity";
                worksheet.Cell(currentrow, 12).Value = "Voucher";
                worksheet.Cell(currentrow, 13).Value = "Remarks";



                foreach (VehicleLog a in vehicleLogList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.UserName;
                    worksheet.Cell(currentrow, 2).Value = a.TimeStamp;
                    worksheet.Cell(currentrow, 3).Value = a.VehicleNo;
                    worksheet.Cell(currentrow, 4).Value = a.Amount;
                    worksheet.Cell(currentrow, 5).Value = a.StartingPoint;
                    worksheet.Cell(currentrow, 6).Value = a.Destination;
                    worksheet.Cell(currentrow, 7).Value = a.Purpose;
                    worksheet.Cell(currentrow, 8).Value = a.StartReading;
                    worksheet.Cell(currentrow, 9).Value = a.EndReading;
                    worksheet.Cell(currentrow, 10).Value = a.FuelFilled;
                    worksheet.Cell(currentrow, 11).Value = a.FuelQuantity;
                    worksheet.Cell(currentrow, 12).Value = a.Voucher;
                    worksheet.Cell(currentrow, 13).Value = a.Remarks;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VehicleLogList.xlsx");
                }
            }

        }
    }
}
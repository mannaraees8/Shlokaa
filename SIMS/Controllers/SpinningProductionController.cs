using ClosedXML.Excel;
using PagedList;
using SIMS.BL;
using SIMS.ViewModels.SpinningProductionModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
namespace SIMS.Controllers
{
    public class SpinningProductionController : Controller
    {
        // GET: SpinningProduction
        [Authorize]
        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SpinningProduction");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetSpinningProductionList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
                    }
                    else
                    {
                        return View(GetSpinningProductionRetreiveByUserIdList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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

        private IEnumerable<SpinningProductionModel> GetSpinningProductionList(string search = "", string startdate = "", string enddate = "")
        {

            List<SpinningProductionModel> spinningProductionModelList = new List<SpinningProductionModel>();
            List<SpinningProduction> spinningProductionList = SpinningProduction.RetrieveAll();



            if (search.Length > 0)
            {
                GenericList<SpinningProduction> g = new GenericList<SpinningProduction>();
                spinningProductionList = g.SerachFun(spinningProductionList, search);
                spinningProductionList = spinningProductionList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                spinningProductionList = spinningProductionList.OfType<SpinningProduction>().Where(s => s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                spinningProductionList = spinningProductionList.OfType<SpinningProduction>().Where(s => s.Date <= endDate).ToList();
            }

            foreach (SpinningProduction a in spinningProductionList)
            {
                spinningProductionModelList.Add(new SpinningProductionModel(a));
            }

            return spinningProductionModelList;
        }

        private IEnumerable<SpinningProductionModel> GetSpinningProductionRetreiveByUserIdList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            List<SpinningProductionModel> spinningProductionModelList = new List<SpinningProductionModel>();
            List<SpinningProduction> spinningProductionList = SpinningProduction.RetrieveAllByUserId(userId);



            if (search.Length > 0)
            {
                GenericList<SpinningProduction> g = new GenericList<SpinningProduction>();
                spinningProductionList = g.SerachFun(spinningProductionList, search);
                spinningProductionList = spinningProductionList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                spinningProductionList = spinningProductionList.OfType<SpinningProduction>().Where(s => s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                spinningProductionList = spinningProductionList.OfType<SpinningProduction>().Where(s => s.Date <= endDate).ToList();
            }

            foreach (SpinningProduction a in spinningProductionList)
            {
                spinningProductionModelList.Add(new SpinningProductionModel(a));
            }

            return spinningProductionModelList;
        }


        public ActionResult Edit(int id = 0)
        {
            List<Users> lstUsers = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            lstUsers.Insert(0, users);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SpinningProduction");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new SpinningProductionModel(SpinningProduction.RetrieveById(id), lstUsers, lstItem));
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
        public ActionResult Edit(SpinningProductionModel spinningProductionModel)
        {
            bool inValidState = false;

            if (spinningProductionModel.EmployeeName == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Employee Name";
            }
            if (spinningProductionModel.ItemName == "Select")
            {
                inValidState = true;
                TempData["errormsg"] = "please select item name";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {

                    spinningProductionModel.SpinningProduction.Update();
                    return RedirectToAction("Index", "SpinningProduction");
                }
            }
            List<Users> lstUsers = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            lstUsers.Insert(0, users);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);

            return View(new SpinningProductionModel(lstUsers, lstItem));
        }

        public ActionResult Create()
        {

            List<Users> lstUsers = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            lstUsers.Insert(0, users);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);


            SpinningProductionModel spinningProductionModel = new SpinningProductionModel(lstUsers, lstItem);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SpinningProduction");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(spinningProductionModel);
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
        public ActionResult Create(SpinningProductionModel spinningProductionModel)
        {
            bool inValidState = false;
            if (spinningProductionModel.EmployeeName == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Employee Name";
            }
            if (spinningProductionModel.ItemName == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Item Name";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    SpinningProduction.Create(spinningProductionModel.StaffId, spinningProductionModel.Date, spinningProductionModel.ItemName, spinningProductionModel.CircleSize, spinningProductionModel.CircleDate, spinningProductionModel.CircleIssued, spinningProductionModel.FGWeight, spinningProductionModel.Trimming, spinningProductionModel.Broken1, spinningProductionModel.BrokenPercentage, spinningProductionModel.ProductionFromBroken, spinningProductionModel.ProductFromBroken, spinningProductionModel.NetBroken, spinningProductionModel.NetBrokenPercentage, spinningProductionModel.Discrepancy, spinningProductionModel.Remarks, spinningProductionModel.Isdeleted);
                    return RedirectToAction("Index", "SpinningProduction");
                }
            }

            List<Users> lstUsers = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            lstUsers.Insert(0, users);

            List<Item> lstItem = Item.RetrieveAll();
            Item item = new Item();
            item.Name = "Select";
            lstItem.Insert(0, item);

            return View(new SpinningProductionModel(lstUsers, lstItem));
        }

        public ActionResult Delete(int id = 0)
        {
            SpinningProduction spinningProduction = SpinningProduction.RetrieveById(id);
            if (spinningProduction == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("SpinningProduction");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new SpinningProductionModel(spinningProduction));
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
            SpinningProduction spinningProduction = SpinningProduction.RetrieveById(id);
            spinningProduction.Delete();
            return RedirectToAction("Index", "SpinningProduction");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new SpinningProductionModel(SpinningProduction.RetrieveById(id)));
        }
        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<SpinningProductionModel> spinningProductionModelList = new List<SpinningProductionModel>();
            List<SpinningProduction> spinningProductionList = SpinningProduction.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<SpinningProduction> g = new GenericList<SpinningProduction>();
                spinningProductionList = g.SerachFun(spinningProductionList, search);
                spinningProductionList = spinningProductionList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                spinningProductionList = spinningProductionList.OfType<SpinningProduction>().Where(s => s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                spinningProductionList = spinningProductionList.OfType<SpinningProduction>().Where(s => s.Date <= endDate).ToList();
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Spinning Production");
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
                worksheet.Cell(currentrow, 14).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 15).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 16).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 17).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Id";
                worksheet.Cell(currentrow, 2).Value = "Date";
                worksheet.Cell(currentrow, 3).Value = "Item";
                worksheet.Cell(currentrow, 4).Value = "Circle Size";
                worksheet.Cell(currentrow, 5).Value = "Circle Date";
                worksheet.Cell(currentrow, 6).Value = "Circle Issued";
                worksheet.Cell(currentrow, 7).Value = "Employee Name";
                worksheet.Cell(currentrow, 8).Value = "FG Weight";
                worksheet.Cell(currentrow, 9).Value = "Total Trimming Weight";
                worksheet.Cell(currentrow, 10).Value = "Broken";
                worksheet.Cell(currentrow, 11).Value = "Broken %";
                worksheet.Cell(currentrow, 12).Value = "Production From Broken";
                worksheet.Cell(currentrow, 13).Value = "Item From Broken";
                worksheet.Cell(currentrow, 14).Value = "NetBroken";
                worksheet.Cell(currentrow, 15).Value = "NetBroken %";
                worksheet.Cell(currentrow, 16).Value = "Discrepancy";
                worksheet.Cell(currentrow, 17).Value = "Remarks";


                foreach (SpinningProduction a in spinningProductionList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Id;
                    worksheet.Cell(currentrow, 2).Value = a.Date;
                    worksheet.Cell(currentrow, 3).Value = a.ItemName;
                    worksheet.Cell(currentrow, 4).Value = a.CircleSize;
                    worksheet.Cell(currentrow, 5).Value = a.CircleDate;
                    worksheet.Cell(currentrow, 6).Value = a.CircleIssued;
                    worksheet.Cell(currentrow, 7).Value = a.EmployeeName;
                    worksheet.Cell(currentrow, 8).Value = a.FGWeight;
                    worksheet.Cell(currentrow, 9).Value = a.Trimming;
                    worksheet.Cell(currentrow, 10).Value = a.Broken;
                    worksheet.Cell(currentrow, 11).Value = a.BrokenPerc;
                    worksheet.Cell(currentrow, 12).Value = a.ProductionFromBroken;
                    worksheet.Cell(currentrow, 13).Value = a.ProductFromBroken;
                    worksheet.Cell(currentrow, 14).Value = a.NetBroken;
                    worksheet.Cell(currentrow, 15).Value = a.NetBrokenPerc;
                    worksheet.Cell(currentrow, 16).Value = a.Discrepancy;
                    worksheet.Cell(currentrow, 17).Value = a.Remarks;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SpinningProduction.xlsx");
                }


            }
        }
    }

}
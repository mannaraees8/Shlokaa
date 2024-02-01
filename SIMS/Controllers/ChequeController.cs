using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SIMS.ViewModels;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using PagedList;
using System.Globalization;

namespace SIMS.Controllers
{
    [Authorize]
    public class ChequeController : Controller
    {
        // GET: /Cheque/

        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Cheque");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetChequeList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
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

        private IEnumerable<ChequeModel> GetChequeList(string search = "", string startdate = "", string enddate = "")
        {
            List<ChequeModel> chequeModelList = new List<ChequeModel>();
            List<Cheque> chequeList = Cheque.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Cheque> g = new GenericList<Cheque>();
                chequeList = g.SerachFun(chequeList, search);
                chequeList = chequeList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                chequeList = chequeList.OfType<Cheque>().Where(s => s.ChequeDate >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                chequeList = chequeList.OfType<Cheque>().Where(s => s.ChequeDate <= endDate).ToList();
            }

            foreach (Cheque a in chequeList)
            {
                chequeModelList.Add(new ChequeModel(a));
            }

            return chequeModelList;
        }

        public ActionResult Edit(int id = 0)
        {

            List<Customer> customerList = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            customerList.Insert(0, customer);

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Cheque");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new ChequeModel(Cheque.RetrieveById(id), customerList));
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
        public ActionResult Edit(ChequeModel chequeModel)
        {
            bool inValidState = false;

            if (chequeModel.ChequeNo == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter ChequeNo";
            }
            if (chequeModel.PartyName == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please select Party Name";
            }
            if (chequeModel.Amount == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Amount";
            }
            if (chequeModel.BankName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Bank Name";
            }

            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    chequeModel.Cheque.Update();
                    return RedirectToAction("Index", "Cheque");
                }
            }


            List<Customer> customerList = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            customerList.Insert(0, customer);
            return View(new ChequeModel(customerList));
        }

        public ActionResult Create()
        {

            List<Customer> customerList = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            customerList.Insert(0, customer);
            ChequeModel chequeModel = new ChequeModel(customerList);

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Cheque");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(chequeModel);
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
        public ActionResult Create(ChequeModel chequeModel)
        {
            bool inValidState = false;

            if (chequeModel.ChequeNo == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter ChequeNo";
            }
            if (chequeModel.PartyName == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please select Party Name";
            }
            if (chequeModel.Amount == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Amount";
            }
            if (chequeModel.BankName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Bank Name";
            }


            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    Cheque.Create(chequeModel.ChequeNo.Trim(), chequeModel.ChequeDate, chequeModel.PartyName.Trim(), chequeModel.Amount.Trim(), chequeModel.BankName.Trim(), chequeModel.Status, chequeModel.Remark.Trim(), chequeModel.Isdeleted);
                    return RedirectToAction("Index", "Cheque");
                }
            }

            List<Customer> customerList = Customer.RetrieveAll();
            Customer customer = new Customer();
            customer.Name = "Select";
            customerList.Insert(0, customer);
            return View(new ChequeModel(customerList));
        }

        public ActionResult Delete(int id = 0)
        {
            Cheque cheque = Cheque.RetrieveById(id);
            if (cheque == null)
            {
                return HttpNotFound();
            }

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Cheque");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new ChequeModel(cheque));
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
            Cheque cheque = Cheque.RetrieveById(id);
            bool status = cheque.Delete();
            if (status == true)
            {

                return RedirectToAction("Index", "Cheque");
            }
            return View("Delete", new ChequeModel(Cheque.RetrieveById(id)));
        }

        public ActionResult Details(int id = 0)
        {
            return View(new ChequeModel(Cheque.RetrieveById(id)));
        }

        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<ChequeModel> chequeModelList = new List<ChequeModel>();
            List<Cheque> chequeList = Cheque.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Cheque> g = new GenericList<Cheque>();
                chequeList = g.SerachFun(chequeList, search);
                chequeList = chequeList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                chequeList = chequeList.OfType<Cheque>().Where(s => s.ChequeDate >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                chequeList = chequeList.OfType<Cheque>().Where(s => s.ChequeDate <= endDate).ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ChequeList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Id ";
                worksheet.Cell(currentrow, 2).Value = "Cheque No ";
                worksheet.Cell(currentrow, 3).Value = "Cheque Date ";
                worksheet.Cell(currentrow, 4).Value = "Party Name";
                worksheet.Cell(currentrow, 5).Value = "Amount";
                worksheet.Cell(currentrow, 6).Value = "Bank Name ";
                worksheet.Cell(currentrow, 7).Value = "Clearing Date";
                worksheet.Cell(currentrow, 8).Value = "Status";

                foreach (Cheque a in chequeList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Id;
                    worksheet.Cell(currentrow, 2).Value = a.ChequeNo;
                    worksheet.Cell(currentrow, 3).Value = a.ChequeDate;
                    worksheet.Cell(currentrow, 4).Value = a.PartyName;
                    worksheet.Cell(currentrow, 5).Value = a.Amount;
                    worksheet.Cell(currentrow, 6).Value = a.BankName;
                    worksheet.Cell(currentrow, 7).Value = a.ClearingDate;
                    worksheet.Cell(currentrow, 8).Value = a.Status;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cheque.xlsx");
                }


            }
        }
    }
}

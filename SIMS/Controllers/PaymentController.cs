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
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/
        public JsonResult getExpenseList()
        {
            List<PaymentExpenseAdd> paymentExpenseAddlist = PaymentExpenseAdd.RetrieveAll().ToList();

            return new JsonResult { Data = paymentExpenseAddlist, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getFrequencyList(int id)
        {
            PaymentExpenseAdd paymentFrequency = PaymentExpenseAdd.RetrieveById(id);

            return new JsonResult { Data = paymentFrequency, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Payment");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetPaymentList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
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


        private IEnumerable<PaymentModel> GetPaymentList(string search = "", string startdate = "", string enddate = "")
        {
            List<PaymentModel> paymentModelList = new List<PaymentModel>();
            List<Payment> paymentList = Payment.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Payment> g = new GenericList<Payment>();
                paymentList = g.SerachFun(paymentList, search);
                paymentList = paymentList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                paymentList = paymentList.OfType<Payment>().Where(s => s.DueDate >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                paymentList = paymentList.OfType<Payment>().Where(s => s.DueDate <= endDate).ToList();
            }

            foreach (Payment a in paymentList)
            {
                paymentModelList.Add(new PaymentModel(a));
            }


            return paymentModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Payment");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new PaymentModel(Payment.RetrieveById(id)));
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
        public ActionResult Edit(PaymentModel paymentModel)
        {
            bool inValidState = false;

            if (paymentModel.Expense == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Expense";
            }
            if (paymentModel.Frequency == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please select Frequency";
            }
            if (paymentModel.Amount.ToString() == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Amount";
            }

            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    paymentModel.Payment.Update();
                    return RedirectToAction("Index", "Payment");
                }
            }

            return View(paymentModel);
        }

        //public ActionResult UpdatePaidDate(PaymentModel paymentModel)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        paymentModel.Payment.UpdateWithPaidDate();
        //        return RedirectToAction("Index", "Payment");
        //    }

        //    List<PaymentExpenseAdd> lstExpense = PaymentExpenseAdd.RetrieveAll();
        //    PaymentExpenseAdd paymentExpenseAdd = new PaymentExpenseAdd();
        //    paymentExpenseAdd.Expense = "Select";
        //    lstExpense.Insert(0, paymentExpenseAdd);
        //    return View(new PaymentModel(lstExpense));
        //}

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Payment");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View();
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
        public ActionResult Create(PaymentModel paymentModel)
        {
            bool inValidState = false;

            if (paymentModel.Expense == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Expense";
            }
            if (paymentModel.Frequency == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please select Frequency";
            }
            if (paymentModel.Amount.ToString() == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Amount";
            }

            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    Payment.Create(paymentModel.Expense.Trim(), paymentModel.Frequency.Trim(), paymentModel.Amount, paymentModel.Narration.Trim(), paymentModel.DueDate, paymentModel.Isdeleted);
                    return RedirectToAction("Index", "Payment");
                }
            }

            //List<PaymentExpenseAdd> lstExpense = PaymentExpenseAdd.RetrieveAll();
            //PaymentExpenseAdd paymentExpenseAdd = new PaymentExpenseAdd();
            //paymentExpenseAdd.Expense = "Select";
            //lstExpense.Insert(0, paymentExpenseAdd);
            //List<PaymentExpenseAdd> lstFrequency = PaymentExpenseAdd.RetrieveAll();
            //paymentExpenseAdd.Frequency = "Select";
            //lstFrequency.Insert(0, paymentExpenseAdd);
            return View(paymentModel);
        }

        public ActionResult Delete(int id = 0)
        {
            Payment payment = Payment.RetrieveById(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Payment");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new PaymentModel(payment));
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
            Payment payment = Payment.RetrieveById(id);
            bool status = payment.Delete();
            if (status == true)
            {

                return RedirectToAction("Index", "Payment");
            }
            return View("Delete", new PaymentModel(Payment.RetrieveById(id)));
        }

        public ActionResult Details(int id = 0)
        {
            return View(new PaymentModel(Payment.RetrieveById(id)));
        }

        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<PaymentModel> paymentModelList = new List<PaymentModel>();
            List<Payment> paymentList = Payment.RetrieveAll();
            if (search.Length > 0)
            {
                GenericList<Payment> g = new GenericList<Payment>();
                paymentList = g.SerachFun(paymentList, search);
                paymentList = paymentList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                paymentList = paymentList.OfType<Payment>().Where(s => s.DueDate >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {

                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                paymentList = paymentList.OfType<Payment>().Where(s => s.DueDate <= endDate).ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("PaymentList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Id ";
                worksheet.Cell(currentrow, 2).Value = "Expense ";
                worksheet.Cell(currentrow, 3).Value = "Frequency ";
                worksheet.Cell(currentrow, 4).Value = "Amount";
                worksheet.Cell(currentrow, 5).Value = "Due Date";
                worksheet.Cell(currentrow, 6).Value = "Days Overdue";

                foreach (Payment a in paymentList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Id;
                    worksheet.Cell(currentrow, 2).Value = a.Expense;
                    worksheet.Cell(currentrow, 3).Value = a.Frequency;
                    worksheet.Cell(currentrow, 4).Value = a.Amount;
                    worksheet.Cell(currentrow, 5).Value = a.DueDate;
                    worksheet.Cell(currentrow, 6).Value = DateTime.Now.Subtract(a.DueDate).Days.ToString();
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Payment.xlsx");
                }
            }

        }
    }
}


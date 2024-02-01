using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using SIMS.Models;
using SIMS.BL;
using ClosedXML.Excel;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class PaymentExpenseAddController : Controller
    {
        //
        // GET: /PaymentExpenseAdd/

        public ActionResult Index(int? pageIndex, string search = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("PaymentExpense");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetPaymentExpenseAddList(search.Trim()).ToPagedList(pageIndex ?? 1, 10)); ;
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


        private IEnumerable<PaymentExpenseAddModel> GetPaymentExpenseAddList(string search = "")
        {
            List<PaymentExpenseAddModel> paymentExpenseAddModelList = new List<PaymentExpenseAddModel>();
            List<PaymentExpenseAdd> paymentExpenseAddList = PaymentExpenseAdd.RetrieveAll();
            if (search.Length > 0)
            {
                GenericList<PaymentExpenseAdd> g = new GenericList<PaymentExpenseAdd>();
                paymentExpenseAddList = g.SerachFun(paymentExpenseAddList, search);
                paymentExpenseAddList = paymentExpenseAddList.Distinct().ToList();
            }
            foreach (PaymentExpenseAdd a in paymentExpenseAddList)
            {
                paymentExpenseAddModelList.Add(new PaymentExpenseAddModel(a));
            }

            return paymentExpenseAddModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("PaymentExpense");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new PaymentExpenseAddModel(PaymentExpenseAdd.RetrieveById(id)));
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
        public ActionResult Edit(PaymentExpenseAddModel paymentExpenseAddModel)
        {
            PaymentExpenseAdd paymentExpenseAdd = PaymentExpenseAdd.RetrieveById(paymentExpenseAddModel.Id);
            if (paymentExpenseAdd != null)
            {
                paymentExpenseAddModel.PaymentExpenseAdd.Isdeleted = paymentExpenseAdd.Isdeleted;
            }
            if (ModelState.IsValid)
            {
                paymentExpenseAddModel.PaymentExpenseAdd.Update();
                return RedirectToAction("Index");
            }
            return View(paymentExpenseAddModel);
        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("PaymentExpense");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new PaymentExpenseAddModel());
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
        public ActionResult Create(PaymentExpenseAddModel paymentExpenseAddModel)
        {
            if (ModelState.IsValid)
            {
                PaymentExpenseAdd.Create(paymentExpenseAddModel.Expense, paymentExpenseAddModel.Frequency, paymentExpenseAddModel.Isdeleted);
                return RedirectToAction("Index");
            }

            return View(paymentExpenseAddModel);
        }

        public ActionResult Delete(int id = 0)
        {
            PaymentExpenseAdd paymentExpenseAdd = PaymentExpenseAdd.RetrieveById(id);
            if (paymentExpenseAdd == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("PaymentExpense");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new PaymentExpenseAddModel(paymentExpenseAdd));
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
            PaymentExpenseAdd paymentExpenseAdd = PaymentExpenseAdd.RetrieveById(id);
            paymentExpenseAdd.Delete();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new PaymentExpenseAddModel(PaymentExpenseAdd.RetrieveById(id)));
        }

        public ActionResult Excel()
        {

            List<PaymentExpenseAdd> paymentExpenseAddList = PaymentExpenseAdd.RetrieveAll();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("PaymentExpense List");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Expense";
                worksheet.Cell(currentrow, 2).Value = "Frequency";


                foreach (PaymentExpenseAdd a in paymentExpenseAddList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Expense;
                    worksheet.Cell(currentrow, 2).Value = a.Frequency;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PaymentExpenseList.xlsx");
                }
            }

        }
    }
}


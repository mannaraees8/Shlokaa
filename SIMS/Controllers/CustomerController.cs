using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.CustomerModel;
using SIMS.BL;
using ClosedXML.Excel;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index(int? pageIndex, string search = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Customer");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetCustomerModelList(search).ToPagedList(pageIndex ?? 1, 10));

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

        private IEnumerable<CustomerModel> GetCustomerModelList(string search = "")
        {
            List<CustomerModel> customerModelList = new List<CustomerModel>();
            List<Customer> customerList = Customer.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Customer> g = new GenericList<Customer>();
                customerList = g.SerachFun(customerList, search);
                customerList = customerList.Distinct().ToList();
            }

            foreach (Customer a in customerList)
            {
                customerModelList.Add(new CustomerModel(a));
            }

            return customerModelList;
        }

        public ActionResult Edit(int id = 0)
        {

            List<Users> userList = Users.RetrieveMarketingExecutiveData();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Customer");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new CustomerModel(Customer.RetrieveById(id), userList));
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
        public ActionResult Edit(CustomerModel customerModel)
        {
            bool inValidState = false;

            if (customerModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Name";
            }
            if (customerModel.Mobile == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Mobile No";
            }
            if (customerModel.Email == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Email";
            }
            if (customerModel.Vendercode == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Vendercode";
            }
            if (customerModel.ContactPerson == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Contact person name";
            }
            if (customerModel.Location == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Location";
            }
            if (customerModel.Address == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Address";
            }
            if (customerModel.Gstno == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Gst No";
            }
            if (customerModel.State == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Route";
            }
            if (customerModel.MarketingExecutive == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Marketing Executive";
            }
            if (customerModel.SalesOrderTarget.ToString() == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Sales Order Target";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    customerModel.Customer.Update();
                    return RedirectToAction("Index", "Customer");
                }
            }

            List<Users> userList = Users.RetrieveMarketingExecutiveData();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);
            return View(new CustomerModel(userList));
        }

        public ActionResult Create()
        {

            List<Users> userList = Users.RetrieveMarketingExecutiveData();
            userList = userList.Where(o => o.Isdeleted != true).ToList();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Customer");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(new CustomerModel(userList));
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
        public ActionResult Create(CustomerModel customerModel)
        {
            bool inValidState = false;

            if (customerModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Name";
            }
            if (customerModel.Mobile == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Mobile No";
            }
            if (customerModel.Email == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Email";
            }
            if (customerModel.Vendercode == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Vendercode";
            }
            if (customerModel.ContactPerson == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Contact person name";
            }
            if (customerModel.Location == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Location";
            }
            if (customerModel.Address == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Address";
            }
            if (customerModel.Gstno == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Gst No";
            }
            if (customerModel.State == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter State";
            }
            if (customerModel.MarketingExecutive == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Marketing Executive";
            }
            if (customerModel.SalesOrderTarget.ToString() == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Sales Order Target";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    Customer.Create(customerModel.Name.Trim(), customerModel.Mobile.Trim(), customerModel.Email.Trim(), customerModel.Vendercode.Trim(), customerModel.ContactPerson.Trim(), customerModel.Location.Trim(), customerModel.State.Trim(), customerModel.Address.Trim(), customerModel.Gstno.Trim(), customerModel.AccountNo.Trim(), customerModel.BankName.Trim(), customerModel.Branch.Trim(), customerModel.IFSC.Trim(), customerModel.StaffID, customerModel.SalesOrderTarget, customerModel.Isdeleted);
                    return RedirectToAction("Index", "Customer");
                }
            }

            List<Users> userList = Users.RetrieveMarketingExecutiveData();
            Users users = new Users();
            users.Name = "Select";
            userList.Insert(0, users);
            return View(new CustomerModel(userList));
        }

        public ActionResult Delete(int id = 0)
        {
            Customer customer = Customer.RetrieveById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Customer");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new CustomerModel(customer));
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
            Customer customer = Customer.RetrieveById(id);
            bool status = customer.Delete();
            if (status == true)
            {

                return RedirectToAction("Index", "Customer");
            }
            return View("Delete", new CustomerModel(Customer.RetrieveById(id)));
        }

        public ActionResult Details(int id = 0)
        {
            return View(new CustomerModel(Customer.RetrieveById(id)));
        }

        public ActionResult Excel(string search = "")
        {
            List<CustomerModel> customerModelList = new List<CustomerModel>();
            List<Customer> customerList = Customer.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Customer> g = new GenericList<Customer>();
                customerList = g.SerachFun(customerList, search);
                customerList = customerList.Distinct().ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CustomerList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Vendor Code";
                worksheet.Cell(currentrow, 2).Value = "Party Name ";
                worksheet.Cell(currentrow, 3).Value = "Contact Person ";
                worksheet.Cell(currentrow, 4).Value = "Mobile No";
                worksheet.Cell(currentrow, 5).Value = "Address";
                worksheet.Cell(currentrow, 6).Value = "City ";
                worksheet.Cell(currentrow, 7).Value = "Route ";
                worksheet.Cell(currentrow, 8).Value = "Marketing Executive";
                worksheet.Cell(currentrow, 9).Value = "Sales Order Target";
                worksheet.Cell(currentrow, 10).Value = "GSTIN";
                worksheet.Cell(currentrow, 11).Value = "Account No";
                worksheet.Cell(currentrow, 12).Value = "IFSC";

                foreach (Customer a in customerList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Vendercode;
                    worksheet.Cell(currentrow, 2).Value = a.Name;
                    worksheet.Cell(currentrow, 3).Value = a.ContactPerson;
                    worksheet.Cell(currentrow, 4).Value = a.Mobile;
                    worksheet.Cell(currentrow, 5).Value = a.Address;
                    worksheet.Cell(currentrow, 6).Value = a.Location;
                    worksheet.Cell(currentrow, 7).Value = a.State;
                    worksheet.Cell(currentrow, 8).Value = a.MarketingExecutive;
                    worksheet.Cell(currentrow, 9).Value = a.SalesOrderTarget;
                    worksheet.Cell(currentrow, 10).Value = a.Gstno;
                    worksheet.Cell(currentrow, 11).Value = a.AccountNo;
                    worksheet.Cell(currentrow, 12).Value = a.IFSC;


                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
                }
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.ViewModels.UsersModel;
using SIMS.BL;
using System.IO;
using SIMS.ViewModels;
using ClosedXML.Excel;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index(int? pageIndex, string search = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Users");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetUsersModelList(search).ToPagedList(pageIndex ?? 1, 10));

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
        public ActionResult AdminIndex(int? pageIndex, string search = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Users");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetUsersModelList(search).ToPagedList(pageIndex ?? 1, 10));

                    }
                    else
            if (accessMatrixDetails.IsIndex == true)
                    {
                        return RedirectToAction("Index", "Users");
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

        private IEnumerable<UsersModel> GetUsersModelList(string search = "")
        {
            List<UsersModel> usersModelList = new List<UsersModel>();
            List<Users> userList = Users.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Users> g = new GenericList<Users>();
                userList = g.SerachFun(userList, search);
                userList = userList.Distinct().ToList();
            }

            userList = userList.OrderBy(o => o.Id).ToList();
            foreach (Users a in userList)
            {
                usersModelList.Add(new UsersModel(a));
            }

            return usersModelList;
        }



        public ActionResult ActivateUsers(int? pageIndex)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(GetDeactivatedUsersModelList().ToPagedList(pageIndex ?? 1, 10));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }
        }


        private IEnumerable<UsersModel> GetDeactivatedUsersModelList()
        {
            List<UsersModel> usersModelList = new List<UsersModel>();
            foreach (Users a in Users.RetrieveDeactivatedUsers())
            {
                usersModelList.Add(new UsersModel(a));
            }


            return usersModelList;
        }

        public ActionResult Edit(int id = 0)
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Users");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        List<Department> lstDepartment = Department.RetrieveAll();
                        Department department = new Department();
                        department.DepartmentName = "Select";
                        lstDepartment.Insert(0, department);

                        return View(new UsersModel(lstDepartment, Users.RetrieveById(id)));
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
        public ActionResult Edit(UsersModel usersModel, HttpPostedFileBase UserPhoto)
        {
            byte[] imgByte = null;
            bool inValidState = false;
            if (UserPhoto != null && UserPhoto.FileName != "")
            {
                string docName = Path.GetFileName(UserPhoto.FileName);
                imgByte = new byte[UserPhoto.ContentLength];
                UserPhoto.InputStream.Read(imgByte, 0, (int)UserPhoto.ContentLength);
                if (UserPhoto.ContentLength >= (1024 * 1024) * 2)
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Invalid Size";
                }

                else if (usersModel.Name == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Name";
                }
                else if (usersModel.Address == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Address";
                }
                else if (usersModel.Mobile == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Mobile No";
                }
                else if (usersModel.Adhar == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter 12 digit Adhar No";
                }
                else if (usersModel.Email == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Email";
                }
                else if (usersModel.Usertype == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Usertype";
                }
                else if (usersModel.Department == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Department";
                }
                if (inValidState == false)
                {
                    if (ModelState.IsValid)
                    {
                        usersModel.Users.Update(imgByte);
                        string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
                        return RedirectToAction("Index", "Users");

                    }
                }
            }
            else
            {

                if (usersModel.Name == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Name";
                }
                else if (usersModel.Address == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Address";
                }
                else if (usersModel.Mobile == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Mobile No";
                }
                else if (usersModel.Adhar == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter 12 digit Adhar No";
                }
                else if (usersModel.Email == "")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Enter Email";
                }
                else if (usersModel.Usertype == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Usertype";
                }
                else if (usersModel.Department == "Select")
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Please Select Department";
                }
                if (inValidState == false)
                {
                    if (ModelState.IsValid)
                    {
                        usersModel.Users.UpdateWithOutPhoto();
                        string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
                        return RedirectToAction("Index", "Users");

                    }
                }
            }

            List<Department> lstDepartment = Department.RetrieveAll();
            Department department = new Department();
            department.DepartmentName = "Select";
            lstDepartment.Insert(0, department);
            Users users = Users.RetrieveById(usersModel.Id);
            return View(new UsersModel(lstDepartment, users));
        }

        public ActionResult Create()
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Users");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {

                    if (accessMatrixDetails.IsCreate == true)
                    {
                        List<Department> lstDepartment = Department.RetrieveAll();
                        Department department = new Department();
                        department.DepartmentName = "Select";
                        lstDepartment.Insert(0, department);

                        return View(new UsersModel(lstDepartment));
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
        public ActionResult Create(UsersModel usersModel, HttpPostedFileBase UserPhoto)
        {
            byte[] imgByte = null;
            bool inValidState = false;

            if (UserPhoto != null && UserPhoto.FileName != "")
            {
                string docName = Path.GetFileName(UserPhoto.FileName);
                imgByte = new byte[UserPhoto.ContentLength];
                UserPhoto.InputStream.Read(imgByte, 0, (int)UserPhoto.ContentLength);
                if (UserPhoto.ContentLength >= (1024 * 1024) * 2)
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "Invalid Size";
                }
            }
            else
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select User Photo";
            }

            if (usersModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Name";
            }
            else if (usersModel.Address == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Address";
            }
            else if (usersModel.Mobile == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Mobile No";
            }
            else if (usersModel.Adhar == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter 12 digit Adhar No";
            }
            else if (usersModel.Email == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Email";
            }
            else if (usersModel.Usertype == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Usertype";
            }
            else if (usersModel.Password == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Password";
            }
            else if (usersModel.Department == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Department";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    Users users = new Users();
                    bool isExist = users.IsEmailExist(usersModel.Email.Trim());
                    if (isExist == true)
                    {
                        TempData["ErrorMsg"] = "Please any other email.This email is already used";
                    }
                    else
                    {

                        Users.Create(usersModel.Name.Trim(), usersModel.Address.Trim(), usersModel.Location.Trim(), usersModel.State.Trim(), usersModel.Dob, usersModel.Mobile.Trim(), usersModel.Doj, usersModel.Adhar.Trim(), usersModel.Esino.Trim(), usersModel.Pfno.Trim(), usersModel.Bankname.Trim(), usersModel.Bankaccountno.Trim(), usersModel.Branch.Trim(), usersModel.Ifsc.Trim(), usersModel.Email.Trim(), usersModel.Password.Trim(), imgByte, usersModel.Usertype, usersModel.EnteringTime, usersModel.LeavingTime, usersModel.Department, usersModel.SalesTarget);
                        string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
                        return RedirectToAction("Index", "Users");

                    }
                }
            }
            List<Department> lstDepartment = Department.RetrieveAll();
            Department department = new Department();
            department.DepartmentName = "Select";
            lstDepartment.Insert(0, department);
            return View(new UsersModel(lstDepartment));
        }

        public ActionResult Delete(int id = 0)
        {
            Users users = Users.RetrieveById(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Users");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);

                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        return View(new UsersModel(users));
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
            Users users = Users.RetrieveById(id);
            users.Delete();
            return RedirectToAction("Index", "Users");
        }

        public ActionResult Activate(int id = 0)
        {
            Users users = Users.RetrieveById(id);
            users.ActivateUsers();
            return RedirectToAction("Index", "Users");
        }

        [HttpPost, ActionName("Activate")]
        public ActionResult ActivateConfirmed(int id = 0)
        {
            Users users = Users.RetrieveById(id);
            users.ActivateUsers();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            return View(new UsersModel(Users.RetrieveById(id)));
        }

        public ActionResult ChangePassword()
        {
            List<Users> lstUsers = Users.RetrieveAll();
            Users users = new Users();
            users.Name = "Select";
            lstUsers.Insert(0, users);
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            if (userType == "Admin" || userType == "Group Head")
            {
                return View(new ChangePasswordModel(lstUsers));
            }
            else
            {
                TempData["ErrorMsg"] = "Your not authorised to access this page";
                return RedirectToAction("Index", "Staff");
            }

        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            bool success = false;
            Users users = new Users();
            success = users.Update(changePasswordModel.Id, changePasswordModel.Password);
            if (success == true)
            {
                TempData["SuccessMsg"] = "Password successfully updated.";
                return RedirectToAction("Index", "Staff");

            }
            else
            {
                TempData["ErrorMsg"] = "Failed Update Password";
                List<Users> lstUsers = Users.RetrieveAll();
                Users users1 = new Users();
                users1.Name = "Select";
                lstUsers.Insert(0, users1);
                return View(new ChangePasswordModel(lstUsers));

            }
        }

        public ActionResult Excel(string search = "")
        {
            List<UsersModel> usersModelList = new List<UsersModel>();
            List<Users> userList = Users.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Users> g = new GenericList<Users>();
                userList = g.SerachFun(userList, search);
                userList = userList.Distinct().ToList();
            }

            userList = userList.OrderBy(o => o.Id).ToList();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("UserList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Employee Id";
                worksheet.Cell(currentrow, 2).Value = "Name";
                worksheet.Cell(currentrow, 3).Value = "Mobile";
                worksheet.Cell(currentrow, 4).Value = "Email";
                worksheet.Cell(currentrow, 5).Value = "Department";
                worksheet.Cell(currentrow, 6).Value = "User Type";

                foreach (Users a in userList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Id;
                    worksheet.Cell(currentrow, 2).Value = a.Name;
                    worksheet.Cell(currentrow, 3).Value = a.Mobile;
                    worksheet.Cell(currentrow, 4).Value = a.Email;
                    worksheet.Cell(currentrow, 5).Value = a.Department;
                    worksheet.Cell(currentrow, 6).Value = a.Usertype;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
                }
            }

        }
        public JsonResult RetrieveTarget(int id)
        {
            int target = Users.RetrieveTarget(id);
            return new JsonResult { Data = target, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }
}


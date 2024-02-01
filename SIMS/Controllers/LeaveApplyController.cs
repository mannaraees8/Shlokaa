using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SIMS.Models;
using SIMS.BL;
using System.Net.Mail;
using System.Net;
using PagedList;
using System.Globalization;

namespace SIMS.Controllers
{
    [Authorize]
    public class LeaveApplyController : Controller
    {
        //
        // GET: /LeaveApply/

        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.UserType != "Admin" && accessMatrixDetails.UserType != "Executive Accounts")
                    {
                        if (accessMatrixDetails.IsIndex == true)
                        {
                            return View(GetLeaveApplyModelList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
                        }
                        else
                        {

                            TempData["ErrorMsg"] = "Your not authorised to access this page";
                            return RedirectToAction("Index", "Staff");
                        }
                    }
                    else if (accessMatrixDetails.IsIndex == true)
                    {
                        return RedirectToAction("AdminIndex", "LeaveApply");
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



        private IEnumerable<LeaveApplyModel> GetLeaveApplyModelList(string search = "", string startdate = "", string enddate = "")
        {
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            List<LeaveApplyModel> leaveApplyModelList = new List<LeaveApplyModel>();
            List<LeaveApply> leaveApplyList = LeaveApply.RetrieveAllById(userId);

            if (search.Length > 0)
            {
                GenericList<LeaveApply> g = new GenericList<LeaveApply>();
                leaveApplyList = g.SerachFun(leaveApplyList, search);
                leaveApplyList = leaveApplyList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                leaveApplyList = leaveApplyList.OfType<LeaveApply>().Where(s => s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                leaveApplyList = leaveApplyList.OfType<LeaveApply>().Where(s => s.Date <= endDate).ToList();
            }
            foreach (LeaveApply a in leaveApplyList)
            {
                leaveApplyModelList.Add(new LeaveApplyModel(a));
            }


            return leaveApplyModelList;
        }

        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        return View(new LeaveApplyModel(LeaveApply.RetrieveById(id)));
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
        public ActionResult Edit(LeaveApplyModel leaveApplyModel)
        {
            bool inValidState = false;

            if (leaveApplyModel.LeaveCategory == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Leave Category";
            }
            else if (leaveApplyModel.Leavetype == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Leave Leave type";
            }
            else if (leaveApplyModel.Reason == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Reason";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    leaveApplyModel.Staffid = userId;
                    leaveApplyModel.LeaveApply.Update();
                    string utype = LeaveApply.CheckUserType(leaveApplyModel.Staffid);
                    string leaveType = leaveApplyModel.Leavetype.Trim();
                    string reason = leaveApplyModel.Reason.Trim();
                    string date = leaveApplyModel.Date.ToLongDateString();
                    LeaveApply leaveApply = new LeaveApply();
                    if (utype.Equals("Group Head"))
                    {
                        string uEmail = leaveApply.CheckAdminEmail();
                        Users users = Users.RetrieveById(userId);
                        try
                        {
                            using (MailMessage mm = new MailMessage("shlokaa.sims@gmail.com", uEmail))
                            {
                                mm.Subject = "Leave Apply";
                                mm.Body = "<img src='https://shlokaa.biz/Photo/Shlokaa%20White%20png.png' width='200' alt='Shlokaa'/><h3>New Request for leave Approval(Edited).</h3><br/><b>Name :</b> " + users.Name + "<br/><b>Applied Date :</b> " + date + "<br/><b>Leave Type :</b> " + leaveType + "<br/><b>Reason :</b> " + reason;
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("shlokaa.sims@gmail.com", "ShlokaaSIMS123$");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);
                                //"Email sent."
                            }
                        }
                        catch (Exception ee)
                        {
                            TempData["ErrorMsg"] = "Failed Send mail.Please connent your system to Internet" + ee.Message;
                        }
                    }
                    else
                    {
                        string uEmail = leaveApply.CheckManagerEmail();
                        Users users = Users.RetrieveById(userId);
                        try
                        {
                            using (MailMessage mm = new MailMessage("shlokaa.sims@gmail.com", uEmail))
                            {
                                mm.Subject = "Leave Apply";
                                mm.Body = "<img src='https://shlokaa.biz/Photo/Shlokaa%20White%20png.png' width='200' alt='Shlokaa'/><h3>New Request for leave Approval(Edited).</h3><br/><b>Name :</b> " + users.Name + "<br/><b>Applied Date :</b> " + date + "<br/><b>Leave Type :</b> " + leaveType + "<br/><b>Reason :</b> " + reason;
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("shlokaa.sims@gmail.com", "ShlokaaSIMS123$");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);
                                //"Email sent."
                            }
                        }
                        catch (Exception ee)
                        {
                            TempData["ErrorMsg"] = "Failed Send mail.Please connent your system to Internet" + ee.Message;
                        }

                    }
                    return RedirectToAction("Index", "LeaveApply");
                }
            }
            return View(leaveApplyModel);
        }

        public ActionResult Create()
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
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
        public ActionResult Create(LeaveApplyModel leaveApplyModel)
        {
            bool inValidState = false;

            if (leaveApplyModel.LeaveCategory == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Leave Category";
            }
            else if (leaveApplyModel.Leavetype == "Select")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Leave Leave type";
            }
            else if (leaveApplyModel.Reason == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Reason";
            }
            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
                    leaveApplyModel.Staffid = userId;
                    LeaveApply.Create(leaveApplyModel.Staffid, leaveApplyModel.Date, leaveApplyModel.LeaveCategory.Trim(), leaveApplyModel.Leavetype.Trim(), leaveApplyModel.Reason.Trim(), leaveApplyModel.Ismanagerauthorised, leaveApplyModel.Isadminauthorised, leaveApplyModel.Isdeleted);
                    string utype = LeaveApply.CheckUserType(leaveApplyModel.Staffid);
                    string leaveType = leaveApplyModel.Leavetype.Trim();
                    string reason = leaveApplyModel.Reason.Trim();
                    string date = leaveApplyModel.Date.ToLongDateString();
                    LeaveApply leaveApply = new LeaveApply();
                    if (utype.Equals("Group Head"))
                    {
                        string uEmail = leaveApply.CheckAdminEmail();
                        Users users = Users.RetrieveById(userId);
                        try
                        {
                            using (MailMessage mm = new MailMessage("shlokaa.sims@gmail.com", uEmail))
                            {
                                mm.Subject = "Leave Apply";
                                mm.Body = "<img src='https://shlokaa.biz/Photo/Shlokaa%20White%20png.png' width='200' alt='Shlokaa'/><h3>New Request for leave Approval.</h3><br/><b>Name :</b> " + users.Name +"<br/><b>Applied Date :</b> "+date +"<br/><b>Leave Type :</b> " + leaveType + "<br/><b>Reason :</b> " + reason;
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("shlokaa.sims@gmail.com", "ShlokaaSIMS123$");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);
                                //"Email sent."
                            }
                        }
                        catch (Exception ee)
                        {
                            TempData["ErrorMsg"] = "Failed Send mail.Please connent your system to Internet" + ee.Message;
                        }
                    }
                    else
                    {
                        string uEmail = leaveApply.CheckManagerEmail();
                        Users users = Users.RetrieveById(userId);
                        try
                        {
                            using (MailMessage mm = new MailMessage("shlokaa.sims@gmail.com", uEmail))
                            {
                                mm.Subject = "Leave Apply";
                                mm.Body = "<img src='https://shlokaa.biz/Photo/Shlokaa%20White%20png.png' width='200' alt='Shlokaa'/><h3>New Request for leave Approval.</h3><br/><b>Name :</b> " + users.Name + "<br/><b>Applied Date :</b> " + date + "<br/><b>Leave Type :</b> " + leaveType + "<br/><b>Reason :</b> " + reason;
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("shlokaa.sims@gmail.com", "ShlokaaSIMS123$");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);
                                //"Email sent."
                            }
                        }
                        catch (Exception ee)
                        {
                            TempData["ErrorMsg"] = "Failed Send mail.Please connent your system to Internet" + ee.Message;
                        }

                    }
                    return RedirectToAction("Index", "LeaveApply");
                }
            }
            return View(leaveApplyModel);
        }
        
        public ActionResult Delete(int id)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        LeaveApply.Delete(id);
                        return RedirectToAction("AdminApprovalPage", "LeaveApply");
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

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id = 0)
        //{
        //    LeaveApply leaveApply = LeaveApply.RetrieveById(id);
        //    leaveApply.Delete();
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head")
        //    {
        //        return RedirectToAction("ManagerApprovalPage", "LeaveApply");
        //    }
        //    else
        //    {
        //        return RedirectToAction("AdminApprovalPage", "LeaveApply");
        //    }
        //}

        public ActionResult Details(int id = 0)
        {
            return View(new LeaveApplyModel(LeaveApply.RetrieveById(id)));
        }

        public ActionResult ManagerApprovalPage(int? pageIndex, string search = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApplyApprove");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true && accessMatrixDetails.UserType == "Group Head")
                    {
                        return View(GetLeaveApproveList(search.Trim()).ToPagedList(pageIndex ?? 1, 10));
                    }
                    if (accessMatrixDetails.IsIndex == true && accessMatrixDetails.UserType == "Admin")
                    {
                        return RedirectToAction("AdminApprovalPage", "LeaveApply");

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


        private IEnumerable<LeaveApplyModel> GetLeaveApproveList(string search = "")
        {
            List<LeaveApplyModel> leaveApplyModelList = new List<LeaveApplyModel>();
            foreach (LeaveApply a in LeaveApply.RetrieveApproveData())
            {
                leaveApplyModelList.Add(new LeaveApplyModel(a));
            }

            return leaveApplyModelList;
        }
        public ActionResult ManagerApprove(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true && (accessMatrixDetails.UserType == "Group Head" || accessMatrixDetails.UserType == "Admin"))
                    {
                        LeaveApply.ManagerApproval(id);
                        return RedirectToAction("ManagerApprovalPage", "LeaveApply");
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

        //[HttpPost, ActionName("ApproveDetails")]
        //public ActionResult ApproveConfirmed(int id = 0)
        //{
        //    LeaveApply leaveApply = LeaveApply.RetrieveById(id);
        //    leaveApply.ManagerApproval();
        //    return RedirectToAction("ManagerApprovalPage", "LeaveApply");
        //}

        private IEnumerable<LeaveApplyModel> GetLeaveAdminApproveList(string search = "")
        {
            List<LeaveApplyModel> leaveApplyModelList = new List<LeaveApplyModel>();
            foreach (LeaveApply a in LeaveApply.RetrieveAdminApproveData())
            {
                leaveApplyModelList.Add(new LeaveApplyModel(a));
            }

            return leaveApplyModelList;
        }
        public ActionResult AdminApprovalPage(int? pageIndex, string search = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true && accessMatrixDetails.UserType == "Admin")
                    {
                        return View(GetLeaveAdminApproveList(search.Trim()).ToPagedList(pageIndex ?? 1, 10));
                    }
                    else if (accessMatrixDetails.IsIndex == true && accessMatrixDetails.UserType == "Group Head")
                    {
                        return RedirectToAction("ManagerApprovalPage", "LeaveApply");

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
        public ActionResult AdminApprove(int id)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("LeaveApply");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true && (accessMatrixDetails.UserType == "Admin" || accessMatrixDetails.UserType == "Group Head"))
                    {
                        LeaveApply.AdminApproval(id);
                        return RedirectToAction("AdminApprovalPage", "LeaveApply");
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

        public ActionResult AdminIndex(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            return View(GetLeaveAdminList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

        }

        private IEnumerable<LeaveApplyModel> GetLeaveAdminList(string search = "", string startdate = "", string enddate = "")
        {
            List<LeaveApplyModel> leaveApplyModelList = new List<LeaveApplyModel>();
            List<LeaveApply> leaveApplyList = LeaveApply.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<LeaveApply> g = new GenericList<LeaveApply>();
                leaveApplyList = g.SerachFun(leaveApplyList, search);
                leaveApplyList = leaveApplyList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                leaveApplyList = leaveApplyList.OfType<LeaveApply>().Where(s => s.Date >= startDate).ToList();

            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                leaveApplyList = leaveApplyList.OfType<LeaveApply>().Where(s => s.Date <= endDate).ToList();
            }
            foreach (LeaveApply a in leaveApplyList)
            {
                leaveApplyModelList.Add(new LeaveApplyModel(a));
            }

            return leaveApplyModelList;
        }
    }
}


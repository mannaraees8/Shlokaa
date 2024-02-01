using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.BL;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        //
        // GET: /Attendance/



        public ActionResult Index(int? pageIndex, string search = "", string startdate = "", string enddate = "")
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Attendance");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetAllAttendanceList(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));
                    }
                    else
                    {
                        return View(GetAttendanceListById(search.Trim(), startdate, enddate).ToPagedList(pageIndex ?? 1, 10));

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


        private IEnumerable<AttendanceModel> GetAllAttendanceList(string search = "", string startdate = "", string enddate = "")
        {
            List<AttendanceModel> attendanceModelList = new List<AttendanceModel>();
            List<Attendance> attendanceList = Attendance.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Attendance> g = new GenericList<Attendance>();
                attendanceList = g.SerachFun(attendanceList, search);
                attendanceList = attendanceList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

                attendanceList = attendanceList.OfType<Attendance>().Where(s => s.Date >= startDate).ToList();
            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                attendanceList = attendanceList.OfType<Attendance>().Where(s => s.Date <= endDate).ToList();
            }

            foreach (Attendance a in attendanceList)
            {
                attendanceModelList.Add(new AttendanceModel(a));
            }

            return attendanceModelList;
        }
        private IEnumerable<AttendanceModel> GetAttendanceListById(string search = "", string startdate = "", string enddate = "")
        {
            List<AttendanceModel> attendanceModelList = new List<AttendanceModel>();
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            List<Attendance> attendanceList = Attendance.RetrieveById(userId);

            if (search.Length > 0)
            {
                GenericList<Attendance> g = new GenericList<Attendance>();
                attendanceList = g.SerachFun(attendanceList, search);
                attendanceList = attendanceList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

                attendanceList = attendanceList.OfType<Attendance>().Where(s => s.Date >= startDate).ToList();
            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                attendanceList = attendanceList.OfType<Attendance>().Where(s => s.Date <= endDate).ToList();
            }

            foreach (Attendance a in attendanceList)
            {
                attendanceModelList.Add(new AttendanceModel(a));
            }

            return attendanceModelList;
        }

        public ActionResult Create(int pageIndex = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Attendance");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        return View(GetAttendanceModelList(pageIndex));
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
        private IEnumerable<AttendanceModel> GetAttendanceModelList(int pageIndex)
        {
            int pageSize = 10;
            DateTime date = DateTime.Today;
            string status = "Absent";
            List<Attendance> attendanceList = Attendance.RetrieveByDate(date);
            List<AttendanceModel> attendanceModelList = new List<AttendanceModel>();



            if (attendanceList.Count == 0)
            {
                foreach (Users b in Users.RetrieveAllWithoutAdmin())
                {
                    Attendance.Create(b.Id, date, b.EnteringTime, b.LeavingTime, b.EnteringTime, b.LeavingTime, b.Department, status);

                }
                foreach (Attendance a in Attendance.RetrieveByDate(date))
                {
                    attendanceModelList.Add(new AttendanceModel(a));
                }
                ViewBag.PageIndex = pageIndex;
                ViewBag.PageCount = (attendanceModelList.Count + pageSize - 1) / pageSize;
                return attendanceModelList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                foreach (Attendance a in Attendance.RetrieveByDate(date))
                {
                    attendanceModelList.Add(new AttendanceModel(a));
                }

                ViewBag.PageIndex = pageIndex;
                ViewBag.PageCount = (attendanceModelList.Count + pageSize - 1) / pageSize;
                return attendanceModelList.Skip(pageIndex * pageSize).Take(pageSize);
            }



        }

        //public ActionResult Edit(int id = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head" || userType == "Admin" || userType == "Executive Admin" || userType == "Executive Accounts")
        //    {
        //        return View(new AttendanceModel(Attendance.RetrieveById(id)));
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Your not authorised to access this page";
        //        return RedirectToAction("Index", "Staff");
        //    }


        //}


        [HttpPost]
        public JsonResult Save(AttendanceModel attendance, string InTime, string OutTime)
        {
            if (attendance.Reason == null)
            {
                attendance.Reason = "";
            }
            bool status = Attendance.Update(attendance.Id, attendance.Enteringtime, attendance.Leavingtime, attendance.Department, attendance.Status, attendance.Reason);


            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult update(AttendanceModel attendance, string InTime, string OutTime)
        {
            if (attendance.Reason == null)
            {
                attendance.Reason = "";
            }
            bool status = Attendance.UpdateAttendance(attendance.Id, attendance.Enteringtime, attendance.Leavingtime, attendance.Department, attendance.Status, attendance.Reason);


            return new JsonResult { Data = new { status = status } };
        }
        //public ActionResult Delete(int id = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head" || userType == "Admin" || userType == "Executive Admin" || userType == "Executive Accounts")
        //    {
        //        Attendance attendance = Attendance.RetrieveById(id);
        //        if (attendance == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(new AttendanceModel(attendance));
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Your not authorised to access this page";
        //        return RedirectToAction("Index", "Staff");
        //    }


        //}

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id = 0)
        //{
        //    Attendance attendance = Attendance.RetrieveById(id);
        //    attendance.Delete();
        //    return RedirectToAction("Index", "Attendance");
        //}

        //public ActionResult Details(int id = 0)
        //{
        //    string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
        //    if (userType == "Group Head" || userType == "Admin" || userType == "Executive Admin" || userType == "Executive Accounts")
        //    {
        //        return View(new AttendanceModel(Attendance.RetrieveById(id)));
        //    }
        //    else
        //    {
        //        TempData["ErrorMsg"] = "Your not authorised to access this page";
        //        return RedirectToAction("Index", "Staff");
        //    }

        //}
        public ActionResult Excel(string search = "", string startdate = "", string enddate = "")
        {
            List<AttendanceModel> attendanceModelList = new List<AttendanceModel>();
            List<Attendance> attendanceList = Attendance.RetrieveAll();

            if (search.Length > 0)
            {
                GenericList<Attendance> g = new GenericList<Attendance>();
                attendanceList = g.SerachFun(attendanceList, search);
                attendanceList = attendanceList.Distinct().ToList();
            }
            if (startdate.Length > 0)
            {
                DateTime startDate = DateTime.ParseExact(startdate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

                attendanceList = attendanceList.OfType<Attendance>().Where(s => s.Date >= startDate).ToList();
            }
            if (enddate.Length > 0)
            {
                DateTime endDate = DateTime.ParseExact(enddate, new string[] { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                attendanceList = attendanceList.OfType<Attendance>().Where(s => s.Date <= endDate).ToList();
            }
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Attendance");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Value = "Id";
                worksheet.Cell(currentrow, 2).Value = "Date";
                worksheet.Cell(currentrow, 3).Value = "Employee Name";
                worksheet.Cell(currentrow, 4).Value = "Department";
                worksheet.Cell(currentrow, 5).Value = "Entering time";
                worksheet.Cell(currentrow, 6).Value = "Leaving time ";
                worksheet.Cell(currentrow, 7).Value = "InTime";
                worksheet.Cell(currentrow, 8).Value = "OutTime";
                worksheet.Cell(currentrow, 9).Value = "Status";
                worksheet.Cell(currentrow, 10).Value = "Reason";

                foreach (Attendance a in attendanceList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Id;
                    worksheet.Cell(currentrow, 2).Value = a.Date;
                    worksheet.Cell(currentrow, 3).Value = a.StaffName;
                    worksheet.Cell(currentrow, 4).Value = a.Department;
                    worksheet.Cell(currentrow, 5).Value = a.Enteringtime;
                    worksheet.Cell(currentrow, 6).Value = a.Leavingtime;
                    worksheet.Cell(currentrow, 7).Value = a.InTime;
                    worksheet.Cell(currentrow, 8).Value = a.OutTime;
                    worksheet.Cell(currentrow, 8).Value = a.Status;
                    worksheet.Cell(currentrow, 8).Value = a.Reason;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Attendance.xlsx");
                }


            }
        }

    }
}


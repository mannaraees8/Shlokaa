using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SIMS.BL
{

    public class Attendance
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffid;
        private string _staffname;
        private DateTime _date;
        private DateTime? _enteringtime;
        private DateTime? _leavingtime;
        private DateTime? _inTime;
        private DateTime? _outTime;
        private string _department;
        private string _status;
        private string _reason;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public int Staffid { get { return _staffid; } set { _staffid = value; } }
        public string StaffName { get { return _staffname; } set { _staffname = value; } }

        public DateTime Date { get { return _date; } set { _date = value; } }

        public DateTime? Enteringtime { get { return _enteringtime; } set { _enteringtime = value; } }

        public DateTime? Leavingtime { get { return _leavingtime; } set { _leavingtime = value; } }
        public DateTime? InTime { get { return _inTime; } set { _inTime = value; } }

        public DateTime? OutTime { get { return _outTime; } set { _outTime = value; } }

        public string Department { get { return _department; } set { _department = value; } }

        public string Status { get { return _status; } set { _status = value; } }

        public string Reason { get { return _reason; } set { _reason = value; } }

        #endregion //Props

        #region CTOR

        public Attendance()
        {
            _id = 0;
            _staffid = 0;
            _staffname = "";
            _date = DateTime.Now;
            _enteringtime = Convert.ToDateTime("09:00:00");
            _leavingtime = Convert.ToDateTime("06:00:00");
            _inTime = Convert.ToDateTime("09:00:00");
            _outTime = Convert.ToDateTime("06:00:00");
            _department = "";
            _status = "Absent";
            _reason = "";
        }


        public Attendance(int id, string staffname, DateTime date, DateTime? enteringtime, DateTime? leavingtime, DateTime? inTime, DateTime? outTime, string department, string status, string reason)
        {
            _id = id;
            _staffname = staffname;
            _date = date;
            _enteringtime = enteringtime;
            _leavingtime = leavingtime;
            _inTime = inTime;
            _outTime = outTime;
            _department = department;
            _status = status;
            _reason = reason;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(int Staffid, DateTime Date, DateTime? enteringtime, DateTime? leavinetime, DateTime? inTime, DateTime? outTime, string Department, string status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sqlStatement;
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Attendance ( Staffid, Date,EnteringTime,LeavingTime,Department,InTime,OutTime, Status ) "
                        + " values ( @Staffid, @Date, @EnteringTime,@LeavingTime,@Department,@inTime,@outTime, @Status )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Staffid", Staffid);
                    manageCommand.Parameters.AddWithValue("@Date", Date);
                    manageCommand.Parameters.AddWithValue("@EnteringTime", enteringtime);
                    manageCommand.Parameters.AddWithValue("@LeavingTime", leavinetime);
                    manageCommand.Parameters.AddWithValue("@Department", Department);
                    manageCommand.Parameters.AddWithValue("@inTime", inTime);
                    manageCommand.Parameters.AddWithValue("@outTime", outTime);
                    manageCommand.Parameters.AddWithValue("@Status", status);


                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();
                    return count;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static List<Attendance> RetrieveAll()
        {
            List<Attendance> attendancelist = new List<Attendance>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select Attendance.ID, Users.Name,Attendance.Date, Attendance.Enteringtime, Attendance.Leavingtime,Attendance.InTime,Attendance.OutTime, Attendance.Department, Attendance.Status, Attendance.Reason from Attendance INNER JOIN Users ON Attendance.StaffID=Users.ID order by Attendance.Date desc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Attendance");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        attendancelist.Add(new Attendance(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["Name"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , (Convert.IsDBNull(r["Enteringtime"]) ? new DateTime() : Convert.ToDateTime(r["Enteringtime"].ToString()))
                            , (Convert.IsDBNull(r["Leavingtime"]) ? new DateTime() : Convert.ToDateTime(r["Leavingtime"].ToString()))
                            , (Convert.IsDBNull(r["InTime"]) ? new DateTime() : Convert.ToDateTime(r["InTime"].ToString()))
                            , (Convert.IsDBNull(r["OutTime"]) ? new DateTime() : Convert.ToDateTime(r["OutTime"].ToString()))
                            , Convert.ToString(r["Department"].ToString())
                            , Convert.ToString(r["Status"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return attendancelist;
        }

        public static List<Attendance> RetrieveByDate(DateTime Date)
        {
            List<Attendance> attendancelist = new List<Attendance>();
        
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select Users.ID, Users.Name,Attendance.Date, Attendance.Enteringtime, Attendance.Leavingtime,Attendance.InTime,Attendance.OutTime,Attendance.Department, Attendance.Status, Attendance.Reason from Attendance INNER JOIN Users ON Attendance.StaffID=Users.ID where Attendance.Date=@Date";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@Date", Date);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Attendance");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        attendancelist.Add(new Attendance(Convert.ToInt32(r["ID"])
                           , Convert.ToString(r["Name"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , (Convert.IsDBNull(r["Enteringtime"]) ? new DateTime() : Convert.ToDateTime(r["Enteringtime"].ToString()))
                            , (Convert.IsDBNull(r["Leavingtime"]) ? new DateTime() : Convert.ToDateTime(r["Leavingtime"].ToString()))
                            , (Convert.IsDBNull(r["InTime"]) ? new DateTime() : Convert.ToDateTime(r["InTime"].ToString()))
                            , (Convert.IsDBNull(r["OutTime"]) ? new DateTime() : Convert.ToDateTime(r["OutTime"].ToString()))
                            , Convert.ToString(r["Department"].ToString())
                            , Convert.ToString(r["Status"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return attendancelist;
        }
        public static List<Attendance> RetrieveById(int Id)
        {
            List<Attendance> result = new List<Attendance>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select Attendance.ID,Attendance.StaffID, Users.Name,Attendance.Date, Attendance.Enteringtime, Attendance.Leavingtime,Attendance.InTime,Attendance.OutTime, Attendance.Department, Attendance.Status, Attendance.Reason from Attendance INNER JOIN Users ON Attendance.StaffID=Users.ID where Users.ID= @Id order by Attendance.Date desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Attendance");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        result.Add(new Attendance(Convert.ToInt32(r["Id"])
                          , Convert.ToString(r["Name"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , (Convert.IsDBNull(r["Enteringtime"]) ? new DateTime() : Convert.ToDateTime(r["Enteringtime"].ToString()))
                            , (Convert.IsDBNull(r["Leavingtime"]) ? new DateTime() : Convert.ToDateTime(r["Leavingtime"].ToString()))
                            , (Convert.IsDBNull(r["InTime"]) ? new DateTime() : Convert.ToDateTime(r["InTime"].ToString()))
                            , (Convert.IsDBNull(r["OutTime"]) ? new DateTime() : Convert.ToDateTime(r["OutTime"].ToString()))
                            , Convert.ToString(r["Department"].ToString())
                            , Convert.ToString(r["Status"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                        ));
                    }
                    r.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed",  ex);
            }
        }

        public static bool Update(int staffId, DateTime? Enteringtime, DateTime? Leavingtime, string Department, string Status, string Reason)
        {
            DateTime Date = DateTime.Today;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sqlStatement;

                    SqlCommand manageCommand;
                    sqlStatement = "Update Attendance Set "

                        + " Enteringtime = @Enteringtime "
                        + ", Leavingtime = @Leavingtime "
                        + ", Department = @Department "
                        + ", Status = @Status "
                        + ", Reason = @Reason "
                        + "  where StaffId= @Id and Date=@Date";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", staffId);
                    manageCommand.Parameters.AddWithValue("@Date", Date);
                    manageCommand.Parameters.AddWithValue("@Enteringtime", Enteringtime);
                    manageCommand.Parameters.AddWithValue("@Leavingtime", Leavingtime);
                    manageCommand.Parameters.AddWithValue("@Department", Department);
                    manageCommand.Parameters.AddWithValue("@Status", Status);
                    manageCommand.Parameters.AddWithValue("@Reason", Reason);

                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();

                    return (count > 0 ? true : false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static bool UpdateAttendance(int staffId, DateTime? Enteringtime, DateTime? Leavingtime, string Department, string Status, string Reason)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    string sqlStatement;

                    SqlCommand manageCommand;
                    sqlStatement = "Update Attendance Set "

                                + " Enteringtime = @Enteringtime "
                                + ", Leavingtime = @Leavingtime "
                                + ", Department = @Department "
                                + ", Status = @Status "
                                + ", Reason = @Reason "
                                + "  where ID= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", staffId);
                    manageCommand.Parameters.AddWithValue("@Enteringtime", Enteringtime);
                    manageCommand.Parameters.AddWithValue("@Leavingtime", Leavingtime);
                    manageCommand.Parameters.AddWithValue("@Department", Department);
                    manageCommand.Parameters.AddWithValue("@Status", Status);
                    manageCommand.Parameters.AddWithValue("@Reason", Reason);

                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();

                    return (count > 0 ? true : false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public bool Delete()
        {
            try
            {
                string sqlStatement = "Update Attendance Set isDeleted = 1 where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();
                    return (count > 0 ? true : false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        #endregion //CRUD

    }

}
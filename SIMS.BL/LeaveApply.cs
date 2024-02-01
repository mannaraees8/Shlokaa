using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace SIMS.BL
{

    public class LeaveApply
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffid;
        private string _userType;
        private string _userName;
        private DateTime _date;
        private DateTime _approveddate;
        private bool _isfullday;
        private string _leavetype;
        private string _leaveCategory;
        private string _reason;
        private bool _ismanagerauthorised;
        private bool _isadminauthorised;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public int Staffid { get { return _staffid; } set { _staffid = value; } }

        public DateTime Date { get { return _date; } set { _date = value; } }

        public DateTime ApprovedDate { get { return _approveddate; } set { _approveddate = value; } }

        public bool Isfullday { get { return _isfullday; } set { _isfullday = value; } }

        public string Leavetype { get { return _leavetype; } set { _leavetype = value; } }
        public string LeaveCategory { get { return _leaveCategory; } set { _leaveCategory = value; } }

        public string Reason { get { return _reason; } set { _reason = value; } }

        public string UserType { get { return _userType; } set { _userType = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }

        public bool Ismanagerauthorised { get { return _ismanagerauthorised; } set { _ismanagerauthorised = value; } }

        public bool Isadminauthorised { get { return _isadminauthorised; } set { _isadminauthorised = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }


        #endregion //Props

        #region CTOR

        public LeaveApply()
        {
            _id = 0;
            _staffid = 0;
            _date = DateTime.Now;
            _approveddate = DateTime.Now;
            _isfullday = false;
            _leavetype = "";
            _ismanagerauthorised = false;
            _isadminauthorised = false;
            _isdeleted = false;
            _reason = "";
            _leaveCategory = "";
            _userName = "";
            _userType = "";
        }
        public LeaveApply(DateTime ApprovedDate)
        {
            _approveddate = ApprovedDate;
        }

        public LeaveApply(int id, int staffid, DateTime date, string leavetype, string LeaveCategory, string Reason, bool ismanagerauthorised, bool isadminauthorised, bool isdeleted, string UserName, string UserType)
        {
            _id = id;
            _staffid = staffid;
            _date = date;
            //    _isfullday = isfullday;
            _leavetype = leavetype;
            _reason = Reason;
            _ismanagerauthorised = ismanagerauthorised;
            _isadminauthorised = isadminauthorised;
            _isdeleted = isdeleted;
            _leaveCategory = LeaveCategory;
            _userType = UserType;
            _userName = UserName;
        }


        #endregion //CTOR

        #region CRUD
        public static int Create(int Staffid, DateTime Date, string Leavetype, string LeaveCategory, string Reason, bool Ismanagerauthorised, bool Isadminauthorised, bool Isdeleted)
        {
            try
            {
                //  LeaveApply leaveApply=new LeaveApply();
                // string usertype= leaveApply.CheckUserType(Staffid);
                //   if (usertype == "") { }
                // Users users = new Users();
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert LeaveApply ( Staffid, Date, Leavetype,LeaveCategory,Reason, Ismanagerauthorised, Isadminauthorised, Isdeleted ) "
                        + " values ( @Staffid, @Date, @Leavetype,@LeaveCategory,@Reason, @Ismanagerauthorised, @Isadminauthorised, @Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Staffid", Staffid);
                    manageCommand.Parameters.AddWithValue("@Date", Date);
                    manageCommand.Parameters.AddWithValue("@LeaveCategory", LeaveCategory);
                    manageCommand.Parameters.AddWithValue("@Leavetype", Leavetype);
                    manageCommand.Parameters.AddWithValue("@Reason", Reason);
                    manageCommand.Parameters.AddWithValue("@Ismanagerauthorised", Ismanagerauthorised);
                    manageCommand.Parameters.AddWithValue("@Isadminauthorised", Isadminauthorised);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", Isdeleted);

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

        public static List<LeaveApply> RetrieveAll()
        {
            List<LeaveApply> leaveapplylist = new List<LeaveApply>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select L.Id, L.Staffid, L.Date, L.Isfullday, L.Leavetype,L.LeaveCategory,L.Reason, L.Ismanagerauthorised, L.Isadminauthorised, L.Isdeleted,U.Name As UserName,U.UserType from LeaveApply L,Users U where U.Id=L.StaffId ORDER BY U.Name ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "LeaveApply");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        leaveapplylist.Add(new LeaveApply(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["LeaveCategory"].ToString())
                            , Convert.ToString(r["Leavetype"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                            , Convert.ToBoolean(r["Ismanagerauthorised"].ToString())
                            , Convert.ToBoolean(r["Isadminauthorised"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                             , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return leaveapplylist;
        }

        public static List<LeaveApply> RetrieveAllById(int Id)
        {
            List<LeaveApply> leaveapplylist = new List<LeaveApply>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select L.Id, L.Staffid, L.Date, L.Isfullday, L.Leavetype,L.LeaveCategory,L.Reason, L.Ismanagerauthorised, L.Isadminauthorised, L.Isdeleted,U.Name As UserName,U.UserType from LeaveApply L,Users U where L.IsDeleted=0 and U.Id=L.StaffId and L.StaffId=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "LeaveApply");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        leaveapplylist.Add(new LeaveApply(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["LeaveCategory"].ToString())
                            , Convert.ToString(r["Leavetype"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                            , Convert.ToBoolean(r["Ismanagerauthorised"].ToString())
                            , Convert.ToBoolean(r["Isadminauthorised"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                             , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return leaveapplylist;
        }

        public static LeaveApply RetrieveById(int Id)
        {
            LeaveApply result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select L.Id, L.Staffid, L.Date, L.Isfullday, L.Leavetype,L.LeaveCategory,L.Reason, L.Ismanagerauthorised, L.Isadminauthorised, L.Isdeleted,U.Name As UserName,U.UserType from LeaveApply L,Users U where  U.Id=L.StaffId and L.Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "LeaveApply");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new LeaveApply(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["LeaveCategory"].ToString())
                            , Convert.ToString(r["Leavetype"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                            , Convert.ToBoolean(r["Ismanagerauthorised"].ToString())
                            , Convert.ToBoolean(r["Isadminauthorised"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                        );
                    }
                    r.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static List<LeaveApply> RetrieveApproveData()
        {
            List<LeaveApply> leaveapplylist = new List<LeaveApply>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select L.Id, L.Staffid, L.Date, L.Isfullday, L.Leavetype,L.LeaveCategory,L.Reason, L.Ismanagerauthorised, L.Isadminauthorised, L.Isdeleted,U.Name As UserName,U.UserType from LeaveApply L,Users U where  U.Id=L.StaffId and UserType!='Group Head' and UserType!='Admin'";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "LeaveApply");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        leaveapplylist.Add(new LeaveApply(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["LeaveCategory"].ToString())
                            , Convert.ToString(r["Leavetype"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                            , Convert.ToBoolean(r["Ismanagerauthorised"].ToString())
                            , Convert.ToBoolean(r["Isadminauthorised"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return leaveapplylist;
        }


        public static List<LeaveApply> RetrieveAdminApproveData()
        {
            List<LeaveApply> leaveapplylist = new List<LeaveApply>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select L.Id,L.Staffid, L.Date, L.Isfullday, L.Leavetype,L.LeaveCategory,L.Reason, L.Ismanagerauthorised,L.Isadminauthorised,L.Isdeleted,U.Name As UserName,U.UserType from LeaveApply L,Users U where  U.Id=L.StaffId and U.UserType='Group Head' and L.Isadminauthorised=0 and L.Isdeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        leaveapplylist.Add(new LeaveApply(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["LeaveCategory"].ToString())
                            , Convert.ToString(r["Leavetype"].ToString())
                            , Convert.ToString(r["Reason"].ToString())
                            , Convert.ToBoolean(r["Ismanagerauthorised"].ToString())
                            , Convert.ToBoolean(r["Isadminauthorised"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return leaveapplylist;
        }

        public bool Update()
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update LeaveApply Set "
                        + "  Staffid = @Staffid "
                        + ", Date = @Date "
                        + ", Leavetype = @Leavetype "
                                            + ", LeaveCategory= @LeaveCategory "
                                            + ", Reason = @Reason "
                        + ", Ismanagerauthorised = @Ismanagerauthorised "
                        + ", Isadminauthorised = @Isadminauthorised "
                        + ", Isdeleted = @Isdeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Staffid", this.Staffid);
                    manageCommand.Parameters.AddWithValue("@Date", this.Date);
                    manageCommand.Parameters.AddWithValue("@LeaveCategory", this.LeaveCategory);
                    manageCommand.Parameters.AddWithValue("@Leavetype", this.Leavetype);
                    manageCommand.Parameters.AddWithValue("@Reason", this.Reason);
                    manageCommand.Parameters.AddWithValue("@Ismanagerauthorised", this.Ismanagerauthorised);
                    manageCommand.Parameters.AddWithValue("@Isadminauthorised", this.Isadminauthorised);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", this.Isdeleted);

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


        public static bool Delete(int id)
        {
            try
            {
                string sqlStatement = "Update LeaveApply Set isDeleted = 1 where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", id);
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

        public static bool ManagerApproval(int id)
        {
            try
            {
                string sqlStatement = "Update LeaveApply Set Ismanagerauthorised = 1,ApprovedDate=@ApprovedDate where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                    manageCommand.Parameters.AddWithValue("@Id", id);
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


        public static bool AdminApproval(int id)
        {
            try
            {
                string sqlStatement = "Update LeaveApply Set Isadminauthorised = 1 , ApprovedDate=@ApprovedDate where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                    manageCommand.Parameters.AddWithValue("@Id", id);
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
      


        public static string CheckUserType(int Staffid)
        {
            string userType = "";

            try
            {
                string sqlStatement = "Select UserType from Users where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Staffid);
                    connection.Open();
                    SqlDataReader r = manageCommand.ExecuteReader();
                    if (r.HasRows)
                    {
                        if (r.Read())
                        {
                            userType = r.GetValue(0).ToString();
                        }
                    }
                    r.Close();
                    int count = manageCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return userType;
        }

        public static string RetrieveUserEmail(int Staffid)
        {
            string Email = "";
            try
            {
                string sqlStatement = "Select Email from Users where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Staffid);
                    connection.Open();
                    SqlDataReader r = manageCommand.ExecuteReader();
                    if (r.HasRows)
                    {
                        if (r.Read())
                        {
                            Email = r.GetValue(0).ToString();
                        }
                    }
                    r.Close();

                    int count = manageCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return Email;

        }
        public string CheckManagerEmail()
        {
            string Email = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Select Email from Users where UserType='Group Head' and IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        Email = r["Email"].ToString();
                    }
                    r.Close();
                    return Email;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }

        }
        public string CheckAdminEmail()
        {
            string Email = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Select Email from Users where UserType='Admin' and IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        Email = r["Email"].ToString();
                    }
                    r.Close();
                    return Email;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }

        }
    }

}
#endregion //CRUD
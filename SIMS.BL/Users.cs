using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace SIMS.BL
{

    public class Users
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _name;
        private string _address;
        private string _location;
        private string _state;
        private DateTime _dob;
        private string _mobile;
        private DateTime _doj;
        private string _adhar;
        private string _esino;
        private string _pfno;
        private string _bankname;
        private string _bankaccountno;
        private string _branch;
        private string _ifsc;
        private string _email;
        private string _password;
        private byte[] _photo;
        private DateTime? _dol;
        private string _usertype;
        private string _department;
        private DateTime? _enteringTime;
        private DateTime? _leavingTime;
        private int _target;
        private int _salesTarget;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public string Name { get { return _name; } set { _name = value; } }

        public string Address { get { return _address; } set { _address = value; } }

        public string Location { get { return _location; } set { _location = value; } }

        public string State { get { return _state; } set { _state = value; } }

        public DateTime Dob { get { return _dob; } set { _dob = value; } }

        public string Mobile { get { return _mobile; } set { _mobile = value; } }

        public DateTime Doj { get { return _doj; } set { _doj = value; } }

        public string Adhar { get { return _adhar; } set { _adhar = value; } }

        public string Esino { get { return _esino; } set { _esino = value; } }

        public string Pfno { get { return _pfno; } set { _pfno = value; } }

        public string Bankname { get { return _bankname; } set { _bankname = value; } }

        public string Bankaccountno { get { return _bankaccountno; } set { _bankaccountno = value; } }

        public string Branch { get { return _branch; } set { _branch = value; } }

        public string Ifsc { get { return _ifsc; } set { _ifsc = value; } }

        public string Email { get { return _email; } set { _email = value; } }

        public string Password { get { return _password; } set { _password = value; } }

        public byte[] Photo { get { return _photo; } set { _photo = value; } }

        public DateTime? Dol { get { return _dol; } set { _dol = value; } }
        public DateTime? EnteringTime { get { return _enteringTime; } set { _enteringTime = value; } }
        public DateTime? LeavingTime { get { return _leavingTime; } set { _leavingTime = value; } }
        public string Department { get { return _department; } set { _department = value; } }
        public string Usertype { get { return _usertype; } set { _usertype = value; } }
        public int Target { get { return _target; } set { _target = value; } }
        public int SalesTarget { get { return _salesTarget; } set { _salesTarget = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Users()
        {
            _id = 0;
            _name = "";
            _address = "";
            _location = "";
            _state = "";
            _dob = DateTime.Now;
            _mobile = "";
            _doj = DateTime.Now;
            _enteringTime = DateTime.Now; ;
            _leavingTime = DateTime.Now; ;
            _adhar = "";
            _esino = "";
            _pfno = "";
            _bankname = "";
            _bankaccountno = "";
            _branch = "";
            _ifsc = "";
            _email = "";
            _password = "";
            _photo = null;
            _dol = null;
            _usertype = "";
            _target = 0;
            _salesTarget = 0;
            _isdeleted = false;
        }
        public Users(string Email)
        {
            _email = Email;
        }


        public Users(int id, string name, string address, string location, string state, DateTime dob, string mobile, DateTime doj, string adhar, string esino, string pfno, string bankname, string bankaccountno, string branch, string ifsc, string email, string password, byte[] photo, DateTime? dol, string usertype, bool isdeleted, string Department, DateTime? EnteringTime, DateTime? LeavingTime, int target, int salesTarget)
        {
            _id = id;
            _name = name;
            _address = address;
            _location = location;
            _state = state;
            _dob = dob;
            _mobile = mobile;
            _doj = doj;
            _adhar = adhar;
            _esino = esino;
            _pfno = pfno;
            _bankname = bankname;
            _bankaccountno = bankaccountno;
            _branch = branch;
            _ifsc = ifsc;
            _email = email;
            _password = password;
            _photo = photo;
            _dol = dol;
            _usertype = usertype;
            _enteringTime = EnteringTime;
            _leavingTime = LeavingTime;
            _department = Department;
            _target = target;
            _salesTarget = salesTarget;
            _isdeleted = isdeleted;
        }

        public Users(string userType, bool IsDeleted)
        {
            _usertype = userType;
            _isdeleted = IsDeleted;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(string Name, string Address, string Location, string State, DateTime? Dob, string Mobile, DateTime Doj, string Adhar, string Esino, string Pfno, string Bankname, string Bankaccountno, string Branch, string Ifsc, string Email, string Password, byte[] Photo, string Usertype, DateTime? EnteringTime, DateTime? LeavingTime, string Department, int salesTarget)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Users ( Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Usertype,Department,EnteringTime,LeavingTime,SalesTarget ) "
                        + " values ( @Name, @Address, @Location, @State, @Dob, @Mobile, @Doj, @Adhar, @Esino, @Pfno, @Bankname, @Bankaccountno, @Branch, @Ifsc, @Email, @Password, @Photo, @Usertype,@Department,@EnteringTime,@LeavingTime,@SalesTarget  )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Name", Name);
                    manageCommand.Parameters.AddWithValue("@Address", Address);
                    manageCommand.Parameters.AddWithValue("@Location", Location);

                    manageCommand.Parameters.AddWithValue("@State", State);
                    manageCommand.Parameters.AddWithValue("@Dob", Dob);
                    manageCommand.Parameters.AddWithValue("@Mobile", Mobile);
                    manageCommand.Parameters.AddWithValue("@Doj", Doj);
                    manageCommand.Parameters.AddWithValue("@Adhar", Adhar);
                    manageCommand.Parameters.AddWithValue("@Esino", Esino);
                    manageCommand.Parameters.AddWithValue("@Pfno", Pfno);
                    manageCommand.Parameters.AddWithValue("@Bankname", Bankname);
                    manageCommand.Parameters.AddWithValue("@Bankaccountno", Bankaccountno);
                    manageCommand.Parameters.AddWithValue("@Branch", Branch);
                    manageCommand.Parameters.AddWithValue("@Ifsc", Ifsc);
                    manageCommand.Parameters.AddWithValue("@Email", Email);
                    manageCommand.Parameters.AddWithValue("@Password", Password);
                    manageCommand.Parameters.AddWithValue("@Usertype", Usertype);
                    if (Photo == null)
                    {
                        manageCommand.Parameters.Add("@Photo", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    }
                    else
                    {
                        manageCommand.Parameters.AddWithValue("@Photo", Photo);
                    }
                    manageCommand.Parameters.AddWithValue("@Department", Department);
                    manageCommand.Parameters.AddWithValue("@EnteringTime", EnteringTime);
                    manageCommand.Parameters.AddWithValue("@LeavingTime", LeavingTime);
                    manageCommand.Parameters.AddWithValue("@SalesTarget", salesTarget);
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

        public static List<Users> RetrieveAll()
        {
            List<Users> userslist = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted,Department,EnteringTime,LeavingTime,Target,SalesTarget from Users where  Isdeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        userslist.Add(new Users(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , (Convert.IsDBNull(r["Dob"]) ? new DateTime() : Convert.ToDateTime(r["Dob"].ToString()))
                            , Convert.ToString(r["Mobile"].ToString())
                            , (Convert.IsDBNull(r["Doj"]) ? new DateTime() : Convert.ToDateTime(r["Doj"].ToString()))
                            , Convert.ToString(r["Adhar"].ToString())
                            , Convert.ToString(r["Esino"].ToString())
                            , Convert.ToString(r["Pfno"].ToString())
                            , Convert.ToString(r["Bankname"].ToString())
                            , Convert.ToString(r["Bankaccountno"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["Ifsc"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Password"].ToString())
                            , (r["Photo"]) != DBNull.Value ? ((byte[])r["Photo"]) : null
                            , (Convert.IsDBNull(r["Dol"]) ? new DateTime() : Convert.ToDateTime(r["Dol"].ToString()))
                            , Convert.ToString(r["Usertype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["Department"].ToString())
                              , (Convert.IsDBNull(r["EnteringTime"]) ? new DateTime() : Convert.ToDateTime(r["EnteringTime"].ToString()))
                            , (Convert.IsDBNull(r["LeavingTime"]) ? new DateTime() : Convert.ToDateTime(r["LeavingTime"].ToString()))
                            , (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"])
                            , (r["SalesTarget"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesTarget"])
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return userslist;
        }

        public static List<Users> RetrieveUniqueUserType()
        {
            List<Users> userslist = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Distinct UserType,IsDeleted from Users where IsDeleted=0";

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        userslist.Add(new Users(Convert.ToString(r["UserType"])
                                                    , Convert.ToBoolean(r["IsDeleted"].ToString())
                                                ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return userslist;
        }
        public static List<Users> RetrieveMarketingExecutiveData()
        {
            List<Users> userslist = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted,Department,EnteringTime,LeavingTime,Target,SalesTarget from Users where  Isdeleted=0 and Department='Admin' or Department='Marketing'";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        userslist.Add(new Users(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , (Convert.IsDBNull(r["Dob"]) ? new DateTime() : Convert.ToDateTime(r["Dob"].ToString()))
                            , Convert.ToString(r["Mobile"].ToString())
                            , (Convert.IsDBNull(r["Doj"]) ? new DateTime() : Convert.ToDateTime(r["Doj"].ToString()))
                            , Convert.ToString(r["Adhar"].ToString())
                            , Convert.ToString(r["Esino"].ToString())
                            , Convert.ToString(r["Pfno"].ToString())
                            , Convert.ToString(r["Bankname"].ToString())
                            , Convert.ToString(r["Bankaccountno"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["Ifsc"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Password"].ToString())
                            , (r["Photo"]) != DBNull.Value ? ((byte[])r["Photo"]) : null
                            , (Convert.IsDBNull(r["Dol"]) ? new DateTime() : Convert.ToDateTime(r["Dol"].ToString()))
                            , Convert.ToString(r["Usertype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["Department"].ToString())
                              , (Convert.IsDBNull(r["EnteringTime"]) ? new DateTime() : Convert.ToDateTime(r["EnteringTime"].ToString()))
                            , (Convert.IsDBNull(r["LeavingTime"]) ? new DateTime() : Convert.ToDateTime(r["LeavingTime"].ToString()))
                            , (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"])
                            , (r["SalesTarget"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesTarget"])

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return userslist;
        }

        public static List<Users> RetrieveAllWithoutAdmin()
        {
            List<Users> userslist = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted,Department,EnteringTime,LeavingTime,Target,SalesTarget from Users where Isdeleted=0 and UserType!='Group Head' and UserType!='Admin'";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        userslist.Add(new Users(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , (Convert.IsDBNull(r["Dob"]) ? new DateTime() : Convert.ToDateTime(r["Dob"].ToString()))
                            , Convert.ToString(r["Mobile"].ToString())
                            , (Convert.IsDBNull(r["Doj"]) ? new DateTime() : Convert.ToDateTime(r["Doj"].ToString()))
                            , Convert.ToString(r["Adhar"].ToString())
                            , Convert.ToString(r["Esino"].ToString())
                            , Convert.ToString(r["Pfno"].ToString())
                            , Convert.ToString(r["Bankname"].ToString())
                            , Convert.ToString(r["Bankaccountno"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["Ifsc"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Password"].ToString())
                            , (r["Photo"]) != DBNull.Value ? ((byte[])r["Photo"]) : null
                            , (Convert.IsDBNull(r["Dol"]) ? new DateTime() : Convert.ToDateTime(r["Dol"].ToString()))
                            , Convert.ToString(r["Usertype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["Department"].ToString())
                              , (Convert.IsDBNull(r["EnteringTime"]) ? new DateTime() : Convert.ToDateTime(r["EnteringTime"].ToString()))
                            , (Convert.IsDBNull(r["LeavingTime"]) ? new DateTime() : Convert.ToDateTime(r["LeavingTime"].ToString()))
                            , (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"])
                            , (r["SalesTarget"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesTarget"])

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return userslist;
        }


        public static List<Users> RetrieveDeactivatedUsers()
        {
            List<Users> userslist = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted,Department,EnteringTime,LeavingTime,Target,SalesTarget from Users where Isdeleted=1";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        userslist.Add(new Users(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , (Convert.IsDBNull(r["Dob"]) ? new DateTime() : Convert.ToDateTime(r["Dob"].ToString()))
                            , Convert.ToString(r["Mobile"].ToString())
                            , (Convert.IsDBNull(r["Doj"]) ? new DateTime() : Convert.ToDateTime(r["Doj"].ToString()))
                            , Convert.ToString(r["Adhar"].ToString())
                            , Convert.ToString(r["Esino"].ToString())
                            , Convert.ToString(r["Pfno"].ToString())
                            , Convert.ToString(r["Bankname"].ToString())
                            , Convert.ToString(r["Bankaccountno"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["Ifsc"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Password"].ToString())
                            , (r["Photo"]) != DBNull.Value ? ((byte[])r["Photo"]) : null
                            , (Convert.IsDBNull(r["Dol"]) ? new DateTime() : Convert.ToDateTime(r["Dol"].ToString()))
                            , Convert.ToString(r["Usertype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["Department"].ToString())
                              , (Convert.IsDBNull(r["EnteringTime"]) ? new DateTime() : Convert.ToDateTime(r["EnteringTime"].ToString()))
                            , (Convert.IsDBNull(r["LeavingTime"]) ? new DateTime() : Convert.ToDateTime(r["LeavingTime"].ToString()))
                            , (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"])
                            , (r["SalesTarget"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesTarget"])

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return userslist;
        }


        public static Users RetrieveById(int Id)
        {
            Users result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted,Department,EnteringTime,LeavingTime,Target,SalesTarget from Users Where Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Users(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , (Convert.IsDBNull(r["Dob"]) ? new DateTime() : Convert.ToDateTime(r["Dob"].ToString()))
                            , Convert.ToString(r["Mobile"].ToString())
                            , (Convert.IsDBNull(r["Doj"]) ? new DateTime() : Convert.ToDateTime(r["Doj"].ToString()))
                            , Convert.ToString(r["Adhar"].ToString())
                            , Convert.ToString(r["Esino"].ToString())
                            , Convert.ToString(r["Pfno"].ToString())
                            , Convert.ToString(r["Bankname"].ToString())
                            , Convert.ToString(r["Bankaccountno"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["Ifsc"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Password"].ToString())
                            , (r["Photo"]) != DBNull.Value ? ((byte[])r["Photo"]) : null
                            , (Convert.IsDBNull(r["Dol"]) ? new DateTime() : Convert.ToDateTime(r["Dol"].ToString()))
                            , Convert.ToString(r["Usertype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["Department"].ToString())
                              , (Convert.IsDBNull(r["EnteringTime"]) ? new DateTime() : Convert.ToDateTime(r["EnteringTime"].ToString()))
                            , (Convert.IsDBNull(r["LeavingTime"]) ? new DateTime() : Convert.ToDateTime(r["LeavingTime"].ToString()))
                            , (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"])
                            , (r["SalesTarget"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesTarget"])

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

        public static Users RetrieveByEmail(string Email, string Password)
        {
            Users result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted,Department,EnteringTime,LeavingTime,Target,SalesTarget from Users Where Email = @Email and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Email", Email);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        if (r["Email"].ToString() == Email && r["Password"].ToString() == Password)
                        {
                            result = new Users(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , (Convert.IsDBNull(r["Dob"]) ? new DateTime() : Convert.ToDateTime(r["Dob"].ToString()))
                            , Convert.ToString(r["Mobile"].ToString())
                            , (Convert.IsDBNull(r["Doj"]) ? new DateTime() : Convert.ToDateTime(r["Doj"].ToString()))
                            , Convert.ToString(r["Adhar"].ToString())
                            , Convert.ToString(r["Esino"].ToString())
                            , Convert.ToString(r["Pfno"].ToString())
                            , Convert.ToString(r["Bankname"].ToString())
                            , Convert.ToString(r["Bankaccountno"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["Ifsc"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Password"].ToString())
                            , (r["Photo"]) != DBNull.Value ? ((byte[])r["Photo"]) : null
                            , (Convert.IsDBNull(r["Dol"]) ? new DateTime() : Convert.ToDateTime(r["Dol"].ToString()))
                            , Convert.ToString(r["Usertype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                              , Convert.ToString(r["Department"].ToString())
                              , (Convert.IsDBNull(r["EnteringTime"]) ? new DateTime() : Convert.ToDateTime(r["EnteringTime"].ToString()))
                            , (Convert.IsDBNull(r["LeavingTime"]) ? new DateTime() : Convert.ToDateTime(r["LeavingTime"].ToString()))
                            , (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"])
                            , (r["SalesTarget"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesTarget"])

                          );
                        }
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

        public static int RetrieveStaffIdByEmail(string Email)
        {
            int staffId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Name, Address, Location, State, Dob, Mobile, Doj, Adhar, Esino, Pfno, Bankname, Bankaccountno, Branch, Ifsc, Email, Password, Photo, Dol, Usertype, Isdeleted from Users Where Email = @Email and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Email", Email);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        staffId = r.GetInt32(0);
                    }
                    r.Close();
                    return staffId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static string RetrieveStaffUserTypeByEmail(string Email)
        {
            string userType = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Usertype, Isdeleted from Users Where Email = @Email and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Email", Email);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Users");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        userType = r.GetValue(0).ToString();
                    }
                    r.Close();
                    return userType;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public bool Update(byte[] Photo)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Users Set "
                        + "  Name = @Name "
                        + ", Address = @Address "
                        + ", Location = @Location "
                        + ", State = @State "
                        + ", Dob = @Dob "
                        + ", Mobile = @Mobile "
                        + ", Doj = @Doj "
                        + ", Adhar = @Adhar "
                        + ", Esino = @Esino "
                        + ", Pfno = @Pfno "
                        + ", Bankname = @Bankname "
                        + ", Bankaccountno = @Bankaccountno "
                        + ", Branch = @Branch "
                        + ", Ifsc = @Ifsc "
                        + ", Email = @Email "
                        + ", Photo = @Photo "
                        + ", Usertype = @Usertype "
                        + ", Department = @Department "
                        + ", EnteringTime = @EnteringTime "
                        + ", LeavingTime = @LeavingTime "
                        + ", Target = @Target "
                        + ", SalesTarget = @SalesTarget "
                        + ", Isdeleted = @Isdeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@Address", this.Address);
                    manageCommand.Parameters.AddWithValue("@Location", this.Location);
                    manageCommand.Parameters.AddWithValue("@State", this.State);
                    manageCommand.Parameters.AddWithValue("@Dob", this.Dob);
                    manageCommand.Parameters.AddWithValue("@Mobile", this.Mobile);
                    manageCommand.Parameters.AddWithValue("@Doj", this.Doj);
                    manageCommand.Parameters.AddWithValue("@Adhar", this.Adhar);
                    manageCommand.Parameters.AddWithValue("@Esino", this.Esino);
                    manageCommand.Parameters.AddWithValue("@Pfno", this.Pfno);
                    manageCommand.Parameters.AddWithValue("@Bankname", this.Bankname);
                    manageCommand.Parameters.AddWithValue("@Bankaccountno", this.Bankaccountno);
                    manageCommand.Parameters.AddWithValue("@Branch", this.Branch);
                    manageCommand.Parameters.AddWithValue("@Ifsc", this.Ifsc);
                    manageCommand.Parameters.AddWithValue("@Email", this.Email);
                    if (Photo == null)
                    {
                        manageCommand.Parameters.Add("@Photo", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        //manageCommand.Parameters.AddWithValue("@Picture", DBNull.Value);
                    }
                    else
                    {
                        manageCommand.Parameters.AddWithValue("@Photo", Photo);
                    }

                    manageCommand.Parameters.AddWithValue("@Usertype", this.Usertype);
                    manageCommand.Parameters.AddWithValue("@Department", this.Department);
                    manageCommand.Parameters.AddWithValue("@EnteringTime", this.EnteringTime);
                    manageCommand.Parameters.AddWithValue("@LeavingTime", this.LeavingTime);
                    manageCommand.Parameters.AddWithValue("@Target", this.Target);
                    manageCommand.Parameters.AddWithValue("@SalesTarget", this.SalesTarget);
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

        public bool UpdateWithOutPhoto()
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Users Set "
                        + "  Name = @Name "
                        + ", Address = @Address "
                        + ", Location = @Location "
                        + ", State = @State "
                        + ", Dob = @Dob "
                        + ", Mobile = @Mobile "
                        + ", Doj = @Doj "
                        + ", Adhar = @Adhar "
                        + ", Esino = @Esino "
                        + ", Pfno = @Pfno "
                        + ", Bankname = @Bankname "
                        + ", Bankaccountno = @Bankaccountno "
                        + ", Branch = @Branch "
                        + ", Ifsc = @Ifsc "
                        + ", Email = @Email "
                        + ", Usertype = @Usertype "
                          + ", Department = @Department "
                     + ", EnteringTime = @EnteringTime "
                        + ", LeavingTime = @LeavingTime "
                        + ", Target = @Target "
                        + ", SalesTarget = @SalesTarget "
                        + ", Isdeleted = @Isdeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@Address", this.Address);
                    manageCommand.Parameters.AddWithValue("@Location", this.Location);
                    manageCommand.Parameters.AddWithValue("@State", this.State);
                    manageCommand.Parameters.AddWithValue("@Dob", this.Dob);
                    manageCommand.Parameters.AddWithValue("@Mobile", this.Mobile);
                    manageCommand.Parameters.AddWithValue("@Doj", this.Doj);
                    manageCommand.Parameters.AddWithValue("@Adhar", this.Adhar);
                    manageCommand.Parameters.AddWithValue("@Esino", this.Esino);
                    manageCommand.Parameters.AddWithValue("@Pfno", this.Pfno);
                    manageCommand.Parameters.AddWithValue("@Bankname", this.Bankname);
                    manageCommand.Parameters.AddWithValue("@Bankaccountno", this.Bankaccountno);
                    manageCommand.Parameters.AddWithValue("@Branch", this.Branch);
                    manageCommand.Parameters.AddWithValue("@Ifsc", this.Ifsc);
                    manageCommand.Parameters.AddWithValue("@Email", this.Email);
                    manageCommand.Parameters.AddWithValue("@Usertype", this.Usertype);
                    manageCommand.Parameters.AddWithValue("@Department", this.Department);
                    manageCommand.Parameters.AddWithValue("@EnteringTime", this.EnteringTime);
                    manageCommand.Parameters.AddWithValue("@LeavingTime", this.LeavingTime);
                    manageCommand.Parameters.AddWithValue("@Target", this.Target);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", this.Isdeleted);
                    manageCommand.Parameters.AddWithValue("@SalesTarget", this.SalesTarget);

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

        public bool Update(int Id, string Password)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Users Set "
                        + "Password = @Password "
                        + "  where Id= @Id ";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    manageCommand.Parameters.AddWithValue("@Password", Password);
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
                string sqlStatement = "Update Users Set IsDeleted = 1 where Id = @Id ";
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

        public bool IsEmailExist(string Email)
        {
            try
            {
                string sqlStatement = "Select Email from Users where Email = @Email ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Email", Email);
                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();
                    SqlDataReader rd = manageCommand.ExecuteReader();
                    if (rd.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }


        public bool ActivateUsers()
        {
            try
            {
                string sqlStatement = "Update Users Set IsDeleted = 0,DOL=@DOL where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@DOL", DateTime.Now);
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

        public static int RetrieveTarget(int id)
        {
            int target = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select sum(C.SalesOrderTarget) as Target from Customer C,Users U where C.StaffId=U.Id and U.IsDeleted=0 and C.IsDeleted=0 and C.StaffID=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Customer");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        target = (r["Target"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Target"]);

                    }
                    r.Close();
                    return target;
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
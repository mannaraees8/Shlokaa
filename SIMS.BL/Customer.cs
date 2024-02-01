using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMS.BL
{

    public class Customer
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffId;
        private string _name;
        private string _mobile;
        private string _email;
        private string _vendercode;
        private string _contactPerson;
        private string _location;
        private string _state;
        private string _accountNo;
        private string _bankName;
        private string _ifsc;
        private string _branch;
        private string _address;
        private string _gstno;
        private string _password;
        private string _place;
        private string _marketingExecutive;
        private int _salesOrderTarget;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int StaffId { get { return _staffId; } set { _staffId = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        public string Mobile { get { return _mobile; } set { _mobile = value; } }

        public string Email { get { return _email; } set { _email = value; } }

        public string Vendercode { get { return _vendercode; } set { _vendercode = value; } }

        public string ContactPerson { get { return _contactPerson; } set { _contactPerson = value; } }

        public string Location { get { return _location; } set { _location = value; } }
        public string State { get { return _state; } set { _state = value; } }
        public string AccountNo { get { return _accountNo; } set { _accountNo = value; } }
        public string BankName { get { return _bankName; } set { _bankName = value; } }
        public string IFSC { get { return _ifsc; } set { _ifsc = value; } }

        public string Branch { get { return _branch; } set { _branch = value; } }
        public string Address { get { return _address; } set { _address = value; } }

        public string Gstno { get { return _gstno; } set { _gstno = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Place { get { return _place; } set { _place = value; } }
        public string MarketingExecutive { get { return _marketingExecutive; } set { _marketingExecutive = value; } }
        public int SalesOrderTarget { get { return _salesOrderTarget; } set { _salesOrderTarget = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Customer()
        {
            _id = 0;
            _staffId = 0;
            _name = "";
            _mobile = "";
            _email = "";
            _vendercode = "";
            _contactPerson = "";
            _location = "";
            _address = "";
            _gstno = "";
            _branch = "";
            _accountNo = "";
            _bankName = "";
            _state = "";
            _ifsc = "";
            _salesOrderTarget = 0;
            _marketingExecutive = "";
            _isdeleted = false;
        }


        public Customer(int id, string name, string mobile, string email, string vendercode, string ContactPerson, string location, string State, string address, string gstno,string AccountNo, string BankName, string Branch, string IFSC,int staffId,string marketingExecutive, int salesOrderTarget, bool isdeleted)
        {
            _id = id;
            _staffId = staffId;
            _name = name;
            _mobile = mobile;
            _email = email;
            _vendercode = vendercode;
            _contactPerson = ContactPerson;
            _location = location;
            _address = address;
            _gstno = gstno;
            _branch = Branch;
            _accountNo = AccountNo;
            _bankName = BankName;
            _state = State;
            _ifsc = IFSC;
            _marketingExecutive = marketingExecutive;
            _salesOrderTarget = salesOrderTarget;
            _isdeleted = isdeleted;
        }
        public Customer(string place)
        {         
            _place = place;
        }


        #endregion //CTOR

        #region CRUD
        public static int Create(string Name, string Mobile, string Email, string Vendercode, string ContactPerson, string Location, string State, string Address, string Gstno, string AccountNo, string BankName, string Branch, string IFSC, int StaffId, int salesOrderTarget, bool Isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Customer ( Name, Mobile, Email, Vendercode, ContactPerson, Location,State, Address, Gstno,AccountNo,BankName,Branch,IFSC,StaffId,SalesOrderTarget, Isdeleted ) "
                        + " values ( @Name, @Mobile, @Email, @Vendercode, @ContactPerson, @Location,@State, @Address, @Gstno,@AccountNo,@BankName,@Branch,@IFSC,@StaffId,@SalesOrderTarget, @Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Name", Name);
                    manageCommand.Parameters.AddWithValue("@Mobile", Mobile);
                    manageCommand.Parameters.AddWithValue("@Email", Email);
                    manageCommand.Parameters.AddWithValue("@Vendercode", Vendercode);
                    manageCommand.Parameters.AddWithValue("@ContactPerson", ContactPerson);
                    manageCommand.Parameters.AddWithValue("@Location", Location);
                    manageCommand.Parameters.AddWithValue("@State", State);
                    manageCommand.Parameters.AddWithValue("@Address", Address);
                    manageCommand.Parameters.AddWithValue("@Gstno", Gstno);
                    manageCommand.Parameters.AddWithValue("@AccountNo", AccountNo);
                    manageCommand.Parameters.AddWithValue("@BankName", BankName);
                    manageCommand.Parameters.AddWithValue("@Branch", Branch);
                    manageCommand.Parameters.AddWithValue("@IFSC", IFSC);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    manageCommand.Parameters.AddWithValue("@SalesOrderTarget", salesOrderTarget);
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

        public static List<Customer> RetrieveAll()
        {
            List<Customer> customerlist = new List<Customer>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select distinct C.Id,C.Name, C.Mobile,C.Email,C.Vendercode,C.ContactPerson,C.Location,C.State, C.Address,C.Gstno,C.AccountNo,C.BankName,C.Branch,C.IFSC,C.StaffId,C.SalesOrderTarget,C.Isdeleted,U.Name As StaffName from Customer C,Users U where C.StaffId=U.Id and C.IsDeleted=0 order by Name ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Customer");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        customerlist.Add(new Customer(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Mobile"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Vendercode"].ToString())
                            , Convert.ToString(r["ContactPerson"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Gstno"].ToString())
                            , Convert.ToString(r["AccountNo"].ToString())
                            , Convert.ToString(r["BankName"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["IFSC"].ToString())
                            , Convert.ToInt32(r["StaffId"])
                           , Convert.ToString(r["StaffName"].ToString())
                            , Convert.ToInt32(r["SalesOrderTarget"])
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return customerlist;
        }

        public static List<Customer> RetrieveAllLocation()
        {
            List<Customer> customerlist = new List<Customer>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select * from (Select distinct Location As PlaceVal from Customer where IsDeleted=0 UNION select distinct Place As PlaceVal from Place where IsDeleted=0) As A order by A.PlaceVal ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Customer");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        customerlist.Add(new Customer(Convert.ToString(r["PlaceVal"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return customerlist;
        }


        public static Customer RetrieveById(int Id)
        {
            Customer result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select C.Id,C.Name, C.Mobile,C.Email,C.Vendercode,C.ContactPerson,C.Location,C.State, C.Address,C.Gstno,C.AccountNo,C.BankName,C.Branch,C.IFSC,C.StaffId,C.SalesOrderTarget,C.Isdeleted,U.Name As StaffName from Customer C,Users U Where  C.StaffId=U.Id and C.IsDeleted=0 and C.Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Customer");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Customer(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Mobile"].ToString())
                            , Convert.ToString(r["Email"].ToString())
                            , Convert.ToString(r["Vendercode"].ToString())
                            , Convert.ToString(r["ContactPerson"].ToString())
                            , Convert.ToString(r["Location"].ToString())
                            , Convert.ToString(r["State"].ToString())
                            , Convert.ToString(r["Address"].ToString())
                            , Convert.ToString(r["Gstno"].ToString())
                            , Convert.ToString(r["AccountNo"].ToString())
                            , Convert.ToString(r["BankName"].ToString())
                            , Convert.ToString(r["Branch"].ToString())
                            , Convert.ToString(r["IFSC"].ToString())
                            , Convert.ToInt32(r["StaffId"])
                             , Convert.ToString(r["StaffName"].ToString())
                            , Convert.ToInt32(r["SalesOrderTarget"])
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
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

        public bool Update()
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Customer Set "
                        + "  Name = @Name "
                        + ", Mobile = @Mobile "
                        + ", Email = @Email "
                        + ", Vendercode = @Vendercode "
                        + ", ContactPerson = @ContactPerson "
                        + ", Location = @Location "
                         + ", State = @State "
                        + ", Address = @Address "
                        + ", Gstno = @Gstno "
                        + ", Isdeleted = @Isdeleted "
                        + ", AccountNo = @AccountNo "
                        + ", BankName = @BankName "
                        + ", Branch = @Branch "
                        + ", IFSC = @IFSC "
                        + ", StaffId = @StaffId "
                        + ", SalesOrderTarget = @SalesOrderTarget "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@Mobile", this.Mobile);
                    manageCommand.Parameters.AddWithValue("@Email", this.Email);
                    manageCommand.Parameters.AddWithValue("@Vendercode", this.Vendercode);
                    manageCommand.Parameters.AddWithValue("@ContactPerson", this.ContactPerson);
                    manageCommand.Parameters.AddWithValue("@Location", this.Location);
                    manageCommand.Parameters.AddWithValue("@State", this.State);
                    manageCommand.Parameters.AddWithValue("@Address", this.Address);
                    manageCommand.Parameters.AddWithValue("@Gstno", this.Gstno);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", this.Isdeleted);
                    manageCommand.Parameters.AddWithValue("@AccountNo", this.AccountNo);
                    manageCommand.Parameters.AddWithValue("@BankName", this.BankName);
                    manageCommand.Parameters.AddWithValue("@Branch", this.Branch);
                    manageCommand.Parameters.AddWithValue("@IFSC", this.IFSC);
                    manageCommand.Parameters.AddWithValue("@StaffId", this.StaffId);
                    manageCommand.Parameters.AddWithValue("@SalesOrderTarget", this.SalesOrderTarget);

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
                string sqlStatement = "Update Customer Set isDeleted = 1 where Id = @Id ";
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
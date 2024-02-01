using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMS.BL
{

    public class VisitLog
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private DateTime _datetime;
        private int _staffid;
        private int _month;
        private int _customerid;
        private string _collection;
        private string _orderstatus;
        private string _ordervalue;
        private int _salesorderid;
        private string _paymentmode;
        private string _amount;
        private string _reasonfornopayment;
        private string _remarks;
        private string _reasonfornoorder;
        private string _userName;
        private string _userType;
        private string _customerName;
        private int _orderStatusCount;

        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int OrderStatusCount { get { return _orderStatusCount; } set { _orderStatusCount = value; } }
        public int Month { get { return _month; } set { _month = value; } }

        public DateTime Datetime { get { return _datetime; } set { _datetime = value; } }

        public int Staffid { get { return _staffid; } set { _staffid = value; } }

        public int Customerid { get { return _customerid; } set { _customerid = value; } }

        public string Collection { get { return _collection; } set { _collection = value; } }

        public string Orderstatus { get { return _orderstatus; } set { _orderstatus = value; } }

        public string Ordervalue { get { return _ordervalue; } set { _ordervalue = value; } }

        public int Salesorderid { get { return _salesorderid; } set { _salesorderid = value; } }

        public string Paymentmode { get { return _paymentmode; } set { _paymentmode = value; } }

        public string Amount { get { return _amount; } set { _amount = value; } }

        public string Reasonfornopayment { get { return _reasonfornopayment; } set { _reasonfornopayment = value; } }

        public string Remarks { get { return _remarks; } set { _remarks = value; } }

        public string Reasonfornoorder { get { return _reasonfornoorder; } set { _reasonfornoorder = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string UserType { get { return _userType; } set { _userType = value; } }
        public string CustomerName { get { return _customerName; } set { _customerName = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public VisitLog()
        {
            _id = 0;
            _datetime = DateTime.Now;
            _staffid = 0;
            _customerid = 0;
            _orderstatus = "";
            _ordervalue = "";
            _salesorderid = 0;
            _paymentmode = "";
            _amount = "";
            _reasonfornopayment = "";
            _remarks = "";
            _reasonfornoorder = "";
            _isdeleted = false;
            _userName = "";
            _customerName = "";
            _userType = "";
            _orderStatusCount = 0;
        }
        public VisitLog(string amount, int month, string name)
        {
            _amount = amount;
            _month = month;
            _userName = name;
        }
        public VisitLog(string collection, string name)
        {
            _collection = collection;
            _userName = name;
        }
        public VisitLog(int id, DateTime datetime, int staffid, int customerid, string orderstatus, string ordervalue, string paymentmode, string amount, string reasonfornopayment, string remarks, string reasonfornoorder, string UserName, string CustomerName, string UserType, bool isdeleted)
        {
            _id = id;
            _datetime = datetime;
            _staffid = staffid;
            _customerid = customerid;
            _orderstatus = orderstatus;
            _ordervalue = ordervalue;
            _paymentmode = paymentmode;
            _amount = amount;
            _reasonfornopayment = reasonfornopayment;
            _remarks = remarks;
            _reasonfornoorder = reasonfornoorder;
            _isdeleted = isdeleted;
            _userName = UserName;
            _customerName = CustomerName;
            _userType = UserType;
        }

        public VisitLog(int count, string name, string orderStatus)
        {
            _orderStatusCount = count;
            _userName = name;
            _orderstatus = orderStatus;
        }
        public VisitLog(int count, string paymentmode)
        {
            _orderStatusCount = count;
            _paymentmode = paymentmode;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(DateTime Datetime, int Staffid, int Customerid, string Orderstatus, string Ordervalue, string Paymentmode, string Amount, string Reasonfornopayment, string Remarks, string Reasonfornoorder, bool Isdeleted)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement;

                    SqlCommand manageCommand;
                    sqlStatement = "Insert VisitLog ( Datetime, Staffid, Customerid, Orderstatus, Ordervalue, Paymentmode, Amount, Reasonfornopayment, Remarks, Reasonfornoorder, Isdeleted ) "
                        + " values ( @Datetime, @Staffid, @Customerid, @Orderstatus, @Ordervalue, @Paymentmode, @Amount, @Reasonfornopayment, @Remarks, @Reasonfornoorder, @Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Datetime", Datetime);
                    manageCommand.Parameters.AddWithValue("@Staffid", Staffid);
                    manageCommand.Parameters.AddWithValue("@Customerid", Customerid);
                    manageCommand.Parameters.AddWithValue("@Orderstatus", Orderstatus);
                    manageCommand.Parameters.AddWithValue("@Ordervalue", Ordervalue);
                    manageCommand.Parameters.AddWithValue("@Paymentmode", Paymentmode);
                    manageCommand.Parameters.AddWithValue("@Amount", Amount);
                    manageCommand.Parameters.AddWithValue("@Reasonfornopayment", Reasonfornopayment);
                    manageCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    manageCommand.Parameters.AddWithValue("@Reasonfornoorder", Reasonfornoorder);
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

        public static List<VisitLog> RetrieveAll()
        {
            List<VisitLog> visitloglist = new List<VisitLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select V.Id, V.Datetime, V.Staffid, V.Customerid, V.Orderstatus, V.Ordervalue, V.Paymentmode, V.Amount, V.Reasonfornopayment, V.Remarks, V.Reasonfornoorder, V.Isdeleted,U.Name As UserName,U.UserType,C.Name As CustomerName from VisitLog V,Users U,Customer C where V.Isdeleted=0 and U.Id=V.StaffId and C.Id=V.CustomerId order by C.Name desc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        visitloglist.Add(new VisitLog(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Datetime"]) ? new DateTime() : Convert.ToDateTime(r["Datetime"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToString(r["Orderstatus"].ToString())
                            , Convert.ToString(r["Ordervalue"].ToString())
                            , Convert.ToString(r["Paymentmode"].ToString())
                            , Convert.ToString(r["Amount"].ToString())
                            , Convert.ToString(r["Reasonfornopayment"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Reasonfornoorder"].ToString())
                             , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
                                                     , Convert.ToString(r["UserType"].ToString())
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
            return visitloglist;
        }

        public static List<VisitLog> RetrieveAllByUserId(int UserID)
        {
            List<VisitLog> visitloglist = new List<VisitLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select V.Id, V.Datetime, V.Staffid, V.Customerid, V.Orderstatus, V.Ordervalue, V.Paymentmode, V.Amount, V.Reasonfornopayment, V.Remarks, V.Reasonfornoorder, V.Isdeleted,U.Name As UserName,U.UserType,C.Name As CustomerName from VisitLog V,Users U,Customer C where V.Isdeleted=0 and U.Id=V.StaffId and C.Id=V.CustomerId and V.StaffId=@UserID order by C.Name desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@UserID", UserID);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        visitloglist.Add(new VisitLog(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Datetime"]) ? new DateTime() : Convert.ToDateTime(r["Datetime"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToString(r["Orderstatus"].ToString())
                            , Convert.ToString(r["Ordervalue"].ToString())
                            , Convert.ToString(r["Paymentmode"].ToString())
                            , Convert.ToString(r["Amount"].ToString())
                            , Convert.ToString(r["Reasonfornopayment"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Reasonfornoorder"].ToString())
                             , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
                                                     , Convert.ToString(r["UserType"].ToString())
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
            return visitloglist;
        }

        public static List<VisitLog> RetrieveByDate()
        {
            List<VisitLog> visitloglist = new List<VisitLog>();
            DateTime Date = DateTime.Today;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select V.Id, V.Datetime, V.Staffid, V.Customerid, V.Orderstatus, V.Ordervalue, V.Paymentmode, V.Amount, V.Reasonfornopayment, V.Remarks, V.Reasonfornoorder, V.Isdeleted,U.Name As UserName,U.UserType,C.Name As CustomerName from VisitLog V,Users U,Customer C where V.Isdeleted=0 and U.Id=V.StaffId and C.Id=V.CustomerId and V.Datetime=@Datetime order by C.Name desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Datetime", Date);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        visitloglist.Add(new VisitLog(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Datetime"]) ? new DateTime() : Convert.ToDateTime(r["Datetime"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToString(r["Orderstatus"].ToString())
                            , Convert.ToString(r["Ordervalue"].ToString())
                            , Convert.ToString(r["Paymentmode"].ToString())
                            , Convert.ToString(r["Amount"].ToString())
                            , Convert.ToString(r["Reasonfornopayment"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Reasonfornoorder"].ToString())
                             , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
                                                     , Convert.ToString(r["UserType"].ToString())
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
            return visitloglist;
        }
        public static VisitLog RetrieveById(int Id)
        {
            VisitLog result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select V.Id, V.Datetime, V.Staffid, V.Customerid, V.Orderstatus, V.Ordervalue, V.Paymentmode, V.Amount, V.Reasonfornopayment, V.Remarks, V.Reasonfornoorder, V.Isdeleted,U.Name As UserName,U.UserType,C.Name As CustomerName from VisitLog V,Users U,Customer C where V.Isdeleted=0 and U.Id=V.StaffId and C.Id=V.CustomerId and V.Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new VisitLog(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Datetime"]) ? new DateTime() : Convert.ToDateTime(r["Datetime"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToString(r["Orderstatus"].ToString())
                            , Convert.ToString(r["Ordervalue"].ToString())
                            , Convert.ToString(r["Paymentmode"].ToString())
                            , Convert.ToString(r["Amount"].ToString())
                            , Convert.ToString(r["Reasonfornopayment"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Reasonfornoorder"].ToString())
                             , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
                                                     , Convert.ToString(r["UserType"].ToString())
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sqlStatement;

                    SqlCommand manageCommand;
                    sqlStatement = "Update VisitLog Set "
                        + "  Datetime = @Datetime "
                        + ", Staffid = @Staffid "
                        + ", Customerid = @Customerid "
                        + ", Orderstatus = @Orderstatus "
                        + ", Ordervalue = @Ordervalue "
                        + ", Paymentmode = @Paymentmode "
                        + ", Amount = @Amount "
                        + ", Reasonfornopayment = @Reasonfornopayment "
                        + ", Remarks = @Remarks "
                        + ", Reasonfornoorder = @Reasonfornoorder "
                        + ", Isdeleted = @Isdeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Datetime", this.Datetime);
                    manageCommand.Parameters.AddWithValue("@Staffid", this.Staffid);
                    manageCommand.Parameters.AddWithValue("@Customerid", this.Customerid);
                    manageCommand.Parameters.AddWithValue("@Orderstatus", this.Orderstatus);
                    manageCommand.Parameters.AddWithValue("@Ordervalue", this.Ordervalue);
                    manageCommand.Parameters.AddWithValue("@Paymentmode", this.Paymentmode);
                    manageCommand.Parameters.AddWithValue("@Amount", this.Amount);
                    manageCommand.Parameters.AddWithValue("@Reasonfornopayment", this.Reasonfornopayment);
                    manageCommand.Parameters.AddWithValue("@Remarks", this.Remarks);
                    manageCommand.Parameters.AddWithValue("@Reasonfornoorder", this.Reasonfornoorder);
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


        public bool Delete()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Update VisitLog Set isDeleted = 1 where Id = @Id ";
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


        public static List<VisitLog> RetrieveOrderStatusChartDataById(int id, DateTime startDate, DateTime endDate)
        {
            List<VisitLog> visitLogList = new List<VisitLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "select count(V.OrderStatus) as Count,U.Name,V.OrderStatus from VisitLog V,Users U where V.StaffID=U.ID and V.StaffID=@Id and V.IsDeleted=0 and U.IsDeleted=0 and (FORMAT(V.DateTime,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.DateTime,'yyyy-MM-dd')<=@EndDate) group by V.OrderStatus,U.Name  order by V.OrderStatus desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", id);
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        visitLogList.Add(new VisitLog(Convert.ToInt32(r["Count"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["OrderStatus"].ToString())

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return visitLogList;
        }

        public static List<object> RetrievePaymentModeChartDataById(int id, DateTime startDate, DateTime endDate)
        {
            List<object> visitLogList = new List<object>();
            visitLogList.Insert(0, 0);
            visitLogList.Insert(1, 0);
            visitLogList.Insert(2, 0);
            visitLogList.Insert(3, 0);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "select count(V.PaymentMode) as Count,U.Name,V.PaymentMode from VisitLog V,Users U where V.StaffID=U.ID and V.StaffID=@Id and V.IsDeleted=0 and U.IsDeleted=0 and (FORMAT(V.DateTime,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.DateTime,'yyyy-MM-dd')<=@EndDate) group by V.PaymentMode,U.Name  order by V.PaymentMode asc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", id);
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        if (r["PaymentMode"].ToString() == "Cash")
                        {
                            visitLogList.Insert(0, Convert.ToInt32(r["Count"]));
                        }
                        if (r["PaymentMode"].ToString() == "Cheque")
                        {
                            visitLogList.Insert(1, Convert.ToInt32(r["Count"]));
                        }
                        if (r["PaymentMode"].ToString() == "NEFT")
                        {
                            visitLogList.Insert(2, Convert.ToInt32(r["Count"]));
                        }
                        if (r["PaymentMode"].ToString() == "No Payment")
                        {
                            visitLogList.Insert(3, Convert.ToInt32(r["Count"]));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return visitLogList;
        }



        public static List<VisitLog> RetrieveCollectionTrend(int id, DateTime startDate, DateTime endDate)
        {
            List<VisitLog> visitLogList = new List<VisitLog>();


            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (id == 0)
                    {
                        selectStatement = "select sum(CAST((V.Amount) as int)) as Amount,MONTH(V.DateTime) as Month  from VisitLog V,Users U where V.StaffId=U.Id and V.IsDeleted=0 and (FORMAT(V.DateTime,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.DateTime,'yyyy-MM-dd')<=@EndDate)  group by MONTH(V.DateTime) ";
                        manageCommand = new SqlCommand(selectStatement, connection);

                    }
                    else
                    {
                        selectStatement = "select sum(CAST((V.Amount) as int)) as Amount,MONTH(V.DateTime) as Month,U.Name  from VisitLog V,Users U where V.StaffId=U.Id and V.IsDeleted=0 and (FORMAT(V.DateTime,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.DateTime,'yyyy-MM-dd')<=@EndDate) and V.StaffID=@StaffId group by MONTH(V.DateTime),U.Name";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", id);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (id == 0)
                    {
                        while (r.Read())
                        {
                            visitLogList.Add(new VisitLog(Convert.ToString(r["Amount"])
                                , Convert.ToInt32(r["Month"])
                                , ""));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            visitLogList.Add(new VisitLog(Convert.ToString(r["Amount"])
                                , Convert.ToInt32(r["Month"])
                                , Convert.ToString(r["Name"])));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return visitLogList;
        }
        public static List<VisitLog> RetrieveCollection(int id, DateTime startDate, DateTime endDate)
        {
            List<VisitLog> visitLogList = new List<VisitLog>();


            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (id == 0)
                    {
                        selectStatement = "select (sum(CAST((V.Amount)AS float))/CAST((U.SalesTarget)AS float)) * 100 as Collection  from VisitLog V,Users U where V.StaffID=U.ID and U.SalesTarget>0 and (FORMAT(V.DateTime,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.DateTime,'yyyy-MM-dd')<=@EndDate) group by U.SalesTarget";
                        manageCommand = new SqlCommand(selectStatement, connection);

                    }
                    else
                    {
                        selectStatement = "select (sum(CAST((V.Amount)AS float) )/CAST((U.SalesTarget)AS float)) * 100 as Collection,U.Name  from VisitLog V,Users U where V.StaffID=U.ID and U.SalesTarget>0 and (FORMAT(V.DateTime,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.DateTime,'yyyy-MM-dd')<=@EndDate) and V.StaffID=@StaffId group by U.SalesTarget,U.Name";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", id);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VisitLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (id == 0)
                    {
                        while (r.Read())
                        {
                            visitLogList.Add(new VisitLog(Convert.ToString(r["Collection"])
                                , ""));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            visitLogList.Add(new VisitLog(Convert.ToString(r["Collection"])
                                , Convert.ToString(r["Name"])));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return visitLogList;
        }
        #endregion //CRUD

    }

}
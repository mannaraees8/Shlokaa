using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SIMS.BL
{

    public class SalesOrder
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _orderValue;
        private int _achieved;
        private int _pending;
        private int _salesOrderDetailsId;
        private DateTime _timestamp;
        //private int  _staffid;
        private int _customerId;
        private int _staffId;
        private int _itemId;
        private int _SizeId;
        private int _categoryId;
        private int _subCategoryId;
        private int _unitOfMeasurementId;
        private string _customerName;
        private string _staffName;
        private string _itemName;
        private string _Size;
        private string _categoryName;
        private string _subCategoryName;
        private string _unitOfMeasurement;
        private string _usertype;
        private int _quantity;
        //private string _amount;
        private string _paymentmode;
        //private string _pendingamount;
        private bool _isaddedtotally;
        private bool _isdeleted;
        private int _totalOrderValue;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int Achieved { get { return _achieved; } set { _achieved = value; } }
        public int Pending { get { return _pending; } set { _pending = value; } }
        public int SalesOrderDetailsId { get { return _salesOrderDetailsId; } set { _salesOrderDetailsId = value; } }

        public DateTime Timestamp { get { return _timestamp; } set { _timestamp = value; } }

        //public int  Staffid {get{return _staffid;} set{_staffid=value;}}

        public int Customerid { get { return _customerId; } set { _customerId = value; } }
        public int StaffId { get { return _staffId; } set { _staffId = value; } }
        public int ItemId { get { return _itemId; } set { _itemId = value; } }
        public int SizeId { get { return _SizeId; } set { _SizeId = value; } }
        public int OrderValue { get { return _orderValue; } set { _orderValue = value; } }
        public int CategoryId { get { return _categoryId; } set { _categoryId = value; } }
        public int SubCategoryId { get { return _subCategoryId; } set { _subCategoryId = value; } }
        public int UnitOfMeasurementId { get { return _unitOfMeasurementId; } set { _unitOfMeasurementId = value; } }
        public string CustomerName { get { return _customerName; } set { _customerName = value; } }
        public string StaffName { get { return _staffName; } set { _staffName = value; } }
        public string ItemName { get { return _itemName; } set { _itemName = value; } }
        public string UserType { get { return _usertype; } set { _usertype = value; } }
        public string Size { get { return _Size; } set { _Size = value; } }
        public string CategoryName { get { return _categoryName; } set { _categoryName = value; } }
        public string SubCategoryName { get { return _subCategoryName; } set { _subCategoryName = value; } }
        public string UnitOfMeasurement { get { return _unitOfMeasurement; } set { _unitOfMeasurement = value; } }
        public int Quantity { get { return _quantity; } set { _quantity = value; } }

        //public string Amount { get { return _amount; } set { _amount = value; } }

        public string Paymentmode { get { return _paymentmode; } set { _paymentmode = value; } }

        //public string Pendingamount { get { return _pendingamount; } set { _pendingamount = value; } }

        public bool Isaddedtotally { get { return _isaddedtotally; } set { _isaddedtotally = value; } }
        public int TotalOrderValue { get { return _totalOrderValue; } set { _totalOrderValue = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public SalesOrder()
        {
            _id = 0;
            _salesOrderDetailsId = 0;
            _timestamp = DateTime.Now;
            _totalOrderValue = 0;
            _customerId = 0;
            _itemId = 0;
            _SizeId = 0;
            _categoryId = 0;
            _quantity = 0;
            _subCategoryId = 0;
            _unitOfMeasurementId = 0;
            _customerName = "";
            _itemName = "";
            _Size = "";
            _categoryName = "";
            _subCategoryName = "";
            _unitOfMeasurement = "";
            _usertype = "";
            _isaddedtotally = false;
            _paymentmode = "";
            _isdeleted = false;
            _achieved = 0;
            _pending = 0;
            _orderValue = 0;
        }


        public SalesOrder(int id, int itemid, string customername, DateTime timestamp, string size, int quantity,int orderValue, string itemname, string category, string subcategory,int subcategoryid, int salesorderdetailsid)
        {
            _id = id;
            _itemId = itemid;
            _timestamp = timestamp;
            _customerName = customername;
            _timestamp = timestamp;
            _Size = size;
            _quantity = quantity;
            _orderValue = orderValue;
            _itemName = itemname;
            _categoryName = category;
            _subCategoryName = subcategory;
            _subCategoryId = subcategoryid;
            _salesOrderDetailsId = salesorderdetailsid;
            //_totalOrderValue = TotalOrderValue;



        }

        public SalesOrder(int id, int salesorderdetailsid, string customername, string staffName, DateTime timestamp, int itemId, string itemName, int sizeid, string size, int quantity,int orderValue, int categoryid, int subcategoryid, string userType, int ordervalue)
        {
            _id = id;
            _salesOrderDetailsId = salesorderdetailsid;
            _staffName = staffName;
            _timestamp = timestamp;
            _customerName = customername;
            _timestamp = timestamp;
            _itemId = itemId;
            _itemName = itemName;
            _SizeId = sizeid;
            _Size = size;
            _quantity = quantity;
            _categoryId = categoryid;
            _subCategoryId = subcategoryid;
            _totalOrderValue = ordervalue;
            _usertype = userType;
            _orderValue = orderValue;


        }
        public SalesOrder(int achieved, int pending, string staffName)
        {
            _achieved = achieved;
            _pending = pending;
            _staffName = staffName;
            //_isaddedtotally = Isaddedtotally;
            //_isdeleted = IsDeleted;
        }
        public SalesOrder(int StaffId, string staffName)
        {
            _staffId = StaffId;
            _staffName = staffName;

        }
        public SalesOrder(int id, string customername, DateTime timestamp, bool isaddedtotally, int StaffId, string StaffName, string userType, int totalordervalue)
        {
            _id = id;

            _timestamp = timestamp;
            _customerName = customername;
            _timestamp = timestamp;
            _isaddedtotally = isaddedtotally;
            _staffId = StaffId;
            _staffName = StaffName;
            _usertype = userType;
            _totalOrderValue = totalordervalue;


        }

        #endregion //CTOR

        #region CRUD
        public static int Create(int StaffId, DateTime timestamp, int customerId, string paymentmode, int totalordervalue/*string pendingamount,*/ )
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert SalesOrder (StaffId, Timestamp, Customerid, Paymentmode,TotalOrderValue, Isdeleted ) "
                        + " values ( @Staffid,@Timestamp, @Customerid, @Paymentmode,@TotalOrderValue, @Isdeleted )";
                    sqlStatement = sqlStatement + " SELECT SCOPE_IDENTITY();";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Timestamp", timestamp);
                    manageCommand.Parameters.AddWithValue("@Staffid", StaffId);
                    manageCommand.Parameters.AddWithValue("@Customerid", customerId);

                    manageCommand.Parameters.AddWithValue("@Paymentmode", paymentmode);
                    manageCommand.Parameters.AddWithValue("@TotalOrderValue", totalordervalue);
                    //manageCommand.Parameters.AddWithValue("@Pendingamount", Pendingamount);
                    //manageCommand.Parameters.AddWithValue("@Isaddedtotally", Isaddedtotally);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", 0);

                    connection.Open();

                    int SalesOrderId = Convert.ToInt32(manageCommand.ExecuteScalar());

                    return SalesOrderId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static List<SalesOrder> RetrieveAll()
        {
            List<SalesOrder> salesorderlist = new List<SalesOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "SELECT SalesOrder.ID,SalesOrder.StaffId,SalesOrder.TotalOrderValue,SalesOrder.TimeStamp,Customer.Name,SalesOrder.Isaddedtotally,Users.Name As StaffName,Users.UserType FROM SalesOrder INNER JOIN Customer ON SalesOrder.CustomerId=Customer.Id INNER JOIN Users on SalesOrder.StaffId=Users.Id Where SalesOrder.IsDeleted=0 order by SalesOrder.TimeStamp DESC";

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderlist.Add(new SalesOrder(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["Name"].ToString())
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToBoolean(r["Isaddedtotally"].ToString())
                            , Convert.ToInt32(r["StaffId"].ToString())
                            , Convert.ToString(r["StaffName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , (Convert.IsDBNull(r["TotalOrderValue"]) ? 0 : Convert.ToInt32(r["TotalOrderValue"]))

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderlist;
        }
        public static List<SalesOrder> RetrieveAllByStaffId(int userId)
        {
            List<SalesOrder> salesorderlist = new List<SalesOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "SELECT SalesOrder.ID,SalesOrder.StaffId,SalesOrder.TotalOrderValue,SalesOrder.TimeStamp,Customer.Name,SalesOrder.Isaddedtotally,Users.Name As StaffName,Users.UserType FROM SalesOrder INNER JOIN Customer ON SalesOrder.CustomerId=Customer.Id INNER JOIN Users on SalesOrder.StaffId=Users.Id Where SalesOrder.IsDeleted=0 and SalesOrder.StaffId=@StaffId order by SalesOrder.TimeStamp DESC";

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@StaffId", userId);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderlist.Add(new SalesOrder(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["Name"].ToString())
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToBoolean(r["Isaddedtotally"].ToString())
                            , Convert.ToInt32(r["StaffId"].ToString())
                            , Convert.ToString(r["StaffName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , (Convert.IsDBNull(r["TotalOrderValue"]) ? 0 : Convert.ToInt32(r["TotalOrderValue"]))

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderlist;
        }

        public static List<SalesOrder> RetrieveOnlyStaff(int staffId)
        {
            List<SalesOrder> salesorderlist = new List<SalesOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = " SELECT SalesOrder.ID,SalesOrder.StaffId,SalesOrder.TotalOrderValue,SalesOrder.TimeStamp,Customer.Name,SalesOrder.Isaddedtotally,Users.Name As StaffName,Users.UserType FROM SalesOrder INNER JOIN Customer ON SalesOrder.CustomerId=Customer.Id INNER JOIN Users on SalesOrder.StaffId=Users.Id Where SalesOrder.StaffId=@Id and SalesOrder.IsDeleted=0 ORDER BY SalesOrder.TimeStamp desc";

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@Id", staffId);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderlist.Add(new SalesOrder(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["Name"].ToString())
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToBoolean(r["Isaddedtotally"].ToString())
                            , Convert.ToInt32(r["StaffId"].ToString())
                            , Convert.ToString(r["StaffName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , (Convert.IsDBNull(r["TotalOrderValue"]) ? 0 : Convert.ToInt32(r["TotalOrderValue"]))


                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderlist;
        }
        public static SalesOrder RetrieveById(int Id)
        {
            SalesOrder result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "SELECT SalesOrder.Id as SalesOrderId,SalesOrder.TotalOrderValue,SalesOrderDetails.OrderValue,SalesOrderDetails.ID as SalesOrderDetailsId,Customer.Name,Users.Name as StaffName,SalesOrder.TimeStamp,Item.Id as ItemId,Item.Name as ItemName,SalesOrder.PaymentMode,ItemSize.Id as ItemSizeId,ItemSize.Size, SalesOrderDetails.Quantity,Categories.Id as CategoryId,SubCategories.Id as SubCategoryId,Users.UserType FROM ItemSize INNER JOIN SalesOrder INNER JOIN SalesOrderDetails ON SalesOrder.ID = SalesOrderDetails.SalesOrderId ON ItemSize.Id = SalesOrderDetails.SizeId INNER JOIN Categories ON SalesOrderDetails.CategoryId = Categories.ID INNER JOIN Customer ON SalesOrder.CustomerId=Customer.ID INNER JOIN SubCategories ON SubCategories.Id=SalesOrderDetails.SubCategoryId INNER JOIN Users ON Users.Id=SalesOrder.StaffId INNER JOIN Item On Item.Id=SalesOrderDetails.ItemId where SalesOrder.Id=@Id and SalesOrder.IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ID", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new SalesOrder(Convert.ToInt32(r["SalesOrderId"])
                         , Convert.ToInt32(r["SalesOrderDetailsId"])
                        , Convert.ToString(r["Name"])
                        , Convert.ToString(r["StaffName"])
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToInt32(r["ItemId"])
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToInt32(r["ItemSizeId"])
                            , Convert.ToString(r["Size"])
                            , Convert.ToInt32(r["Quantity"].ToString())
                            , Convert.ToInt32(r["OrderValue"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToInt32(r["SubCategoryId"])
                            , Convert.ToString(r["UserType"].ToString())
                            , (Convert.IsDBNull(r["TotalOrderValue"]) ? 0 : Convert.ToInt32(r["TotalOrderValue"]))


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

        public static SalesOrder RetrieveOrderDetailsById(int Id)
        {
            SalesOrder result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "SELECT SalesOrder.Id as SalesOrderId,SalesOrder.TotalOrderValue,SalesOrderDetails.OrderValue,SalesOrderDetails.ID as SalesOrderDetailsId,Customer.Name,Users.Name as StaffName,SalesOrder.TimeStamp,Item.Id as ItemId,Item.Name as ItemName,SalesOrder.PaymentMode,ItemSize.Id as ItemSizeId,ItemSize.Size, SalesOrderDetails.Quantity,Categories.Id as CategoryId,SubCategories.Id as SubCategoryId,Users.UserType FROM ItemSize INNER JOIN SalesOrder INNER JOIN SalesOrderDetails ON SalesOrder.ID = SalesOrderDetails.SalesOrderId ON ItemSize.ID = SalesOrderDetails.SizeId INNER JOIN Categories ON SalesOrderDetails.CategoryId = Categories.ID INNER JOIN Customer ON SalesOrder.CustomerId=Customer.ID INNER JOIN SubCategories ON SubCategories.Id=SalesOrderDetails.SubCategoryId INNER JOIN Users ON Users.Id=SalesOrder.StaffId INNER JOIN Item On Item.Id=SalesOrderDetails.ItemId where SalesOrderDetails.SalesOrderId=@Id and SalesOrder.IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ID", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new SalesOrder(Convert.ToInt32(r["SalesOrderId"])
                         , Convert.ToInt32(r["SalesOrderDetailsId"])
                        , Convert.ToString(r["Name"])
                        , Convert.ToString(r["StaffName"])
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToInt32(r["ItemId"])
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToInt32(r["ItemSizeId"])
                            , Convert.ToString(r["Size"].ToString())
                            , Convert.ToInt32(r["Quantity"].ToString())
                            , Convert.ToInt32(r["OrderValue"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToInt32(r["SubCategoryId"])
                            , Convert.ToString(r["UserType"].ToString())
                            , (Convert.IsDBNull(r["TotalOrderValue"]) ? 0 : Convert.ToInt32(r["TotalOrderValue"]))

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
        public static List<SalesOrder> RetrieveAllDetails(int Id)
        {
            List<SalesOrder> salesorderlist = new List<SalesOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = " select distinct S.Id  as SalesOrderId,SD.Id as SalesOrderDetailsId,S.TimeStamp,S.PaymentMode,ISize.Size, SD.Quantity,SD.OrderValue,I.Name as ItemName,I.ID as ItemId,Cat.Name as CategoryName,Sub.Name as SubCategoryName,Sub.Id as SubCategoryId From Item I,SalesOrder S,SalesOrderDetails SD,ItemSize ISize,Categories Cat,SubCategories Sub,ProductSize P where Sd.SalesOrderId=S.ID and SD.SubCategoryId=Sub.Id and SD.SizeId=ISize.ID and SD.CategoryId=Cat.ID and SD.ItemId=I.ID and SD.SalesOrderId=@Id  ";


                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@ID", Id);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderlist.Add(new SalesOrder(Convert.ToInt32(r["SalesOrderId"]),
                            Convert.ToInt32(r["ItemId"])
                            , Convert.ToString(r.GetValue(1).ToString())
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToString(r["Size"].ToString())
                            , Convert.ToInt32(r["Quantity"].ToString())
                             , Convert.ToInt32(r["OrderValue"].ToString())
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CategoryName"].ToString())
                            , Convert.ToString(r["SubCategoryName"].ToString())
                            , Convert.ToInt32(r["SubCategoryId"])
                            , Convert.ToInt32(r["SalesOrderDetailsId"])

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderlist;
        }
        public bool Update(int Id)
        {
            try
            {
                string sqlStatement;
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand manageCommand;
                sqlStatement = "Update SalesOrderDetails Set "
                    + " ItemId = @ItemId "
                    + ", SizeId = @SizeId "
                    + ", CategoryId = @CategoryId "
                    + ", SubCategoryId = @SubCategoryId "
                    + ", Quantity = @Quantity "
                   + ", OrderValue = @OrderValue "
                    + "  where Id= @Id";
                manageCommand = new SqlCommand(sqlStatement, connection);
                manageCommand.Parameters.AddWithValue("@Id", this.SalesOrderDetailsId);
                manageCommand.Parameters.AddWithValue("@ItemId", this.ItemId);
                manageCommand.Parameters.AddWithValue("@SizeId", this.SizeId);
                manageCommand.Parameters.AddWithValue("@CategoryId", this.CategoryId);
                manageCommand.Parameters.AddWithValue("@SubCategoryId", this.SubCategoryId);
                manageCommand.Parameters.AddWithValue("@Quantity", this.Quantity);
                manageCommand.Parameters.AddWithValue("@OrderValue", this.OrderValue);
                connection.Open();
                int count = manageCommand.ExecuteNonQuery();

                if (count > 0)
                {
                    sqlStatement = "Update SalesOrder Set TotalOrderValue = @TotalOrderValue where Id=@Id";

                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    manageCommand.Parameters.AddWithValue("@TotalOrderValue", this.TotalOrderValue);
                    count = manageCommand.ExecuteNonQuery();
                }
                return (count > 0 ? true : false);
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
                string sqlStatement = "Delete from SalesOrder where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();
                    manageCommand.ExecuteNonQuery();

                    sqlStatement = "Delete from SalesOrderDetails where SalesOrderId = @Id ";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    int count = manageCommand.ExecuteNonQuery();
                    return (count > 0 ? true : false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }
        public int DeleteOrderDetails(int Id, int salesorderdetailsid)
        {
            try
            {
                string sqlStatement = "Select Count(*) from SalesOrderDetails Where SalesOrderId=@Id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    int count = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (count == 1)
                    {
                        sqlStatement = "Delete from SalesOrderDetails where Id = @Id ";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", salesorderdetailsid);
                        manageCommand.ExecuteNonQuery();

                        sqlStatement = "Delete from SalesOrder where Id = @Id ";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", Id);
                        manageCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        sqlStatement = "Delete from SalesOrderDetails where Id = @Id ";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", salesorderdetailsid);
                        manageCommand.ExecuteNonQuery();
                    }


                    return count;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static bool UpdateIsAddedToTally(int Id, bool Isaddedtotally)
        {

            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update SalesOrder Set "
                        + "  Isaddedtotally = @Isaddedtotally where ID=@Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    manageCommand.Parameters.AddWithValue("@Isaddedtotally", Isaddedtotally);
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



        public static List<SalesOrder> RetrieveAllChartTallyData(int id, DateTime startDate, DateTime endDate)
        {
            List<SalesOrder> salesorderlist = new List<SalesOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //string selectStatement = "select sum(S.TotalOrderValue) as Amount,(select sum(C.SalesOrderTarget) from Customer C,Users U where C.StaffId=U.ID and C.StaffId=@Id) as TargetAmount,(case when sum(S.TotalOrderValue) >= (select sum(C.SalesOrderTarget) from Customer C, Users U where C.StaffId = U.ID and C.StaffId = @Id) then sum(S.TotalOrderValue)-(select sum(C.SalesOrderTarget) from Customer C,Users U where C.StaffId = U.ID and C.StaffId = @Id)when sum(S.TotalOrderValue)< (select sum(C.SalesOrderTarget) from Customer C,Users U where C.StaffId = U.ID and C.StaffId =@Id) then 0 end) as Extra,U.Name   from SalesOrder S,Users U where S.StaffID = U.ID and S.StaffId =@Id and S.IsDeleted = 0 and U.IsDeleted = 0 and (FORMAT(S.TimeStamp,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.TimeStamp,'yyyy-MM-dd')<=@EndDate)  group by U.Name ";
                    string selectStatement = "select sum(S.TotalOrderValue) as Achieved,U.Name," +
                                            "Case " +
                                            "when((select sum(C.SalesOrderTarget) from Customer C, Users U where C.StaffId = U.ID and C.StaffId = @Id))> sum(S.TotalOrderValue) then((select sum(C.SalesOrderTarget) from Customer C, Users U where C.StaffId = U.ID and C.StaffId = @Id) - sum(S.TotalOrderValue))" +
                                            " when sum(S.TotalOrderValue)> ((select sum(C.SalesOrderTarget) from Customer C,Users U where C.StaffId = U.ID and C.StaffId = @Id)) then 0" +
                                            " end as Pending" +
                                            " from SalesOrder S,Users U where S.StaffID = U.ID and S.StaffId = @Id and S.IsDeleted = 0 and U.IsDeleted = 0" +
                                            " and (FORMAT(S.TimeStamp, 'yyyy-MM-dd') >= @StartDate AND FORMAT(S.TimeStamp, 'yyyy-MM-dd') <= @EndDate)  group by U.Name ";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    manageCommand.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderlist.Add(new SalesOrder(Convert.ToInt32(r["Achieved"])
                            , (r["Pending"]) == DBNull.Value ? 0 : Convert.ToInt32(r["Pending"])
                          , Convert.ToString(r["Name"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderlist;
        }
        public static List<SalesOrder> RetrieveTallyChartUserList()
        {
            List<SalesOrder> salesorderlist = new List<SalesOrder>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "select distinct U.Id,U.Name from SalesOrder S,Users U where S.StaffId=U.Id and U.IsDeleted=0 and S.IsDeleted=0 order by U.Name Asc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderlist.Add(new SalesOrder(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderlist;
        }
        public static int TotalTallyPandingCount()
        {
            int tallyCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "select count(IsAddedToTally) as Count from SalesOrder where IsAddedToTally=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    { 
                        tallyCount = Convert.ToInt32(r["Count"]);
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return tallyCount;
        }
        public static int TotalTallyPandingCountById(int userId)
        {
            int tallyCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "select count(IsAddedToTally) as Count from SalesOrder where IsAddedToTally=0 and StaffId=@StaffId";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", userId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrder");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        tallyCount = Convert.ToInt32(r["Count"]);
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return tallyCount;
        }

        #endregion //CRUD

    }

}
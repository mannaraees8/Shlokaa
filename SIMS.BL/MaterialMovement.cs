using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMS.BL
{

    public class MaterialMovement
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private DateTime _timestamp;
        private int _staffid;
        private int _customerid;
        private int _itemid;
        private string _invoiceno;
        private DateTime? _invoicedate;
        private int _unitId;
        private string _unitName;
        private string _itemSize;
        private string _quantity;
        private string _invoiceamount;
        private string _remarks;
        private string _movementtype;
        private bool _isdeleted;

        private string _searchText;
        private string _searchType;
        private string _username;
        private string _userType;
        private string _customerName;
        private string _itemName;


        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public DateTime Timestamp { get { return _timestamp; } set { _timestamp = value; } }

        public int Staffid { get { return _staffid; } set { _staffid = value; } }

        public int Customerid { get { return _customerid; } set { _customerid = value; } }

        public int Itemid { get { return _itemid; } set { _itemid = value; } }
        public string ItemSize { get { return _itemSize; } set { _itemSize = value; } }

        public string Invoiceno { get { return _invoiceno; } set { _invoiceno = value; } }

        public DateTime? Invoicedate { get { return _invoicedate; } set { _invoicedate = value; } }

        public int UnitId { get { return _unitId; } set { _unitId = value; } }
        public string UnitName { get { return _unitName; } set { _unitName = value; } }

        public string Quantity { get { return _quantity; } set { _quantity = value; } }

        public string Invoiceamount { get { return _invoiceamount; } set { _invoiceamount = value; } }

        public string Remarks { get { return _remarks; } set { _remarks = value; } }

        public string Movementtype { get { return _movementtype; } set { _movementtype = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        public string SearchText { get { return _searchText; } set { _searchText = value; } }

        public string SearchType { get { return _searchType; } set { _searchType = value; } }

        public string UserName { get { return _username; } set { _username = value; } }
        public string UserType { get { return _userType; } set { _userType = value; } }
        public string CustomerName { get { return _customerName; } set { _customerName = value; } }
        public string ItemName { get { return _itemName; } set { _itemName = value; } }

        #endregion //Props

        #region CTOR

        public MaterialMovement()
        {
            _userType = "";
            _id = 0;
            _timestamp = DateTime.Now;
            _staffid = 0;
            _customerid = 0;
            _itemid = 0;
            _itemSize = "";
            _invoiceno = "";
            _invoicedate = null;
            _unitId = 0;
            _unitName = "";
            _quantity = "";
            _invoiceamount = "";
            _remarks = "";
            _movementtype = "";
            _isdeleted = false;
            _itemName = "";
            _username = "";
            _customerName = "";
            _searchText = "";
            _searchType = "";
        }

        public MaterialMovement(int id, DateTime timestamp, int staffid, int customerid, int itemid, string size, string invoiceno, DateTime? invoicedate, int unitId,string unitName, string quantity, string invoiceamount, string remarks, string movementtype, bool isdeleted, string UserName, string ItemName, string CustomerName, string UserType)
        {
            _id = id;
            _timestamp = timestamp;
            _staffid = staffid;
            _customerid = customerid;
            _itemid = itemid;
            _itemSize = size;
            _invoiceno = invoiceno;
            _invoicedate = invoicedate;
            _unitId = unitId;
            _unitName = unitName;
            _quantity = quantity;
            _invoiceamount = invoiceamount;
            _remarks = remarks;
            _movementtype = movementtype;
            _isdeleted = isdeleted;
            _itemName = ItemName;
            _username = UserName;
            _customerName = CustomerName;
            _userType = UserType;
        }



        #endregion //CTOR

        #region CRUD
        public static int Create(DateTime Timestamp, int Staffid, int Customerid, int Itemid, string Invoiceno, DateTime? Invoicedate, int UnitId, string Quantity, string Invoiceamount, string Remarks, string Movementtype, bool Isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert MaterialMovement ( Timestamp, Staffid, Customerid, Itemid, Invoiceno, Invoicedate, UnitId, Quantity, Invoiceamount, Remarks, Movementtype, IsDeleted ) "
                        + " values ( @Timestamp, @Staffid, @Customerid, @Itemid, @Invoiceno, @Invoicedate, @UnitId, @Quantity, @Invoiceamount, @Remarks, @Movementtype, @IsDeleted)";
                    sqlStatement = sqlStatement + " SELECT SCOPE_IDENTITY();";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Timestamp", Timestamp);
                    manageCommand.Parameters.AddWithValue("@Staffid", Staffid);
                    manageCommand.Parameters.AddWithValue("@Customerid", Customerid);
                    manageCommand.Parameters.AddWithValue("@Itemid", Itemid);
                    manageCommand.Parameters.AddWithValue("@Invoiceno", Invoiceno);
                    manageCommand.Parameters.AddWithValue("@Invoicedate", Invoicedate);
                    manageCommand.Parameters.AddWithValue("@UnitId", UnitId);
                    manageCommand.Parameters.AddWithValue("@Quantity", Quantity);
                    manageCommand.Parameters.AddWithValue("@Invoiceamount", Invoiceamount);
                    manageCommand.Parameters.AddWithValue("@Remarks", Remarks);
                    manageCommand.Parameters.AddWithValue("@Movementtype", Movementtype);
                    manageCommand.Parameters.AddWithValue("@IsDeleted", 0);

                    connection.Open();
                    int count = Convert.ToInt32(manageCommand.ExecuteScalar());
                    return count;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }
        public static int AddItemSize(int MaterialMovementId, int Itemid, string Size)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert MaterialMovementItemSizes ( MaterialMovementId, ItemId, Size) "
                        + " values ( @MaterialMovementId, @ItemId, @Size)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@MaterialMovementId", MaterialMovementId);
                    manageCommand.Parameters.AddWithValue("@ItemId", Itemid);
                    manageCommand.Parameters.AddWithValue("@Size", Size);
         
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

        public static List<MaterialMovement> RetrieveAll()
        {
            List<MaterialMovement> materialmovementlist = new List<MaterialMovement>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select M.Id, M.Timestamp, M.Staffid, MIS.Size, M.Customerid, M.Itemid, M.Invoiceno, M.Invoicedate, M.UnitId, M.Quantity, M.Invoiceamount, M.Remarks, M.Movementtype, M.Isdeleted,U.Name AS UserName,U.UserType,C.Name AS CustomerName,I.Name AS ItemName,UN.UnitOfMeasurement from MaterialMovement M, MaterialMovementItemSizes MIS, Users U,Customer C, Item I,UnitOfMeasurement UN where M.Isdeleted = 0 and U.ID = M.StaffID and C.ID = M.CustomerID and I.ID = M.ItemID and M.UnitId=UN.Id and M.Id=MIS.MaterialMovementId order by M.Timestamp desc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "MaterialMovement");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        materialmovementlist.Add(new MaterialMovement(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToInt32(r["Itemid"].ToString())
                            , Convert.ToString(r["Size"].ToString())
                            , Convert.ToString(r["Invoiceno"].ToString())
                            , (Convert.IsDBNull(r["Invoicedate"]) ? new DateTime() : Convert.ToDateTime(r["Invoicedate"].ToString()))
                            , Convert.ToInt32(r["UnitId"].ToString())
                           , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToString(r["Quantity"].ToString())
                            , Convert.ToString(r["Invoiceamount"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Movementtype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
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
            return materialmovementlist;
        }

        public static List<MaterialMovement> RetrieveAllByUserId(int StaffId)
        {
            List<MaterialMovement> materialmovementlist = new List<MaterialMovement>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select M.Id, M.Timestamp, M.Staffid, M.Customerid, M.Itemid, M.Invoiceno, M.Invoicedate, M.UnitId, M.Quantity, M.Invoiceamount, M.Remarks, M.Movementtype, M.Isdeleted,U.Name AS UserName,U.UserType,C.Name AS CustomerName,I.Name AS ItemName,UN.UnitOfMeasurement from MaterialMovement M, Users U,Customer C, Item I,UnitOfMeasurement UN where M.Isdeleted = 0 and U.ID = M.StaffID and C.ID = M.CustomerID and I.ID = M.ItemID and M.UnitId=UN.Id and M.StaffId=@StaffId order by M.Timestamp desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "MaterialMovement");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        materialmovementlist.Add(new MaterialMovement(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToInt32(r["Itemid"].ToString())
                            , ""
                            , Convert.ToString(r["Invoiceno"].ToString())
                            , (Convert.IsDBNull(r["Invoicedate"]) ? new DateTime() : Convert.ToDateTime(r["Invoicedate"].ToString()))
                            , Convert.ToInt32(r["UnitId"].ToString())
                           , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToString(r["Quantity"].ToString())
                            , Convert.ToString(r["Invoiceamount"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Movementtype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
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
            return materialmovementlist;
        }



        public static MaterialMovement RetrieveById(int Id)
        {
            MaterialMovement result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select M.Id, M.Timestamp, M.Staffid, M.Customerid, M.Itemid, M.Invoiceno, M.Invoicedate, M.UnitId, M.Quantity, M.Invoiceamount, M.Remarks, M.Movementtype, M.Isdeleted,U.Name AS UserName,U.UserType,C.Name AS CustomerName,I.Name AS ItemName,UN.UnitOfmeasurement from MaterialMovement M, Users U,Customer C, Item I,UnitOfmeasurement UN where M.Isdeleted = 0 and U.ID = M.StaffID and C.ID = M.CustomerID and I.ID = M.ItemID and M.UnitId=UN.ID and M.Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "MaterialMovement");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new MaterialMovement(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString()))
                            , Convert.ToInt32(r["Staffid"].ToString())
                            , Convert.ToInt32(r["Customerid"].ToString())
                            , Convert.ToInt32(r["Itemid"].ToString())
                            ,""
                            , Convert.ToString(r["Invoiceno"].ToString())
                            , (Convert.IsDBNull(r["Invoicedate"]) ? new DateTime() : Convert.ToDateTime(r["Invoicedate"].ToString()))
                            , Convert.ToInt32(r["UnitId"].ToString())
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToString(r["Quantity"].ToString())
                            , Convert.ToString(r["Invoiceamount"].ToString())
                            , Convert.ToString(r["Remarks"].ToString())
                            , Convert.ToString(r["Movementtype"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToString(r["UserName"].ToString())
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CustomerName"].ToString())
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





        public bool Update()
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update MaterialMovement Set "
                        + "  Timestamp = @Timestamp "
                        + ", StaffID = @Staffid "
                        + ", Customerid = @Customerid "
                        + ", Itemid = @Itemid "
                        + ", Invoiceno = @Invoiceno "
                        + ", Invoicedate = @Invoicedate "
                        + ", UnitId = @UnitId "
                        + ", Quantity = @Quantity "
                        + ", Invoiceamount = @Invoiceamount "
                        + ", Remarks = @Remarks "
                        + ", Movementtype = @Movementtype "
                        + ", Isdeleted = @Isdeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Timestamp", this.Timestamp);
                    manageCommand.Parameters.AddWithValue("@Staffid", this.Staffid);
                    manageCommand.Parameters.AddWithValue("@Customerid", this.Customerid);
                    manageCommand.Parameters.AddWithValue("@Itemid", this.Itemid);
                    manageCommand.Parameters.AddWithValue("@Invoiceno", this.Invoiceno);
                    manageCommand.Parameters.AddWithValue("@Invoicedate", this.Invoicedate);
                    manageCommand.Parameters.AddWithValue("@UnitId", this.UnitId);
                    manageCommand.Parameters.AddWithValue("@Quantity", this.Quantity);
                    manageCommand.Parameters.AddWithValue("@Invoiceamount", this.Invoiceamount);
                    manageCommand.Parameters.AddWithValue("@Remarks", this.Remarks);
                    manageCommand.Parameters.AddWithValue("@Movementtype", this.Movementtype);
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
                string sqlStatement = "Update MaterialMovement Set Isdeleted = 1 where Id = @Id ";
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
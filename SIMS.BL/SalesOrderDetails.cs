using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SIMS.BL
{

    public class SalesOrderDetails
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _salesorderid;
        private int _itemid;
        private int _sizeid;
        private int _unitofmeasurementid;
        private int _categoryid;
        private int _subcategoryid;
        private int _quantity;
        private int _orderValue;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public int SalesorderId { get { return _salesorderid; } set { _salesorderid = value; } }

        public int ItemId { get { return _itemid; } set { _itemid = value; } }

        public int SizeId { get { return _sizeid; } set { _sizeid = value; } }

        public int UnitOfMeasurementId { get { return _unitofmeasurementid; } set { _unitofmeasurementid = value; } }

        public int CategoryId { get { return _categoryid; } set { _categoryid = value; } }

        public int SubCategoryId { get { return _subcategoryid; } set { _subcategoryid = value; } }

        public int Quantity { get { return _quantity; } set { _quantity = value; } }

        public int OrderValue { get { return _orderValue; } set { _orderValue = value; } }
        #endregion //Props

        #region CTOR

        public SalesOrderDetails()
        {
            _id = 0;
            _salesorderid = 0;
            _itemid = 0;
            _sizeid = 0;
            _unitofmeasurementid = 0;
            _categoryid = 0;
            _subcategoryid = 0;
            _quantity = 0;
            _orderValue = 0;
        }


        public SalesOrderDetails(int id, int salesorderid, int categoryid, int subcategoryid, int itemid,int sizeid, int quantity,int OrderValue)
        {
            _id = id;
            _salesorderid = salesorderid;
            _itemid = itemid;
            _sizeid = sizeid;
            _categoryid = categoryid;
            _subcategoryid = subcategoryid;
            _quantity = quantity;
            _orderValue = OrderValue;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(int Salesorderid, int Categoryid, int Subcategoryid, int Itemid,int sizeid, int Quantity, int OrderValue)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Insert SalesOrderDetails ( SalesOrderId, ItemId, SizeId, CategoryId, SubCategoryId, Quantity,OrderValue) "
                        + " values ( @SalesOrderId, @ItemId, @SizeId,  @CategoryId, @SubCategoryId, @Quantity,@OrderValue )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@SalesOrderId", Salesorderid);
                    manageCommand.Parameters.AddWithValue("@ItemId", Itemid);
                    manageCommand.Parameters.AddWithValue("@SizeId", sizeid);                
                    manageCommand.Parameters.AddWithValue("@CategoryId", Categoryid);
                    manageCommand.Parameters.AddWithValue("@SubCategoryId", Subcategoryid);
                    manageCommand.Parameters.AddWithValue("@Quantity", Quantity);
                    manageCommand.Parameters.AddWithValue("@OrderValue", OrderValue);
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

        public static List<SalesOrderDetails> RetrieveAll()
        {
            List<SalesOrderDetails> salesorderdetailslist = new List<SalesOrderDetails>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, SalesOrderId,CategoryId,SubCategoryId, ItemId, SizeId, Quantity,OrderValue from SalesOrderDetails where IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrderDetails");

                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        salesorderdetailslist.Add(new SalesOrderDetails(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["SalesOrderId"].ToString())
                            , Convert.ToInt32(r["CategoryId"].ToString())
                            , Convert.ToInt32(r["SubCategoryId"].ToString())
                            , Convert.ToInt32(r["Itemid"].ToString())
                            , Convert.ToInt32(r["SizeId"].ToString())
                            , Convert.ToInt32(r["Quantity"].ToString())
                           , Convert.ToInt32(r["OrderValue"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesorderdetailslist;
        }


        public static SalesOrderDetails RetrieveById(int Id)
        {
            SalesOrderDetails result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, SalesOrderId,CategoryId,SubCategoryId, ItemId, SizeId,Quantity,OrderValue from SalesOrderDetails Where Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SalesOrderDetails");

                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new SalesOrderDetails(Convert.ToInt32(r["Id"])
                         , Convert.ToInt32(r["SalesOrderId"].ToString())
                            , Convert.ToInt32(r["CategoryId"].ToString())
                            , Convert.ToInt32(r["SubCategoryId"].ToString())
                            , Convert.ToInt32(r["Itemid"].ToString())
                            , Convert.ToInt32(r["SizeId"].ToString())
                            , Convert.ToInt32(r["Quantity"].ToString())
                         , Convert.ToInt32(r["OrderValue"].ToString())
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
                    sqlStatement = "Update SalesOrderDetails Set "
                        + "  SalesorderId = @SalesorderId "
                        + "  CategoryId = @CategoryId "
                        + "  SubCategoryId = @SubCategoryId "
                        + ", ItemId = @ItemId "
                        + ", SizeId = @SizeId "
                        + ", Quantity = @Quantity "
                        + ", OrderValue = @OrderValue "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Salesorderid", this.SalesorderId);
                    manageCommand.Parameters.AddWithValue("@CategoryId", this.CategoryId);
                    manageCommand.Parameters.AddWithValue("@Salesorderid", this.SalesorderId);
                    manageCommand.Parameters.AddWithValue("@SubCategoryId", this.SubCategoryId);
                    manageCommand.Parameters.AddWithValue("@Itemid", this.ItemId);
                    manageCommand.Parameters.AddWithValue("@Size", this.SizeId);
                    manageCommand.Parameters.AddWithValue("@Quantity", this.Quantity);
                    manageCommand.Parameters.AddWithValue("@OrderValue", this.OrderValue);
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
                    string sqlStatement = "Update SalesOrderDetails Set isDeleted = 1 where Id = @Id ";

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMS.BL
{
    public class ProductSize
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _sizeId;
        private string _size;
        private string _unitofmeasurement;
        private int _itemId;

        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int SizeId { get { return _sizeId; } set { _sizeId = value; } }
        public string Size { get { return _size; } set { _size = value; } }
        public int ItemId { get { return _itemId; } set { _itemId = value; } }
        public string UnitOfMeasurement { get { return _unitofmeasurement; } set { _unitofmeasurement = value; } }


        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        public ProductSize()
        {
            _id = 0;
            _sizeId = 0;
            _size = "";
            _itemId = 0;

        }
        public ProductSize(int sizeid, string size)
        {
            _sizeId = sizeid;
            _size = size;

        }
        public ProductSize(int sizeid, string size, string unitofmeasurement)
        {
            _sizeId = sizeid;
            _size = size;
            _unitofmeasurement = unitofmeasurement;

        }
        #endregion

        #region CRUD

        public static int Create(int itemId, int sizeId)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert ProductSize ( ItemId, SizeId) "
                        + " values ( @ItemId, @SizeId )";

                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ItemId", itemId);
                    manageCommand.Parameters.AddWithValue("@SizeId", sizeId);

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

        public static List<ProductSize> RetrieveById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<ProductSize> productSizeList = new List<ProductSize>();
                string sqlStatement = "SELECT ItemSize.ID as SizeId, ItemSize.Size, UnitOfMeasurement.UnitOfMeasurement FROM ItemSize INNER JOIN ProductSize ON ItemSize.ID = ProductSize.SizeId  INNER JOIN UnitOfMeasurement ON ItemSize.UnitOfMeasurementId = UnitOfMeasurement.ID AND ProductSize.ItemId = @Id order by isnumeric(ItemSize.Size) desc, case when isnumeric(ItemSize.Size) = 1 then cast(ItemSize.Size as float) else null end, ItemSize.Size";
                SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                DataSet sqlDataset = new DataSet();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlStatement, connection);
                sqlAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlAdapter.Fill(sqlDataset, "ProductSize");
                DataTableReader r = sqlDataset.CreateDataReader();
                while (r.Read())
                {
                    productSizeList.Add(new ProductSize(Convert.ToInt32(r["SizeId"])
                     , Convert.ToString(r["Size"].ToString())
                     , Convert.ToString(r["UnitOfMeasurement"].ToString())));
                }


                return productSizeList;
            }
        }
        public static List<ProductSize> RetrievebymaterialMovementId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<ProductSize> productSizeList = new List<ProductSize>();
                string sqlStatement = "Select Id,Size from MaterialMovementItemSizes where MaterialMovementId=@Id order by isnumeric(Size) desc, case when isnumeric(Size) = 1 then cast(Size as float) else null end, Size";
                SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                DataSet sqlDataset = new DataSet();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlStatement, connection);
                sqlAdapter.SelectCommand.Parameters.AddWithValue("Id", id);
                sqlAdapter.Fill(sqlDataset, "ProductSize");
                DataTableReader r = sqlDataset.CreateDataReader();
                while (r.Read())
                {
                    productSizeList.Add(new ProductSize(Convert.ToInt32(r["Id"])
                     , Convert.ToString(r["Size"].ToString())));
                }
                return productSizeList;
            }
        }
        public static List<ProductSize> RetrieveAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<ProductSize> productSizeList = new List<ProductSize>();
                string sqlStatement = "Select ProductSize.SizeId,ItemSize.Size from ProductSize INNER JOIN ItemSize ON ProductSize.SizeId=ItemSize.ID order by isnumeric(ItemSize.Size) desc, case when isnumeric(ItemSize.Size) = 1 then cast(ItemSize.Size as float) else null end, ItemSize.Size";
                SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                DataSet sqlDataset = new DataSet();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlStatement, connection);

                sqlAdapter.Fill(sqlDataset, "ProductSize");
                DataTableReader r = sqlDataset.CreateDataReader();
                while (r.Read())
                {
                    productSizeList.Add(new ProductSize(Convert.ToInt32(r["SizeId"])
                     , Convert.ToString(r["Size"].ToString())));
                }

            
                return productSizeList;
            }
        }

        public static bool Delete(int itemId)
        {

            bool status = true;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement;

                    sqlStatement = "Delete from ProductSize where ItemId = @Id ";
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", itemId);
                    connection.Open();
                    manageCommand.ExecuteNonQuery();
                    status = false;
                    return status;

                }

            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static bool DeleteMaterialMovementItemSize(int Id)
        {

            bool status = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement;

                    sqlStatement = "Delete from MaterialMovementItemSizes where MaterialMovementId = @Id ";
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    manageCommand.ExecuteNonQuery();
                    status = false;
                    return status;

                }

            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }
    }
}
#endregion
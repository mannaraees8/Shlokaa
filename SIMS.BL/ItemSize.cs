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
    public class ItemSize
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _size;
        private string _unitOfMeasurement;
        private int _unitOfMeasurementId;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public string Size { get { return _size; } set { _size = value; } }

        public int UnitOfMeasurementId { get { return _unitOfMeasurementId; } set { _unitOfMeasurementId = value; } }
        public string UnitOfMeasurement { get { return _unitOfMeasurement; } set { _unitOfMeasurement = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        public ItemSize()
        {
            _id = 0;
            _size = "";
            _unitOfMeasurementId = 0;
            _unitOfMeasurement = "";
            _isdeleted = false;
        }


        public ItemSize(int Id, string Size, string UnitOfMeasurement, int UnitOfMeasurementid, bool Isdeleted)
        {
            _id = Id;
            _size = Size;
            _unitOfMeasurement = UnitOfMeasurement;
            _unitOfMeasurementId = UnitOfMeasurementid;
            _isdeleted = false;
        }
        public ItemSize(int Id, string Size, bool Isdeleted)
        {
            _id = Id;
            _size = Size;

            _isdeleted = false;
        }


        #region CRUD
        public static int Create(string Size, int UnitOfMeasurementId)
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from ItemSize where Size=@Size and UnitOfMeasurementId=@UnitOfMeasurementId and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Size", Size);
                    manageCommand.Parameters.AddWithValue("@UnitOfMeasurementId", UnitOfMeasurementId);
                    connection.Open();
                    int size = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (size <= 0)
                    {

                        sqlStatement = "Insert ItemSize ( Size, UnitOfMeasurementId ) "
                        + " values ( @Size, @UnitOfMeasurementId)";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Size", Size);
                        manageCommand.Parameters.AddWithValue("@UnitOfMeasurementId", UnitOfMeasurementId);

                        count = manageCommand.ExecuteNonQuery();
                        return count;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public static List<ItemSize> RetrieveAll()
        {
            List<ItemSize> itemSizelist = new List<ItemSize>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select ItemSize.ID,ItemSize.Size,UnitOfMeasurement.UnitOfMeasurement,ItemSize.UnitOfMeasurementId, ItemSize.Isdeleted from ItemSize INNER JOIN UnitOfMeasurement ON ItemSize.UnitOfMeasurementId=UnitOfMeasurement.Id where ItemSize.IsDeleted=0 order by isnumeric(ItemSize.Size) desc, case when isnumeric(ItemSize.Size) = 1 then cast(ItemSize.Size as float) else null end, ItemSize.Size";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "ItemSize");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        itemSizelist.Add(new ItemSize(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Size"].ToString())
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToInt32(r["UnitOfMeasurementId"])
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

            return itemSizelist;
        }

        public static List<ItemSize> RetrieveByUnitOfMeasurementId(int UnitOfMeasurementId)
        {
            List<ItemSize> itemSizelist = new List<ItemSize>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select ItemSize.ID,ItemSize.Size,UnitOfMeasurement.UnitOfMeasurement,ItemSize.UnitOfMeasurementId, ItemSize.Isdeleted from ItemSize INNER JOIN UnitOfMeasurement ON ItemSize.UnitOfMeasurementId=UnitOfMeasurement.Id where UnitOfMeasurementId=@UnitOfMeasurementId and ItemSize.IsDeleted=0 order by isnumeric(ItemSize.Size) desc, case when isnumeric(ItemSize.Size) = 1 then cast(ItemSize.Size as float) else null end, ItemSize.Size";


                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@UnitOfMeasurementId", UnitOfMeasurementId);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "ItemSize");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        itemSizelist.Add(new ItemSize(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["Size"].ToString())
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToInt32(r["UnitOfMeasurementId"])
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

            return itemSizelist;
        }

        public static List<ItemSize> RetrieveByItemId(int Id)
        {
            List<ItemSize> itemSizelist = new List<ItemSize>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select ProductSize.SizeID,ItemSize.Size,ProductSize.Isdeleted from ProductSize INNER JOIN ItemSize ON ProductSize.SizeId=ItemSize.Id WHERE ProductSize.ItemId=@Id and ProductSize.IsDeleted=0 order by isnumeric(ItemSize.Size) desc, case when isnumeric(ItemSize.Size) = 1 then cast(ItemSize.Size as float) else null end, ItemSize.Size";


                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@Id", Id);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "ItemSize");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        itemSizelist.Add(new ItemSize(Convert.ToInt32(r["SizeID"])
                            , Convert.ToString(r["Size"].ToString())
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

            return itemSizelist;
        }
        public static ItemSize RetrieveById(int Id)
        {
            ItemSize result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select ItemSize.ID,ItemSize.Size,UnitOfMeasurement.UnitOfMeasurement,ItemSize.UnitOfMeasurementId, ItemSize.Isdeleted from ItemSize INNER JOIN UnitOfMeasurement ON ItemSize.UnitOfMeasurementId=UnitOfMeasurement.Id  where ItemSize.ID=@Id and  ItemSize.IsDeleted=0 ";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Categories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new ItemSize(Convert.ToInt32(r["Id"])
                             , Convert.ToString(r["Size"].ToString())
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToInt32(r["UnitOfMeasurementId"])
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
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from ItemSize where Size=@Size and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Size", this.Size);
                    connection.Open();
                    int size = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (size <= 0)
                    {

                        sqlStatement = "Update ItemSize Set "
                        + "  Size = @Size "
                        + ", UnitOfMeasurementId = @UnitOfMeasurementId "
                        + ", Isdeleted = @Isdeleted "
                        + "  where ID= @Id";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", this.Id);
                        manageCommand.Parameters.AddWithValue("@Size", this.Size);
                        manageCommand.Parameters.AddWithValue("@UnitOfMeasurementId", this.UnitOfMeasurementId);
                        manageCommand.Parameters.AddWithValue("@Isdeleted", this.Isdeleted);
                        count = manageCommand.ExecuteNonQuery();

                        return (count > 0 ? true : false);
                    }
                    else
                    {
                        return (count > 0 ? true : false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
        }

        public bool Delete()
        {
            int SizeCount = 0;
            bool status = true;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Select count(*) from ProductSize where SizeId=@Id and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();

                    SizeCount = Convert.ToInt32(manageCommand.ExecuteScalar());        

                    if (SizeCount == 0)
                    {
                        sqlStatement = "Update ItemSize Set IsDeleted = 1 where Id = @Id ";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", this.Id);
                        manageCommand.ExecuteNonQuery();
                        status = false;
                        return status;
                    }
                    else
                    {
                        return status;
                    }


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

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
    public class UnitOfMeasurement
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _unitOfMeasurement;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public string UnitOfMeasurementName { get { return _unitOfMeasurement; } set { _unitOfMeasurement = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        public UnitOfMeasurement()
        {
            _id = 0;
            _unitOfMeasurement = "";
            _isdeleted = false;
        }


        public UnitOfMeasurement(int Id, string UnitOfMeasurement, bool Isdeleted)
        {
            _id = Id;
            _unitOfMeasurement = UnitOfMeasurement;
            _isdeleted = false;
        }


        #region CRUD
        public static int Create(string UnitOfMeasurement)
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from UnitOfMeasurement where UnitOfMeasurement=@UnitOfMeasurement and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@UnitOfMeasurement", UnitOfMeasurement);
                    connection.Open();
                    int unitOfMeasurement = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (unitOfMeasurement <= 0)
                    {

                        sqlStatement = "Insert UnitOfMeasurement (UnitOfMeasurement,IsDeleted ) "
                        + " values (@UnitOfMeasurement,0)";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@UnitOfMeasurement", UnitOfMeasurement);
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

        public static List<UnitOfMeasurement> RetrieveAll()
        {
            List<UnitOfMeasurement> UnitOfMeasurementList = new List<UnitOfMeasurement>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select ID,UnitOfMeasurement, IsDeleted from UnitOfMeasurement where IsDeleted=0 ORDER BY UnitOfMeasurement ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "UnitOfMeasurement");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        UnitOfMeasurementList.Add(new UnitOfMeasurement(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
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
            return UnitOfMeasurementList;
        }


        public static UnitOfMeasurement RetrieveById(int Id)
        {
            UnitOfMeasurement result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select ID,UnitOfMeasurement, IsDeleted from UnitOfMeasurement where ID=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SubCategories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new UnitOfMeasurement(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["UnitOfMeasurement"]).ToString()
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

                    sqlStatement = "Select count(*) from UnitOfMeasurement where UnitOfMeasurement=@UnitOfMeasurement and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@UnitOfMeasurement", this.UnitOfMeasurementName);
                    connection.Open();
                    int unitOfMeasurement = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (unitOfMeasurement <= 0)
                    {

                        sqlStatement = "Update UnitOfMeasurement Set "
                    + "  UnitOfMeasurement = @UnitOfMeasurement "
                    + ", IsDeleted = @Isdeleted "
                    + "  where ID= @Id";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", this.Id);
                        manageCommand.Parameters.AddWithValue("@UnitOfMeasurement", this.UnitOfMeasurementName);
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
            bool status = true;
            int unit = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Select count(*) from ItemSize where UnitOfMeasurementId=@Id and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();

                    unit = Convert.ToInt32(manageCommand.ExecuteScalar());

                    if (unit == 0)
                    {
                        sqlStatement = "Update UnitOfMeasurement Set IsDeleted = 1 where Id = @Id ";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", this.Id);
                        connection.Open();
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

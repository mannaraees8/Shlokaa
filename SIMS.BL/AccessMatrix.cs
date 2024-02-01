using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SIMS.BL
{
    public class AccessMatrix
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        //private int _staffId;
        private string _moduleName;
        //private string _staffName;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        //public int StaffId { get { return _staffId; } set { _staffId = value; } }
        public string ModuleName { get { return _moduleName; } set { _moduleName = value; } }
        //public string StaffName { get { return _staffName; } set { _staffName = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public AccessMatrix()
        {
            _id = 0;
            //_staffId = 0;
            _moduleName = "";
            //_staffName = "";
            _isdeleted = false;
        }


        public AccessMatrix(int id, string moduleName, bool isdeleted)
        {
            _id = id;
            //_staffId = staffId;
            //_staffName = staffName;
            _moduleName = moduleName;
            _isdeleted = isdeleted;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(string moduleName, bool Isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert AccessMatrix (ModuleName, Isdeleted ) "
                        + " values (@ModuleName, @Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    //manageCommand.Parameters.AddWithValue("@StaffId", staffId);
                    manageCommand.Parameters.AddWithValue("@ModuleName", moduleName);
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

        public static List<AccessMatrix> RetrieveAll()
        {
            List<AccessMatrix> accessMatrixList = new List<AccessMatrix>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AM.Id, AM.ModuleName, AM.Isdeleted from AccessMatrix AM Where Am.Isdeleted=0 and AM.ModuleName!='Accountant' ORDER BY AM.ModuleName ASC ";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrix");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        accessMatrixList.Add(new AccessMatrix(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["ModuleName"].ToString())
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
            return accessMatrixList;
        }


        public static AccessMatrix RetrieveById(int Id)
        {
            AccessMatrix result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AM.Id, AM.ModuleName, AM.Isdeleted from AccessMatrix AM Where Am.Isdeleted=0 and AM.Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrix");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new AccessMatrix(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["ModuleName"].ToString())
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
        public static AccessMatrix RetrieveByModuleName(string moduleName)
        {
            AccessMatrix result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AM.Id, AM.ModuleName, AM.Isdeleted from AccessMatrix AM Where Am.Isdeleted=0 and AM.ModuleName = @ModuleName";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ModuleName", moduleName);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrix");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new AccessMatrix(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["ModuleName"].ToString())
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
                    sqlStatement = "Update AccessMatrix set ModuleName=@ModuleName,IsDeleted=@IsDeleted"
                                 + " where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    //manageCommand.Parameters.AddWithValue("@StaffId", this.StaffId);
                    manageCommand.Parameters.AddWithValue("@ModuleName", this.ModuleName);
                    manageCommand.Parameters.AddWithValue("@IsDeleted", this.Isdeleted);
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
            string sqlStatement = "";
            SqlCommand manageCommand = new SqlCommand();
            try
            {
                sqlStatement = "Update AccessMatrix Set isDeleted = 1 where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();

                    sqlStatement = "Update AccessMatrixDetails Set IsRowDeleted = 1 where AccessMatrixId = @Id ";

                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.ExecuteNonQuery();
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

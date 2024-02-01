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
    public class AccessMatrixDetails
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _accessmatrixid;
        private string _modulename;
        private string _usertype;
        private bool _isIndex;
        private bool _isCreate;
        private bool _isEdit;
        private bool _isUpdate;
        private bool _isDetails;
        private bool _isTally;
        private bool _isSearch;
        private bool _isExport;
        private bool _isdeleted;
        private bool _isRowDeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int AccessMatrixId { get { return _accessmatrixid; } set { _accessmatrixid = value; } }
        public string ModuleName { get { return _modulename; } set { _modulename = value; } }
        public string UserType { get { return _usertype; } set { _usertype = value; } }
        public bool IsIndex { get { return _isIndex; } set { _isIndex = value; } }
        public bool IsCreate { get { return _isCreate; } set { _isCreate = value; } }
        public bool IsEdit { get { return _isEdit; } set { _isEdit = value; } }
        public bool IsDetails { get { return _isDetails; } set { _isDetails = value; } }
        public bool IsTally { get { return _isTally; } set { _isTally = value; } }
        public bool IsUpdate { get { return _isUpdate; } set { _isUpdate = value; } }
        public bool IsSearch { get { return _isSearch; } set { _isSearch = value; } }
        public bool IsExport { get { return _isExport; } set { _isExport = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }
        public bool IsRowDeleted { get { return _isRowDeleted; } set { _isRowDeleted = value; } }
        #endregion //Props

        #region CTOR

        public AccessMatrixDetails()
        {
            _id = 0;
            _accessmatrixid = 0;
            _modulename = "";
            _usertype = "";
            _isIndex = false;
            _isCreate = false;
            _isEdit = false;
            _isUpdate = false;
            _isTally = false;
            _isDetails = false;
            _isSearch = false;
            _isExport = false;
            _isdeleted = false;
            _isRowDeleted = false;
        }


        public AccessMatrixDetails(int id, int accessMatrixid, string moduleName, string userType, bool isIndex, bool isCreate, bool isEdit, bool isDetails, bool isTally, bool isUpdate, bool isSearch, bool isExport, bool isdeleted, bool isRowDeleted)
        {
            _id = id;
            _accessmatrixid = accessMatrixid;
            _modulename = moduleName;
            _usertype = userType;
            _isIndex = isIndex;
            _isCreate = isCreate;
            _isEdit = isEdit;
            _isDetails = isDetails;
            _isTally = isTally;
            _isUpdate = isUpdate;
            _isSearch = isSearch;
            _isExport = isExport;
            _isdeleted = isdeleted;
            _isRowDeleted = isRowDeleted;
        }

        #endregion //CTOR

        #region CRUD

        public static int Create(int accessMatrixid, string userType, bool isIndex, bool isCreate, bool isEdit, bool isDetails, bool isTally, bool isUpdate, bool isSearch, bool isExport, bool isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert AccessMatrixDetails (AccessMatrixId,UserType,IsIndex,IsCreate,IsEdit,IsDetails,IsTally,IsUpdate,IsSearch,IsExport,Isdeleted) "
                        + " values(@AccessMatrixId,@UserType,@IsIndex,@IsCreate,@IsEdit,@IsDetails,@IsTally,@IsUpdate,@IsSearch,@IsExport,@Isdeleted)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@AccessMatrixId", accessMatrixid);
                    manageCommand.Parameters.AddWithValue("@userType", userType);
                    manageCommand.Parameters.AddWithValue("@IsIndex", isIndex);
                    manageCommand.Parameters.AddWithValue("@IsCreate", isCreate);
                    manageCommand.Parameters.AddWithValue("@IsEdit", isEdit);
                    manageCommand.Parameters.AddWithValue("@IsDetails", isDetails);
                    manageCommand.Parameters.AddWithValue("@IsTally", isTally);
                    manageCommand.Parameters.AddWithValue("@IsUpdate", isUpdate);
                    manageCommand.Parameters.AddWithValue("@IsSearch", isSearch);
                    manageCommand.Parameters.AddWithValue("@IsExport", isExport);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", isdeleted);

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

        public static List<AccessMatrixDetails> RetrieveAll()
        {
            List<AccessMatrixDetails> accessMatrixDetailsLsit = new List<AccessMatrixDetails>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AMD.Id,AMD.AccessMatrixId,AMD.UserType,AMD.IsIndex,AMD.IsCreate,AMD.IsEdit,AMD.IsDetails,AMD.IsTally,AMD.IsUpdate,AMD.IsSearch,AMD.IsExport,AMD.Isdeleted,AMD.IsRowDeleted,AM.ModuleName from AccessMatrixDetails AMD,AccessMatrix AM where AMD.AccessMatrixId=AM.Id and AMD.IsRowDeleted=0 and AM.ModuleName!='Accountant'";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrixDetails");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        accessMatrixDetailsLsit.Add(new AccessMatrixDetails(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["AccessMatrixId"])
                            , Convert.ToString(r["ModuleName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , Convert.ToBoolean(r["IsIndex"].ToString())
                            , Convert.ToBoolean(r["IsCreate"].ToString())
                            , Convert.ToBoolean(r["IsEdit"].ToString())
                            , Convert.ToBoolean(r["IsDetails"].ToString())
                            , Convert.ToBoolean(r["IsTally"].ToString())
                            , Convert.ToBoolean(r["IsUpdate"].ToString())
                            , Convert.ToBoolean(r["IsSearch"].ToString())
                            , Convert.ToBoolean(r["IsExport"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToBoolean(r["IsRowDeleted"].ToString())

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return accessMatrixDetailsLsit;
        }



        public static AccessMatrixDetails RetrieveById(int Id)
        {
            AccessMatrixDetails result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AMD.ID,AMD.AccessMatrixId,AMD.UserType,AMD.IsIndex,AMD.IsCreate,AMD.IsEdit,AMD.IsDetails,AMD.IsTally,AMD.IsUpdate,AMD.IsSearch,AMD.IsExport,AMD.Isdeleted,AMD.IsRowDeleted,AM.ModuleName from AccessMatrixDetails AMD,AccessMatrix AM where AMD.AccessMatrixId=AM.Id and AMD.IsRowDeleted=0 and AMD.Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrixDetails");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new AccessMatrixDetails(Convert.ToInt32(r["Id"])
                              , Convert.ToInt32(r["AccessMatrixId"])
                                , Convert.ToString(r["ModuleName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , Convert.ToBoolean(r["IsIndex"].ToString())
                            , Convert.ToBoolean(r["IsCreate"].ToString())
                            , Convert.ToBoolean(r["IsEdit"].ToString())
                            , Convert.ToBoolean(r["IsDetails"].ToString())
                            , Convert.ToBoolean(r["IsTally"].ToString())
                            , Convert.ToBoolean(r["IsUpdate"].ToString())
                            , Convert.ToBoolean(r["IsSearch"].ToString())
                            , Convert.ToBoolean(r["IsExport"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToBoolean(r["IsRowDeleted"].ToString())
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
        //public static AccessMatrixDetails RetrieveAccessByModuleId(int userId, int moduleId)
        //{
        //    AccessMatrixDetails result = null;
        //    try
        //    {

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            string selectStatement = "Select AMD.ID,AMD.AccessMatrixId,AMD.IsIndex,AMD.IsCreate,AMD.IsEdit,AMD.IsDetails,AMD.IsTally,AMD.IsUpdate,AMD.IsSearch,AMD.IsExport,AMD.Isdeleted,AMD.StaffId,U.Name,AMD.IsRowDeleted,AM.ModuleName,U.UserType from AccessMatrixDetails AMD,Users U,AccessMatrix AM where AMD.AccessMatrixId=AM.Id and AMD.StaffId=U.Id and AMD.IsRowDeleted=0 and AMD.AccessMatrixId = @AccessMatrixId and AMD.StaffId=@StaffId";
        //            SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
        //            manageCommand.Parameters.AddWithValue("@AccessMatrixId", moduleId);
        //            manageCommand.Parameters.AddWithValue("@StaffId", userId);
        //            connection.Open();
        //            SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
        //            DataSet sqlDataset = new DataSet();
        //            sqlAdapter.Fill(sqlDataset, "AccessMatrixDetails");
        //            DataTableReader r = sqlDataset.CreateDataReader();
        //            if (r.Read())
        //            {
        //                result = new AccessMatrixDetails(Convert.ToInt32(r["Id"])
        //                    , Convert.ToInt32(r["AccessMatrixId"])
        //                    , Convert.ToString(r["ModuleName"].ToString())
        //                    , Convert.ToInt32(r["StaffId"])
        //                    , Convert.ToString(r["Name"].ToString())
        //                    , Convert.ToString(r["UserType"].ToString())
        //                    , Convert.ToBoolean(r["IsIndex"].ToString())
        //                    , Convert.ToBoolean(r["IsCreate"].ToString())
        //                    , Convert.ToBoolean(r["IsEdit"].ToString())
        //                    , Convert.ToBoolean(r["IsDetails"].ToString())
        //                    , Convert.ToBoolean(r["IsTally"].ToString())
        //                    , Convert.ToBoolean(r["IsUpdate"].ToString())
        //                    , Convert.ToBoolean(r["IsSearch"].ToString())
        //                    , Convert.ToBoolean(r["IsExport"].ToString())
        //                    , Convert.ToBoolean(r["Isdeleted"].ToString())
        //                    , Convert.ToBoolean(r["IsRowDeleted"].ToString())
        //                );
        //            }
        //            r.Close();
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("The Database transaction failed", ex);
        //    }
        //}

        public static AccessMatrixDetails RetrieveAccessById(int accessMatrixId)
        {
            AccessMatrixDetails result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AMD.ID,AMD.AccessMatrixId,AMD.UserType,AMD.IsIndex,AMD.IsCreate,AMD.IsEdit,AMD.IsDetails,AMD.IsTally,AMD.IsUpdate,AMD.IsSearch,AMD.IsExport,AMD.Isdeleted,AMD.IsRowDeleted,AM.ModuleName from AccessMatrixDetails AMD,AccessMatrix AM where AMD.AccessMatrixId=@AccessMatrixId and AMD.IsRowDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@AccessMatrixId", accessMatrixId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrixDetails");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new AccessMatrixDetails(Convert.ToInt32(r["Id"])
                             , Convert.ToInt32(r["AccessMatrixId"])
                               , Convert.ToString(r["ModuleName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , Convert.ToBoolean(r["IsIndex"].ToString())
                            , Convert.ToBoolean(r["IsCreate"].ToString())
                            , Convert.ToBoolean(r["IsEdit"].ToString())
                            , Convert.ToBoolean(r["IsDetails"].ToString())
                            , Convert.ToBoolean(r["IsTally"].ToString())
                            , Convert.ToBoolean(r["IsUpdate"].ToString())
                            , Convert.ToBoolean(r["IsSearch"].ToString())
                            , Convert.ToBoolean(r["IsExport"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToBoolean(r["IsRowDeleted"].ToString())
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

        public static AccessMatrixDetails RetrieveAccess(int accessMatrixId, string userType)
        {
            AccessMatrixDetails result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select AMD.ID,AMD.AccessMatrixId,AMD.UserType,AMD.IsIndex,AMD.IsCreate,AMD.IsEdit,AMD.IsDetails,AMD.IsTally,AMD.IsUpdate,AMD.IsSearch,AMD.IsExport,AMD.Isdeleted,AMD.IsRowDeleted,AM.ModuleName from AccessMatrixDetails AMD, AccessMatrix AM where AMD.AccessMatrixId=AM.Id and AMD.AccessMatrixId = @AccessMatrixId and AMD.UserType=@UserType and AMD.IsRowDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@AccessMatrixId", accessMatrixId);
                    manageCommand.Parameters.AddWithValue("@UserType", userType);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "AccessMatrixDetails");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new AccessMatrixDetails(Convert.ToInt32(r["Id"])
                      , Convert.ToInt32(r["AccessMatrixId"])
                        , Convert.ToString(r["ModuleName"].ToString())
                            , Convert.ToString(r["UserType"].ToString())
                            , Convert.ToBoolean(r["IsIndex"].ToString())
                            , Convert.ToBoolean(r["IsCreate"].ToString())
                            , Convert.ToBoolean(r["IsEdit"].ToString())
                            , Convert.ToBoolean(r["IsDetails"].ToString())
                            , Convert.ToBoolean(r["IsTally"].ToString())
                            , Convert.ToBoolean(r["IsUpdate"].ToString())
                            , Convert.ToBoolean(r["IsSearch"].ToString())
                            , Convert.ToBoolean(r["IsExport"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                            , Convert.ToBoolean(r["IsRowDeleted"].ToString())
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
        //public static AccessMatrixDetails RetrieveByStaffId(int Id)
        //{
        //    AccessMatrixDetails result = null;
        //    try
        //    {

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            string selectStatement = "Select AMD.ID,AMD.AccessMatrixId,AMD.IsIndex,AMD.IsCreate,AMD.IsEdit,AMD.IsDetails,AMD.IsTally,AMD.IsUpdate,AMD.IsSearch,AMD.IsExport,AMD.Isdeleted,AMD.StaffId,U.Name,AMD.IsRowDeleted,AM.ModuleName,U.UserType from AccessMatrixDetails AMD,Users U,AccessMatrix AM where AMD.AccessMatrixId=AM.Id and AMD.StaffId=U.Id and AMD.IsRowDeleted=0 and AMD.StaffId = @Id";
        //            SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
        //            manageCommand.Parameters.AddWithValue("@Id", Id);
        //            connection.Open();
        //            SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
        //            DataSet sqlDataset = new DataSet();
        //            sqlAdapter.Fill(sqlDataset, "AccessMatrixDetails");
        //            DataTableReader r = sqlDataset.CreateDataReader();
        //            if (r.Read())
        //            {
        //                result = new AccessMatrixDetails(Convert.ToInt32(r["Id"])
        //                    , Convert.ToInt32(r["AccessMatrixId"])
        //                    , Convert.ToString(r["ModuleName"].ToString())
        //                    , Convert.ToInt32(r["StaffId"])
        //                    , Convert.ToString(r["Name"].ToString())
        //                    , Convert.ToString(r["UserType"].ToString())
        //                    , Convert.ToBoolean(r["IsIndex"].ToString())
        //                    , Convert.ToBoolean(r["IsCreate"].ToString())
        //                    , Convert.ToBoolean(r["IsEdit"].ToString())
        //                    , Convert.ToBoolean(r["IsDetails"].ToString())
        //                    , Convert.ToBoolean(r["IsTally"].ToString())
        //                    , Convert.ToBoolean(r["IsUpdate"].ToString())
        //                    , Convert.ToBoolean(r["IsSearch"].ToString())
        //                    , Convert.ToBoolean(r["IsExport"].ToString())
        //                    , Convert.ToBoolean(r["Isdeleted"].ToString())
        //                    , Convert.ToBoolean(r["IsRowDeleted"].ToString())
        //                );
        //            }
        //            r.Close();
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("The Database transaction failed", ex);
        //    }
        //}

        public bool Update()
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update AccessMatrixDetails set AccessMatrixId=@AccessMatrixId,UserType=@UserType,IsIndex=@IsIndex,IsCreate=@IsCreate,IsEdit=@IsEdit,IsUpdate=@IsUpdate,IsSearch=@IsSearch,IsExport=@IsExport,Isdeleted=@Isdeleted,IsDetails=@IsDetails,IsTally=@IsTally"
                                 + " where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@AccessMatrixId", this.AccessMatrixId);
                    manageCommand.Parameters.AddWithValue("@UserType", this.UserType);
                    manageCommand.Parameters.AddWithValue("@IsIndex", this.IsIndex);
                    manageCommand.Parameters.AddWithValue("@IsCreate", this.IsCreate);
                    manageCommand.Parameters.AddWithValue("@IsEdit", this.IsEdit);
                    manageCommand.Parameters.AddWithValue("@IsDetails", this.IsDetails);
                    manageCommand.Parameters.AddWithValue("@IsTally", this.IsTally);
                    manageCommand.Parameters.AddWithValue("@IsUpdate", this.IsUpdate);
                    manageCommand.Parameters.AddWithValue("@IsSearch", this.IsSearch);
                    manageCommand.Parameters.AddWithValue("@IsExport", this.IsExport);
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
                string sqlStatement = "Update AccessMatrixDetails Set IsRowDeleted = 1 where Id = @Id ";
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

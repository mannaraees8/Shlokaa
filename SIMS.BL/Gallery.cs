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
    public class Gallery
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _fileName;
        private string _fileData;
        private string _fileDataOldData;
        private string _thumbnail;
        private string _thumbnailOldData;
        private string _fileType;
        private string _fileTitle;
        private string _fileDiscription;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public string FileName { get { return _fileName; } set { _fileName = value; } }

        public string FileData { get { return _fileData; } set { _fileData = value; } }
        public string FileDataOldData { get { return _fileDataOldData; } set { _fileDataOldData = value; } }
        public string Thumbnail { get { return _thumbnail; } set { _thumbnail = value; } }
        public string ThumbnailOldData { get { return _thumbnailOldData; } set { _thumbnailOldData = value; } }
        public string FileType { get { return _fileType; } set { _fileType = value; } }
        public string Title { get { return _fileTitle; } set { _fileTitle = value; } }
        public string FileDiscription { get { return _fileDiscription; } set { _fileDiscription = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Gallery()
        {
            _id = 0;
            _fileName = "";
            _fileData = null;
            _fileType = "";
            _fileDiscription = "";
            _fileTitle = "";
            _isdeleted = false;
            _thumbnailOldData = "";
            _fileDataOldData = "";
        }
        public Gallery(int id, string FileName, string FileData, string Thumbnail, string FileType, string Title, bool isdeleted)
        {
            _id = id;
            _fileName = FileName;
            _fileData = FileData;
            _fileDataOldData = FileData;
            _thumbnail = Thumbnail;
            _thumbnailOldData = Thumbnail;
            _fileType = FileType;
            _isdeleted = isdeleted;
            _fileDiscription = FileDiscription;
            _fileTitle = Title;
        }
        public Gallery(int id, string FileName, string Thumbnail, string FileType, string Title, bool isdeleted)
        {
            _id = id;
            _fileName = FileName;
            _thumbnail = Thumbnail;
            _thumbnailOldData = Thumbnail;
            _fileType = FileType;
            _isdeleted = isdeleted;
            _fileDiscription = FileDiscription;
            _fileTitle = Title;
        }
        public Gallery(string FileData)
        {

            _fileData = FileData;

        }


        #endregion //CTOR

        #region CRUD
        public static int Create(string FileName, string FileData, string Thumbnail, string FileType, string Title)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement;
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Gallery ( FileName, FileData, FileType,Title,Thumbnail) "
                        + " values ( @FileName, @FileData, @FileType,@Title,@Thumbnail)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@FileName", FileName);

                    if (FileData == null)
                    {
                        manageCommand.Parameters.Add("@FileData", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    }
                    else
                    {
                        manageCommand.Parameters.AddWithValue("@FileData", FileData);
                    }
                    if (Thumbnail == null)
                    {
                        manageCommand.Parameters.Add("@Thumbnail", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    }
                    else
                    {
                        manageCommand.Parameters.AddWithValue("@Thumbnail", Thumbnail);
                    }
                    manageCommand.Parameters.AddWithValue("@FileType", FileType);
                    manageCommand.Parameters.AddWithValue("@Title", Title);
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

        public static List<Gallery> RetrieveAll()
        {
            List<Gallery> galleryList = new List<Gallery>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, FileName,FileData,FileType,Thumbnail,Title, Isdeleted from Gallery where IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Gallery");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        galleryList.Add(new Gallery(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["FileName"].ToString())
                            , Convert.ToString(r["FileData"])
                            , Convert.ToString(r["Thumbnail"])
                            , Convert.ToString(r["FileType"].ToString())
                            , Convert.ToString(r["Title"].ToString())
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
            return galleryList;
        }

        public static List<Gallery> RetrieveWithoutFileData()
        {
            List<Gallery> galleryList = new List<Gallery>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, FileName,FileType,Thumbnail,Title, Isdeleted from Gallery where IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Gallery");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        galleryList.Add(new Gallery(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["FileName"].ToString())
                            , Convert.ToString(r["Thumbnail"])
                            , Convert.ToString(r["FileType"].ToString())
                            , Convert.ToString(r["Title"].ToString())
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
            return galleryList;
        }
        public static string RetrieveOnlyFileData(int Id)
        {
            string fileData = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select FileData from Gallery where Id=@Id";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@Id", Id);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Gallery");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        fileData = Convert.ToString(r["FileData"]);
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return fileData;
        }


        public static Gallery RetrieveById(int Id)
        {
            Gallery result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, FileName,FileData,Thumbnail,FileType,Title, Isdeleted from Gallery where IsDeleted=0 and Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Gallery");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Gallery(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["FileName"].ToString())
                           , Convert.ToString(r["FileData"].ToString())
                           , Convert.ToString(r["Thumbnail"].ToString())
                            , Convert.ToString(r["FileType"].ToString())
                             , Convert.ToString(r["Title"].ToString())
                            , Convert.ToBoolean(r["Isdeleted"].ToString()));
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
                    sqlStatement = "Update Gallery Set "
                        + "  FileName = @FileName "
                        + ", FileData = @FileData "
                                                + ", Thumbnail = @Thumbnail "
                        + ", FileType = @FileType "
                        + ", Isdeleted = @Isdeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@FileName", this.FileName);
                    manageCommand.Parameters.AddWithValue("@FileData", this.FileData);
                    manageCommand.Parameters.AddWithValue("@FileData", this.Thumbnail);
                    manageCommand.Parameters.AddWithValue("@FileType", this.FileType);
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
                string sqlStatement = "Delete from Gallery where Id = @Id ";
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

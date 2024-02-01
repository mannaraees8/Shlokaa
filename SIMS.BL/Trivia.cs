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
    public class Trivia
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _tabName;
        private string _contents;
        private string _title;
        private string _subContents;
        private string _point1;
        private string _point2;
        private string _point3;
        private string _point4;
        private string _point5;
        private string _point6;
        private string _point7;
        private string _point8;
        private string _point9;
        private string _point10;
        private string _type;
   //     private string _subtitle;

        private byte[] _image;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public string TabName { get { return _tabName; } set { _tabName = value; } }

        public string Contents { get { return _contents; } set { _contents = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public string SubContents { get { return _subContents; } set { _subContents = value; } }
        public string Point1 { get { return _point1; } set { _point1 = value; } }
        public string Point2 { get { return _point2; } set { _point2 = value; } }
        public string Point3 { get { return _point3; } set { _point3 = value; } }
        public string Point4 { get { return _point4; } set { _point4 = value; } }
        public string Point5 { get { return _point5; } set { _point5 = value; } }
        public string Point6 { get { return _point6; } set { _point6 = value; } }
        public string Point7 { get { return _point7; } set { _point7 = value; } }
        public string Point8 { get { return _point8; } set { _point8 = value; } }
        public string Point9 { get { return _point9; } set { _point9 = value; } }
        public string Point10 { get { return _point10; } set { _point10 = value; } }

        public string Type { get { return _type; } set { _type = value; } }

     //   public string SubTitle { get { return _subContents; } set { _subContents = value; } }
        public byte[] Image { get { return _image; } set { _image = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        public Trivia()
        {
            _id = 0;
            _tabName = "";
            _contents = "";
            _subContents = "";
            _title = "";
            _image = null;
            _isdeleted = false;
            _point1 = "";
            _point2 = "";
            _point3 = "";
            _point4 = "";
            _point5 = "";
            _point6 = "";
            _point7 = "";
            _point8 = "";
            _point9 = "";
            _point10 = "";
            _type = "";
        }


        public Trivia(int Id, string TabName, string Contents, string Title,string SubContents, string Point1, string Point2, string Point3, string Point4, string Point5, string Point6, string Point7, string Point8, string Point9, string Point10, string Type, byte[] Image, bool Isdeleted)
        {
            _id = Id;
            _tabName = TabName;
            _contents = Contents;
            _subContents = SubContents;
            _point1 = Point1;
            _point2 = Point2;
            _point3 = Point3;
            _point4 = Point4;
            _point5 = Point5;
            _point6 = Point6;
            _point7 = Point7;
            _point8 = Point8;
            _point9 = Point9;
            _point10 = Point10;
            _type = Type;
            _title = Title;
            _image = Image;
            _isdeleted = false;
        }


        #region CRUD
        public static int Create(string TabName, string Contents, string Title, string SubContents, string Point1, string Point2, string Point3, string Point4, string Point5, string Point6, string Point7, string Point8, string Point9, string Point10, string Type, byte[] Image)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Trivia ( TabName, Contents,Title,SubContents,Point1,Point2,Point3,Point4,Point5,Point6,Point7,Point8,Point9,Point10,Type,Image ) "
                        + " values ( @TabName, @Contents,@Title,@SubContents,@Point1,@Point2,@Point3,@Point4,@Point5,@Point6,@Point7,@Point8,@Point9,@Point10,@Type,@Image)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@TabName", TabName);
                    manageCommand.Parameters.AddWithValue("@Contents", Contents);
                    manageCommand.Parameters.AddWithValue("@Title", Title);
                    manageCommand.Parameters.AddWithValue("@SubContents", SubContents);
                    manageCommand.Parameters.AddWithValue("@Point1", Point1);
                    manageCommand.Parameters.AddWithValue("@Point2", Point2);
                    manageCommand.Parameters.AddWithValue("@Point3", Point3);
                    manageCommand.Parameters.AddWithValue("@Point4", Point4);
                    manageCommand.Parameters.AddWithValue("@Point5", Point5);
                    manageCommand.Parameters.AddWithValue("@Point6", Point6);
                    manageCommand.Parameters.AddWithValue("@Point7", Point7);
                    manageCommand.Parameters.AddWithValue("@Point8", Point8);
                    manageCommand.Parameters.AddWithValue("@Point9", Point9);
                    manageCommand.Parameters.AddWithValue("@Point10", Point10);
                    manageCommand.Parameters.AddWithValue("@Type", Type);


                    if (Image == null)
                    {
                        manageCommand.Parameters.Add("@Image", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        //manageCommand.Parameters.AddWithValue("@Picture", DBNull.Value);
                    }
                    else
                    {
                        manageCommand.Parameters.AddWithValue("@Image", Image);
                    }

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

        public static List<Trivia> RetrieveAll()
        {
            List<Trivia> trivialist = new List<Trivia>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id,TabName,Contents,Title,SubContents,Point1,Point2,Point3,Point4,Point5,Point6,Point7,Point8,Point9,Point10,Type,Image,Isdeleted from Trivia where IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Trivia");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        trivialist.Add(new Trivia(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["TabName"].ToString())
                            , Convert.ToString(r["Contents"].ToString())
                             , Convert.ToString(r["Title"].ToString())
                            , Convert.ToString(r["SubContents"].ToString())
                           , Convert.ToString(r["Point1"].ToString())
                              , Convert.ToString(r["Point2"].ToString())
                           , Convert.ToString(r["Point3"].ToString())
                           , Convert.ToString(r["Point4"].ToString())
                           , Convert.ToString(r["Point5"].ToString())
                           , Convert.ToString(r["Point6"].ToString())
                           , Convert.ToString(r["Point7"].ToString())
                           , Convert.ToString(r["Point8"].ToString())
                           , Convert.ToString(r["Point9"].ToString())
                           , Convert.ToString(r["Point10"].ToString())
                          , Convert.ToString(r["Type"].ToString())
                            , (r["Image"]) != DBNull.Value ? (byte[])r["Image"] : null
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
            return trivialist;
        }


        public static Trivia RetrieveById(int Id)
        {
            Trivia result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id,TabName,Contents,Title,SubContents,Type,Point1,Point2,Point3,Point4,Point5,Point6,Point7,Point8,Point9,Point10,Image,Isdeleted from Trivia where IsDeleted=0 and Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Trivia");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Trivia(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["TabName"].ToString())
                            , Convert.ToString(r["Contents"].ToString())
                             , Convert.ToString(r["Title"].ToString())
                            , Convert.ToString(r["SubContents"].ToString())
                            , Convert.ToString(r["Point1"].ToString())
                              , Convert.ToString(r["Point2"].ToString())
                           , Convert.ToString(r["Point3"].ToString())
                           , Convert.ToString(r["Point4"].ToString())
                           , Convert.ToString(r["Point5"].ToString())
                           , Convert.ToString(r["Point6"].ToString())
                           , Convert.ToString(r["Point7"].ToString())
                           , Convert.ToString(r["Point8"].ToString())
                           , Convert.ToString(r["Point9"].ToString())
                           , Convert.ToString(r["Point10"].ToString())
                                                  , Convert.ToString(r["Type"].ToString())
                            , (r["Image"]) != DBNull.Value ? (byte[])r["Image"] : null
                            , Convert.ToBoolean(r["IsDeleted"].ToString())
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

        public bool Update(byte[] Image)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Trivia Set "
                        + "  TabName = @TabName "
                        + ", Contents = @Contents "
                         + ", Title = @Title "
                        + ", SubContents = @SubContents "
                                            + ", Point1 = @Point1 "
                                            + ", Point2 = @Point2 "
                                            + ", Point3 = @Point3 "
                                            + ", Point4 = @Point4 "
                                            + ", Point5 = @Point5 "
                                            + ", Point6 = @Point6 "
                                            + ", Point7 = @Point7 "
                                            + ", Point8 = @Point8 "
                                            + ", Point9 = @Point9 "
                                            + ", Point10 = @Point10 "
                                            + ", Type = @Type "

                        + ", Image = @Image "
                        + ", Isdeleted = @Isdeleted "
                        + "  where ID= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@TabName", this.TabName);
                    manageCommand.Parameters.AddWithValue("@Contents", this.Contents);
                    manageCommand.Parameters.AddWithValue("@SubContents", this.SubContents);
                    manageCommand.Parameters.AddWithValue("@Point1", this.Point1);
                    manageCommand.Parameters.AddWithValue("@Point2", this.Point2);
                    manageCommand.Parameters.AddWithValue("@Point3", this.Point3);
                    manageCommand.Parameters.AddWithValue("@Point4", this.Point4);
                    manageCommand.Parameters.AddWithValue("@Point5", this.Point5);
                    manageCommand.Parameters.AddWithValue("@Point6", this.Point6);
                    manageCommand.Parameters.AddWithValue("@Point7", this.Point7);
                    manageCommand.Parameters.AddWithValue("@Point8", this.Point8);
                    manageCommand.Parameters.AddWithValue("@Point9", this.Point9);
                    manageCommand.Parameters.AddWithValue("@Point10", this.Point10);
                    manageCommand.Parameters.AddWithValue("@Title", this.Title);
                    manageCommand.Parameters.AddWithValue("@Type", this.Type);
                    manageCommand.Parameters.AddWithValue("@Image", Image);

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

        public bool Update()
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Trivia Set "
                        + "  TabName = @TabName "
                        + ", Contents = @Contents "
                         + ", Title = @Title "
                        + ", SubContents = @SubContents "
                                            + ", Point1 = @Point1 "
                                            + ", Point2 = @Point2 "
                                            + ", Point3 = @Point3 "
                                            + ", Point4 = @Point4 "
                                            + ", Point5 = @Point5 "
                                            + ", Point6 = @Point6 "
                                            + ", Point7 = @Point7 "
                                            + ", Point8 = @Point8 "
                                            + ", Point9 = @Point9 "
                                            + ", Point10 = @Point10 "
                                            + ", Type = @Type "
                        + ", Isdeleted = @Isdeleted "
                        + "  where ID= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@TabName", this.TabName);
                    manageCommand.Parameters.AddWithValue("@Contents", this.Contents);
                    manageCommand.Parameters.AddWithValue("@SubContents", this.SubContents);
                    manageCommand.Parameters.AddWithValue("@Point1", this.Point1);
                    manageCommand.Parameters.AddWithValue("@Point2", this.Point2);
                    manageCommand.Parameters.AddWithValue("@Point3", this.Point3);
                    manageCommand.Parameters.AddWithValue("@Point4", this.Point4);
                    manageCommand.Parameters.AddWithValue("@Point5", this.Point5);
                    manageCommand.Parameters.AddWithValue("@Point6", this.Point6);
                    manageCommand.Parameters.AddWithValue("@Point7", this.Point7);
                    manageCommand.Parameters.AddWithValue("@Point8", this.Point8);
                    manageCommand.Parameters.AddWithValue("@Point9", this.Point9);
                    manageCommand.Parameters.AddWithValue("@Point10", this.Point10);
                    manageCommand.Parameters.AddWithValue("@Title", this.Title);
                    manageCommand.Parameters.AddWithValue("@Type", this.Type);

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
                string sqlStatement = "Update Trivia Set isDeleted = 1 where Id = @Id ";
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

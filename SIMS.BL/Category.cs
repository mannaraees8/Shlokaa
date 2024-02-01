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
    public class Category
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;
        #region Fields
        private int _id;
        private string _thumbnail;
        private string _oldFile;
        private string _name;
        private string _discription;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public string Name { get { return _name; } set { _name = value; } }
        public string Discription { get { return _discription; } set { _discription = value; } }

        public string ImagePath { get { return _thumbnail; } set { _thumbnail = value; } }
        public string OldFile { get { return _oldFile; } set { _oldFile = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CONST
        public Category()
        {
            _id = 0;
            _name = "";
            _discription = "";
            _thumbnail = "";
            _oldFile = "";
            _isdeleted = false;
        }


        public Category(int Id, string Name,string Discription, string categoryImage, bool Isdeleted)
        {
            _id = Id;
            _name = Name;
            _discription = Discription;
            _thumbnail = categoryImage;
            _oldFile = categoryImage;
            _isdeleted = false;
        }
        #endregion

        #region CRUD
        public static int Create(string Name, string Discription, string categoryImage)
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from Categories where Name=@Name and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Name", Name);
                    connection.Open();
                    int SubCategory = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (SubCategory <= 0)
                    {

                        sqlStatement = "Insert Categories ( Name, Discription,ImagePath,IsDeleted ) "
                        + " values (@Name, @Discription, @ImagePath, @IsDeleted)";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Name", Name);
                        manageCommand.Parameters.AddWithValue("@Discription", Discription);
                        manageCommand.Parameters.AddWithValue("@ImagePath", categoryImage);
                        manageCommand.Parameters.AddWithValue("@IsDeleted", 0);

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

        public static List<Category> RetrieveAll()
        {
            List<Category> SubCategoryInfolist = new List<Category>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id,Name,Discription,ImagePath,IsDeleted from Categories where IsDeleted=0 ORDER BY Name ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Categories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        SubCategoryInfolist.Add(new Category(Convert.ToInt32(r["Id"]),
                            Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Discription"].ToString())
                            , Convert.ToString(r["ImagePath"].ToString())
                            //, (r["Thumbnail"]) != DBNull.Value ? (byte[])r["Thumbnail"] : null
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
            return SubCategoryInfolist;
        }  
        
        public static List<Category> RetrieveAllForHomeproduct()
        {
            List<Category> SubCategoryInfolist = new List<Category>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Distinct c.Id, c.Name, c.Discription, c.ImagePath, c.IsDeleted from Categories c, Item i where c.Id=i.CategoryId and c.IsDeleted=0 ORDER BY c.Name ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Categories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        SubCategoryInfolist.Add(new Category(Convert.ToInt32(r["Id"]),
                            Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Discription"].ToString())
                            , Convert.ToString(r["ImagePath"].ToString())
                            //, (r["Thumbnail"]) != DBNull.Value ? (byte[])r["Thumbnail"] : null
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
            return SubCategoryInfolist;
        }


        public static Category RetrieveById(int Id)
        {
            Category result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id,Name,Discription,ImagePath,Isdeleted from Categories where Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Categories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Category(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["Discription"].ToString())
                            , Convert.ToString(r["ImagePath"].ToString())
                              //, (r["Thumbnail"]) != DBNull.Value ? (byte[])r["Thumbnail"] : null
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

        public bool Update(string ImagePath)
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from Categories where Name=@Name and IsDeleted=0 and Id!=@Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();
                    int SubCategory = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (SubCategory <= 0)
                    {

                        sqlStatement = "Update Categories Set "
                        + "  Name = @Name "
                        + ", Discription = @Discription "
                        + ", ImagePath = @ImagePath "
                        + ", Isdeleted = @Isdeleted "
                        + "  where ID= @Id";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Id", this.Id);
                        manageCommand.Parameters.AddWithValue("@Name", this.Name);
                        manageCommand.Parameters.AddWithValue("@Discription", this.Discription);
                        manageCommand.Parameters.AddWithValue("@ImagePath", ImagePath);
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

        public bool Update()
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Update Categories Set "
                         + "  Name = @Name "
                         + ", Discription = @Discription "
                         + ", Isdeleted = @Isdeleted "
                         + "  where ID= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@Discription", this.Discription);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", this.Isdeleted);
                    connection.Open();
                    count = manageCommand.ExecuteNonQuery();

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

            bool status = true;
            int subCategory = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Select count(*) from SubCategories where CategoriesId=@Id and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    connection.Open();
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    subCategory = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (subCategory == 0)
                    {
                        sqlStatement = "Update Categories Set IsDeleted = 1 where Id = @Id ";
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

        #endregion CRUD

    }
}






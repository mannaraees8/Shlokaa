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
    public class SubCategory
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _categoriesID;
        private string _category;
        private string _name;
        private string _discription;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public int CategoriesId { get { return _categoriesID; } set { _categoriesID = value; } }
        public string Discription { get { return _discription; } set { _discription = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CONST
        public SubCategory()
        {
            _id = 0;
            _categoriesID = 0;
            _name = "";
            _category = "";
            _discription = "";
            _isdeleted = false;
        }


        public SubCategory(int Id, string Name, int categoryid, string category, string Discription, bool Isdeleted)
        {
            _id = Id;
            _categoriesID = categoryid;
            _name = Name;
            _category = category;
            _discription = Discription;
            _isdeleted = false;
        }


        #endregion CONST


        #region CRUD
        public static int Create(int CategoriesID,string Name, string Discription)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string sqlStatement;
                    int count = 0;
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from SubCategories where Name=@Name and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Name", Name);
                    connection.Open();
                    int category = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (category <= 0)
                    {
                        sqlStatement = "Insert SubCategories (CategoriesID,Name, Discription,IsDeleted ) "
                      + " values (@CategoriesID,@Name, @Discription,0)";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Name", Name);
                        manageCommand.Parameters.AddWithValue("@CategoriesID", CategoriesID);
                        manageCommand.Parameters.AddWithValue("@Discription", Discription);

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

        public static List<SubCategory> RetrieveAll()
        {
            List<SubCategory> categoryInfolist = new List<SubCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select SubCategories.Id,SubCategories.Name,SubCategories.IsDeleted,SubCategories.Discription,Categories.Name as Category,Categories.Id as CategoryId from SubCategories,Categories where SubCategories.CategoriesId=Categories.Id and SubCategories.IsDeleted=0 order by SubCategories.Name ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SubCategories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        categoryInfolist.Add(new SubCategory(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToString(r["Category"].ToString())
                            , Convert.ToString(r["Discription"].ToString())
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
            return categoryInfolist;
        }

        public static List<SubCategory> RetrieveByCategoryId(int categoryId)
        {
            List<SubCategory> SubCategoryInfolist = new List<SubCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select SubCategories.Id,SubCategories.Name,SubCategories.IsDeleted,SubCategories.Discription,Categories.Name as Category,Categories.Id as CategoryId from SubCategories,Categories where SubCategories.CategoriesId=Categories.Id and SubCategories.CategoriesId=@Id and SubCategories.IsDeleted=0 ORDER BY SubCategories.Name ASC";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", categoryId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@Id", categoryId);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SubCategories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        SubCategoryInfolist.Add(new SubCategory(Convert.ToInt32(r["ID"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToString(r["Category"].ToString())
                            , Convert.ToString(r["Discription"].ToString())
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

        public static SubCategory RetrieveById(int Id)
        {
            SubCategory result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select SubCategories.Id,SubCategories.Name,SubCategories.Discription,SubCategories.IsDeleted,Categories.Name as Category,Categories.Id as CategoryId from SubCategories,Categories where SubCategories.CategoriesId=Categories.Id and SubCategories.Id=@Id and SubCategories.IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Categories");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new SubCategory(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToString(r["Category"].ToString())
                            , Convert.ToString(r["Discription"].ToString())
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

        public bool Update()
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Update SubCategories Set "
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
            int subCategoryCount = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Select count(*) from Item where SubCategoryId=@Id and IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    connection.Open();
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    subCategoryCount = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (subCategoryCount == 0)
                    {
                        sqlStatement = "Update SubCategories Set IsDeleted = 1 where Id = @Id ";
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

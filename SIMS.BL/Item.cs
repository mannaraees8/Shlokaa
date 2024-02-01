using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SIMS.BL
{

    public class Item
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _name;
        private string _size;
        private string _unitOfMeasurement;
        private string _catagory;
        private string _subCategory;
        private int _sizeId;
        private int _unitOfMeasurementId;
        private string _description;
        private int _catagoryId;
        private int _subCatagoryId;
        private string _price;
        private string _imagepath;
        private string _oldFile;
        private byte[] _photo;
        private byte[] _thumbnail;
        private bool _isdeleted;
        #endregion //Fields.

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public string Name { get { return _name; } set { _name = value; } }
        public string Category { get { return _catagory; } set { _catagory = value; } }
        public string SubCategory { get { return _subCategory; } set { _subCategory = value; } }
        public string Size { get { return _size; } set { _size = value; } }
        public string UnitOfMeasurement { get { return _unitOfMeasurement; } set { _unitOfMeasurement = value; } }
        public string Description { get { return _description; } set { _description = value; } }

        public int SizeId { get { return _sizeId; } set { _sizeId = value; } }

        public int UnitOfMeasurementId { get { return _unitOfMeasurementId; } set { _unitOfMeasurementId = value; } }

        public int CatagoryId { get { return _catagoryId; } set { _catagoryId = value; } }
        public int SubCatagoryId { get { return _subCatagoryId; } set { _subCatagoryId = value; } }

        public string Price { get { return _price; } set { _price = value; } }
        public string Imagepath { get { return _imagepath; } set { _imagepath = value; } }
        public string OldFile { get { return _oldFile; } set { _oldFile = value; } }
        public byte[] Photo { get { return _photo; } set { _photo = value; } }
        public byte[] Thumbnail { get { return _thumbnail; } set { _thumbnail = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Item()
        {
            _id = 0;
            _name = "";
            _catagory = "";
            _subCategory = "";
            _size = "";
            _unitOfMeasurement = "";
            _sizeId = 0;
            _unitOfMeasurementId = 0;
            _subCatagoryId = 0;
            _catagoryId = 0;
            _description = "";
            _price = "";
            _imagepath = "";
            _oldFile = "";
            _photo = null;
            _thumbnail = null;
            _isdeleted = false;
        }


        public Item(int id, string name, string unitofmeasurement, int unitofmeasurementid, string subcategory, string description, int subcategoryid, string catagory, int catagoryid, string price,string imagepath, byte[] thumbnail, bool isdeleted)
        {
            _id = id;
            _name = name;

            _unitOfMeasurement = unitofmeasurement;
            _subCategory = subcategory;
            _catagory = catagory;
            //_sizeId = Sizeid;
            _unitOfMeasurementId = unitofmeasurementid;
            _subCatagoryId = subcategoryid;
            _description = description;
            _catagoryId = catagoryid;
            _price = price;
            _imagepath = imagepath;
            _oldFile = imagepath;
            _thumbnail = thumbnail;

            _isdeleted = isdeleted;
        }
        public Item(int id, string name, string Size, string unitofmeasurement, int unitofmeasurementid, string subcategory, int subcategoryid, int catagoryid, string price, string imagepath)
        {
            _id = id;
            _name = name;
            _size = Size;
            _unitOfMeasurement = unitofmeasurement;
            _subCategory = subcategory;
            //_sizeId = Sizeid;
            _unitOfMeasurementId = unitofmeasurementid;
            _subCatagoryId = subcategoryid;
            _catagoryId = catagoryid;
            _price = price;
            _imagepath = imagepath;
            _oldFile = imagepath;
        }
        public Item(int id, string name)
        {
            _id = id;
            _name = name;

        }

        public Item(int id, string name, int categoryid, string imagepath)
        {
            _id = id;
            _name = name;
            _catagoryId = categoryid;
            _imagepath = imagepath;
            _oldFile = imagepath;


        }
        #endregion //CTOR

        #region CRUD
        public static int Create(string Name, int UnitOfMeasurementId, int SubcategoryId, int CatagoryId, string Price, string imagepath)
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Select count(*) from Item where Name=@Name and IsDeleted=0";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Name", Name);
                    connection.Open();
                    int item = Convert.ToInt32(manageCommand.ExecuteScalar());
                    if (item <= 0)
                    {
                        sqlStatement = "Insert Item ( Name, UnitOfMeasurementId, SubCategoryId, CategoryId, Price, Imagepath, Isdeleted ) "
                       + " values ( @Name, @UnitOfMeasurementId, @SubcategoryId, @CatagoryId, @Price, @Imagepath, @Isdeleted )";
                        sqlStatement = sqlStatement + " SELECT SCOPE_IDENTITY();";
                        manageCommand = new SqlCommand(sqlStatement, connection);
                        manageCommand.Parameters.AddWithValue("@Name", Name);
                        manageCommand.Parameters.AddWithValue("@UnitOfMeasurementId", UnitOfMeasurementId);
                        manageCommand.Parameters.AddWithValue("@SubcategoryId", SubcategoryId);
                        manageCommand.Parameters.AddWithValue("@CatagoryId", CatagoryId);
                        manageCommand.Parameters.AddWithValue("@Price", Price);
                        //if (Photo == null)
                        //{
                        //    manageCommand.Parameters.Add("@Photo", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                        //    //manageCommand.Parameters.AddWithValue("@Picture", DBNull.Value);
                        //}
                        //else
                        //{
                        //    manageCommand.Parameters.AddWithValue("@Photo", Photo);
                        //}
                        manageCommand.Parameters.AddWithValue("@Imagepath", imagepath);
                        manageCommand.Parameters.AddWithValue("@Isdeleted", 0);


                        count = Convert.ToInt32(manageCommand.ExecuteScalar());
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

        public static List<Item> RetrieveAll()
        {
            List<Item> itemlist = new List<Item>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Item.ID,Item.Name as ItemName,Item.Price,Item.Imagepath,UnitOfMeasurement.UnitOfMeasurement,UnitOfMeasurement.Id as UnitId,SubCategories.Name as SubCategory,SubCategories.Id as SubCategoryId,Categories.Name as Category,Categories.Id as CategoryId,SubCategories.Discription,Categories.Thumbnail,Item.Isdeleted from Item"
               + " Inner join UnitOfMeasurement On Item.UnitOfMeasurementId=UnitOfMeasurement.ID inner join SubCategories on Item.SubCategoryId=SubCategories.ID inner join Categories on Item.CategoryId=Categories.ID Where  Item.IsDeleted=0 ORDER BY ItemName ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Item");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {


                        itemlist.Add(new Item(Convert.ToInt32(r["ID"])
                                , Convert.ToString(r["ItemName"].ToString())
                            //, Convert.ToString(r["Size"].ToString())
                            //, Convert.ToInt32(r["ItemSizeId"])
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToInt32(r["UnitId"])
                            , Convert.ToString(r["SubCategory"].ToString())
                             , Convert.ToString(r["Discription"])
                            , Convert.ToInt32(r["SubCategoryId"])
                            , Convert.ToString(r["Category"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToString(r["Price"].ToString())
                            , Convert.ToString(r["Imagepath"].ToString())

                            , (r["Thumbnail"]) != DBNull.Value ? (byte[])r["Thumbnail"] : null
                            , Convert.ToBoolean(r["Isdeleted"].ToString())

                        )); ;
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return itemlist;
        }
        public static List<Item> RetrieveAllWithoutRawMaterial()
        {
            List<Item> itemlist = new List<Item>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select Distinct Item.ID as ItemId,Item.Name as ItemName,Item.Price,Item.ImagePath,Item.CategoryId,UnitOfMeasurement.UnitOfMeasurement,UnitOfMeasurement.Id as UnitId,SubCategories.Name as SubCategory,SubCategories.Id as SubCategoryId,ItemSize.Size from Item,UnitOfMeasurement,SubCategories,Categories,ItemSize,ProductSize WITH (NOLOCK) where Item.UnitOfMeasurementId = UnitOfMeasurement.ID and Item.SubCategoryId = SubCategories.ID  and ItemSize.ID = ProductSize.SizeId  and Item.ID = ProductSize.ItemId and Item.Isdeleted = 0 order by Item.Name ASC";

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.CommandTimeout = 100;
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Item");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {


                        itemlist.Add(new Item(Convert.ToInt32(r["ItemId"])
                                , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["Size"].ToString())
                            //, Convert.ToInt32(r["ItemSizeId"])
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToInt32(r["UnitId"])
                            , Convert.ToString(r["SubCategory"].ToString())
                            , Convert.ToInt32(r["SubCategoryId"])
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToString(r["Price"].ToString())
                            , Convert.ToString(r["Imagepath"].ToString())


                        )); ;
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }

            return itemlist;
        }


        public static List<Item> RetrieveByCategoryId(int CategoryId,int start)
        {

            List<Item> itemlist = new List<Item>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    //string selectStatement = "Select Distinct Item.ID as ItemId,Item.Name as ItemName,Item.Photo,Item.CategoryId from Item,Categories where Item.CategoryId = @CategoryId ORDER BY ItemName ASC OFFSET @start ROWS FETCH NEXT 6 ROWS ONLY";
                    string selectStatement = "Select Distinct Item.ID as ItemId,Item.Name as ItemName,Item.Imagepath,Item.CategoryId from Item,Categories where Item.CategoryId = @CategoryId ORDER BY ItemName ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@CategoryId", CategoryId);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@start", start);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Item");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        itemlist.Add(new Item(Convert.ToInt32(r["ItemId"])
                               , Convert.ToString(r["ItemName"].ToString())
                               , Convert.ToInt32(r["CategoryId"])
                               , Convert.ToString(r["ImagePath"].ToString())
                               //, (r["Photo"]) != DBNull.Value ? (byte[])r["Photo"] : null

                               ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return itemlist;
        }
        public static List<Item> RetrieveBySubCategoryId(int SubCategoryId)
        {

            List<Item> itemlist = new List<Item>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id,Name from Item Where SubCategoryId=@SubCategoryId and IsDeleted=0 ORDER BY Name ASC";

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    sqlAdapter.SelectCommand.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Item");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {


                        itemlist.Add(new Item(Convert.ToInt32(r["Id"])
                                   , Convert.ToString(r["Name"].ToString())

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return itemlist;
        }

        public static Item RetrieveById(int Id)
        {
            Item result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Item.ID,Item.Name as ItemName,Item.Price,Item.Imagepath,UnitOfMeasurement.UnitOfMeasurement,UnitOfMeasurement.Id as UnitId,SubCategories.Name as SubCategory,SubCategories.Id as SubCategoryId,Categories.Name as Category,Categories.Id as CategoryId,SubCategories.Discription,Categories.Thumbnail,Item.Isdeleted from Item "
                      + " Inner join UnitOfMeasurement On Item.UnitOfMeasurementId=UnitOfMeasurement.ID inner join SubCategories on Item.SubCategoryId=SubCategories.ID inner join Categories on Item.CategoryId=Categories.ID Where Item.ID=@Id and Item.IsDeleted=0";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Item");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {

                        result = new Item(Convert.ToInt32(r["Id"])
                                    , Convert.ToString(r["ItemName"].ToString())
                            //, Convert.ToString(r["Size"].ToString())
                            //, Convert.ToInt32(r["ItemSizeId"])
                            , Convert.ToString(r["UnitOfMeasurement"].ToString())
                            , Convert.ToInt32(r["UnitId"])
                            , Convert.ToString(r["SubCategory"].ToString())
                             , Convert.ToString(r["Discription"])
                            , Convert.ToInt32(r["SubCategoryId"])
                            , Convert.ToString(r["Category"].ToString())
                            , Convert.ToInt32(r["CategoryId"])
                            , Convert.ToString(r["Price"].ToString())
                            , Convert.ToString(r["ImagePath"].ToString())
                            //, (r["Photo"]) != DBNull.Value ? (byte[])r["Photo"] : null

                            , (r["Thumbnail"]) != DBNull.Value ? (byte[])r["Thumbnail"] : null
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

        public bool Update(string Imagepath)
        {
            try
            {
                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update Item Set "
                                          + "  Name = @Name "
                                          + ", UnitOfMeasurementId = @UnitOfMeasurementId "
                                          + ", SubCategoryId = @SubCategoryId "
                                          + ", CategoryId = @CategoryId "
                                          + ", Imagepath = @Imagepath "
                                          + ", Price = @Price "
                                          + ", Isdeleted = @Isdeleted "
                                          + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@UnitOfMeasurementId", this.UnitOfMeasurementId);
                    manageCommand.Parameters.AddWithValue("@SubCategoryId", this.SubCatagoryId);
                    manageCommand.Parameters.AddWithValue("@CategoryId", this.CatagoryId);
                    manageCommand.Parameters.AddWithValue("@Imagepath", Imagepath);
                    manageCommand.Parameters.AddWithValue("@Price", this.Price);
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

        public bool Update()
        {
            try
            {

                int count = 0;
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;

                    sqlStatement = "Update Item Set "
                    + "  Name = @Name "
                    + ", UnitOfMeasurementId = @UnitOfMeasurementId "
                    + ", SubCategoryId = @SubCategoryId "
                    + ", CategoryId = @Categoryid "
                    + ", Isdeleted = @Isdeleted "
                    + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Name", this.Name);
                    manageCommand.Parameters.AddWithValue("@UnitOfMeasurementId", this.UnitOfMeasurementId);
                    manageCommand.Parameters.AddWithValue("@SubCategoryId", this.SubCatagoryId);
                    manageCommand.Parameters.AddWithValue("@CategoryId", this.CatagoryId);
                    manageCommand.Parameters.AddWithValue("@Price", this.Price);
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
            try
            {
                string sqlStatement = "Update Item Set isDeleted = 1 where Id = @Id ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    connection.Open();
                    int count = manageCommand.ExecuteNonQuery();
                    sqlStatement = "Update ProductSize Set isDeleted = 1 where ItemId = @Id ";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
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
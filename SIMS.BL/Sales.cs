using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SIMS.BL
{

    public class Sales
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffId;
        private int _month;
        private Double _salesPercentage;
        private string _extraAmount;
        private string _userName;

        private DateTime _date;
        private string _marketingExecutive;
        private string _productCategory;
        private int _salesAmount;
        private int _salesReturnAmount;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int StaffId { get { return _staffId; } set { _staffId = value; } }
        public int Month { get { return _month; } set { _month = value; } }
        public Double SalesPercentage { get { return _salesPercentage; } set { _salesPercentage = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string ExtraAmount { get { return _extraAmount; } set { _extraAmount = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public string MarketingExecutive { get { return _marketingExecutive; } set { _marketingExecutive = value; } }
        public string ProductCategory { get { return _productCategory; } set { _productCategory = value; } }
        public int SalesAmount { get { return _salesAmount; } set { _salesAmount = value; } }
        public int SalesReturnAmount { get { return _salesReturnAmount; } set { _salesReturnAmount = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Sales()
        {
            _id = 0;
            _date = DateTime.Now;
            _marketingExecutive = "";
            _productCategory = "";
            _salesAmount = 0;
            _salesReturnAmount = 0;
            _staffId = 0;
            _isdeleted = false;
        }
        public Sales(int salesAmount, int month, string userName)
        {

            _salesAmount = salesAmount;
            _month = month;
            _userName = userName;

        }
        public Sales(Double salesPercentage, string extraAmount, string userName)
        {

            _salesPercentage = salesPercentage;
            _extraAmount = extraAmount;
            _userName = userName;

        }
        public Sales(int id, DateTime date, int staffId, string marketingExecutive, string productCategory, int salesAmount, int salesReturnAmount, bool isdeleted)
        {
            _id = id;
            _staffId = staffId;
            _date = date;
            _marketingExecutive = marketingExecutive;
            _productCategory = productCategory;
            _salesAmount = salesAmount;
            _salesReturnAmount = salesReturnAmount;
            _isdeleted = isdeleted;
        }


        #endregion //CTOR

        #region CRUD
        public static int Create(DateTime date, int staffId, string marketingExecutive, string productCategory, int salesAmount, int salesReturnAmount)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Sales (Date,StaffId,MarketingExecutive,ProductCategory,SalesAmount,SalesReturnAmount) "
                        + " values (@Date,@StaffId,@MarketingExecutive,@ProductCategory,@SalesAmount,@SalesReturnAmount)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Date", date);
                    manageCommand.Parameters.AddWithValue("@StaffId", staffId);
                    manageCommand.Parameters.AddWithValue("@MarketingExecutive", marketingExecutive);
                    manageCommand.Parameters.AddWithValue("@ProductCategory", productCategory);
                    manageCommand.Parameters.AddWithValue("@SalesAmount", salesAmount);
                    manageCommand.Parameters.AddWithValue("@SalesReturnAmount", salesReturnAmount);

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

        public static List<Sales> RetrieveAll()
        {
            List<Sales> saleslist = new List<Sales>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select S.Id,S.Date, S.StaffId, S.ProductCategory, S.SalesAmount, S.SalesReturnAmount,S.MarketingExecutive,S.Isdeleted from Sales S where S.IsDeleted=0 order by S.Date desc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        saleslist.Add(new Sales(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToInt32(r["StaffId"])
                            , Convert.ToString(r["MarketingExecutive"].ToString())
                            , Convert.ToString(r["ProductCategory"].ToString())
                            , Convert.ToInt32(r["SalesAmount"])
                            , Convert.ToInt32(r["SalesReturnAmount"])
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
            return saleslist;
        }

        public static List<Sales> RetrieveAllByUserId(int userId)
        {
            List<Sales> saleslist = new List<Sales>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select S.Id,S.Date, S.StaffId, S.ProductCategory, S.SalesAmount, S.SalesReturnAmount,S.MarketingExecutive,S.Isdeleted from Sales S where S.IsDeleted=0 and S.StaffId=@StaffId order by S.Date desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", userId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        saleslist.Add(new Sales(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToInt32(r["StaffId"])
                            , Convert.ToString(r["MarketingExecutive"].ToString())
                            , Convert.ToString(r["ProductCategory"].ToString())
                            , Convert.ToInt32(r["SalesAmount"])
                            , Convert.ToInt32(r["SalesReturnAmount"])
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
            return saleslist;
        }

        public static Sales RetrieveById(int Id)
        {
            Sales result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select S.Id,S.Date, S.StaffId, S.ProductCategory, S.SalesAmount, S.SalesReturnAmount,S.MarketingExecutive,S.Isdeleted from Sales S where S.IsDeleted=0 and S.Id=@Id order by S.Date desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Sales(Convert.ToInt32(r["Id"])
                            , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToInt32(r["StaffId"])
                            , Convert.ToString(r["MarketingExecutive"].ToString())
                            , Convert.ToString(r["ProductCategory"].ToString())
                            , (r["SalesAmount"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesAmount"])
                            , (r["SalesReturnAmount"]) == DBNull.Value ? 0 : Convert.ToInt32(r["SalesReturnAmount"])
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
                    sqlStatement = "Update Sales Set "
                        + "  Date = @Date "
                        + ", StaffId = @StaffId "
                         + ", MarketingExecutive = @MarketingExecutive "
                        + ", ProductCategory = @ProductCategory "
                        + ", SalesAmount = @SalesAmount "
                        + ", SalesReturnAmount = @SalesReturnAmount "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Date", this.Date);
                    manageCommand.Parameters.AddWithValue("@StaffId", this.StaffId);
                    manageCommand.Parameters.AddWithValue("@MarketingExecutive", this.MarketingExecutive);
                    manageCommand.Parameters.AddWithValue("@ProductCategory", this.ProductCategory);
                    manageCommand.Parameters.AddWithValue("@SalesAmount", this.SalesAmount);
                    manageCommand.Parameters.AddWithValue("@SalesReturnAmount", this.SalesReturnAmount);

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
                string sqlStatement = "Update Sales Set isDeleted = 1 where Id = @Id ";
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



        public static List<Sales> RetrieveAllSalesTrend(int userId, DateTime startDate, DateTime endDate)
        {
            List<Sales> saleslist = new List<Sales>();
            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (userId == 0)
                    {
                        selectStatement = "select SUM(S.SalesAmount) as SalesAmount ,MONTH(S.Date) as Month from Sales S,Users U where S.StaffId=U.ID and (FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate) GROUP BY MONTH(S.Date)";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement = "select SUM(S.SalesAmount) as SalesAmount ,MONTH(S.Date) as Month,U.Name from Sales S,Users U where S.StaffId=U.ID and S.StaffId=@StaffId and (FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate) GROUP BY MONTH(S.Date),U.Name";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", userId);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (userId == 0)
                    {
                        while (r.Read())
                        {
                            saleslist.Add(new Sales(Convert.ToInt32(r["SalesAmount"])
                                , Convert.ToInt32(r["Month"])
                                , ""
                            ));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            saleslist.Add(new Sales(Convert.ToInt32(r["SalesAmount"])
                                , Convert.ToInt32(r["Month"])
                                , (r["Name"].ToString()) == null ? "" : Convert.ToString(r["Name"])
                            ));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return saleslist;
        }

        public static List<Sales> RetrieveAllSalesReturnTrend(int userId, DateTime startDate, DateTime endDate)
        {
            List<Sales> saleslist = new List<Sales>();
            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (userId == 0)
                    {
                        selectStatement = "select SUM(S.SalesReturnAmount) as SalesReturnAmount ,MONTH(S.Date) as Month from Sales S,Users U where S.StaffId=U.ID and((FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate)) GROUP BY MONTH(S.Date)";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement = "select SUM(S.SalesReturnAmount) as SalesReturnAmount ,MONTH(S.Date) as Month,U.Name from Sales S,Users U where S.StaffId=U.ID and S.StaffId=@StaffId and((FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate)) GROUP BY MONTH(S.Date),U.Name";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", userId);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (userId == 0)
                    {
                        while (r.Read())
                        {
                            saleslist.Add(new Sales(Convert.ToInt32(r["SalesReturnAmount"])
                                , Convert.ToInt32(r["Month"])
                                , ""
                            ));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            saleslist.Add(new Sales(Convert.ToInt32(r["SalesReturnAmount"])
                                , Convert.ToInt32(r["Month"])
                                , (r["Name"].ToString()) == null ? "" : Convert.ToString(r["Name"])
                            ));
                        }

                    }

                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return saleslist;
        }

        public static List<Sales> RetrieveAllSalesData(int userId, DateTime startDate, DateTime endDate)
        {
            List<Sales> saleslist = new List<Sales>();
            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (userId == 0)
                    {
                        selectStatement = "select " +
                                            " CASE WHEN((SUM(CAST(S.SalesAmount as int))/ CAST(U.SalesTarget as int))*100) >= 100 THEN 100 " +
                                            " WHEN((((SUM(CAST(S.SalesAmount as int)))) / CAST(U.SalesTarget as int)) * 100) < 100 THEN((SUM(CAST(S.SalesAmount as decimal)) / CAST(U.SalesTarget as int)) * 100) " +
                                            " WHEN((((SUM(CAST(S.SalesAmount as int)))) / CAST(U.SalesTarget as int)) * 100)IS NULL then 0 " +
                                             " else 0 " +
                                            " END AS SalesPercentage " +
                                            " from Sales S, Users U where S.StaffId = U.ID and S.StaffId != 0 and S.IsDeleted = 0  and(FORMAT(S.Date, 'yyyy-MM-dd') >= @StartDate AND FORMAT(S.Date, 'yyyy-MM-dd') <= @EndDate)  group by U.SalesTarget order by SalesPercentage desc ";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement = "select "+
                                            " CASE WHEN((SUM(CAST(S.SalesAmount as int))/ CAST(U.SalesTarget as int))*100) >= 100 THEN 100 " +
                                            " WHEN((((SUM(CAST(S.SalesAmount as int)))) / CAST(U.SalesTarget as int)) * 100) < 100 THEN((SUM(CAST(S.SalesAmount as decimal)) / CAST(U.SalesTarget as int)) * 100) " +
                                            " WHEN((((SUM(CAST(S.SalesAmount as int)))) / CAST(U.SalesTarget as int)) * 100)IS NULL then 0 " +
                                            " else 0 "+
                                            " END AS SalesPercentage "+
                                            " ,(SUM(CAST(S.SalesAmount as int)))-CAST(U.SalesTarget as int) as ExtraAmount,U.Name from Sales S,Users U where S.StaffId=U.ID and S.StaffId!=0 and S.IsDeleted=0 and  (FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate) and S.StaffId=@StaffId group by U.Name,U.SalesTarget order by SalesPercentage desc ";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", userId);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (userId == 0)
                    {
                        while (r.Read())
                        {
                            saleslist.Add(new Sales(Convert.ToInt32(r["SalesPercentage"])
                                , ""
                                , ""
                            ));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            saleslist.Add(new Sales(Convert.ToInt32(r["SalesPercentage"])
                                , Convert.ToString(r["ExtraAmount"])
                                , (r["Name"].ToString()) == null ? "" : Convert.ToString(r["Name"])
                            ));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return saleslist;
        }

        public static List<Sales> RetrieveAllProductCategoryTrend(string productCategory, DateTime startDate, DateTime endDate)
        {
            List<Sales> salesProductCategoryTrendlist = new List<Sales>();
            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (productCategory.Trim() == "")
                    {
                        selectStatement = "select sum(CAST((S.SalesAmount) as int)) as Amount,MONTH(S.Date) as Month,S.ProductCategory  from Sales S,Users U where S.IsDeleted=0  and (FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate) and S.ProductCategory!='Select'  group by MONTH(S.Date),S.ProductCategory";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement = "select sum(CAST((S.SalesAmount) as int)) as Amount,MONTH(S.Date) as Month,S.ProductCategory  from Sales S,Users U where S.IsDeleted=0  and (FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate) and S.ProductCategory!='Select' and S.ProductCategory=@ProductCategory group by MONTH(S.Date),S.ProductCategory";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@ProductCategory", productCategory);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (productCategory.Trim() == "")
                    {
                        while (r.Read())
                        {
                            salesProductCategoryTrendlist.Add(new Sales(Convert.ToInt32(r["Amount"])
                                , Convert.ToInt32(r["Month"])
                                , Convert.ToString(r["ProductCategory"])
                            ));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            salesProductCategoryTrendlist.Add(new Sales(Convert.ToInt32(r["Amount"])
                                , Convert.ToInt32(r["Month"])
                              , Convert.ToString(r["ProductCategory"])
                          ));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesProductCategoryTrendlist;
        }


        public static List<Sales> RetrieveAllProductCategoryShare(string productCategory, DateTime startDate, DateTime endDate)
        {
            List<Sales> salesProductCategorySharelist = new List<Sales>();
            try
            {
                string selectStatement;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (productCategory.Trim() == "")
                    {
                        selectStatement = "select (sum(CAST((S.SalesAmount) as decimal))/(select sum(CAST((S1.SalesAmount) as decimal)) from Sales S1 where S1.ProductCategory!='Select' and S1.IsDeleted=0))*100 as PercentageShare,S.ProductCategory  from Sales S where S.IsDeleted=0 and (FORMAT(S.Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(S.Date,'yyyy-MM-dd')<=@EndDate) and (S.ProductCategory!='Select' )   group by S.ProductCategory";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement = "select(sum(CAST((S.SalesAmount) as decimal)) / (select sum(CAST((S1.SalesAmount) as decimal)) from Sales S1 where S1.ProductCategory != 'Select' and S1.IsDeleted = 0))*100 as PercentageShare,S.ProductCategory from Sales S where S.IsDeleted = 0 and(FORMAT(S.Date, 'yyyy-MM-dd') >= @StartDate AND FORMAT(S.Date, 'yyyy-MM-dd') <= @EndDate) and(S.ProductCategory != 'Select' and S.ProductCategory = @ProductCategory)   group by S.ProductCategory";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@ProductCategory", productCategory);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Sales");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (productCategory.Trim() == "")
                    {
                        while (r.Read())
                        {
                            salesProductCategorySharelist.Add(new Sales(Convert.ToDouble(r["PercentageShare"])
                                ,""
                                , Convert.ToString(r["ProductCategory"])
                            ));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            salesProductCategorySharelist.Add(new Sales(Convert.ToInt32(r["PercentageShare"])
                                , ""
                                , Convert.ToString(r["ProductCategory"])
                            ));
                        }
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return salesProductCategorySharelist;
        }

        #endregion //CRUD

    }

}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace SIMS.BL
{
    public class SpinningProduction
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffId;
        private Double _brokenPercentage;
        private Double _netBrokenPercentage;
        private string _pending;
        private string _achieved;
        private DateTime _date;
        private string _itemName;
        private string _circleSize;
        private DateTime _circleDate;
        private string _circleIssued;
        private string _employeeName;
        private string _fgWeight;
        private string _trimming;
        private string _broken;
        private string _brokenPerc;
        private string _productionFromBroken;
        private string _productFromBroken;
        private string _netBrokenPerc;
        private string _netBroken;
        private string _discrepancy;
        private string _remarks;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int StaffId { get { return _staffId; } set { _staffId = value; } }
        public Double BrokenPercentage { get { return _brokenPercentage; } set { _brokenPercentage = value; } }
        public Double NetBrokenPercentage { get { return _netBrokenPercentage; } set { _netBrokenPercentage = value; } }
        public string Achieved { get { return _achieved; } set { _achieved = value; } }
        public string Pending { get { return _pending; } set { _pending = value; } }

        public DateTime Date { get { return _date; } set { _date = value; } }

        public string ItemName { get { return _itemName; } set { _itemName = value; } }

        public string CircleSize { get { return _circleSize; } set { _circleSize = value; } }

        public DateTime CircleDate { get { return _circleDate; } set { _circleDate = value; } }

        public string CircleIssued { get { return _circleIssued; } set { _circleIssued = value; } }

        public string EmployeeName { get { return _employeeName; } set { _employeeName = value; } }
        public string FGWeight { get { return _fgWeight; } set { _fgWeight = value; } }
        public string Trimming { get { return _trimming; } set { _trimming = value; } }
        public string Broken { get { return _broken; } set { _broken = value; } }

        public string BrokenPerc { get { return _brokenPerc; } set { _brokenPerc = value; } }

        public string ProductionFromBroken { get { return _productionFromBroken; } set { _productionFromBroken = value; } }
        public string ProductFromBroken { get { return _productFromBroken; } set { _productFromBroken = value; } }
        public string NetBroken { get { return _netBroken; } set { _netBroken = value; } }
        public string Discrepancy { get { return _discrepancy; } set { _discrepancy = value; } }

        public string NetBrokenPerc { get { return _netBrokenPerc; } set { _netBrokenPerc = value; } }
        public string Remarks { get { return _remarks; } set { _remarks = value; } }

        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public SpinningProduction()
        {
            _id = 0;
            _date = DateTime.Now;
            _itemName = "";
            _circleSize = "";
            _circleDate = DateTime.Now;
            _circleIssued = "";
            _employeeName = "";
            _fgWeight = "";
            _trimming = "";
            _broken = "";
            _productionFromBroken = "";
            _productFromBroken = "";
            _netBroken = "";
            _discrepancy = "";
            _remarks = "";
            _isdeleted = false;
            _brokenPerc = "";
            _netBrokenPerc = "";
        }

        public SpinningProduction(Double brokenPerc, Double netBrokenPerc)
        {
            _brokenPercentage = brokenPerc;
            _netBrokenPercentage = netBrokenPerc;
        }
        public SpinningProduction(int id, int staffId, DateTime date, string itemName, string circleSize, DateTime circleDate, string circleIssued, string employeeName, string fgWeight, string trimming, string broken, string brokenPerc, string productionFromBroken, string productFromBroken, string netBroken, string netBrokenPerc, string discrepancy, string remarks, bool isdeleted)
        {
            _id = id;
            _staffId = staffId;
            _date = date;
            _itemName = itemName;
            _circleSize = circleSize;
            _circleDate = circleDate;
            _circleIssued = circleIssued;
            _employeeName = employeeName;
            _fgWeight = fgWeight;
            _trimming = trimming;
            _broken = broken;
            _brokenPerc = brokenPerc;
            _productionFromBroken = productionFromBroken;
            _productFromBroken = productFromBroken;
            _netBroken = netBroken;
            _discrepancy = discrepancy;
            _remarks = remarks;
            _isdeleted = false;
            _netBrokenPerc = netBrokenPerc;
        }
        public SpinningProduction( string achieved, string pending)
        {
            _achieved = achieved;
            _pending = pending;
        }
        public SpinningProduction(int staffId, string name)
        {
            _staffId = staffId;
            _employeeName = name;
        }
        #endregion //CTOR

        #region CRUD
        public static int Create(int staffId, DateTime date, string itemName, string circleSize, DateTime circleDate, string circleIssued, string fgWeight, string trimming, string broken, string brokenPercentage, string productionFromBroken, string ProductFromBroken, string netBroken, string netBrokenPercentage, string discrepancy, string remarks, bool isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert SpinningProduction (Date,ItemName,CircleSize,CircleDate,CircleIssued,StaffId,FGWeight,Trimming,Broken,BrokenPercentage,ProductionFromBroken,ProductFromBroken,NetBroken,NetBrokenPercentage,Discrepancy,Remarks,Isdeleted) "
                        + " values (@Date,@ItemName,@CircleSize,@CircleDate,@CircleIssued,@StaffId,@FGWeight,@Trimming,@Broken,@BrokenPercentage,@ProductionFromBroken,@ProductFromBroken,@NetBroken,@NetBrokenPercentage,@Discrepancy,@Remarks,@Isdeleted)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Date", date);
                    manageCommand.Parameters.AddWithValue("@ItemName", itemName);
                    manageCommand.Parameters.AddWithValue("@CircleSize", circleSize);
                    manageCommand.Parameters.AddWithValue("@CircleDate", circleDate);
                    manageCommand.Parameters.AddWithValue("@CircleIssued", circleIssued);
                    manageCommand.Parameters.AddWithValue("@StaffId", staffId);
                    manageCommand.Parameters.AddWithValue("@FGWeight", fgWeight);
                    manageCommand.Parameters.AddWithValue("@Trimming", trimming);
                    manageCommand.Parameters.AddWithValue("@Broken", broken);
                    manageCommand.Parameters.AddWithValue("@BrokenPercentage", brokenPercentage);
                    manageCommand.Parameters.AddWithValue("@ProductionFromBroken", productionFromBroken);
                    manageCommand.Parameters.AddWithValue("@ProductFromBroken", ProductFromBroken);
                    manageCommand.Parameters.AddWithValue("@NetBroken", netBroken);
                    manageCommand.Parameters.AddWithValue("@Discrepancy", discrepancy);
                    manageCommand.Parameters.AddWithValue("@NetBrokenPercentage", netBrokenPercentage);
                    manageCommand.Parameters.AddWithValue("@Remarks", remarks);
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

        public static List<SpinningProduction> RetrieveAll()
        {
            List<SpinningProduction> spinningProductionist = new List<SpinningProduction>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select S.Id,S.Date,S.StaffId,S.ItemName,S.CircleSize,S.CircleDate,S.CircleIssued,U.Name,S.FGWeight,S.Trimming,S.Broken,S.BrokenPercentage,S.ProductionFromBroken,S.ProductFromBroken,S.NetBroken,S.Discrepancy,S.NetBrokenPercentage,S.Remarks,S.Isdeleted from SpinningProduction S,Users U where S.StaffId=U.Id and S.IsDeleted=0 order by S.Date ASC";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SpinningProduction");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        spinningProductionist.Add(new SpinningProduction(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["StaffId"])
                             , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CircleSize"].ToString())
                            , (Convert.IsDBNull(r["CircleDate"]) ? new DateTime() : Convert.ToDateTime(r["CircleDate"].ToString()))
                            , Convert.ToString(r["CircleIssued"].ToString())
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["FGWeight"].ToString())
                            , Convert.ToString(r["Trimming"].ToString())
                            , Convert.ToString(r["Broken"].ToString())
                             , Convert.ToString(r["BrokenPercentage"].ToString())
                            , Convert.ToString(r["ProductionFromBroken"].ToString())
                            , Convert.ToString(r["ProductFromBroken"].ToString())
                            , Convert.ToString(r["NetBroken"].ToString())
                            , Convert.ToString(r["NetBrokenPercentage"].ToString())
                            , Convert.ToString(r["Discrepancy"].ToString())
                             , Convert.ToString(r["Remarks"].ToString())
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
            return spinningProductionist;
        }

        public static List<SpinningProduction> RetrieveAllByUserId(int StaffId)
        {
            List<SpinningProduction> spinningProductionist = new List<SpinningProduction>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select S.Id,S.Date,S.StaffId,S.ItemName,S.CircleSize,S.CircleDate,S.CircleIssued,U.Name,S.FGWeight,S.Trimming,S.Broken,S.BrokenPercentage,S.ProductionFromBroken,S.ProductFromBroken,S.NetBroken,S.Discrepancy,S.NetBrokenPercentage,S.Remarks,S.Isdeleted from SpinningProduction S,Users U where S.StaffId=U.Id and S.IsDeleted=0 and S.StaffId=@StaffId order by S.Date ASC";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SpinningProduction");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        spinningProductionist.Add(new SpinningProduction(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["StaffId"])
                             , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CircleSize"].ToString())
                            , (Convert.IsDBNull(r["CircleDate"]) ? new DateTime() : Convert.ToDateTime(r["CircleDate"].ToString()))
                            , Convert.ToString(r["CircleIssued"].ToString())
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["FGWeight"].ToString())
                            , Convert.ToString(r["Trimming"].ToString())
                            , Convert.ToString(r["Broken"].ToString())
                             , Convert.ToString(r["BrokenPercentage"].ToString())
                            , Convert.ToString(r["ProductionFromBroken"].ToString())
                            , Convert.ToString(r["ProductFromBroken"].ToString())
                            , Convert.ToString(r["NetBroken"].ToString())
                            , Convert.ToString(r["NetBrokenPercentage"].ToString())
                            , Convert.ToString(r["Discrepancy"].ToString())
                             , Convert.ToString(r["Remarks"].ToString())
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
            return spinningProductionist;
        }


        public static SpinningProduction RetrieveById(int Id)
        {
            SpinningProduction result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select S.Id,S.Date,S.StaffId,S.ItemName,S.CircleSize,S.CircleDate,S.CircleIssued,U.Name,S.FGWeight,S.Trimming,S.Broken,S.BrokenPercentage,S.ProductionFromBroken,S.ProductFromBroken,S.NetBroken,S.Discrepancy,S.NetBrokenPercentage,S.Remarks,S.Isdeleted from SpinningProduction S,Users U where S.StaffId=U.Id and S.IsDeleted=0 and S.Id=@Id order by S.Date ASC";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SpinningProduction");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new SpinningProduction(Convert.ToInt32(r["Id"])
                            , Convert.ToInt32(r["StaffId"])
                             , (Convert.IsDBNull(r["Date"]) ? new DateTime() : Convert.ToDateTime(r["Date"].ToString()))
                            , Convert.ToString(r["ItemName"].ToString())
                            , Convert.ToString(r["CircleSize"].ToString())
                            , (Convert.IsDBNull(r["CircleDate"]) ? new DateTime() : Convert.ToDateTime(r["CircleDate"].ToString()))
                            , Convert.ToString(r["CircleIssued"].ToString())
                            , Convert.ToString(r["Name"].ToString())
                            , Convert.ToString(r["FGWeight"].ToString())
                            , Convert.ToString(r["Trimming"].ToString())
                            , Convert.ToString(r["Broken"].ToString())
                            , Convert.ToString(r["BrokenPercentage"].ToString())
                            , Convert.ToString(r["ProductionFromBroken"].ToString())
                            , Convert.ToString(r["ProductFromBroken"].ToString())
                            , Convert.ToString(r["NetBroken"].ToString())
                           , Convert.ToString(r["NetBrokenPercentage"].ToString())
                            , Convert.ToString(r["Discrepancy"].ToString())
                             , Convert.ToString(r["Remarks"].ToString())
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
                    sqlStatement = "Update SpinningProduction Set "
                        + "  Date = @Date "
                        + ", ItemName = @ItemName "
                        + ", CircleSize = @CircleSize "
                        + ", CircleDate = @CircleDate "
                        + ", CircleIssued = @CircleIssued "
                        + ", StaffId = @StaffId "
                         + ", FGWeight = @FGWeight "
                        + ", Trimming = @Trimming "
                        + ", Broken= @Broken"
                        + ", BrokenPercentage= @BrokenPercentage"
                        + ", ProductionFromBroken= @ProductionFromBroken"
                        + ", ProductFromBroken= @ProductFromBroken"
                        + ", NetBroken = @NetBroken "
                        + ", Discrepancy = @Discrepancy "
                        + ",NetBrokenPercentage = @NetBrokenPercentage "
                        + ", Isdeleted = @Isdeleted "
                        + ", Remarks= @Remarks "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Date", this.Date);
                    manageCommand.Parameters.AddWithValue("@ItemName", this.ItemName);
                    manageCommand.Parameters.AddWithValue("@CircleSize", this.CircleSize);
                    manageCommand.Parameters.AddWithValue("@CircleDate", this.CircleDate);
                    manageCommand.Parameters.AddWithValue("@CircleIssued", this.CircleIssued);
                    manageCommand.Parameters.AddWithValue("@StaffId", this.StaffId);
                    manageCommand.Parameters.AddWithValue("@FGWeight", this.FGWeight);
                    manageCommand.Parameters.AddWithValue("@Trimming", this.Trimming);
                    manageCommand.Parameters.AddWithValue("@Broken", this.Broken);
                    manageCommand.Parameters.AddWithValue("@BrokenPercentage", this.BrokenPerc);
                    manageCommand.Parameters.AddWithValue("@ProductionFromBroken", this.ProductionFromBroken);
                    manageCommand.Parameters.AddWithValue("@ProductFromBroken", this.ProductFromBroken);
                    manageCommand.Parameters.AddWithValue("@NetBroken", this.NetBroken);
                    manageCommand.Parameters.AddWithValue("@Discrepancy", this.Discrepancy);
                    manageCommand.Parameters.AddWithValue("@NetBrokenPercentage", this.NetBrokenPerc);
                    manageCommand.Parameters.AddWithValue("@Remarks", this.Remarks);
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
                string sqlStatement = "Update SpinningProduction Set IsDeleted = 1 where Id = @Id ";
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

        public static List<SpinningProduction> RetrieveSpinningProductionChartUserIdList()
        {
            List<SpinningProduction> spinningProductionUserIdList = new List<SpinningProduction>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "select distinct S.StaffId,U.Name from SpinningProduction S,Users U where S.StaffId=U.Id and S.IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SpinningProduction");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        spinningProductionUserIdList.Add(new SpinningProduction(Convert.ToInt32(r["StaffId"]), Convert.ToString(r["Name"])

                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return spinningProductionUserIdList;
        }
        public static List<SpinningProduction> RetrieveChartData(int id, DateTime startDate, DateTime endDate)
        {
            List<SpinningProduction> spinningProductionlist = new List<SpinningProduction>();
            try
            {
                string selectStatement, selectStatement1;
                SqlCommand manageCommand;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    if (id == 0)
                    {
                        selectStatement= " select (sum(CAST(S.CircleIssued AS float)) - sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float))) as Achieved, " +
                                                 " CASE WHEN(sum(CAST(S.CircleIssued AS float))-sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float)))>= (select sum(Target) from Users U where U.IsDeleted = 0 and U.Id IN(select Distinct StaffId from SpinningProduction where IsDeleted = 0))THEN 0 " +
                                                 " WHEN(select sum(Target) from Users U where U.IsDeleted = 0 and U.Id IN(select Distinct StaffId from SpinningProduction where IsDeleted = 0)) > (sum(CAST(S.CircleIssued AS float)) - sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float))) THEN((select sum(Target) from Users U where U.IsDeleted = 0 and U.Id IN(select Distinct StaffId from SpinningProduction where IsDeleted = 0))) -(sum(CAST(S.CircleIssued AS float)) - sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float))) " +
                                                 " END AS Pending " +
                                                 " from SpinningProduction S where S.IsDeleted = 0 and(FORMAT(S.Date, 'yyyy-MM-dd') >= @StartDate AND FORMAT(S.Date, 'yyyy-MM-dd') <= @EndDate) ";

                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement1 = " select U.Name,U.ID,(sum(CAST(S.CircleIssued AS float))-sum(CAST(S.NetBroken AS float))-sum(CAST(Trimming AS float))) as Achieved, " +
                                                              " CASE WHEN(sum(CAST(S.CircleIssued AS float))-sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float)))>= (U.Target)THEN 0 " +
                                                              " WHEN(U.Target) > (sum(CAST(S.CircleIssued AS float)) - sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float))) THEN((U.Target)) - (sum(CAST(S.CircleIssued AS float)) - sum(CAST(S.NetBroken AS float)) - sum(CAST(Trimming AS float))) " +
                                                              " END AS Pending " +
                                                              " from SpinningProduction S, Users U where S.StaffId = U.ID and StaffId=@StaffId and S.IsDeleted = 0 and (FORMAT(S.Date, 'yyyy-MM-dd') >= @StartDate AND FORMAT(S.Date, 'yyyy-MM-dd') <= @EndDate) group by U.Name,U.ID,U.Target ";

                        manageCommand = new SqlCommand(selectStatement1, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", id);
                    }

                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SpinningProduction");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        spinningProductionlist.Add(new SpinningProduction( (r["Achieved"].ToString()) == null ? null : Convert.ToString(r["Achieved"])
                            , (r["Pending"].ToString()) == null ? null : Convert.ToString(r["Pending"])
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return spinningProductionlist;
        }



        public static List<SpinningProduction> RetrieveSpinningRejection(int id, DateTime startDate, DateTime endDate)
        {
            List<SpinningProduction> spinningProductionlist = new List<SpinningProduction>();
            try
            {
                string selectStatement, selectStatement1;
                SqlCommand manageCommand;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (id == 0)
                    {
                        selectStatement = "select (sum(Cast(Broken  as float))/sum(CAST(CircleIssued  as float)))*100 as BrokenPercentage,(sum(CAST(NetBroken  as float))/ sum(CAST(CircleIssued  as float)))*100 AS NetBrokenPecentage  from SpinningProduction where (FORMAT(Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(Date,'yyyy-MM-dd')<=@EndDate) and IsDeleted=0";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement1 = "select (sum(Cast(Broken  as float))/sum(CAST(CircleIssued  as float)))*100 as BrokenPercentage,(sum(CAST(NetBroken  as float))/ sum(CAST(CircleIssued  as float)))*100 AS NetBrokenPecentage,U.Name  from SpinningProduction S,Users U where (FORMAT(Date,'yyyy-MM-dd')>=@StartDate AND FORMAT(Date,'yyyy-MM-dd')<=@EndDate) and S.IsDeleted=0 and S.StaffId=U.ID and S.StaffId=@StaffId group by U.Name";
                        manageCommand = new SqlCommand(selectStatement1, connection);
                        manageCommand.Parameters.AddWithValue("@StaffId", id);
                    }
                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "SpinningProduction");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        spinningProductionlist.Add(new SpinningProduction((r["BrokenPercentage"])!=DBNull.Value?Convert.ToDouble(r["BrokenPercentage"]):0,
                                                                          (r["NetBrokenPecentage"]) != DBNull.Value ? Convert.ToDouble(r["NetBrokenPecentage"]) : 0
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return spinningProductionlist;
        }
        #endregion //CRUD
    }
}

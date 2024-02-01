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
    public class VehicleLog
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffid;
        private DateTime _timeStamp;
        private string _amount;
        private string _vehicleNo;
        private string _startingPoint;
        private string _destination;
        private string _purpose;
        private string _startReading;
        private string _endReading;
        private string _fuelFilled;
        private string _fuelQuantity;
        private string _userName;
        private string _userType;
        //    private string _billAmount;
        private string _voucher;

        private string _remarks;
        private string _month;
        private string _distanceTravelled;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int StaffId { get { return _staffid; } set { _staffid = value; } }
        public DateTime TimeStamp { get { return _timeStamp; } set { _timeStamp = value; } }
        public string VehicleNo { get { return _vehicleNo; } set { _vehicleNo = value; } }
        public string StartingPoint { get { return _startingPoint; } set { _startingPoint = value; } }
        public string Destination { get { return _destination; } set { _destination = value; } }
        public string Purpose { get { return _purpose; } set { _purpose = value; } }
        public string StartReading { get { return _startReading; } set { _startReading = value; } }
        public string EndReading { get { return _endReading; } set { _endReading = value; } }
        public string FuelFilled { get { return _fuelFilled; } set { _fuelFilled = value; } }
        public string FuelQuantity { get { return _fuelQuantity; } set { _fuelQuantity = value; } }
        public string Amount { get { return _amount; } set { _amount = value; } }
        public string Voucher { get { return _voucher; } set { _voucher = value; } }
        public string Remarks { get { return _remarks; } set { _remarks = value; } }
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string UserType { get { return _userType; } set { _userType = value; } }

        public string Month { get { return _month; } set { _month = value; } }
        public string DistanceTravelled { get { return _distanceTravelled; } set { _distanceTravelled = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        public VehicleLog()
        {
            _id = 0;
            _staffid = 0;
            _timeStamp = DateTime.Now;
            _vehicleNo = "";
            _startingPoint = "";
            _destination = "";
            _purpose = "";
            _startReading = "";
            _endReading = "";
            _fuelFilled = "";
            _fuelQuantity = "";
            _amount = "";
            _voucher = "";
            _remarks = "";
            _isdeleted = false;
            _userName = "";
            _userType = "";
            _month = "";
            _distanceTravelled = "";
        }


        public VehicleLog(int Id, int StaffId, DateTime TimeStamp, string VehicleNo, string StartingPoint, string Destination, string Purpose, string StartReading, string EndReading, string FuelFilled, string FuelQuantity, string Amount, string Voucher, string Remarks, string UserName, string UserType, bool Isdeleted)
        {
            _id = Id;
            _staffid = StaffId;
            _timeStamp = TimeStamp;
            _vehicleNo = VehicleNo;
            _amount = Amount;
            _startingPoint = StartingPoint;
            _destination = Destination;
            _purpose = Purpose;
            _startReading = StartReading;
            _endReading = EndReading;
            _fuelFilled = FuelFilled;
            _fuelQuantity = FuelQuantity;
            _voucher = Voucher;
            _remarks = Remarks;
            _isdeleted = false;
            _userName = UserName;
            _userType = UserType;
        }

        public VehicleLog(string vehicleNo, string distance, string month)
        {
            _vehicleNo = vehicleNo;
            _distanceTravelled = distance;
            _month = month;
        }
        #region CRUD
        public static int Create(int StaffId, DateTime TimeStamp, string VehicleNo, string StartingPoint, string Destination, string Purpose, string StartReading, string EndReading, string FuelFilled, string FuelQuantity, string Amount, string Voucher, string Remarks)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement;
                    SqlCommand manageCommand;
                    sqlStatement = "Insert VehicleLog ( StaffId,TimeStamp, VehicleNo,Amount,StartingPoint,Destination,Purpose,StartReading,EndReading,FuelFilled,FuelQuantity,Voucher,Remarks) "
                        + " values ( @StaffId,@TimeStamp, @VehicleNo,@Amount,@StartingPoint,@Destination,@Purpose,@StartReading,@EndReading,@FuelFilled,@FuelQuantity,@Voucher,@Remarks)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    manageCommand.Parameters.AddWithValue("@TimeStamp", TimeStamp);
                    manageCommand.Parameters.AddWithValue("@VehicleNo", VehicleNo);
                    manageCommand.Parameters.AddWithValue("@Amount", Amount);
                    manageCommand.Parameters.AddWithValue("@StartingPoint", StartingPoint);
                    manageCommand.Parameters.AddWithValue("@Destination", Destination);
                    manageCommand.Parameters.AddWithValue("@Purpose", Purpose);
                    manageCommand.Parameters.AddWithValue("@StartReading", StartReading);
                    manageCommand.Parameters.AddWithValue("@EndReading", EndReading);
                    manageCommand.Parameters.AddWithValue("@FuelFilled", FuelFilled);
                    manageCommand.Parameters.AddWithValue("@FuelQuantity", FuelQuantity);
                    manageCommand.Parameters.AddWithValue("@Voucher", Voucher);
                    manageCommand.Parameters.AddWithValue("@Remarks", Remarks);

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

        public static List<VehicleLog> RetrieveAll()
        {
            List<VehicleLog> expenseReportlist = new List<VehicleLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select  V.Id, V.StaffId,V.TimeStamp, V.VehicleNo,V.Amount,V.Purpose,V.StartingPoint,V.Destination,V.StartReading,V.EndReading,V.FuelFilled,V.FuelQuantity,V.Voucher,V.Remarks, V.IsDeleted,U.Name,U.UserType from VehicleLog V,Users U where V.IsDeleted=0 and U.Id=V.StaffId order by V.TimeStamp desc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VehicleLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        expenseReportlist.Add(new VehicleLog(Convert.ToInt32(r["Id"]),
                                                Convert.ToInt32(r["StaffId"]),
                                                (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString())),
                                               Convert.ToString(r["VehicleNo"].ToString()),

                                               Convert.ToString(r["StartingPoint"].ToString()),
                                               Convert.ToString(r["Destination"].ToString()),
                                               Convert.ToString(r["Purpose"].ToString()),
                                               Convert.ToString(r["StartReading"].ToString()),
                                               Convert.ToString(r["EndReading"].ToString()),
                                               Convert.ToString(r["FuelFilled"].ToString()),
                                               Convert.ToString(r["FuelQuantity"].ToString()),
                                                Convert.ToString(r["Amount"].ToString()),
                                               Convert.ToString(r["Voucher"].ToString()),
                                               Convert.ToString(r["Remarks"].ToString()),
                                                Convert.ToString(r["Name"].ToString()),
                                                 Convert.ToString(r["UserType"].ToString()),
                                               Convert.ToBoolean(r["IsDeleted"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return expenseReportlist;
        }

        public static List<VehicleLog> RetrieveAllByUserId(int StaffId)
        {
            List<VehicleLog> expenseReportlist = new List<VehicleLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select  V.Id, V.StaffId,V.TimeStamp, V.VehicleNo,V.Amount,V.Purpose,V.StartingPoint,V.Destination,V.StartReading,V.EndReading,V.FuelFilled,V.FuelQuantity,V.Voucher,V.Remarks, V.IsDeleted,U.Name,U.UserType from VehicleLog V,Users U where V.IsDeleted=0 and U.Id=V.StaffId and V.StaffId=@StaffId order by V.TimeStamp desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VehicleLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        expenseReportlist.Add(new VehicleLog(Convert.ToInt32(r["Id"]),
                                                Convert.ToInt32(r["StaffId"]),
                                                (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString())),
                                               Convert.ToString(r["VehicleNo"].ToString()),

                                               Convert.ToString(r["StartingPoint"].ToString()),
                                               Convert.ToString(r["Destination"].ToString()),
                                               Convert.ToString(r["Purpose"].ToString()),
                                               Convert.ToString(r["StartReading"].ToString()),
                                               Convert.ToString(r["EndReading"].ToString()),
                                               Convert.ToString(r["FuelFilled"].ToString()),
                                               Convert.ToString(r["FuelQuantity"].ToString()),
                                                Convert.ToString(r["Amount"].ToString()),
                                               Convert.ToString(r["Voucher"].ToString()),
                                               Convert.ToString(r["Remarks"].ToString()),
                                                Convert.ToString(r["Name"].ToString()),
                                                 Convert.ToString(r["UserType"].ToString()),
                                               Convert.ToBoolean(r["IsDeleted"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed", ex);
            }
            return expenseReportlist;
        }


        public static VehicleLog RetrieveById(int Id)
        {
            VehicleLog result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string selectStatement = "Select V.Id, V.StaffId,V.TimeStamp, V.VehicleNo,V.Amount,V.Purpose,V.StartingPoint,V.Destination,V.StartReading,V.EndReading,V.FuelFilled,V.FuelQuantity,V.Voucher,V.Remarks, V.IsDeleted,U.Name,U.UserType from VehicleLog V,Users U where V.IsDeleted=0 and U.Id=V.StaffId and V.Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VehicleLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new VehicleLog(Convert.ToInt32(r["Id"]),
                                                Convert.ToInt32(r["StaffId"]),
                                                (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString())),
                                               Convert.ToString(r["VehicleNo"].ToString()),

                                               Convert.ToString(r["StartingPoint"].ToString()),
                                               Convert.ToString(r["Destination"].ToString()),
                                               Convert.ToString(r["Purpose"].ToString()),
                                               Convert.ToString(r["StartReading"].ToString()),
                                               Convert.ToString(r["EndReading"].ToString()),
                                               Convert.ToString(r["FuelFilled"].ToString()),
                                               Convert.ToString(r["FuelQuantity"].ToString()),
                                                Convert.ToString(r["Amount"].ToString()),
                                               Convert.ToString(r["Voucher"].ToString()),
                                               Convert.ToString(r["Remarks"].ToString()),
                                                Convert.ToString(r["Name"].ToString()),
                                                 Convert.ToString(r["UserType"].ToString()),
                                               Convert.ToBoolean(r["IsDeleted"].ToString())
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement;
                    SqlCommand manageCommand;
                    sqlStatement = "Update VehicleLog Set "
                                            + "  StaffId = @StaffId "
                        + " ,Timestamp = @Timestamp "
                        + ", VehicleNo = @VehicleNo "
                                            + ", Amount = @Amount "
                        + ", StartingPoint = @StartingPoint "
                        + ", Destination = @Destination "
                                            + ", Purpose = @Purpose "
                                            + ", StartReading = @StartReading "
                        + ", EndReading = @EndReading "
                        + ", FuelFilled = @FuelFilled "
                        + ", FuelQuantity = @FuelQuantity "
                        + ", Voucher = @Voucher "
                        + ", Remarks = @Remarks "
                        + ", Isdeleted = @Isdeleted "
                        + "  where ID= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@StaffId", this.StaffId);
                    manageCommand.Parameters.AddWithValue("@Timestamp", this.TimeStamp);
                    manageCommand.Parameters.AddWithValue("@VehicleNo", this.VehicleNo);
                    manageCommand.Parameters.AddWithValue("@Amount", this.Amount);
                    manageCommand.Parameters.AddWithValue("@StartingPoint", this.StartingPoint);
                    manageCommand.Parameters.AddWithValue("@Destination", this.Destination);
                    manageCommand.Parameters.AddWithValue("@Purpose", this.Purpose);
                    manageCommand.Parameters.AddWithValue("@StartReading", this.StartReading);
                    manageCommand.Parameters.AddWithValue("@EndReading", this.EndReading);
                    manageCommand.Parameters.AddWithValue("@FuelFilled", this.FuelFilled);
                    manageCommand.Parameters.AddWithValue("@FuelQuantity", this.FuelQuantity);
                    manageCommand.Parameters.AddWithValue("@Voucher", this.Voucher);
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlStatement = "Update VehicleLog Set isDeleted = 1 where Id = @Id ";
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
        public static List<VehicleLog> RetrieveVehicleLogChartData(string vehicleNo, DateTime startDate, DateTime endDate)
        {


            List<VehicleLog> VehicleLogList = new List<VehicleLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "";
                    SqlCommand manageCommand = new SqlCommand();
                    if (vehicleNo.Trim() == "")
                    {
                        selectStatement = "select(sum(CAST(V.EndReading as numeric)) - sum(Cast(V.StartReading as numeric))) as DistanceTravelled,MONTH(V.TimeStamp) as Month from VehicleLog V where (FORMAT(V.TimeStamp, 'yyyy-MM-dd') >= @StartDate AND FORMAT(V.TimeStamp,'yyyy-MM-dd')<= @EndDate) and V.IsDeleted = 0 group by MONTH(V.TimeStamp),YEAR(V.TimeStamp)";
                        manageCommand = new SqlCommand(selectStatement, connection);
                    }
                    else
                    {
                        selectStatement = "select V.VehicleNo,(sum(CAST(V.EndReading as numeric))-sum(Cast(V.StartReading as numeric))) as DistanceTravelled,MONTH(V.TimeStamp) as Month from VehicleLog V where V.VehicleNo=@VehicleNo and (FORMAT(V.TimeStamp,'yyyy-MM-dd')>=@StartDate AND FORMAT(V.TimeStamp,'yyyy-MM-dd')<=@EndDate) and V.IsDeleted=0 group by V.VehicleNo,MONTH(V.TimeStamp),YEAR(V.TimeStamp)";
                        manageCommand = new SqlCommand(selectStatement, connection);
                        manageCommand.Parameters.AddWithValue("@VehicleNo", vehicleNo);
                    }


                    manageCommand.Parameters.AddWithValue("@StartDate", startDate);
                    manageCommand.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "VehicleLog");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (vehicleNo.Trim() == "")
                    {
                        while (r.Read())
                        {
                            VehicleLogList.Add(new VehicleLog(""
                                , (r["DistanceTravelled"].ToString()) == null ? null : Convert.ToString(r["DistanceTravelled"])
                                , (r["Month"].ToString()) == null ? null : Convert.ToString(r["Month"])
                            ));
                        }
                    }
                    else
                    {
                        while (r.Read())
                        {
                            VehicleLogList.Add(new VehicleLog(Convert.ToString(r["VehicleNo"].ToString())
                                , (r["DistanceTravelled"].ToString()) == null ? null : Convert.ToString(r["DistanceTravelled"])
                                , (r["Month"].ToString()) == null ? null : Convert.ToString(r["Month"])
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
            return VehicleLogList;
        }
        #endregion //CRUD
    }
}

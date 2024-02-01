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
    public class ExpenseReport
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _staffid;
        private DateTime _timeStamp;
        private string _transportVehicle;
        private string _amount;
        private string _startingPoint;
        private string _destination;
        private string _distance;
        private string _parkingCharge;
        private string _tollOrFineCharge;
        private string _tollOrFineDetails;
        private string _foodCharge;
        private string _remarks;
        private string _username;
        private string _userType;
        private bool _isdeleted;

        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int StaffId { get { return _staffid; } set { _staffid = value; } }
        public DateTime TimeStamp { get { return _timeStamp; } set { _timeStamp = value; } }
        public string TransportVehicle { get { return _transportVehicle; } set { _transportVehicle = value; } }
        public string Amount { get { return _amount; } set { _amount = value; } }
        public string StartingPoint { get { return _startingPoint; } set { _startingPoint = value; } }
        public string Destination { get { return _destination; } set { _destination = value; } }
        public string Distance { get { return _distance; } set { _distance = value; } }
        public string FoodCharge { get { return _foodCharge; } set { _foodCharge = value; } }
        public string ParkingCharge { get { return _parkingCharge; } set { _parkingCharge = value; } }
        public string TollOrFineCharge { get { return _tollOrFineCharge; } set { _tollOrFineCharge = value; } }
        public string TollOrFineDetails { get { return _tollOrFineDetails; } set { _tollOrFineDetails = value; } }
        public string Remarks { get { return _remarks; } set { _remarks = value; } }
        public string UserName { get { return _username; } set { _username = value; } }
        public string UserType { get { return _userType; } set { _userType = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        public ExpenseReport()
        {
            _id = 0;
            _staffid = 0;
            _timeStamp = DateTime.Now;
            _transportVehicle = "";
            _amount = "";
            _startingPoint = "";
            _destination = "";
            _distance = "";
            _foodCharge = "";
            _parkingCharge = "";
            _tollOrFineCharge = "";
            _tollOrFineDetails = "";
            _remarks = "";
            _isdeleted = false;
            _username = "";
            _userType = "";
        }


        public ExpenseReport(int Id, int StaffId, DateTime TimeStamp, string TransportVehicle, string Amount, string StartingPoint, string Destination, string Distance, string FoodCharge, string ParkingCharge, string TollOrFineCharge, string TollOrFineDetails, string Remarks, string UserName, string UserType, bool Isdeleted)
        {
            _id = Id;
            _staffid = StaffId;
            _timeStamp = TimeStamp;
            _transportVehicle = TransportVehicle;
            _amount = Amount;
            _startingPoint = StartingPoint;
            _destination = Destination;
            _distance = Distance;
            _foodCharge = FoodCharge;
            _parkingCharge = ParkingCharge;
            _tollOrFineCharge = TollOrFineCharge;
            _tollOrFineDetails = TollOrFineDetails;
            _remarks = Remarks;
            _isdeleted = false;
            _username = UserName;
            _userType = UserType;
        }


        #region CRUD
        public static int Create(int StaffId, DateTime TimeStamp, string TransportVehicle, string Amount, string StartingPoint, string Destination, string Distance, string FoodCharge, string ParkingCharge, string TollOrFineCharge, string TollOrFineDetails, string Remarks)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert ExpenseReport ( StaffId,TimeStamp, TransportVehicle,Amount,StartingPoint,Destination,Distance,FoodCharge,ParkingCharge,TollOrFineCharge,TollOrFineDetails,Remarks) "
                        + " values ( @StaffId,@TimeStamp, @TransportVehicle,@Amount,@StartingPoint,@Destination,@Distance,@FoodCharge,@ParkingCharge,@TollOrFineCharge,@TollOrFineDetails,@Remarks)";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    manageCommand.Parameters.AddWithValue("@TimeStamp", TimeStamp);
                    manageCommand.Parameters.AddWithValue("@TransportVehicle", TransportVehicle);
                    manageCommand.Parameters.AddWithValue("@Amount", Amount);
                    manageCommand.Parameters.AddWithValue("@StartingPoint", StartingPoint);
                    manageCommand.Parameters.AddWithValue("@Destination", Destination);
                    manageCommand.Parameters.AddWithValue("@Distance", Distance);
                    manageCommand.Parameters.AddWithValue("@FoodCharge", FoodCharge);
                    manageCommand.Parameters.AddWithValue("@ParkingCharge", ParkingCharge);
                    manageCommand.Parameters.AddWithValue("@TollOrFineCharge", TollOrFineCharge);
                    manageCommand.Parameters.AddWithValue("@TollOrFineDetails", TollOrFineDetails);
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

        public static List<ExpenseReport> RetrieveAll()
        {
            List<ExpenseReport> expenseReportlist = new List<ExpenseReport>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select E.Id,E.StaffId,E.TimeStamp, E.TransportVehicle,E.Amount,E.StartingPoint,E.Destination,E.Distance,E.FoodCharge,E.ParkingCharge,E.TollOrFineCharge,E.TollOrFineDetails,E.Remarks,E.IsDeleted,U.Name,U.UserType from ExpenseReport E,Users U where E.IsDeleted=0 and U.Id=E.StaffId order by E.TimeStamp desc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "ExpenseReport");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        expenseReportlist.Add(new ExpenseReport(Convert.ToInt32(r["Id"]),
                                                Convert.ToInt32(r["StaffId"]),
                                                (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString())),
                                               Convert.ToString(r["TransportVehicle"].ToString()),
                                               Convert.ToString(r["Amount"].ToString()),
                                               Convert.ToString(r["StartingPoint"].ToString()),
                                               Convert.ToString(r["Destination"].ToString()),
                                               Convert.ToString(r["Distance"].ToString()),
                                               Convert.ToString(r["FoodCharge"].ToString()),
                                               Convert.ToString(r["ParkingCharge"].ToString()),
                                               Convert.ToString(r["TollOrFineCharge"].ToString()),
                                               Convert.ToString(r["TollOrFineDetails"].ToString()),
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
        public static List<ExpenseReport> RetrieveAllByUserId(int StaffId)
        {
            List<ExpenseReport> expenseReportlist = new List<ExpenseReport>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select E.Id,E.StaffId,E.TimeStamp, E.TransportVehicle,E.Amount,E.StartingPoint,E.Destination,E.Distance,E.FoodCharge,E.ParkingCharge,E.TollOrFineCharge,E.TollOrFineDetails,E.Remarks,E.IsDeleted,U.Name,U.UserType from ExpenseReport E,Users U where E.IsDeleted=0 and U.Id=E.StaffId and E.StaffId=@StaffId order by E.TimeStamp desc";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@StaffId", StaffId);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "ExpenseReport");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        expenseReportlist.Add(new ExpenseReport(Convert.ToInt32(r["Id"]),
                                                Convert.ToInt32(r["StaffId"]),
                                                (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString())),
                                               Convert.ToString(r["TransportVehicle"].ToString()),
                                               Convert.ToString(r["Amount"].ToString()),
                                               Convert.ToString(r["StartingPoint"].ToString()),
                                               Convert.ToString(r["Destination"].ToString()),
                                               Convert.ToString(r["Distance"].ToString()),
                                               Convert.ToString(r["FoodCharge"].ToString()),
                                               Convert.ToString(r["ParkingCharge"].ToString()),
                                               Convert.ToString(r["TollOrFineCharge"].ToString()),
                                               Convert.ToString(r["TollOrFineDetails"].ToString()),
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



        public static ExpenseReport RetrieveById(int Id)
        {
            ExpenseReport result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select E.Id,E.StaffId,E.TimeStamp, E.TransportVehicle,E.Amount,E.StartingPoint,E.Destination,E.Distance,E.FoodCharge,E.ParkingCharge,E.TollOrFineCharge,E.TollOrFineDetails,E.Remarks,E.IsDeleted,U.Name,U.UserType from ExpenseReport E,Users U where E.IsDeleted=0 and U.Id=E.StaffId and E.Id=@Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "ExpenseReport");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new ExpenseReport(Convert.ToInt32(r["Id"]),
                                            Convert.ToInt32(r["StaffId"]),
                                                (Convert.IsDBNull(r["Timestamp"]) ? new DateTime() : Convert.ToDateTime(r["Timestamp"].ToString())),
                                               Convert.ToString(r["TransportVehicle"].ToString()),
                                               Convert.ToString(r["Amount"].ToString()),
                                               Convert.ToString(r["StartingPoint"].ToString()),
                                               Convert.ToString(r["Destination"].ToString()),
                                               Convert.ToString(r["Distance"].ToString()),
                                               Convert.ToString(r["FoodCharge"].ToString()),
                                               Convert.ToString(r["ParkingCharge"].ToString()),
                                               Convert.ToString(r["TollOrFineCharge"].ToString()),
                                               Convert.ToString(r["TollOrFineDetails"].ToString()),
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
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Update ExpenseReport Set "
                                            + "  StaffId = @StaffId "
                        + " ,Timestamp = @Timestamp "
                        + ", TransportVehicle = @TransportVehicle "
                                            + ", Amount = @Amount "
                        + ", StartingPoint = @StartingPoint "
                        + ", Destination = @Destination "
                                            + ", Distance = @Distance "
                        + ", FoodCharge = @FoodCharge "
                        + ", ParkingCharge = @ParkingCharge "
                        + ", TollOrFineCharge = @TollOrFineCharge "
                        + ", TollOrFineDetails = @TollOrFineDetails "
                        + ", Remarks = @Remarks "
                        + ", Isdeleted = @Isdeleted "
                        + "  where ID= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@StaffId", this.StaffId);
                    manageCommand.Parameters.AddWithValue("@Timestamp", this.TimeStamp);
                    manageCommand.Parameters.AddWithValue("@TransportVehicle", this.TransportVehicle);
                    manageCommand.Parameters.AddWithValue("@Amount", this.Amount);
                    manageCommand.Parameters.AddWithValue("@StartingPoint", this.StartingPoint);
                    manageCommand.Parameters.AddWithValue("@Destination", this.Destination);
                    manageCommand.Parameters.AddWithValue("@Distance", this.Distance);
                    manageCommand.Parameters.AddWithValue("@FoodCharge", this.FoodCharge);
                    manageCommand.Parameters.AddWithValue("@ParkingCharge", this.ParkingCharge);
                    manageCommand.Parameters.AddWithValue("@TollOrFineCharge", this.TollOrFineCharge);
                    manageCommand.Parameters.AddWithValue("@TollOrFineDetails", this.TollOrFineDetails);
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
                string sqlStatement = "Update ExpenseReport Set isDeleted = 1 where Id = @Id ";
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

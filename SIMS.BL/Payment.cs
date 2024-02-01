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

    public class Payment
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _pendingAmount;
        private string _expense;
        private string _frequency;
        private int _amount;
        private string _narration;
        private DateTime _dueDate;
        private DateTime _paidDate;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int PendingAmount { get { return _pendingAmount; } set { _pendingAmount = value; } }
        public string Expense { get { return _expense; } set { _expense = value; } }
        public string Frequency { get { return _frequency; } set { _frequency = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        public string Narration { get { return _narration; } set { _narration = value; } }
        public DateTime DueDate { get { return _dueDate; } set { _dueDate = value; } }
        public DateTime PaidDate { get { return _paidDate; } set { _paidDate = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Payment()
        {
            _id = 0;
            _expense = "";
            _frequency = "";
            _amount = 0;
            _dueDate = DateTime.Now;
            _paidDate = DateTime.Now;
            _isdeleted = false;
            _narration = "";
        }

        public Payment(int pendingAmount)
        {
            _pendingAmount = pendingAmount;
        }

        public Payment(int id, string expense,string frequency,int amount,string narration, DateTime dueDate, DateTime PaidDate, bool isdeleted)
        {
            _id = id;
            _expense = expense;
            _frequency = frequency;
            _amount = amount;
            _narration = narration;
             _dueDate = dueDate;
            _paidDate = PaidDate;
            _isdeleted = isdeleted;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(string expense, string frequency, int amount, string narration, DateTime dueDate, bool isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert into payment (Expense,Frequency,Amount,Narration,DueDate,Isdeleted) "
                        + " values (@Expense,@Frequency,@Amount,@Narration,@DueDate,@Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Expense", expense);
                    manageCommand.Parameters.AddWithValue("@Frequency", frequency);
                    manageCommand.Parameters.AddWithValue("@Amount", amount);
                    manageCommand.Parameters.AddWithValue("@Narration", narration);
                    manageCommand.Parameters.AddWithValue("@DueDate", dueDate);
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

        public static List<Payment> RetrieveAll()
        {
            List<Payment> paymentList = new List<Payment>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Expense,Frequency,Amount,Narration,DueDate,PaidDate,Isdeleted from Payment Where Isdeleted=0 ORDER BY DueDate ASC ";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Payment");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        paymentList.Add(new Payment(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Expense"].ToString())
                             , Convert.ToString(r["Frequency"].ToString())
                            , Convert.ToInt32(r["Amount"].ToString())
                             , Convert.ToString(r["Narration"].ToString())
                            , (Convert.IsDBNull(r["DueDate"]) ? new DateTime() : Convert.ToDateTime(r["DueDate"].ToString()))
                            , (Convert.IsDBNull(r["PaidDate"]) ? DateTime.MinValue : Convert.ToDateTime(r["PaidDate"].ToString()))
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed"+ex.Message);
            }
            return paymentList;
        }


        public static Payment RetrieveById(int Id)
        {
            Payment result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Expense,Frequency,Amount,Narration,DueDate,PaidDate,Isdeleted from Payment Where Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Payment");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Payment(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Expense"].ToString())
                             , Convert.ToString(r["Frequency"].ToString())
                            , Convert.ToInt32(r["Amount"].ToString())
                            , Convert.ToString(r["Narration"].ToString())
                            , (Convert.IsDBNull(r["DueDate"]) ? new DateTime() : Convert.ToDateTime(r["DueDate"].ToString()))
                            , (Convert.IsDBNull(r["PaidDate"]) ? new DateTime() : Convert.ToDateTime(r["PaidDate"].ToString()))
                            , Convert.ToBoolean(r["Isdeleted"].ToString())
                        );
                    }
                    r.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed"+ex.Message);
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
                    sqlStatement = "Update Payment Set "
                        + "  Expense = @Expense "
                        + ", Frequency = @Frequency "
                        + ", Amount = @Amount "
                        + ", Narration = @Narration "
                        + ", DueDate = @DueDate " 
                        + ", PaidDate = @PaidDate "
                        + ", IsDeleted = @IsDeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@Expense", this.Expense);
                    manageCommand.Parameters.AddWithValue("@Frequency", this.Frequency);
                    manageCommand.Parameters.AddWithValue("@Amount", this.Amount);
                    manageCommand.Parameters.AddWithValue("@Narration", this.Narration);
                    manageCommand.Parameters.AddWithValue("@DueDate", this.DueDate);
                    manageCommand.Parameters.AddWithValue("@PaidDate", this.PaidDate);
                    manageCommand.Parameters.AddWithValue("@IsDeleted", this.Isdeleted);
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
                string sqlStatement = "Update Payment Set IsDeleted = 1 where Id = @Id ";
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


        public static int RetrievePendingPaymentAmount()
        {
            int paymentPending = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "select  sum(Cast(P.Amount as numeric)) As PaymentPending from Payment P where P.IsDeleted=0 and (P.PaidDate IS NULL) ";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Payment");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        paymentPending = Convert.ToInt32(r["PaymentPending"]);
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed" + ex.Message);
            }
            return paymentPending;
        }


        #endregion //CRUD

    }

}
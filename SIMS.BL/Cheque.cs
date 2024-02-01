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

    public class Cheque
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private int _pendingCount;
        private int _pendingAmount;
        private string _chequeNo;
        private DateTime _chequeDate;
        private string _partyName;
        private string _amount;
        private string _bankName;
        private DateTime _clearingDate;
        private string _status;
        private string _remark;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }
        public int PendingCount { get { return _pendingCount; } set { _pendingCount = value; } }
        public int PendingAmount { get { return _pendingAmount; } set { _pendingAmount = value; } }
        public string ChequeNo { get { return _chequeNo; } set { _chequeNo = value; } }
        public DateTime ChequeDate { get { return _chequeDate; } set { _chequeDate = value; } }
        public string PartyName { get { return _partyName; } set { _partyName = value; } }
        public string Amount { get { return _amount; } set { _amount = value; } }
        public string BankName { get { return _bankName; } set { _bankName = value; } }
        public DateTime ClearingDate { get { return _clearingDate; } set { _clearingDate = value; } }
        public string Status { get { return _status; } set { _status = value; } }
        public string Remark { get { return _remark; } set { _remark = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public Cheque()
        {
            _id = 0;
            _chequeNo = "";
            _partyName = "";
            _amount = "";
            _bankName = "";
            _status = "Pending";
            _chequeDate = DateTime.Now;
            _clearingDate = DateTime.Now;
            _remark = "";
            _isdeleted = false;
        }

        public Cheque(int pendingCount,int pendingAmount)
        {
            _pendingCount = pendingCount;
            _pendingAmount = pendingAmount;
        }
        public Cheque(int id, string chequeNo,DateTime chequeDate, string partyName, string amount,string bankName, DateTime clearingDate,string status,string remark, bool isdeleted)
        {
            _id = id;
            _chequeNo = chequeNo;
            _chequeDate = chequeDate;
            _partyName = partyName;
             _amount = amount;
            _bankName = bankName;
            _clearingDate = clearingDate;
            _status = status;
            _remark = remark;
            _isdeleted = isdeleted;
        }
      
        #endregion //CTOR

        #region CRUD
        public static int Create(string chequeNo, DateTime chequeDate, string partyName, string amount, string bankName,string status,string remark, bool isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert into Cheque (ChequeNo,ChequeDate,PartyName,Amount,BankName,Status,Remark,Isdeleted) "
                        + " values (@ChequeNo,@ChequeDate,@PartyName,@Amount,@BankName,@Status,@Remark,@Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@ChequeNo", chequeNo);
                    manageCommand.Parameters.AddWithValue("@ChequeDate", chequeDate);
                    manageCommand.Parameters.AddWithValue("@PartyName", partyName);
                    manageCommand.Parameters.AddWithValue("@Amount", amount);
                    manageCommand.Parameters.AddWithValue("@BankName", bankName);
                    manageCommand.Parameters.AddWithValue("@Status", status);
                    manageCommand.Parameters.AddWithValue("@Remark", remark);
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

        public static List<Cheque> RetrieveAll()
        {
            List<Cheque> chequeList = new List<Cheque>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, ChequeNo,ChequeDate,PartyName,Amount,BankName,ClearingDate,Status,Remark,Isdeleted from Cheque Where Isdeleted=0 ORDER BY ChequeDate ASC ";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Cheque");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        chequeList.Add(new Cheque(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["ChequeNo"].ToString())
                            , (Convert.IsDBNull(r["ChequeDate"]) ? new DateTime() : Convert.ToDateTime(r["ChequeDate"].ToString()))
                             , Convert.ToString(r["PartyName"].ToString())
                            , Convert.ToString(r["Amount"].ToString())
                            , Convert.ToString(r["BankName"].ToString())
                            , (Convert.IsDBNull(r["ClearingDate"]) ? new DateTime() : Convert.ToDateTime(r["ClearingDate"].ToString()))
                            , Convert.ToString(r["Status"].ToString())
                            , Convert.ToString(r["Remark"].ToString())
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
            return chequeList;
        }


        public static Cheque RetrieveById(int Id)
        {
            Cheque result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, ChequeNo,ChequeDate,PartyName,Amount,BankName,ClearingDate,Status,Remark,Isdeleted from Cheque Where Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Cheque");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new Cheque(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["ChequeNo"].ToString())
                            , (Convert.IsDBNull(r["ChequeDate"]) ? new DateTime() : Convert.ToDateTime(r["ChequeDate"].ToString()))
                             , Convert.ToString(r["PartyName"].ToString())
                            , Convert.ToString(r["Amount"].ToString())
                            , Convert.ToString(r["BankName"].ToString())
                            , (Convert.IsDBNull(r["ClearingDate"]) ?DateTime.MinValue: Convert.ToDateTime(r["ClearingDate"].ToString()))
                            , Convert.ToString(r["Status"].ToString())
                             , Convert.ToString(r["Remark"].ToString())
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
                    sqlStatement = "Update Cheque Set "
                        + "  ChequeNo = @ChequeNo "
                        + ", ChequeDate = @ChequeDate "
                        + ", PartyName = @PartyName "
                        + ", Amount = @Amount "
                        + ", BankName = @BankName "
                        + ", ClearingDate = @ClearingDate "
                        + ", Status = @Status "
                        + ", Remark = @Remark "
                        + ", IsDeleted = @IsDeleted "
                        + "  where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", this.Id);
                    manageCommand.Parameters.AddWithValue("@ChequeNo", this.ChequeNo);
                    manageCommand.Parameters.AddWithValue("@ChequeDate", this.ChequeDate);
                    manageCommand.Parameters.AddWithValue("@PartyName", this.PartyName);
                    manageCommand.Parameters.AddWithValue("@Amount", this.Amount);
                    manageCommand.Parameters.AddWithValue("@BankName", this.BankName);
                    manageCommand.Parameters.AddWithValue("@ClearingDate", this.ClearingDate);
                    manageCommand.Parameters.AddWithValue("@Status", this.Status);
                    manageCommand.Parameters.AddWithValue("@Remark", this.Remark);
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
                string sqlStatement = "Update Cheque Set IsDeleted = 1 where Id = @Id ";
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
        public static List<Cheque> RetrieveChequePending()
        {
            List<Cheque> chequeList = new List<Cheque>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "select count(Status) as ChequePending,sum(Amount) as PendingAmount from Cheque where Status='Pending' and IsDeleted=0";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Cheque");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        chequeList.Add(new Cheque(Convert.ToInt32(r["ChequePending"]), 
                                                  Convert.ToInt32(r["PendingAmount"])
                        ));
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The Database transaction failed" + ex.Message);
            }
            return chequeList;
        }

        #endregion //CRUD

    }

}
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

    public class PaymentExpenseAdd
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SIMSDbConnection"].ConnectionString;

        #region Fields
        private int _id;
        private string _expense;
        private string _frequency;
        private bool _isdeleted;
        #endregion //Fields

        #region Props
        public int Id { get { return _id; } set { _id = value; } }

        public string Expense { get { return _expense; } set { _expense = value; } }
        public string Frequency { get { return _frequency; } set { _frequency = value; } }
        public bool Isdeleted { get { return _isdeleted; } set { _isdeleted = value; } }

        #endregion //Props

        #region CTOR

        public PaymentExpenseAdd()
        {
            _id = 0;
            _expense = "";
            _frequency = "";
            _isdeleted = false;
        }


        public PaymentExpenseAdd(int id, string expense,string frequency, bool isdeleted)
        {
            _id = id;
            _expense = expense;
            _frequency = frequency;
            _isdeleted = isdeleted;
        }

        #endregion //CTOR

        #region CRUD
        public static int Create(string Expense, string Frequency, bool Isdeleted)
        {
            try
            {
                string sqlStatement;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand manageCommand;
                    sqlStatement = "Insert Expense ( Expense,Frequency, Isdeleted ) "
                        + " values ( @Expense,@Frequency, @Isdeleted )";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Expense", Expense);
                    manageCommand.Parameters.AddWithValue("@Frequency", Frequency);
                    manageCommand.Parameters.AddWithValue("@Isdeleted", Isdeleted);

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

        public static List<PaymentExpenseAdd> RetrieveAll()
        {
            List<PaymentExpenseAdd> expenselist = new List<PaymentExpenseAdd>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Expense,Frequency, Isdeleted from Expense Where Isdeleted=0 ORDER BY Expense asc";
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectStatement, connection);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Expense");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    while (r.Read())
                    {
                        expenselist.Add(new PaymentExpenseAdd(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Expense"].ToString())
                            , Convert.ToString(r["Frequency"].ToString())
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
            return expenselist;
        }


        public static PaymentExpenseAdd RetrieveById(int Id)
        {
            PaymentExpenseAdd result = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string selectStatement = "Select Id, Expense,Frequency, Isdeleted from Expense Where Id = @Id";
                    SqlCommand manageCommand = new SqlCommand(selectStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(manageCommand);
                    DataSet sqlDataset = new DataSet();
                    sqlAdapter.Fill(sqlDataset, "Expense");
                    DataTableReader r = sqlDataset.CreateDataReader();
                    if (r.Read())
                    {
                        result = new PaymentExpenseAdd(Convert.ToInt32(r["Id"])
                            , Convert.ToString(r["Expense"].ToString())
                            , Convert.ToString(r["Frequency"].ToString())
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
                    sqlStatement = "update Expense set Expense=@Expense,Frequency=@Frequency "
                                 + "where Id= @Id";
                    manageCommand = new SqlCommand(sqlStatement, connection);
                    manageCommand.Parameters.AddWithValue("@Expense", this.Expense);
                    manageCommand.Parameters.AddWithValue("@Frequency", this.Frequency);
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


        public bool Delete()
        {
            try
            {
                string sqlStatement = "Update Expense Set isDeleted = 1 where Id = @Id ";
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
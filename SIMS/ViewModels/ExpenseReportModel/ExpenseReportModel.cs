using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.ExpenseReportModel
{
    public class ExpenseReportModel
    {
        private ExpenseReport _expenseReport;
        private List<Customer> lstCustomerPlace;
        #region Props

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _expenseReport.Id; } set { _expenseReport.Id = value; } }

        [Display(Name = "Staff ID")]
        [Required]
        public int Staffid { get { return _expenseReport.StaffId; } set { _expenseReport.StaffId = value; } }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TimeStamp { get { return _expenseReport.TimeStamp; } set { _expenseReport.TimeStamp = value; } }

        [Display(Name = "Transport Vehicle")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TransportVehicle { get { return _expenseReport.TransportVehicle; } set { _expenseReport.TransportVehicle = value; } }

        [Display(Name = "Transport Charge")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string Amount { get { return _expenseReport.Amount; } set { _expenseReport.Amount = value; } }

        [Display(Name = "From")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string StartingPoint { get { return _expenseReport.StartingPoint; } set { _expenseReport.StartingPoint = value; } }

        [Display(Name = "To")]
        //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Destination { get { return _expenseReport.Destination; } set { _expenseReport.Destination = value; } }

        [Display(Name = "Distance")]
        //     [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string Distance { get { return _expenseReport.Distance; } set { _expenseReport.Distance = value; } }

        [Display(Name = "Food Charge")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string FoodCharge { get { return _expenseReport.FoodCharge; } set { _expenseReport.FoodCharge = value; } }

        [Display(Name = "Parking Charge")]
        //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string ParkingCharge { get { return _expenseReport.ParkingCharge; } set { _expenseReport.ParkingCharge = value; } }

        [Display(Name = "Toll/Fine Charge")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string TollOrFineCharge { get { return _expenseReport.TollOrFineCharge; } set { _expenseReport.TollOrFineCharge = value; } }

        [Display(Name = "Toll/Fine Details")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TollOrFineDetails { get { return _expenseReport.TollOrFineDetails; } set { _expenseReport.TollOrFineDetails = value; } }

        [Display(Name = "Remarks")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remarks { get { return _expenseReport.Remarks; } set { _expenseReport.Remarks = value; } }


        [Display(Name = "Employee Name")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserName { get { return _expenseReport.UserName; } set { _expenseReport.UserName = value; } }


        [Display(Name = "User Type")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserType { get { return _expenseReport.UserType; } set { _expenseReport.UserType = value; } }


        [Display(Name = "IsDeleted")]
        //	[Required]
        public bool IsDeleted { get { return _expenseReport.Isdeleted; } set { _expenseReport.Isdeleted = value; } }

        public ExpenseReport ExpenseReport { get { return _expenseReport; } set { _expenseReport = value; } }

        public List<Customer> CustomerLocationList { get { return lstCustomerPlace; } set { lstCustomerPlace = value; } }
        #endregion //Props


        #region CTOR

        public ExpenseReportModel()
        {
            _expenseReport = new ExpenseReport();
        }

        public ExpenseReportModel(ExpenseReport expenseReport)
        {
            _expenseReport = expenseReport;
        }


        public ExpenseReportModel(List<Customer> customerList)
        {
            _expenseReport = new ExpenseReport();
            lstCustomerPlace = customerList;
        }

        public ExpenseReportModel(ExpenseReport expenseReport, List<Customer> customerList)
        {
            _expenseReport = expenseReport;
            lstCustomerPlace = customerList;

        }

        #endregion //CTOR

    }
}
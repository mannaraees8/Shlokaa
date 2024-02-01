using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.VehicleLogModel
{
    public class VehicleLogModel
    {
        private VehicleLog _vehicleLog;
     private   List<Customer> lstCustomerPlace;
        private List<VehicleNo> lstVehicleNo;

        #region Props

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _vehicleLog.Id; } set { _vehicleLog.Id = value; } }

        [Display(Name = "Staff ID")]
        //     [Required]
        public int Staffid { get { return _vehicleLog.StaffId; } set { _vehicleLog.StaffId = value; } }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TimeStamp { get { return _vehicleLog.TimeStamp; } set { _vehicleLog.TimeStamp = value; } }

        [Display(Name = "Vehicle No.")]
     //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string VehicleNo { get { return _vehicleLog.VehicleNo; } set { _vehicleLog.VehicleNo = value; } }


        [Display(Name = "Employee Name")]
    //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserName { get { return _vehicleLog.UserName; } set { _vehicleLog.UserName = value; } }

        [Display(Name = "Invoice Amount")]
     //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string Amount { get { return _vehicleLog.Amount; } set { _vehicleLog.Amount = value; } }

        [Display(Name = "From")]
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string StartingPoint { get { return _vehicleLog.StartingPoint; } set { _vehicleLog.StartingPoint = value; } }

        [Display(Name = "To")]
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Destination { get { return _vehicleLog.Destination; } set { _vehicleLog.Destination = value; } }

        [Display(Name = "Purpose")]
     //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Purpose { get { return _vehicleLog.Purpose; } set { _vehicleLog.Purpose = value; } }

        [Display(Name = "Start Reading")]
    //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string StartReading { get { return _vehicleLog.StartReading; } set { _vehicleLog.StartReading = value; } }

        [Display(Name = "End Reading")]
       // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string EndReading { get { return _vehicleLog.EndReading; } set { _vehicleLog.EndReading = value; } }

        [Display(Name = "Fuel Filled")]
      //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FuelFilled { get { return _vehicleLog.FuelFilled; } set { _vehicleLog.FuelFilled = value; } }

        [Display(Name = "Fuel Quantity")]
      //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string FuelQuantity { get { return _vehicleLog.FuelQuantity; } set { _vehicleLog.FuelQuantity = value; } }

        [Display(Name = "Invoice No.")]
      //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Voucher { get { return _vehicleLog.Voucher; } set { _vehicleLog.Voucher = value; } }

        [Display(Name = "Remarks")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remarks { get { return _vehicleLog.Remarks; } set { _vehicleLog.Remarks = value; } }

        [Display(Name = "Month")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Month { get { return _vehicleLog.Month; } set { _vehicleLog.Month = value; } }

        [Display(Name = "DistanceTravelled")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DistanceTravelled { get { return _vehicleLog.DistanceTravelled; } set { _vehicleLog.DistanceTravelled = value; } }


        [Display(Name = "UserType")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserType { get { return _vehicleLog.UserType; } set { _vehicleLog.UserType = value; } }

        [Display(Name = "IsDeleted")]
        //	[Required]
        public bool IsDeleted { get { return _vehicleLog.Isdeleted; } set { _vehicleLog.Isdeleted = value; } }

        public VehicleLog VehicleLog { get { return _vehicleLog; } set { _vehicleLog = value; } }

        public List<Customer> CustomerLocationList { get { return lstCustomerPlace; } set { lstCustomerPlace = value; } }
        public List<VehicleNo> VehicleNoList { get { return lstVehicleNo; } set { lstVehicleNo = value; } }

        #endregion //Props


        #region CTOR

        public VehicleLogModel()
        {
            _vehicleLog = new VehicleLog();
        }

        public VehicleLogModel(VehicleLog vehicleLog)
        {
            _vehicleLog = vehicleLog;
        }

        public VehicleLogModel(List<Customer> customerList, List<VehicleNo> vehicleNoList)
        {
            _vehicleLog = new VehicleLog();
            lstCustomerPlace = customerList;
            lstVehicleNo = vehicleNoList;
        }

        public VehicleLogModel(List<Customer> customerList, List<VehicleNo> vehicleNoList, VehicleLog vehicleLog)
        {
            _vehicleLog = vehicleLog;
            lstCustomerPlace = customerList;
            lstVehicleNo = vehicleNoList;
        }
        #endregion //CTOR
    }
}
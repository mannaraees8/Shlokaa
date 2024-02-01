using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.MaterialMovementModel
{
    public class MaterialMovementModel
    {
        private MaterialMovement _materialMovement;

        private List<Customer> _customerList;
        private List<Item> _itemList;
        private List<UnitOfMeasurement> _unitOfMeasurements;

        #region Props

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _materialMovement.Id; } set { _materialMovement.Id = value; } }

        [Display(Name = "Party Name")]
        //[Required]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string CustomerName { get { return _materialMovement.CustomerName; } set { _materialMovement.CustomerName = value; } }

        [Display(Name = "Item Name")]
        //[Required]
        public string ItemName { get { return _materialMovement.ItemName; } set { _materialMovement.ItemName = value; } }


        [Display(Name = "Employee Name")]
        //[Required]
        public string UserName { get { return _materialMovement.UserName; } set { _materialMovement.UserName = value; } }


        [Display(Name = "UserType")]
        //[Required]
        public string UserType { get { return _materialMovement.UserType; } set { _materialMovement.UserType = value; } }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Timestamp { get { return _materialMovement.Timestamp; } set { _materialMovement.Timestamp = value; } }

        [Display(Name = "Staff ID")]
        [Required]
        public int Staffid { get { return _materialMovement.Staffid; } set { _materialMovement.Staffid = value; } }

        [Display(Name = "Customer ID")]
        //     [Required]
        public int Customerid { get { return _materialMovement.Customerid; } set { _materialMovement.Customerid = value; } }

        [Display(Name = "Item ID")]
        //  [Required]
        public int Itemid { get { return _materialMovement.Itemid; } set { _materialMovement.Itemid = value; } }

        [Display(Name = "Invoice No")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Invoiceno { get { return _materialMovement.Invoiceno; } set { _materialMovement.Invoiceno = value; } }

        [Display(Name = "Invoice Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Invoicedate { get { return _materialMovement.Invoicedate; } set { _materialMovement.Invoicedate = value; } }

        [Display(Name = "Unit")]
        //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public int UnitId { get { return _materialMovement.UnitId; } set { _materialMovement.UnitId = value; } }


        [Display(Name = "Unit Name")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UnitName { get { return _materialMovement.UnitName; } set { _materialMovement.UnitName = value; } }

        [Display(Name = "Quantity")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\d+.\d{0,5}$", ErrorMessage = "Please Enter Valid Quantity")]
        public string Quantity { get { return _materialMovement.Quantity; } set { _materialMovement.Quantity = value; } }

        [Display(Name = "Invoice Amount")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string Invoiceamount { get { return _materialMovement.Invoiceamount; } set { _materialMovement.Invoiceamount = value; } }

        [Display(Name = "Remarks")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remarks { get { return _materialMovement.Remarks; } set { _materialMovement.Remarks = value; } }

        [Display(Name = "Movement Type")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Movementtype { get { return _materialMovement.Movementtype; } set { _materialMovement.Movementtype = value; } }

        public string ItemSize { get { return _materialMovement.ItemSize; } set { _materialMovement.ItemSize = value; } }

        [Display(Name = "Is Deleted")]
        //[Required]
        public bool Isdeleted { get { return _materialMovement.Isdeleted; } set { _materialMovement.Isdeleted = value; } }
        public List<Customer> CustomerList { get { return _customerList; } }

        public List<Item> ItemList { get { return _itemList; } }


        public List<UnitOfMeasurement> UnitOfMeasurementList { get { return _unitOfMeasurements; } }

        public MaterialMovement MaterialMovement { get { return _materialMovement; } set { _materialMovement = value; } }

        #endregion //Props

        #region CTOR
        public MaterialMovementModel()
        {
            _materialMovement = new MaterialMovement();


        }
        public MaterialMovementModel(MaterialMovement materialMovement)
        {
            _materialMovement = materialMovement;
        }

        public MaterialMovementModel(List<Customer> customerList, List<Item> itemList, List<UnitOfMeasurement> unitOfMeasurementsList)
        {
            _materialMovement = new MaterialMovement();
            _customerList = customerList;
            _itemList = itemList;
            _unitOfMeasurements = unitOfMeasurementsList;
        }

        public MaterialMovementModel(MaterialMovement materialMovement, List<Customer> customerList, List<Item> itemList, List<UnitOfMeasurement> unitOfMeasurementsList)
        {
            _materialMovement = materialMovement;
            _customerList = customerList;
            _itemList = itemList;
            _unitOfMeasurements = unitOfMeasurementsList;

        }

        #endregion //CTOR

    }
}
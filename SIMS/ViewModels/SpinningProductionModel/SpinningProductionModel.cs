using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.SpinningProductionModel
{
    public class SpinningProductionModel
    {
        private SpinningProduction _spinningProduction;

        private List<Item> _itemList;
        private List<Users> _usersList;

        #region Props

        [Display(Name = "Id")]
        //[Required]
        public int Id { get { return _spinningProduction.Id; } set { _spinningProduction.Id = value; } }

        [Display(Name = "StaffId")]
        //[Required]
        public int StaffId { get { return _spinningProduction.StaffId; } set { _spinningProduction.StaffId = value; } }

        [Display(Name = "Achieved")]
        //[Required]
        public string Achieved { get { return _spinningProduction.Achieved; } set { _spinningProduction.Achieved = value; } }

        [Display(Name = "Actual")]
        //[Required]
        public string Pending { get { return _spinningProduction.Pending; } set { _spinningProduction.Pending = value; } }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get { return _spinningProduction.Date; } set { _spinningProduction.Date = value; } }

        [Display(Name = "Item Name")]
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ItemName { get { return _spinningProduction.ItemName; } set { _spinningProduction.ItemName = value; } }


        [Display(Name = "Employee Name")]
      //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string EmployeeName { get { return _spinningProduction.EmployeeName; } set { _spinningProduction.EmployeeName = value; } }

        [Display(Name = "Circle Size")]
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string CircleSize { get { return _spinningProduction.CircleSize; } set { _spinningProduction.CircleSize = value; } }

        [Display(Name = "Circle Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CircleDate { get { return _spinningProduction.CircleDate; } set { _spinningProduction.CircleDate = value; } }

        [Display(Name = "Circle Issued")]
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string CircleIssued { get { return _spinningProduction.CircleIssued; } set { _spinningProduction.CircleIssued = value; } }

        [Display(Name = "FG Weight")]
        //     [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string FGWeight { get { return _spinningProduction.FGWeight; } set { _spinningProduction.FGWeight = value; } }

        [Display(Name = "Total Trimming Weight")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string Trimming { get { return _spinningProduction.Trimming; } set { _spinningProduction.Trimming = value; } }

        [Display(Name = "Broken Weight")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        //  [Required]
        public string Broken1 { get { return _spinningProduction.Broken; } set { _spinningProduction.Broken= value; } }


        [Display(Name = "Broken %")]
        //   [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string BrokenPercentage { get { return _spinningProduction.BrokenPerc; } set { _spinningProduction.BrokenPerc = value; } }

        [Display(Name = "Production From Broken")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string ProductionFromBroken { get { return _spinningProduction.ProductionFromBroken; } set { _spinningProduction.ProductionFromBroken= value; } }

        [Display(Name = "Product From Broken")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string ProductFromBroken { get { return _spinningProduction.ProductFromBroken; } set { _spinningProduction.ProductFromBroken = value; } }

        [Display(Name = "Remarks")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remarks { get { return _spinningProduction.Remarks; } set { _spinningProduction.Remarks = value; } }

        [Display(Name = "Net Broken Weight")]
        // [Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NetBroken { get { return _spinningProduction.NetBroken; } set { _spinningProduction.NetBroken = value; } }

        [Display(Name = "Discrepancy")]
        // [Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Discrepancy { get { return _spinningProduction.Discrepancy; } set { _spinningProduction.Discrepancy = value; } }

        [Display(Name = "Net Broken %")]
        // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string NetBrokenPercentage { get { return _spinningProduction.NetBrokenPerc; } set { _spinningProduction.NetBrokenPerc = value; } }

        [Display(Name = "Is Deleted")]
        //[Required]
        public bool Isdeleted { get { return _spinningProduction.Isdeleted; } set { _spinningProduction.Isdeleted = value; } }
        public List<Users> UsersList { get { return _usersList; } }

        public List<Item> ItemList { get { return _itemList; } }

        public SpinningProduction SpinningProduction { get { return _spinningProduction; } set { _spinningProduction = value; } }

        #endregion //Props

        #region CTOR
        public SpinningProductionModel()
        {
            _spinningProduction = new SpinningProduction();


        }
        public SpinningProductionModel(SpinningProduction spinningProduction)
        {
            _spinningProduction = spinningProduction;
        }

        public SpinningProductionModel(List<Users> usersList, List<Item> itemList)
        {
            _spinningProduction = new SpinningProduction();
            _usersList = usersList;
            _itemList = itemList;
        }

        public SpinningProductionModel(SpinningProduction spinningProduction, List<Users> usersList, List<Item> itemList)
        {
            _spinningProduction = spinningProduction;
            _usersList = usersList;
            _itemList = itemList;
        }

        #endregion //CTOR
    }
}
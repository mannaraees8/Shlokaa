using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.SalesModel
{
    public class SalesModel
    {
        private Sales _sales;
        private List<Users> users;
        private List<Category> categories;
        #region Props

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _sales.Id; } set { _sales.Id = value; } }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get { return _sales.Date; } set { _sales.Date = value; } }

        [Display(Name = "Marketing Executive")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int StaffID { get { return _sales.StaffId; } set { _sales.StaffId = value; } }

        [Display(Name = "Marketing Executive")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MarketingExecutive { get { return _sales.MarketingExecutive; } set { _sales.MarketingExecutive = value; } }

        [Display(Name = "Product Category")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ProductCategory { get { return _sales.ProductCategory; } set { _sales.ProductCategory = value; } }

        [Display(Name = "Sales Amount")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public int SalesAmount { get { return _sales.SalesAmount; } set { _sales.SalesAmount = value; } }

        [Display(Name = "Sales Return Amount")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public int SalesReturnAmount { get { return _sales.SalesReturnAmount; } set { _sales.SalesReturnAmount = value; } }

        [Display(Name = "IsDeleted")]
        //[Required]
        public bool Isdeleted { get { return _sales.Isdeleted; } set { _sales.Isdeleted = value; } }
        public int Month { get { return _sales.Month; } set { _sales.Month = value; } }
        public Double SalesPercentage { get { return _sales.SalesPercentage; } set { _sales.SalesPercentage = value; } }
        public string UserName { get { return _sales.UserName; } set { _sales.UserName = value; } }
        public string ExtraAmount { get { return _sales.ExtraAmount; } set { _sales.ExtraAmount = value; } }
        public Sales Sales { get { return _sales; } set { _sales = value; } }
        public List<Users> UserList { get { return users; } }
        public List<Category> CategoryList { get { return categories; } }
        #endregion //Props

        #region CTOR

        public SalesModel()
        {
            _sales = new Sales();
        }

        public SalesModel(Sales sales)
        {
            _sales = sales;
        }
        public SalesModel(List<Users> usersList, List<Category> categoryList)
        {
            _sales = new Sales();
            users = usersList;
            categories = categoryList;
        }

        public SalesModel(Sales sales, List<Users> usersList, List<Category> categoryList)
        {
            _sales = sales;
            users = usersList;
            categories = categoryList;
        }
        #endregion //CTOR
    }
}
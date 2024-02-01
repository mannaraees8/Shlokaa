using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.CustomerModel
{
    public class CustomerModel
    {
        private Customer _customer;
        private List<Users> users;
        #region Props

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _customer.Id; } set { _customer.Id = value; } }

        [Display(Name = "Party Name")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string Name { get { return _customer.Name; } set { _customer.Name = value; } }

        [Display(Name = "Mobile No.")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Please Enter Valid Mobile Number")]
        public string Mobile { get { return _customer.Mobile; } set { _customer.Mobile = value; } }

        [Display(Name = "Email")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.EmailAddress)]

        public string Email { get { return _customer.Email; } set { _customer.Email = value; } }

        [Display(Name = "Vendor Code")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Vendercode { get { return _customer.Vendercode; } set { _customer.Vendercode = value; } }

        [Display(Name = "Contact Person")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string ContactPerson { get { return _customer.ContactPerson; } set { _customer.ContactPerson = value; } }

        [Display(Name = "City")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string Location { get { return _customer.Location; } set { _customer.Location = value; } }


        [Display(Name = "Route")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string State { get { return _customer.State; } set { _customer.State = value; } }

        [Display(Name = "Account No.")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AccountNo { get { return _customer.AccountNo; } set { _customer.AccountNo = value; } }

        [Display(Name = "Bank Name")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BankName { get { return _customer.BankName; } set { _customer.BankName = value; } }

        [Display(Name = "IFSC")]
        [StringLength(11, ErrorMessage = "Please Enter Valid IFSC Code.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IFSC { get { return _customer.IFSC; } set { _customer.IFSC = value; } }

        [Display(Name = "Branch")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Branch { get { return _customer.Branch; } set { _customer.Branch = value; } }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address { get { return _customer.Address; } set { _customer.Address = value; } }

        [Display(Name = "GSTIN")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^([0][1-9]|[1-2][0-9]|[3][0-7])([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$",
                   ErrorMessage = "Please Enter proper GSTIN")]
        public string Gstno { get { return _customer.Gstno; } set { _customer.Gstno = value; } }


        [Display(Name = "Marketing Executive")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MarketingExecutive { get { return _customer.MarketingExecutive; } set { _customer.MarketingExecutive = value; } }

        [Display(Name = "Marketing Executive")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int StaffID { get { return _customer.StaffId; } set { _customer.StaffId = value; } }

        [Display(Name = "Sales Order Target")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public int SalesOrderTarget { get { return _customer.SalesOrderTarget; } set { _customer.SalesOrderTarget = value; } }


        [Display(Name = "IsDeleted")]
        //[Required]
        public bool Isdeleted { get { return _customer.Isdeleted; } set { _customer.Isdeleted = value; } }

        public Customer Customer { get { return _customer; } set { _customer = value; } }
        public List<Users> userList { get { return users; } }
        #endregion //Props

        #region CTOR

        public CustomerModel()
        {
            _customer = new Customer();
        }

        public CustomerModel(Customer customer)
        {
            _customer = customer;
        }
        public CustomerModel(List<Users> usersList)
        {
            _customer = new Customer();
            users = usersList;
        }

        public CustomerModel(Customer customer, List<Users> usersList)
        {
            _customer = customer;
            users = usersList;
        }
        #endregion //CTOR
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.ViewModels.UsersModel
{
    public class UsersModel
    {
        private Users _users;
        private List<Department> lstDepartment;

        #region Props

        [Display(Name = "Id")]
        //[Required]
        public int Id { get { return _users.Id; } set { _users.Id = value; } }

        [Display(Name = "Name")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string Name { get { return _users.Name; } set { _users.Name = value; } }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address { get { return _users.Address; } set { _users.Address = value; } }

        [Display(Name = "City")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Location { get { return _users.Location; } set { _users.Location = value; } }

        [Display(Name = "State")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string State { get { return _users.State; } set { _users.State = value; } }

        [Display(Name = "Date Of Birth")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get { return _users.Dob; } set { _users.Dob = value; } }

        [Display(Name = "Mobile No.")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Please Enter Valid Mobile Number")]
        public string Mobile { get { return _users.Mobile; } set { _users.Mobile = value; } }

        [Display(Name = "Joining Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Doj { get { return _users.Doj; } set { _users.Doj = value; } }

        [Display(Name = "Aadhar No.")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[0-9]{12}",
                   ErrorMessage = "Please Enter 12 Digit Adhar Number")]
        public string Adhar { get { return _users.Adhar; } set { _users.Adhar = value; } }

        [Display(Name = "ESIC No.")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[0-9]{17}",
                   ErrorMessage = "Please Enter Proper ESIC Number")]
        public string Esino { get { return _users.Esino; } set { _users.Esino = value; } }

        [Display(Name = "PF No.")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[0-9]{12}",
                   ErrorMessage = "Please Enter 12 Digit PF")]
        public string Pfno { get { return _users.Pfno; } set { _users.Pfno = value; } }

        [Display(Name = "Bank Name")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string Bankname { get { return _users.Bankname; } set { _users.Bankname = value; } }

        [Display(Name = "Account No.")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Bankaccountno { get { return _users.Bankaccountno; } set { _users.Bankaccountno = value; } }

        [Display(Name = "Branch")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("[a-zA-Z ]*$",
                   ErrorMessage = "Please Enter Alphabets Only")]
        public string Branch { get { return _users.Branch; } set { _users.Branch = value; } }

        [Display(Name = "IFSC Code")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(11, ErrorMessage = "Please Enter Valid IFSC Code.")]

        public string Ifsc { get { return _users.Ifsc; } set { _users.Ifsc = value; } }

        [Display(Name = "Email")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get { return _users.Email; } set { _users.Email = value; } }

        [Display(Name = "Password")]
        //		[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password)]
        [RegularExpression("(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+",
                   ErrorMessage = "Use uppercase,lowercase,special character of 8 characters length ex:Abcde@12")]
        public string Password { get { return _users.Password; } set { _users.Password = value; } }

        [Display(Name = "Photo")]
        //	[Required]
        public byte[] Photo { get { return _users.Photo; } set { _users.Photo = value; } }

        [Display(Name = "Leaving Date")]
        //	[Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Dol { get { return _users.Dol; } set { _users.Dol = value; } }

        [Display(Name = "User Type")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Usertype { get { return _users.Usertype; } set { _users.Usertype = value; } }

        [Display(Name = "Is Deleted")]
        //[Required]
        public bool Isdeleted { get { return _users.Isdeleted; } set { _users.Isdeleted = value; } }

        [Display(Name = "Department")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Department { get { return _users.Department; } set { _users.Department = value; } }


        [Display(Name = "IN Time")]
       // [Required]
       [DataType(DataType.DateTime)]
       [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EnteringTime { get { return _users.EnteringTime; } set { _users.EnteringTime = value; } }


        [Display(Name = "OUT Time")]
       // [Required]
        [DataType(DataType.DateTime)]
       [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? LeavingTime { get { return _users.LeavingTime; } set { _users.LeavingTime = value; } }

        [Display(Name = "Target")]
        //[Required]
        public int Target { get { return _users.Target; } set { _users.Target = value; } }


        [Display(Name = "Sales Target")]
        //[Required]
        public int SalesTarget { get { return _users.SalesTarget; } set { _users.SalesTarget = value; } }
        public Users Users { get { return _users; } set { _users = value; } }
        public List<Department> DepartmentList { get { return lstDepartment; } set { lstDepartment = value; } }
        #endregion //Props
        #region CTOR

        public UsersModel()
        {
            _users = new Users();
        }
        public UsersModel(Users users)
        {
            _users = users;
        }

        public UsersModel(List<Department> departmentList)
        {
            _users = new Users();
            lstDepartment = departmentList;
        }

        public UsersModel(List<Department> departmentList, Users users)
        {
            lstDepartment = departmentList;
            _users = users;

        }  
       
        #endregion //CTOR
    }
}
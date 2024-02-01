using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.ViewModels
{
    public class ChangePasswordModel
    {
        private Users _users;
        private List<Users> lstusers;

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _users.Id; } set { _users.Id = value; } }

        [Display(Name = "Password")]
        //		[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password)]
        [RegularExpression("(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+",
               ErrorMessage = "Use uppercase,lowercase,special character of 8 characters length ex:Abcde@12")]
        public string Password { get { return _users.Password; } set { _users.Password = value; } }

        [NotMapped]
        [DataType(DataType.Password)]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        public Users Users { get { return _users; } set { _users = value; } }

        public List<Users> UsersList { get { return lstusers; } set { lstusers = value; } }


        public ChangePasswordModel()
        {
            _users = new Users();
        }

        public ChangePasswordModel(Users users)
        {
            _users = users;
        }
        public ChangePasswordModel(List<Users> usersList)
        {
            _users = new Users();
            lstusers = usersList;
        }

        public ChangePasswordModel(Users users, List<Users> usersList)
        {
            _users = new Users();
            lstusers = usersList;

        }
    }
}
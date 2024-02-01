using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.Models
{
    public class LoginModel
	{
		private Users _users;

		[Display(Name = "ID")]
        //[Required]
        public int Id { get { return _users.Id; } set { _users.Id = value; } }

		[Display(Name = "Email")]
		//[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		[DataType(DataType.EmailAddress)]
		public string Email { get { return _users.Email; } set { _users.Email = value; } }

		[Display(Name = "Password")]
		//[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		[DataType(DataType.Password)]

		public string Password { get { return _users.Password; } set { _users.Password = value; } }
		public string ReturnUrl { get; set; }

		#region CTOR

		public LoginModel()
		{
			_users = new Users();
		}

		public LoginModel(Users users)
		{
			_users = users;
		}

		#endregion //CTOR
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.Models
{
	public class LeaveApplyModel
	{
		private LeaveApply _leaveApply;
		#region Props

		[Display(Name = "ID")]
		//[Required]
		public int Id { get { return _leaveApply.Id; } set { _leaveApply.Id = value; } }

		[Display(Name = "Staff ID")]
		[Required]
		public int Staffid { get { return _leaveApply.Staffid; } set { _leaveApply.Staffid = value; } }

		[Display(Name = "Date")]
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get { return _leaveApply.Date; } set { _leaveApply.Date = value; } }


		[Display(Name = "Approved Date")]
	//	[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ApprovedDate { get { return _leaveApply.Date; } set { _leaveApply.Date = value; } }

		[Display(Name = "Is Full Day")]
		[Required]
		public bool Isfullday { get { return _leaveApply.Isfullday; } set { _leaveApply.Isfullday = value; } }

		[Display(Name = "Leave Type")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Leavetype { get { return _leaveApply.Leavetype; } set { _leaveApply.Leavetype = value; } }

		[Display(Name = "Leave Category")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string LeaveCategory { get { return _leaveApply.LeaveCategory; } set { _leaveApply.LeaveCategory = value; } }


		[Display(Name = "UserType")]
	//	[Required]
	//	[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string UserType { get { return _leaveApply.UserType; } set { _leaveApply.UserType = value; } }


		[Display(Name = "Employee Name")]
		//	[Required]
		//	[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string UserName { get { return _leaveApply.UserName; } set { _leaveApply.UserName = value; } }

		[Display(Name = "Reason")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Reason { get { return _leaveApply.Reason; } set { _leaveApply.Reason = value; } }


		[Display(Name = "Manager Approval")]
	//	[Required]
		public bool Ismanagerauthorised { get { return _leaveApply.Ismanagerauthorised; } set { _leaveApply.Ismanagerauthorised = value; } }

		[Display(Name = "Admin Approval")]
	//	[Required]
		public bool Isadminauthorised { get { return _leaveApply.Isadminauthorised; } set { _leaveApply.Isadminauthorised = value; } }

		[Display(Name = "Is Deleted")]
		//[Required]
		public bool Isdeleted { get { return _leaveApply.Isdeleted; } set { _leaveApply.Isdeleted = value; } }

		public LeaveApply LeaveApply { get { return _leaveApply; } set { _leaveApply = value; } }

		#endregion //Props

		#region CTOR

		public LeaveApplyModel()
		{
			_leaveApply = new LeaveApply();
		}

		public LeaveApplyModel(LeaveApply leaveApply)
		{
			_leaveApply = leaveApply;
		}

		#endregion //CTOR

	}
}


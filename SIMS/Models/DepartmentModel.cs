using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.Models
{
	public class DepartmentModel
	{
		private Department _department;

		#region Props

		[Display(Name = "I D")]
		//[Required]
		public int Id { get { return _department.Id; } set { _department.Id = value; } }

		[Display(Name = "Department")]
		[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Department { get { return _department.DepartmentName; } set { _department.DepartmentName = value; } }

		[Display(Name = "Is Deleted")]
		//[Required]
		public bool Isdeleted { get { return _department.Isdeleted; } set { _department.Isdeleted = value; } }

		public Department DepartmentObj { get { return _department; } set { _department = value; } }

		#endregion //Props

		#region CTOR

		public DepartmentModel()
		{
			_department = new Department();
		}

		public DepartmentModel(Department department)
		{
			_department = department;
		}

		#endregion //CTOR

	}
}


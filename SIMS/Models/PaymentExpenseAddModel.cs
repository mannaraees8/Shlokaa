using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.Models
{
	public class PaymentExpenseAddModel
	{
		private PaymentExpenseAdd _paymentExpenseAdd;

		#region Props

		[Display(Name = "ID")]
		//[Required]
		public int Id { get { return _paymentExpenseAdd.Id; } set { _paymentExpenseAdd.Id = value; } }

		[Display(Name = "Expense")]
		[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Expense { get { return _paymentExpenseAdd.Expense; } set { _paymentExpenseAdd.Expense = value; } }


		[Display(Name = "Frequency")]
		//[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Frequency { get { return _paymentExpenseAdd.Frequency; } set { _paymentExpenseAdd.Frequency = value; } }

		[Display(Name = "Is Deleted")]
		//[Required]
		public bool Isdeleted { get { return _paymentExpenseAdd.Isdeleted; } set { _paymentExpenseAdd.Isdeleted = value; } }

		public PaymentExpenseAdd PaymentExpenseAdd { get { return _paymentExpenseAdd; } set { _paymentExpenseAdd = value; } }

		#endregion //Props

		#region CTOR

		public PaymentExpenseAddModel()
		{
			_paymentExpenseAdd = new PaymentExpenseAdd();
		}

		public PaymentExpenseAddModel(PaymentExpenseAdd paymentExpenseAdd)
		{
			_paymentExpenseAdd = paymentExpenseAdd;
		}

		#endregion //CTOR

	}
}


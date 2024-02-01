using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.ViewModels.VisitLogModel
{
    public class VisitLogModel
    {
		private VisitLog _visitLog;
		private List<Customer> _customersList;
		#region Props

		[Display(Name = "ID")]
		//[Required]
		public int Id { get { return _visitLog.Id; } set { _visitLog.Id = value; } }

		[Display(Name = "Date")]
		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Datetime { get { return _visitLog.Datetime; } set { _visitLog.Datetime = value; } }

		[Display(Name = "Staff ID")]
		[Required]
		public int Staffid { get { return _visitLog.Staffid; } set { _visitLog.Staffid = value; } }

		[Display(Name = "Party Name")]
	//	[Required]
		public int Customerid { get { return _visitLog.Customerid; } set { _visitLog.Customerid = value; } }

		[Display(Name = "Employee Name")]
		//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string UserName { get { return _visitLog.UserName; } set { _visitLog.UserName = value; } }

		[Display(Name = "Party Name")]
		//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string CustomerName { get { return _visitLog.CustomerName; } set { _visitLog.CustomerName = value; } }

		[Display(Name = "Order Status")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]		
		public string Orderstatus { get { return _visitLog.Orderstatus; } set { _visitLog.Orderstatus = value; } }

		[Display(Name = "Order Value")]
		//[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Ordervalue { get { return _visitLog.Ordervalue; } set { _visitLog.Ordervalue = value; } }

		[Display(Name = "Sales Order ID")]
		//[Required]
		public int Salesorderid { get { return _visitLog.Salesorderid; } set { _visitLog.Salesorderid = value; } }

		[Display(Name = "Payment Status")]
		//[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Paymentmode { get { return _visitLog.Paymentmode; } set { _visitLog.Paymentmode = value; } }

		[Display(Name = "Amount")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
		public string Amount { get { return _visitLog.Amount; } set { _visitLog.Amount = value; } }

		[Display(Name = "Reason For No Payment")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Reasonfornopayment { get { return _visitLog.Reasonfornopayment; } set { _visitLog.Reasonfornopayment = value; } }

		[Display(Name = "Remarks")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Remarks { get { return _visitLog.Remarks; } set { _visitLog.Remarks = value; } }

		[Display(Name = "Reason For No Order")]
	//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Reasonfornoorder { get { return _visitLog.Reasonfornoorder; } set { _visitLog.Reasonfornoorder = value; } }

		[Display(Name = "UserType")]
		//	[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string UserType { get { return _visitLog.UserType; } set { _visitLog.UserType = value; } }

		[Display(Name = "Is Deleted")]
		//[Required]
		public bool Isdeleted { get { return _visitLog.Isdeleted; } set { _visitLog.Isdeleted = value; } }

		public int Month { get { return _visitLog.Month; } set { _visitLog.Month = value; } }

		public int OrderStatusCount { get { return _visitLog.OrderStatusCount; } set { _visitLog.OrderStatusCount = value; } }
		public List<Customer> CustomerList { get { return _customersList; } }

		public VisitLog VisitLog { get { return _visitLog; } set { _visitLog = value; } }

		#endregion //Props

		#region CTOR

		public VisitLogModel(List<Customer> customerList)
		{
			_visitLog = new VisitLog();
			_customersList = customerList;
		}

		public VisitLogModel(List<Customer> customerList,VisitLog visitLog)
		{
			_customersList = customerList;
			_visitLog = visitLog;

		}
		public VisitLogModel()
		{
			_visitLog = new VisitLog();
		}

		public VisitLogModel(VisitLog visitLog)
		{
			_visitLog = visitLog;
		}

		#endregion //CTOR
	}
}
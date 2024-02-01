using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.Models
{
    public class PaymentModel
    {
        private Payment _payment;

        #region Props

        [Display(Name = "ID")]
        public int Id { get { return _payment.Id; } set { _payment.Id = value; } }

        [Display(Name = "Expense")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string Expense { get { return _payment.Expense; } set { _payment.Expense = value; } }

        [Display(Name = "Frequency")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Frequency { get { return _payment.Frequency; } set { _payment.Frequency = value; } }

        [Display(Name = "Amount")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public int Amount { get { return _payment.Amount; } set { _payment.Amount = value; } }


        [Display(Name = "Due Date")]
       	[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get { return _payment.DueDate; } set { _payment.DueDate = value; } }

        [Display(Name = "IsDeleted")]
        //[Required]
        public bool Isdeleted { get { return _payment.Isdeleted; } set { _payment.Isdeleted = value; } }

        public Payment Payment { get { return _payment; } set { _payment = value; } }

        #endregion //Props

        #region CTOR

        public PaymentModel()
        {
            _payment = new Payment();
        }

        public PaymentModel(Payment payment)
        {
            _payment = payment;
        }

        #endregion //CTOR

    }
}

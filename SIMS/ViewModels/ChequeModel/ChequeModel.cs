using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.ViewModels
{
    public class ChequeModel
    {
        private Cheque _cheque;
        private List<Customer> _customerList;

        #region Props

        [Display(Name = "Id")]
        public int Id { get { return _cheque.Id; } set { _cheque.Id = value; } }

        [Display(Name = "Cheque No")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string ChequeNo { get { return _cheque.ChequeNo; } set { _cheque.ChequeNo = value; } }

        [Display(Name = "Cheque Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ChequeDate { get { return _cheque.ChequeDate; } set { _cheque.ChequeDate = value; } }

        [Display(Name = "Party Name")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PartyName { get { return _cheque.PartyName; } set { _cheque.PartyName = value; } }

        [Display(Name = "Amount")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please Enter Only Numbers")]
        public string Amount { get { return _cheque.Amount; } set { _cheque.Amount = value; } }

        [Display(Name = "Bank Name")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BankName { get { return _cheque.BankName; } set { _cheque.BankName = value; } }

        [Display(Name = "Clearing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClearingDate { get { return _cheque.ClearingDate; } set { _cheque.ClearingDate = value; } }

        [Display(Name = "Status")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Status { get { return _cheque.Status; } set { _cheque.Status = value; } }

        [Display(Name = "Remark")]
        //	[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Remark { get { return _cheque.Remark; } set { _cheque.Remark = value; } }


        [Display(Name = "IsDeleted")]
        public bool Isdeleted { get { return _cheque.Isdeleted; } set { _cheque.Isdeleted = value; } }

        public Cheque Cheque { get { return _cheque; } set { _cheque = value; } }
        public List<Customer> CustomerList { get { return _customerList; } }

        #endregion //Props

        #region CTOR

        public ChequeModel()
        {
            _cheque = new Cheque();
        }

        public ChequeModel(Cheque cheque)
        {
            _cheque = cheque;
        }
        public ChequeModel(List<Customer> customerList)
        {
            _cheque = new Cheque();
            _customerList = customerList;
        }

        public ChequeModel(Cheque cheque, List<Customer> customerList)
        {
            _cheque = cheque;
            _customerList = customerList;
        }
        #endregion //CTOR

    }
}

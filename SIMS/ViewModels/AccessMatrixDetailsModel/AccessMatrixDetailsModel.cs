using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
using SIMS.Models;
namespace SIMS.ViewModels.AccessMatrixDetailsModel
{
    public class AccessMatrixDetailsModel
    {

        AccessMatrixDetails _accessMatrixDetails;
        List<AccessMatrix> _accessMatrixList;
        List<Users> _userType;

        [Display(Name = "ID")]
        public int Id { get { return _accessMatrixDetails.Id; } set { _accessMatrixDetails.Id = value; } }

        [Display(Name = "Access Matrix Id")]
        public int AccessMatrixId { get { return _accessMatrixDetails.AccessMatrixId; } set { _accessMatrixDetails.AccessMatrixId = value; } }


        [Display(Name = "Module Name")]
        public string ModuleName { get { return _accessMatrixDetails.ModuleName; } set { _accessMatrixDetails.ModuleName = value; } }

      
        [Display(Name = "User Type")]
        public string UserType { get { return _accessMatrixDetails.UserType; } set { _accessMatrixDetails.UserType = value; } }


        [Display(Name = "Index")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsIndex { get { return _accessMatrixDetails.IsIndex; } set { _accessMatrixDetails.IsIndex = value; } }

        [Display(Name = "Create")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsCreate { get { return _accessMatrixDetails.IsCreate; } set { _accessMatrixDetails.IsCreate = value; } }

        [Display(Name = "Edit")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsEdit { get { return _accessMatrixDetails.IsEdit; } set { _accessMatrixDetails.IsEdit = value; } }

        [Display(Name = "Details")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDetails { get { return _accessMatrixDetails.IsDetails; } set { _accessMatrixDetails.IsDetails = value; } }
        
        [Display(Name = "Tally")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsTally { get { return _accessMatrixDetails.IsTally; } set { _accessMatrixDetails.IsTally = value; } }

        [Display(Name = "Update")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsUpdate { get { return _accessMatrixDetails.IsUpdate; } set { _accessMatrixDetails.IsUpdate = value; } }

        [Display(Name = "Search")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsSearch { get { return _accessMatrixDetails.IsSearch; } set { _accessMatrixDetails.IsSearch = value; } }

        [Display(Name = "Export")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsExport { get { return _accessMatrixDetails.IsExport; } set { _accessMatrixDetails.IsExport = value; } }


        [Display(Name = "Delete")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _accessMatrixDetails.Isdeleted; } set { _accessMatrixDetails.Isdeleted = value; } }

        [Display(Name = "IsRowDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsRowDeleted { get { return _accessMatrixDetails.IsRowDeleted; } set { _accessMatrixDetails.IsRowDeleted = value; } }

        public AccessMatrixDetails AccessMatrixDetails { get { return _accessMatrixDetails; } set { _accessMatrixDetails = value; } }
        public List<AccessMatrix> AccessMatrixList { get { return _accessMatrixList; } set { value = _accessMatrixList; } }
        public List<Users> UserTypeList { get { return _userType; } set { value = _userType; } }
        #region CTOR
        public AccessMatrixDetailsModel()
        {
            _accessMatrixDetails = new AccessMatrixDetails();
        }
        public AccessMatrixDetailsModel(AccessMatrixDetails accessMatrixDetails)
        {
            _accessMatrixDetails = accessMatrixDetails;
        }
        public AccessMatrixDetailsModel(List<AccessMatrix> accessMatrix, List<Users> userType)
        {
            _accessMatrixDetails = new AccessMatrixDetails();
            _accessMatrixList = accessMatrix;
            _userType = userType;
        }
        public AccessMatrixDetailsModel(AccessMatrixDetails accessMatrixDetails, List<AccessMatrix> accessMatrix, List<Users> userType)
        {
            _accessMatrixDetails = accessMatrixDetails;
            _accessMatrixList = accessMatrix;
            _userType = userType;

        }


        #endregion //CTOR
    }


}
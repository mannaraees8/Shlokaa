using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
using SIMS.Models;
namespace SIMS.ViewModels.AccessMatrixModel
{
    public class AccessMatrixModel
    {

        AccessMatrix _accessMatrix;
        List<Users> _userList;

        [Display(Name = "ID")]
        public int Id { get { return _accessMatrix.Id; } set { _accessMatrix.Id = value; } }

        //[Display(Name = "StaffId")]
        //public int StaffId { get { return _accessMatrix.StaffId; } set { _accessMatrix.StaffId = value; } }
        
        //[Display(Name = "Employee Name")]
        //// [Required]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //public string StaffName { get { return _accessMatrix.StaffName; } set { _accessMatrix.StaffName = value; } }


        [Display(Name = "Module Name")]
       // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ModuleName { get { return _accessMatrix.ModuleName; } set { _accessMatrix.ModuleName = value; } }

     
        [Display(Name = "IsDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _accessMatrix.Isdeleted; } set { _accessMatrix.Isdeleted = value; } }

        public AccessMatrix AccessMatrix { get { return _accessMatrix; } set { _accessMatrix = value; } }
        //public List<Users> UsersList { get { return _userList; } set { value = _userList; } }
        #region CTOR
        public AccessMatrixModel()
        {
            _accessMatrix = new AccessMatrix();
        }
        public AccessMatrixModel(AccessMatrix accessMatrix)
        {
            _accessMatrix = accessMatrix;
        }
        //public AccessMatrixModel(List<Users> users)
        //{
        //    _accessMatrix = new AccessMatrix();
        //    _userList = users;
        //}
        //public AccessMatrixModel(AccessMatrix accessMatrix, List<Users> users)
        //{
        //    _accessMatrix = accessMatrix;
        //    _userList = users;

        //}


        #endregion //CTOR
    }


}
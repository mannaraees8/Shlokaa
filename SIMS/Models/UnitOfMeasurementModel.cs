using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.Models
{
    public class UnitOfMeasurementModel
    {
        private UnitOfMeasurement _unitOfMeasurement;


        [Display(Name = "ID")]
        public int Id { get { return _unitOfMeasurement.Id; } set { _unitOfMeasurement.Id = value; } }


        [Display(Name = "Unit Of Measurement")]
    //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UnitOfMeasurementName { get { return _unitOfMeasurement.UnitOfMeasurementName; } set { _unitOfMeasurement.UnitOfMeasurementName = value; } }

        [Display(Name = "IsDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _unitOfMeasurement.Isdeleted; } set { _unitOfMeasurement.Isdeleted = value; } }

        public UnitOfMeasurement UnitOfMeasurement { get { return _unitOfMeasurement; } set { _unitOfMeasurement = value; } }


        #region CTOR

        public UnitOfMeasurementModel()
        {
            _unitOfMeasurement = new UnitOfMeasurement();
        }

        public UnitOfMeasurementModel(UnitOfMeasurement UnitOfMeasurement)
        {
            _unitOfMeasurement = UnitOfMeasurement;
        }

        #endregion //CTOR
    }
}
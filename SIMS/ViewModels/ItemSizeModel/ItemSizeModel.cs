using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.ViewModels.ItemSizeModel
{
    public class ItemSizeModel
    {
        private List<UnitOfMeasurement> _unitOfMeasurementList;
        ItemSize _itemSize;

        [Display(Name = "ID")]
        public int Id { get { return _itemSize.Id; } set { _itemSize.Id = value; } }


        [Display(Name = "Size")]
       // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Size { get { return _itemSize.Size; } set { _itemSize.Size = value; } }

        [Display(Name = "Unit Of Measurement")]
      //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int UnitOfMeasurementId { get { return _itemSize.UnitOfMeasurementId; } set { _itemSize.UnitOfMeasurementId = value; } }

        public string UnitOfMeasurement { get { return _itemSize.UnitOfMeasurement; } set { _itemSize.UnitOfMeasurement = value; } }


        [Display(Name = "IsDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _itemSize.Isdeleted; } set { _itemSize.Isdeleted = value; } }

        public ItemSize itemSize { get { return _itemSize; } set { _itemSize = value; } }
        public List<UnitOfMeasurement> UnitOfMeasurementList { get { return _unitOfMeasurementList; } set { _unitOfMeasurementList = value; } }


        #region CTOR

        public ItemSizeModel()
        {
            _itemSize = new ItemSize();
        }

        public ItemSizeModel(ItemSize itemSize)
        {
            _itemSize = itemSize;
        }

        public ItemSizeModel(List<UnitOfMeasurement> UnitOfMeasurementList)
        {
            _itemSize = new ItemSize();
            _unitOfMeasurementList = UnitOfMeasurementList;
        }
        public ItemSizeModel(List<UnitOfMeasurement> UnitOfMeasurementList, ItemSize itemSize)
        {
            _itemSize = itemSize;
            _unitOfMeasurementList = UnitOfMeasurementList;
        }
        #endregion //CTOR
    }
}
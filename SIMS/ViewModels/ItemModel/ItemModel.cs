using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.ViewModels.ItemModel
{
    public class ItemModel
    {
        private Item _item;

        private List<Category> _categoryList;
        private List<SubCategory> _subCategoryList;
        private List<ItemSize> _itemSizeList;
        private List<UnitOfMeasurement> _unitOfMeasurementList;

        #region Props

        [Display(Name = "I D")]
        //[Required]
        public int Id { get { return _item.Id; } set { _item.Id = value; } }

        [Display(Name = "Item Name")]
       // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get { return _item.Name; } set { _item.Name = value; } }

        [Display(Name = "Size")]
      //  [Required]

        public int SizeId { get { return _item.SizeId; } set { _item.SizeId = value; } }

        [Display(Name = "Size")]


        public string Size { get { return _item.Size; } set { _item.Size = value; } }

        [Display(Name = "Unit")]
     //   [Required]

        public int UnitOfMeasurementId { get { return _item.UnitOfMeasurementId; } set { _item.UnitOfMeasurementId = value; } }

        [Display(Name = "Unit")]


        public string UnitOfMeasurement { get { return _item.UnitOfMeasurement; } set { _item.UnitOfMeasurement = value; } }



        [Display(Name = "Category")]
    //  [Required]

        public int CatagoryId { get { return _item.CatagoryId; } set { _item.CatagoryId = value; } }

        [Display(Name = "Category")]


        public string Category { get { return _item.Category; } set { _item.Category = value; } }

        [Display(Name = "Sub Category")]
    //    [Required]
        public int SubCatagoryId { get { return _item.SubCatagoryId; } set { _item.SubCatagoryId = value; } }


        [Display(Name = "Sub Category")]

        public string SubCategory { get { return _item.SubCategory; } set { _item.SubCategory = value; } }


        [Display(Name = "Price")]
         
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Price { get { return _item.Price; } set { _item.Price = value; } }

        [Display(Name = "Image")]
        public string Photo { get { return _item.Imagepath; } set { _item.Imagepath = value; } }
        public string OldFile { get { return _item.OldFile; } set { _item.OldFile = value; } }

        public string Description { get { return _item.Description; } set { _item.Description =  value; } }

        //[Required]
        public bool Isdeleted { get { return _item.Isdeleted; } set { _item.Isdeleted = value; } }

        [Display(Name = "Image")]

        //public byte[] Photo { get { return _item.Photo; } set { _item.Photo = value; } }
        public byte[] Thumbnail { get { return _item.Thumbnail; } set { _item.Thumbnail = value; } }
        public Item Item { get { return _item; } set { _item = value; } }
  
        public List<Category> CategoryList { get { return _categoryList; } }
        public List<SubCategory> SubCategoryList { get { return _subCategoryList; } }
        public List<ItemSize> ItemSizeList { get { return _itemSizeList; } }
        public List<UnitOfMeasurement> UnitOfMeasurementList { get { return _unitOfMeasurementList; } }
     

        #endregion //Props

        #region CTOR

        public ItemModel()
        {
            _item = new Item();
        }

        public ItemModel(Item item)
        {
            _item = item;
        }
   
      
        public ItemModel(List<Category> CategoryList, List<SubCategory> SubCategoryList, List<ItemSize> ItemSizeList, List<UnitOfMeasurement> UnitOfMeasurementList)
        {
            _item = new Item();
            _categoryList = CategoryList;
            _subCategoryList = SubCategoryList;
            _itemSizeList = ItemSizeList;
            _unitOfMeasurementList = UnitOfMeasurementList;
        }

        public ItemModel(List<Category> CategoryList,List<SubCategory> SubCategoryList, List<ItemSize> ItemSizeList, List<UnitOfMeasurement> UnitOfMeasurementList, Item item)
        {
            _item = item;         
            _categoryList = CategoryList;
            _subCategoryList = SubCategoryList;
            _itemSizeList = ItemSizeList;
            _unitOfMeasurementList = UnitOfMeasurementList;
        }


        #endregion //CTOR

    }
}


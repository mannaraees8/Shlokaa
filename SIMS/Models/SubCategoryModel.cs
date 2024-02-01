using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.Models
{
    public class SubCategoryModel
    {
        private SubCategory _subcategory;
        private List<Category> _categoryList;



        [Display(Name = "ID")]
        public int Id { get { return _subcategory.Id; } set { _subcategory.Id = value; } }

        public int CategoryID { get { return _subcategory.CategoriesId; } set { _subcategory.CategoriesId = value; } }

        [Display(Name = "Category")]
        public string Category { get { return _subcategory.Category; } set { _subcategory.Category = value; } }

        [Display(Name = "Sub Category")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get { return _subcategory.Name; } set { _subcategory.Name = value; } }


        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Discription { get { return _subcategory.Discription; } set { _subcategory.Discription = value; } }

        [Display(Name = "IsDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _subcategory.Isdeleted; } set { _subcategory.Isdeleted = value; } }

        public SubCategory SubCategory { get { return _subcategory; } set { _subcategory = value; } }
        public List<Category> CategoryList { get { return _categoryList; } set { _categoryList = value; } }



        #region CTOR

        public SubCategoryModel()
        {
            _subcategory = new SubCategory();
        }

        public SubCategoryModel(SubCategory subcategory)
        {
            _subcategory = subcategory;
        }
        public SubCategoryModel(List<Category> categoryList)
        {
            _subcategory = new SubCategory();
            _categoryList = categoryList;
        } 
        public SubCategoryModel(List<Category> categoryList, SubCategory subcategory)
        {
            _subcategory = subcategory;
            _categoryList = categoryList;
        }

        #endregion //CTOR
    }
}

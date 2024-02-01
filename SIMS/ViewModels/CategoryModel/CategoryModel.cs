using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
using SIMS.Models;
namespace SIMS.ViewModels.CategoryModel
{
    public class CategoryModel
    {
           
        Category _category;

        [Display(Name = "ID")]
        public int Id { get { return _category.Id; } set { _category.Id = value; } }
       

        [Display(Name = "Category")]
       // [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get { return _category.Name; } set { _category.Name = value; } }

     

        [Display(Name = "Description")]
    //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Discription { get { return _category.Discription; } set { _category.Discription = value; } }

        [Display(Name = "Image")]
      
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Thumbnail { get { return _category.ImagePath; } set { _category.ImagePath = value; } }
        public string OldFile { get { return _category.OldFile; } set { _category.OldFile = value; } }

        [Display(Name = "IsDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _category.Isdeleted; } set { _category.Isdeleted = value; } }

        public Category Category { get { return _category; } set { _category = value; } }

        #region CTOR
        public CategoryModel()
        {
            _category = new Category();
        }
        public CategoryModel(Category category)
        {
            _category = category;
        }
     

        #endregion //CTOR
    }


}
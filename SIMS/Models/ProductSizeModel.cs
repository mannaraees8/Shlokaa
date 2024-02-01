using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;


namespace SIMS.Models
{
    public class ProductSizeModel
    {
        private ProductSize _productsize;
        private List<ProductSize> _productsizelist;

        [Display(Name = "ID")]
        //[Required]
        public int Id { get { return _productsize.Id; } set { _productsize.Id = value; } }

        [Display(Name = "Size Id")]
        // [Required]

        public int SizeId { get { return _productsize.SizeId; } set { _productsize.SizeId = value; } }
        [Display(Name = "Size")]
        // [Required]

        public string Size { get { return _productsize.Size; } set { _productsize.Size = value; } }
        [Display(Name = "Item Id")]
        // [Required]

        public int ItemId { get { return _productsize.ItemId; } set { _productsize.ItemId = value; } }
        public List<ProductSize> ProductSizeList { get { return _productsizelist; } set { _productsizelist = value; } }



        public ProductSizeModel()
        {
            _productsize = new ProductSize();
        }
        public ProductSizeModel(ProductSize productSizelist)
        {
            _productsize = productSizelist;
           
        }
    }
}
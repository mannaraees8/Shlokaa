using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.ViewModels.SalesOrderModel
{
    public class SalesOrderModel
    {
        private SalesOrder _salesOrder;

        private List<Category> _categoryList;
        private List<SubCategory> _subCategoryList;
        private List<ProductSize> _itemSizeList;
        private List<Item> _itemList;
        private List<UnitOfMeasurement> _unitOfMeasurementList;
        private List<SalesOrder> _usersList;


        #region Props

        [Display(Name = "Order No")]
        //[Required]
        public int Id { get { return _salesOrder.Id; } set { _salesOrder.Id = value; } }

        [Display(Name = "Sales Order Details No")]
        //[Required]
        public int SalesOrderDetailsId { get { return _salesOrder.SalesOrderDetailsId; } set { _salesOrder.SalesOrderDetailsId = value; } }

        [Display(Name = "Order Date")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Timestamp { get { return _salesOrder.Timestamp; } set { _salesOrder.Timestamp = value; } }

        [Display(Name = "Customer ID")]
        //   [Required]
        public int CustomerId { get { return _salesOrder.Customerid; } set { _salesOrder.Customerid = value; } }

        [Display(Name = "Party Name")]

        public string CustomerName { get { return _salesOrder.CustomerName; } set { _salesOrder.CustomerName = value; } }

        [Display(Name = "Staff ID")]
        //  [Required]
        public int StaffId { get { return _salesOrder.StaffId; } set { _salesOrder.StaffId = value; } }

        [Display(Name = "Employee Name")]

        public string StaffName { get { return _salesOrder.StaffName; } set { _salesOrder.StaffName = value; } }
        public string UserType { get { return _salesOrder.UserType; } set { _salesOrder.UserType = value; } }

        [Display(Name = "Item ID")]
        //   [Required]
        public int ItemId { get { return _salesOrder.ItemId; } set { _salesOrder.ItemId = value; } }
        [Display(Name = "Item")]

        public string ItemName { get { return _salesOrder.ItemName; } set { _salesOrder.ItemName = value; } }

        [Display(Name = "Category ID")]
        //   [Required]
        public int CategoryId { get { return _salesOrder.CategoryId; } set { _salesOrder.CategoryId = value; } }
        [Display(Name = "Category")]

        public string CategoryName { get { return _salesOrder.CategoryName; } set { _salesOrder.CategoryName = value; } }

        [Display(Name = "Sub Category ID")]
        //  [Required]
        public int SubCategoryId { get { return _salesOrder.SubCategoryId; } set { _salesOrder.SubCategoryId = value; } }
        [Display(Name = "Sub Category")]

        public string SubCategoryName { get { return _salesOrder.SubCategoryName; } set { _salesOrder.SubCategoryName = value; } }

        [Display(Name = "Unit ID")]
        //       [Required]
        public int UnitOfMeasurementId { get { return _salesOrder.UnitOfMeasurementId; } set { _salesOrder.UnitOfMeasurementId = value; } }


        [Display(Name = "Unit")]

        public string UnitOfMeasurement { get { return _salesOrder.UnitOfMeasurement; } set { _salesOrder.UnitOfMeasurement = value; } }

        [Display(Name = "Size ID")]
        //     [Required]
        public int SizeId { get { return _salesOrder.SizeId; } set { _salesOrder.SizeId = value; } }

        [Display(Name = "Size")]
        public string SizeName { get { return _salesOrder.Size; } set { _salesOrder.Size = value; } }


        [Display(Name = "Quantity")]
        //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int Quantity { get { return _salesOrder.Quantity; } set { _salesOrder.Quantity = value; } }

        [Display(Name = "Payment Mode")]

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Paymentmode { get { return _salesOrder.Paymentmode; } set { _salesOrder.Paymentmode = value; } }


        [Display(Name = "Order Value")]
        public int OrderValue { get { return _salesOrder.OrderValue; } set { _salesOrder.OrderValue = value; } }

        [Display(Name = "Is Deleted")]

        public bool Isdeleted { get { return _salesOrder.Isdeleted; } set { _salesOrder.Isdeleted = value; } }

        [Display(Name = "Add to tally")]
        public bool Isaddedtotally { get { return _salesOrder.Isaddedtotally; } set { _salesOrder.Isaddedtotally = value; } }
      
        [Display(Name = "Total order value")]
        public int TotalOrderValue { get { return _salesOrder.TotalOrderValue; } set { _salesOrder.TotalOrderValue = value; } }

        [Display(Name = "Achieved")]
        //[Required]
        public int Achieved { get { return _salesOrder.Achieved; } set { _salesOrder.Achieved = value; } }

        [Display(Name = "Pending")]
        //[Required]
        public int Pending { get { return _salesOrder.Pending; } set { _salesOrder.Pending = value; } }

        public SalesOrder SalesOrder { get { return _salesOrder; } set { _salesOrder = value; } }

        public List<Item> ItemList { get { return _itemList; } }
        public List<ProductSize> ItemSizeList { get { return _itemSizeList; } }
        public List<Category> CategoryList { get { return _categoryList; } }
        public List<SubCategory> SubCategoryList { get { return _subCategoryList; } }
        public List<UnitOfMeasurement> UnitOfMeasurementList { get { return _unitOfMeasurementList; } }
        public List<SalesOrder> UsersList { get { return _usersList; } }





        #endregion //Props

        #region CTOR

        public SalesOrderModel()
        {
            _salesOrder = new SalesOrder();
        }

        public SalesOrderModel(SalesOrder salesOrder)
        {
            _salesOrder = salesOrder;
        }
        public SalesOrderModel(List<Item> ItemList, List<ProductSize> ItemSizeList, List<Category> CategoryList, List<SubCategory> SubCategoryList, List<UnitOfMeasurement> UnitOfMeasurementList, SalesOrder salesOrder)
        {
            _salesOrder = salesOrder;

            _itemList = ItemList;
            _itemSizeList = ItemSizeList;
            _categoryList = CategoryList;
            _subCategoryList = SubCategoryList;
            _unitOfMeasurementList = UnitOfMeasurementList;



        }
        public SalesOrderModel(List<SalesOrder> userSalesOrderList)
        {
            _usersList = userSalesOrderList;
        }

        #endregion //CTOR

    }
}
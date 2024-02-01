using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using SIMS.ViewModels.CategoryModel;
using SIMS.ViewModels.ItemModel;
using SIMS.Models;
using SIMS.BL;
using System.IO;
using ClosedXML.Excel;
using PagedList;

namespace SIMS.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public ActionResult Index(int? pageIndex, string search = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Item");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetItemModelList(search).ToPagedList(pageIndex ?? 1, 10));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

        private IEnumerable<ItemModel> GetItemModelList(string search = "")
        {
            List<ItemModel> itemModelList = new List<ItemModel>();
            List<Item> itemList = Item.RetrieveAll();

            if (search.Length > 0)
            {
                try
                {
                    int s = Convert.ToInt32(search);
                    char c = (char)s;
                    itemList = itemList.OfType<Item>().Where(x => char.IsDigit(c)).ToList();

                }
                catch
                {
                    GenericList<Item> g = new GenericList<Item>();
                    itemList = g.SerachFun(itemList, search);
                    itemList = itemList.Distinct().ToList();
                }

            }

            foreach (Item a in itemList)
            {
                itemModelList.Add(new ItemModel(a));
            }

            return itemModelList;
        }

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Item");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        List<Category> lstCategoryList = Category.RetrieveAll();
                        Category category = new Category();
                        category.Name = "Select";
                        lstCategoryList.Insert(0, category);


                        List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
                        SubCategory subCategory = new SubCategory();
                        subCategory.Name = "Select";
                        lstSubCategoryList.Insert(0, subCategory);



                        List<ItemSize> lstItemSizeList = ItemSize.RetrieveAll();
                        ItemSize itemSize = new ItemSize();
                        itemSize.Size = "Select";
                        lstItemSizeList.Insert(0, itemSize);


                        List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                        UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
                        uniOfMeasurement.UnitOfMeasurementName = "Select";
                        lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);


                        ItemModel itemModel = new ItemModel(lstCategoryList, lstSubCategoryList, lstItemSizeList, lstUnitOfMeasurementList);


                        return View(itemModel);
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }

        }

        [HttpPost]
        public ActionResult Create(ItemModel itemModel, HttpPostedFileBase ImageFile, List<int> SizeId)
        {

            bool inValidState = false;
            //byte[] byteDocContents = null;
            string fullpath = "";

            if (itemModel.CatagoryId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select category";
            }
            else if (itemModel.SubCatagoryId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Size";

            }
            else if (SizeId == null)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Size";

            }

            else if (itemModel.UnitOfMeasurementId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Unit";
            }
            else if (itemModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Category Name";
            }
            else if (ImageFile == null || ImageFile.FileName == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Upload Image";

            }

            if (ImageFile != null && ImageFile.FileName != "")
            {
                if (ImageFile.ContentLength >= (1024 * 1024) * 2)
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "File size must be less than 2MB";
                }
                else
                {

                    string itemImage = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    ImageFile.SaveAs(Server.MapPath("~/ProductImages/" + itemImage));
                    fullpath = "~/ProductImages/" + itemImage;

                }
            }


            if (inValidState == false)
            {


                if (ModelState.IsValid)
                {

                    int ItemId = Item.Create(itemModel.Name.Trim(), itemModel.UnitOfMeasurementId, itemModel.SubCatagoryId, itemModel.CatagoryId, itemModel.Price, fullpath);
                    if (ItemId == 0)
                    {
                        TempData["ErrorMsg"] = "Item already exists.";
                        List<Category> lstCategoryList = Category.RetrieveAll();
                        Category category = new Category();
                        category.Name = "Select";
                        lstCategoryList.Insert(0, category);

                        List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
                        SubCategory subCategory = new SubCategory();
                        subCategory.Name = "Select";
                        lstSubCategoryList.Insert(0, subCategory);

                        List<ItemSize> lstItemSizeList = ItemSize.RetrieveAll();
                        ItemSize itemSize = new ItemSize();
                        itemSize.Size = "Select";
                        lstItemSizeList.Insert(0, itemSize);

                        List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                        UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
                        uniOfMeasurement.UnitOfMeasurementName = "Select";
                        lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

                        ItemModel newitemModel = new ItemModel(lstCategoryList, lstSubCategoryList, lstItemSizeList, lstUnitOfMeasurementList);


                        return View(newitemModel);
                    }
                    else
                    {
                        foreach (int a in SizeId)
                        {
                            ProductSize.Create(ItemId, a);
                        }
                    }

                }
                return RedirectToAction("Index", "Item");
            }
            else
            {
                List<Category> lstCategoryList = Category.RetrieveAll();
                Category category = new Category();
                category.Name = "Select";
                lstCategoryList.Insert(0, category);





                List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
                SubCategory subCategory = new SubCategory();
                subCategory.Name = "Select";
                lstSubCategoryList.Insert(0, subCategory);



                List<ItemSize> lstItemSizeList = ItemSize.RetrieveAll();
                ItemSize itemSize = new ItemSize();
                itemSize.Size = "Select";
                lstItemSizeList.Insert(0, itemSize);


                List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
                uniOfMeasurement.UnitOfMeasurementName = "Select";
                lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);


                ItemModel newitemModel = new ItemModel(lstCategoryList, lstSubCategoryList, lstItemSizeList, lstUnitOfMeasurementList);


                return View(newitemModel);
            }



        }

        public JsonResult GetSubCategories(int CategoryID)
        {

            List<SubCategory> subCategory = SubCategory.RetrieveByCategoryId(CategoryID).ToList();


            return new JsonResult { Data = subCategory, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetItemSize(int UnitOfMeasurementId)
        {

            List<ItemSize> ItemSizes = ItemSize.RetrieveByUnitOfMeasurementId(UnitOfMeasurementId).ToList();
            return new JsonResult { Data = ItemSizes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getProductSizeList(int ItemId)
        {

            List<ProductSize> productSizes = ProductSize.RetrieveById(ItemId).ToList();

            return new JsonResult { Data = productSizes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        public ActionResult AddCategoryInfo()
        {
            return View();
        }


        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Item");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        List<Category> lstCategoryList = Category.RetrieveAll();
                        Category category = new Category();
                        category.Name = "Select";
                        lstCategoryList.Insert(0, category);





                        List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
                        SubCategory subCategory = new SubCategory();
                        subCategory.Name = "Select";
                        lstSubCategoryList.Insert(0, subCategory);



                        List<ItemSize> lstItemSizeList = ItemSize.RetrieveAll();
                        ItemSize itemSize = new ItemSize();
                        itemSize.Size = "Select";
                        lstItemSizeList.Insert(0, itemSize);


                        List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                        UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
                        uniOfMeasurement.UnitOfMeasurementName = "Select";
                        lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

                        return View(new ItemModel(lstCategoryList, lstSubCategoryList, lstItemSizeList, lstUnitOfMeasurementList, Item.RetrieveById(id)));

                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost]
        public ActionResult Edit(ItemModel itemModel, HttpPostedFileBase ImageFile, List<int> SizeId)
        {
            bool inValidState = false;
            //byte[] byteDocContents = null;

            if (itemModel.CatagoryId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select category";
            }

            else if (itemModel.SubCatagoryId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Sub Category";
            }
            else if (itemModel.UnitOfMeasurementId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Unit";
            }
            else if (itemModel.Name == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Category Name";
            }
            else if (SizeId == null)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Size";
            }

            if (ImageFile != null && ImageFile.FileName != "")
            {
                if (ImageFile.ContentLength >= (1024 * 1024) * 2)
                {
                    inValidState = true;
                    TempData["ErrorMsg"] = "File size must be less than 2MB";
                }

            }
            if (inValidState == false)
            {

                if (ImageFile != null && ImageFile.FileName != "")
                {

                    string OldFile = Request.MapPath(itemModel.OldFile);
                    if (System.IO.File.Exists(OldFile))
                    {
                        System.IO.File.Delete(OldFile);
                    }

                    string itemImage = "";
                    itemImage = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    ImageFile.SaveAs(Server.MapPath("~/ProductImages/" + itemImage));
                    string fullpath = "~/ProductImages/" + itemImage;


                    if (ModelState.IsValid)
                    {
                        bool status = itemModel.Item.Update(fullpath);
                        if (status == true)
                        {
                            ProductSize.Delete(itemModel.Id);
                            foreach (int a in SizeId)
                            {
                                ProductSize.Create(itemModel.Id, a);
                            }
                            return RedirectToAction("Index", "Item");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Item already exists.";

                        }
                    }

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        bool status = itemModel.Item.Update();
                        if (status == true)
                        {
                            ProductSize.Delete(itemModel.Id);
                            foreach (int a in SizeId)
                            {
                                ProductSize.Create(itemModel.Id, a);
                            }
                            return RedirectToAction("Index", "Item");
                        }
                        else
                        {
                            TempData["ErrorMsg"] = "Item already exists.";
                        }
                    }
                }
            }

            List<Category> lstCategoryList = Category.RetrieveAll();
            Category category = new Category();
            category.Name = "Select";
            lstCategoryList.Insert(0, category);





            List<SubCategory> lstSubCategoryList = SubCategory.RetrieveAll();
            SubCategory subCategory = new SubCategory();
            subCategory.Name = "Select";
            lstSubCategoryList.Insert(0, subCategory);



            List<ItemSize> lstItemSizeList = ItemSize.RetrieveAll();
            ItemSize itemSize = new ItemSize();
            itemSize.Size = "Select";
            lstItemSizeList.Insert(0, itemSize);


            List<UnitOfMeasurement> lstUnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement uniOfMeasurement = new UnitOfMeasurement();
            uniOfMeasurement.UnitOfMeasurementName = "Select";
            lstUnitOfMeasurementList.Insert(0, uniOfMeasurement);

            return View(new ItemModel(lstCategoryList, lstSubCategoryList, lstItemSizeList, lstUnitOfMeasurementList, Item.RetrieveById(itemModel.Id)));


        }




        public ActionResult Delete(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Item");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        Item item = Item.RetrieveById(id);
                        if (item == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new ItemModel(item));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Item item = Item.RetrieveById(id);
            item.Delete();
            ProductSize.Delete(id);
            return RedirectToAction("Index", "Item");
        }

        public ActionResult Details(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("Item");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsDetails == true)
                    {
                        ProductSizeModel productSizeModel = new ProductSizeModel();

                        return View(new ItemModel(Item.RetrieveById(id)));
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Your not authorised to access this page";
                        return RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    TempData["ErrorMsg"] = "Access permission not defined";
                    return RedirectToAction("Index", "Staff");

                }
            }
            else
            {
                TempData["ErrorMsg"] = "Module Not defined";
                return RedirectToAction("Index", "Staff");
            }
        }
        [ChildActionOnly]
        public ActionResult RenderItemISize(ItemModel itemModel)
        {
            List<ProductSizeModel> ProductSizeModelList = new List<ProductSizeModel>();
            foreach (ProductSize a in ProductSize.RetrieveById(itemModel.Id))
            {
                ProductSizeModelList.Add(new ProductSizeModel(a));
            }
            return PartialView("_ItemSizes", ProductSizeModelList);
        }

        public ActionResult Excel(string search = "")
        {
            List<ItemModel> itemModelList = new List<ItemModel>();
            List<Item> itemList = Item.RetrieveAll();

            if (search.Length > 0)
            {
                try
                {
                    int s = Convert.ToInt32(search);
                    char c = (char)s;
                    itemList = itemList.OfType<Item>().Where(x => char.IsDigit(c)).ToList();

                }
                catch
                {
                    GenericList<Item> g = new GenericList<Item>();
                    itemList = g.SerachFun(itemList, search);
                    itemList = itemList.Distinct().ToList();
                }

            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ItemList");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 4).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Item Name ";
                worksheet.Cell(currentrow, 2).Value = "Unit";
                worksheet.Cell(currentrow, 3).Value = "Category";
                worksheet.Cell(currentrow, 4).Value = "Sub Category";


                foreach (Item a in itemList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Name;
                    worksheet.Cell(currentrow, 2).Value = a.UnitOfMeasurement;
                    worksheet.Cell(currentrow, 3).Value = a.Category;
                    worksheet.Cell(currentrow, 4).Value = a.SubCategory;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ItemList.xlsx");
                }
            }

        }
    }
}


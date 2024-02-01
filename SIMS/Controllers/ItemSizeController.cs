using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.ViewModels.ItemSizeModel;
using SIMS.BL;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using System.IO;
using PagedList;

namespace SIMS.Controllers
{
    public class ItemSizeController : Controller
    {
        // GET: ItemSize
        [Authorize]

        public ActionResult Index(int? pageIndex, string search = "")
        {

            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ItemSize");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);

                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsIndex == true)
                    {
                        return View(GetItemSizeModelList(search).ToPagedList(pageIndex ?? 1, 10));
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


        private IEnumerable<ItemSizeModel> GetItemSizeModelList(string search = "")
        {
            List<ItemSizeModel> itemSizeModelList = new List<ItemSizeModel>();
            List<ItemSize> itemSizeList = ItemSize.RetrieveAll();

            if (search.Length > 0)
            {
                itemSizeList = itemSizeList.OfType<ItemSize>().Where(s => s.Size.Contains(search) || s.Size.ToUpper().Contains(search.ToUpper()) || s.UnitOfMeasurement.ToUpper().Contains(search.ToUpper())).ToList();
            }
            foreach (ItemSize a in itemSizeList)
            {
                itemSizeModelList.Add(new ItemSizeModel(a));
            }


            return itemSizeModelList;
        }



        public ActionResult Edit(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ItemSize");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsEdit == true)
                    {
                        List<UnitOfMeasurement> UnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                        UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
                        unitOfMeasurement.UnitOfMeasurementName = "Select";
                        UnitOfMeasurementList.Insert(0, unitOfMeasurement);
                        return View(new ItemSizeModel(UnitOfMeasurementList, ItemSize.RetrieveById(id)));
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

        public ActionResult Create()
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ItemSize");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsCreate == true)
                    {
                        List<UnitOfMeasurement> UnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
                        UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
                        unitOfMeasurement.UnitOfMeasurementName = "Select";
                        UnitOfMeasurementList.Insert(0, unitOfMeasurement);
                        ItemSizeModel itemSize = new ItemSizeModel(UnitOfMeasurementList);
                        return View(itemSize);
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
        public ActionResult Edit(ItemSizeModel itemSizeModel)
        {
            bool inValidState = false;


            if (itemSizeModel.Size == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Size";
            }
            else if (itemSizeModel.UnitOfMeasurementId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Unit";

            }


            if (inValidState == false)
            {
                if (ModelState.IsValid)
                {
                    bool status = itemSizeModel.itemSize.Update();
                    if (status == true)
                    {
                        return RedirectToAction("Index", "ItemSize");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Size already exists.";
                    }
                }

            }

            List<UnitOfMeasurement> UnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
            unitOfMeasurement.UnitOfMeasurementName = "Select";
            UnitOfMeasurementList.Insert(0, unitOfMeasurement);

            return View(new ItemSizeModel(UnitOfMeasurementList));



        }


        [HttpPost]
        public ActionResult Create(ItemSizeModel itemSizeModel)
        {
            bool inValidState = false;


            if (itemSizeModel.Size == "")
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Enter Size";
            }
            else if (itemSizeModel.UnitOfMeasurementId == 0)
            {
                inValidState = true;
                TempData["ErrorMsg"] = "Please Select Unit";

            }


            if (inValidState == false)
            {

                if (ModelState.IsValid)
                {
                    int count = ItemSize.Create(itemSizeModel.Size.Trim(), itemSizeModel.UnitOfMeasurementId);
                    if (count != 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Size already exists.";
                    }
                }


            }

            List<UnitOfMeasurement> UnitOfMeasurementList = UnitOfMeasurement.RetrieveAll();
            UnitOfMeasurement unitOfMeasurement = new UnitOfMeasurement();
            unitOfMeasurement.UnitOfMeasurementName = "Select";
            UnitOfMeasurementList.Insert(0, unitOfMeasurement);

            return View(new ItemSizeModel(UnitOfMeasurementList));
        }

        public ActionResult Delete(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ItemSize");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.Isdeleted == true)
                    {
                        ItemSize itemSize = ItemSize.RetrieveById(id);
                        if (itemSize == null)
                        {
                            return HttpNotFound();
                        }
                        return View(new ItemSizeModel(itemSize));
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
            bool status = false;
            ItemSize itemSize = ItemSize.RetrieveById(id);
            status = itemSize.Delete();
            if (status == false)
            {
                return RedirectToAction("Index", "ItemSize");
            }

            ViewBag.count = true;
            return View("Delete", new ItemSizeModel(itemSize));
        }

        public ActionResult Details(int id = 0)
        {
            string userType = Users.RetrieveStaffUserTypeByEmail(HttpContext.User.Identity.Name);
            AccessMatrix accessMatrix = AccessMatrix.RetrieveByModuleName("ItemSize");
            if (accessMatrix != null)
            {
                AccessMatrixDetails accessMatrixDetails = AccessMatrixDetails.RetrieveAccess(accessMatrix.Id, userType);
                if (accessMatrixDetails != null)
                {
                    if (accessMatrixDetails.IsDetails == true)
                    {
                        return View(new ItemSizeModel(ItemSize.RetrieveById(id)));
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

        public ActionResult Excel(string search = "")
        {
            List<ItemSizeModel> itemSizeModelList = new List<ItemSizeModel>();
            List<ItemSize> itemSizeList = ItemSize.RetrieveAll();

            if (search.Length > 0)
            {
                itemSizeList = itemSizeList.OfType<ItemSize>().Where(s => s.Size.Contains(search) || s.Size.ToUpper().Contains(search.ToUpper()) || s.UnitOfMeasurement.ToUpper().Contains(search.ToUpper())).ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ItemSize List");
                var currentrow = 1;
                worksheet.Cell(currentrow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentrow, 2).Style.Font.Bold = true;

                worksheet.Cell(currentrow, 1).Value = "Size";
                worksheet.Cell(currentrow, 2).Value = "Unit Of Measurement";


                foreach (ItemSize a in itemSizeList)
                {
                    currentrow++;
                    worksheet.Cell(currentrow, 1).Value = a.Size;
                    worksheet.Cell(currentrow, 2).Value = a.UnitOfMeasurement;

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ItemSizeList.xlsx");
                }
            }

        }
    }
}
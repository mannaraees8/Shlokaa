using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SIMS.Models;
using SIMS.BL;
using SIMS.ViewModels.ItemModel;

namespace SIMS.Controllers
{
    [Authorize]
    public class SalesOrderDetailsController : Controller
    {
        //	//
        //	// GET: /SalesOrderDetails/

        //	public ActionResult Index(int pageIndex = 0)
        //	{
        //		return View(GetSalesOrderDetailsModelList(pageIndex));
        //	}


        //	private IEnumerable<SalesOrderDetailsModel> GetSalesOrderDetailsModelList(int pageIndex)
        //	{
        //		int pageSize = 10;
        //		List<SalesOrderDetailsModel> salesOrderDetailsModelList = new List<SalesOrderDetailsModel>();
        //		foreach (SalesOrderDetails a in SalesOrderDetails.RetrieveAll())
        //		{
        //			salesOrderDetailsModelList.Add(new SalesOrderDetailsModel(a));
        //		}

        //		ViewBag.PageIndex = pageIndex;
        //		ViewBag.PageCount = (salesOrderDetailsModelList.Count + pageSize - 1) / pageSize;

        //		return salesOrderDetailsModelList.Skip(pageIndex * pageSize).Take(pageSize);
        //	}

        //	public ActionResult Edit(int id=0)
        //	{
        //		return View(new SalesOrderDetailsModel(SalesOrderDetails.RetrieveById(id)));
        //	}

        //	[HttpPost]
        //	public ActionResult Edit(SalesOrderDetailsModel salesOrderDetailsModel)
        //	{
        //		if (ModelState.IsValid)
        //		{
        //			salesOrderDetailsModel.SalesOrderDetails.Update();
        //			return RedirectToAction("Index");
        //		}
        //		return View(salesOrderDetailsModel);
        //	}

        public ActionResult Create(int pageIndex=0)
        {
            return View(GetItemModelListWithoutRawMaterial(pageIndex));
        }
        private IEnumerable<ItemModel> GetItemModelListWithoutRawMaterial(int pageIndex)
        {

            int pageSize = 10;
            List<ItemModel> itemModelList = new List<ItemModel>();
            List<Item> lstItem = Item.RetrieveAllWithoutRawMaterial();

            List<Item> lstFirstItem = lstItem.GroupBy(o => o.Id).Select(g => g.First()).ToList();
            List<Item> lstLastItem = lstItem.GroupBy(o => o.Id).Select(g => g.Last()).ToList();
            foreach (Item a in lstFirstItem)
            {
                var i = lstLastItem.OfType<Item>().Where(s => s.Id == a.Id).ToList();

                if (a.UnitOfMeasurement != "Inches" || a.UnitOfMeasurement != "Inch" || a.UnitOfMeasurement != "inches" || a.UnitOfMeasurement != "inch")
                {
                    if (i != null)
                    {
                        if (a.Size != i[0].Size)
                        {
                            a.Size = a.Size + "," + i[0].Size;
                        }
                        else
                        {
                            a.Size = a.Size;
                        }

                    }

                }
                else
                {
                    var j = lstItem.OfType<Item>().Where(s => s.Id == a.Id).ToList();


                    if (j != null)
                    {
                        if (a.Size != j[0].Size)
                        {
                            a.Size = a.Size + "-" + j[0].Size;
                        }
                        else
                        {
                            a.Size = a.Size;
                        }


                    }



                }


            }

            foreach (Item a in lstFirstItem)
            {
                itemModelList.Add(new ItemModel(a));
            }
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = (itemModelList.Count + pageSize - 1) / pageSize;

            return itemModelList.Skip(pageIndex * pageSize).Take(pageSize);
        }


        //	[HttpPost]
        //	public ActionResult Create(SalesOrderDetailsModel salesOrderDetailsModel)
        //	{
        //		if (ModelState.IsValid)
        //		{
        //			Count = SalesOrderDetails.Create(SalesOrderId, a.CategoryId, a.SubCategoryId, a.ItemId, a.UnitOfMeasurementId, a.SizeId, a.Quantity);

        //			SalesOrderDetails.Create(salesOrderDetailsModel.Salesorderid, salesOrderDetailsModel.Itemid, salesOrderDetailsModel.Size, salesOrderDetailsModel.Unit, salesOrderDetailsModel.Quantity);
        //			return RedirectToAction("Index");
        //		}

        //		return View(salesOrderDetailsModel);
        //	}

        //	public ActionResult Delete(int id=0)
        //	{
        //		SalesOrderDetails salesOrderDetails = SalesOrderDetails.RetrieveById(id);
        //		if (salesOrderDetails == null)
        //		{
        //			return HttpNotFound();
        //		}
        //		return View(new SalesOrderDetailsModel(salesOrderDetails));
        //	}

        //	[HttpPost, ActionName("Delete")]
        //	public ActionResult DeleteConfirmed(int id=0)
        //	{
        //		SalesOrderDetails salesOrderDetails = SalesOrderDetails.RetrieveById(id);
        //		salesOrderDetails.Delete();
        //		return RedirectToAction("Index");
        //	}

        //	public ActionResult Details(int id=0)
        //	{
        //		return View(new SalesOrderDetailsModel(SalesOrderDetails.RetrieveById(id)));
        //	}
    }
}


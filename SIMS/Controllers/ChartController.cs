using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.ViewModels.SalesOrderModel;
using SIMS.ViewModels.UsersModel;
using SIMS.BL;
namespace SIMS.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        //Tally Data Retrieve start
        public JsonResult TallyPendingCount()
        {
            int tallyCount = SalesOrder.TotalTallyPandingCount();
            return new JsonResult { Data = tallyCount, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult TallyPendingCountById(int userId)
        {
            int tallyCount = SalesOrder.TotalTallyPandingCountById(userId);
            return new JsonResult { Data = tallyCount, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult TallyChartUserIdList()
        {
            List<SalesOrder> salesOrderUserList = SalesOrder.RetrieveTallyChartUserList().ToList();
            return new JsonResult { Data = salesOrderUserList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult TallyChart(int userId, DateTime startDate, DateTime endDate)
        {
            List<SalesOrder> salesOrderLsit = SalesOrder.RetrieveAllChartTallyData(userId, startDate, endDate).ToList();
            return new JsonResult { Data = salesOrderLsit, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult TallyChartRetreiveStaffId()
        {
            int staffId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);

            return new JsonResult { Data = staffId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Tally Data Retrieve end

        //Order status
        public JsonResult RetrieveOrderStatusChartDataById(int id, DateTime startDate, DateTime endDate)
        {
            List<VisitLog> visitLogLsit = VisitLog.RetrieveOrderStatusChartDataById(id, startDate, endDate).ToList();
            return new JsonResult { Data = visitLogLsit, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Payment
        public JsonResult RetrievePaymentChartDataById(int id, DateTime startDate, DateTime endDate)
        {
            List<object> visitLogLsit = new List<object>();
            visitLogLsit = VisitLog.RetrievePaymentModeChartDataById(id, startDate, endDate).ToList();
            return new JsonResult { Data = visitLogLsit, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //SpinningProduction
        public JsonResult SpinningProductionChartUserIdList()
        {
            List<SpinningProduction> spinningProductionUserIdList = SpinningProduction.RetrieveSpinningProductionChartUserIdList().ToList();
            return new JsonResult { Data = spinningProductionUserIdList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult RetrieveSpinningProductionChartDataById(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0"){
                uId = 0;
            }
            else
            {
                uId =Convert.ToInt32(id);
            }
            List<SpinningProduction> spinningProductionList = SpinningProduction.RetrieveChartData(uId, startDate, endDate).ToList();
            return new JsonResult { Data = spinningProductionList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //VehicleLog Distance
        public JsonResult RetrieveVehicleLogChartDataById(string vehicleNo, DateTime startDate, DateTime endDate)
        {
            List<VehicleLog> vehicleLogList = VehicleLog.RetrieveVehicleLogChartData(vehicleNo, startDate, endDate).ToList();
            return new JsonResult { Data = vehicleLogList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Chequepending count and amount
        public JsonResult RetrieveChequePending()
        {
            List<Cheque> chequeList = Cheque.RetrieveChequePending().ToList();
            return new JsonResult { Data = chequeList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Payment Pending Amount
        public JsonResult RetrievePaymentAmountPending()
        {
            int paymentPending = Payment.RetrievePendingPaymentAmount();
            return new JsonResult { Data = paymentPending, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //SpinningRejection
        public JsonResult RetrieveSpinningRejection(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0")
            {
                uId = 0;
            }
            else
            {
                uId = Convert.ToInt32(id);
            }
            List<SpinningProduction> spinningProductionList = SpinningProduction.RetrieveSpinningRejection(uId, startDate, endDate).ToList();
            return new JsonResult { Data = spinningProductionList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Sales Trend
        public JsonResult RetrieveSalesTrendData(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0")
            {
                uId = 0;
            }
            else
            {
                uId = Convert.ToInt32(id);
            }
            List<Sales> salesList = Sales.RetrieveAllSalesTrend(uId, startDate, endDate).ToList();
            return new JsonResult { Data = salesList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
      
        
        //Sales Return Trend
        public JsonResult RetrieveSalesReturnTrendData(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0")
            {
                uId = 0;
            }
            else
            {
                uId = Convert.ToInt32(id);
            }
            List<Sales> salesList = Sales.RetrieveAllSalesReturnTrend(uId, startDate, endDate).ToList();
            return new JsonResult { Data = salesList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
      
        //Sales 
        public JsonResult RetrieveSalesData(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0")
            {
                uId = 0;
            }
            else
            {
                uId = Convert.ToInt32(id);
            }
            List<Sales> salesList = Sales.RetrieveAllSalesData(uId, startDate, endDate).ToList();
            return new JsonResult { Data = salesList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        //Product Category Trend
        public JsonResult RetrieveProductCategoryTrendData(string productCategory, DateTime startDate, DateTime endDate)
        {
            List<Sales> salesList = Sales.RetrieveAllProductCategoryTrend(productCategory.Trim(), startDate, endDate).ToList();
            return new JsonResult { Data = salesList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        //Product Category Share
        public JsonResult RetrieveProductCategoryShareData(string productCategory, DateTime startDate, DateTime endDate)
        {
            List<Sales> salesList = Sales.RetrieveAllProductCategoryShare(productCategory.Trim(), startDate, endDate).ToList();
            return new JsonResult { Data = salesList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
     
        //Collection Trend 
        public JsonResult RetrieveCollectionTrend(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0")
            {
                uId = 0;
            }
            else
            {
                uId = Convert.ToInt32(id);
            }
            List<VisitLog> visitLogList = VisitLog.RetrieveCollectionTrend(uId, startDate, endDate).ToList();
            return new JsonResult { Data = visitLogList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //Collection Trend 
        public JsonResult RetrieveCollection(string id, DateTime startDate, DateTime endDate)
        {
            int uId;
            if (id == "0")
            {
                uId = 0;
            }
            else
            {
                uId = Convert.ToInt32(id);
            }
            List<VisitLog> visitLogList = VisitLog.RetrieveCollection(uId, startDate, endDate).ToList();
            return new JsonResult { Data = visitLogList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }

}
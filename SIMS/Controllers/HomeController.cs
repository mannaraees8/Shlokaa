using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIMS.Models;
using SIMS.ViewModels.ItemModel;
using SIMS.BL;
using SIMS.ViewModels.ItemHomeModel;
using SIMS.ViewModels.UsersModel; 

namespace SIMS.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductPage()
        {
            return View();
        }
        
        public JsonResult getfiledata(int Id)
        {
            string fileDataPath = GalleryBl.RetrieveOnlyFileData(Id);
            var jasonResult = Json(fileDataPath, JsonRequestBehavior.AllowGet);
            jasonResult.MaxJsonLength = int.MaxValue;
            return jasonResult;
        }
        
        public ActionResult Product()
        {
            return View(GetItemModelListWithoutRawMaterial());
        }

        public JsonResult getProducts(int Id, int start)
        {
            List<ItemModel> itemModelList = new List<ItemModel>();
            List<Item> lstItem = Item.RetrieveByCategoryId(Id, start);
            foreach (Item a in lstItem)
            {
                itemModelList.Add(new ItemModel(a));
            }
            
            var jasonResult = Json(itemModelList, JsonRequestBehavior.AllowGet);
            jasonResult.MaxJsonLength = int.MaxValue;
            return jasonResult;
        }
        
        public JsonResult GetItemSize(int Itemid)
        {
           
            List<ProductSize> ItemSizes = ProductSize.RetrieveById(Itemid).ToList();
            var jasonResult = Json(ItemSizes, JsonRequestBehavior.AllowGet);
            jasonResult.MaxJsonLength = int.MaxValue;
            return jasonResult;
        }
        private IEnumerable<ItemHomeModel> GetItemModelListWithoutRawMaterial()
        {
            List<ItemHomeModel> itemModelList = new List<ItemHomeModel>();
            List<Category> categoryLst = Category.RetrieveAllForHomeproduct();
            
            ViewBag.Categories = categoryLst;
            
            return itemModelList;
            
            
        }
        public ActionResult TriviaPage()
        {
            return View(GetTriviaModelList());
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Values()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View(GetGalleryList());
        }
        public ActionResult Contact()
        {
            return View();
        }
        
        private IEnumerable<GalleryModel> GetGalleryList()
        {
            List<GalleryModel> galleryModelList = new List<GalleryModel>();
            foreach (GalleryBl a in GalleryBl.RetrieveAll())
            {
                galleryModelList.Add(new GalleryModel(a));
            }
            
            return galleryModelList;
        }
        private IEnumerable<TriviaModel> GetTriviaModelList()
        {
            List<TriviaModel> triviaModelList = new List<TriviaModel>();
            foreach (Trivia a in Trivia.RetrieveAll())
            {
                triviaModelList.Add(new TriviaModel(a));
            }
            return triviaModelList;
        }
        
        [ChildActionOnly]
        public ActionResult GetUserName()
        {
            List<UsersModel> usersModel = new List<UsersModel>();
            int userId = Users.RetrieveStaffIdByEmail(HttpContext.User.Identity.Name);
            Users user = Users.RetrieveById(userId);
            usersModel.Add(new UsersModel(user));
            return PartialView("_LoggedInUser", usersModel);
        }
        
    }
}





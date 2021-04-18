using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpravRemontSite.DataObject;

using SpravRemontSite.Models; 

namespace SpravRemontSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IWebHostEnvironment _appEnvironment;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            createDir();
        }


        private void createDir()
        {

            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/driveto");// схема проезда
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/map");  //магазин на карте
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/item"); // картинка из категории товаров ITEM а так же картинки услуг
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/logo"); // логотип компании
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/chat"); // картинки из чата            
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/itemsbuy"); //картинки
            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/images/reklama"); //картинки

        }


        public IActionResult LoadMetro()
        {
            //if (!HttpContext.User.Identity.IsAuthenticated)
            //    return RedirectToAction("Login", "Account");


            //if (!CheckAdmin())
            //    return RedirectToAction("index", "Home");


           // Metro.loadMetroMoscow();


            return RedirectToAction("admIndex", "admpanel");
        }


        /////////////////////////
        // Главная
        /////////////////////////


        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)            
                return RedirectToAction("Login", "Account");
            
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Contact()
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        /////////////////////////
        // Комментарии
        /////////////////////////
        #region
        //[Authorize]
        public IActionResult Comment()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            List<CommentClient> cm = CommentClient.GetComments(User.Identity.Name);

            return View(cm);
        }
        //[Authorize]
        [HttpGet]
        public IActionResult AnswerComment(string ID_comment_client)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            var CC = CommentClient.GetComment(ID_comment_client);

            return View(CC);
        }
        //[Authorize]
        [HttpPost]
        public IActionResult AnswerComment(CommentClient cc)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            CommentShop.Create(cc.ID_comment_client, cc.CommentShop.Comment_shop);

            return Redirect("Comment");
        }

        //[Authorize]
        public IActionResult DeleteAnswerComment(string ID_comment_shop, string ID_comment_client)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            CommentShop.Delete(ID_comment_shop, ID_comment_client);

            var referrer = Request.Headers["Referer"];

            return Redirect(referrer);

        }
        //[Authorize]
        public IActionResult DeleteComment(string ID_comment_client)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            CommentClient.DeleteVisible(ID_comment_client);

            var referrer = Request.Headers["Referer"];

            return Redirect(referrer);

        }




        #endregion

        /////////////////////////
        // Услуги магазина
        /////////////////////////
        #region
       // [Authorize]
        public IActionResult Uslug()
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");


            var uslugs = USLUG.GetUSLUGs();


            return View(uslugs);
        }

        //[Authorize]
        public IActionResult UslugItems(string ID_USLUG, string NAME_USLUG)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");


            ViewData["NAME_USLUG"] = NAME_USLUG;
            ViewData["ID_USLUG"] = ID_USLUG;

            //User.Identity.Name //передача логина, для пользователя
            // var uslugItems = USLUGS_SHOP.GetUslugItems(ID_USLUG, User.Identity.Name);
            var uslugItem = USLUGS_SHOP.GetUslugItem(ID_USLUG, User.Identity.Name);

            return View(uslugItem);
        }

        //удаление товара
       // [Authorize]
        public IActionResult DeleteUslug(string ID_USLUGS_SHOP)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");
            // удаление итема с ID_ITEMS_BUY
            //и далее редирект на адрес, откуда пришли
            USLUGS_SHOP.Delete(ID_USLUGS_SHOP);

            var referrer = Request.Headers["Referer"];

            return Redirect(referrer);
        }

        // изменение товара
        //[Authorize]
        [HttpGet]
        public IActionResult EditUslug(string ID_USLUGS_SHOP)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");
            // запрос итема с ID_ITEMS_BUY

            var US = USLUGS_SHOP.GetUslug(ID_USLUGS_SHOP);


            return View(US);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult EditUslug(USLUGS_SHOP US)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");

            if (ModelState.IsValid)
            {
                US.Update();
            }


            return Redirect("UslugItems?ID_USLUG=" + US.Uslug.ID_USLUG);
        }

        //[Authorize]
        [HttpGet]
        public IActionResult UslugAdd(string ID_USLUG, string NAME_USLUG)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");


            ViewData["NAME_USLUG"] = NAME_USLUG;


            ViewBag.NAME_USLUG = NAME_USLUG;
            ViewBag.ID_USLUG = ID_USLUG;
            ViewBag.ID_SHOP = Models.User.GetIDUser(User.Identity.Name);

            return View();
        }
        //[Authorize]
        [HttpPost]
        public IActionResult UslugAdd(USLUGS_SHOP uSLUGS_SHOP)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesUslug())// если куки не равны услугам то редиеркт на категории
                return RedirectToAction("Kategor");

            if (ModelState.IsValid)
            {
                uSLUGS_SHOP.CreateUslugShop();
            }


            return Redirect("UslugItems?ID_USLUG=" + uSLUGS_SHOP.Uslug.ID_USLUG);//+ "&NAME_USLUG="+ uSLUGS_SHOP.Uslug.NAME_USLUG);
        }
        #endregion


        /////////////////////////
        // Категории товаров
        /////////////////////////
        #region
        //[Authorize]
        public IActionResult Kategor()
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");


            // проверка и насыщение данными категорий
            ITEMS_SHOP.Check(User.Identity.Name);

            var kATEGORs = KATEGOR.GetKATEGORs();




            return View(kATEGORs);
        }

        //[Authorize]
        public IActionResult ItemKategor(KATEGOR kat)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");

            ViewBag.NameKategor = kat.NAME_KATEGOR;

            var iTEMS_SHOPs = ITEMS_SHOP.GetITEMS_SHOPs(User.Identity.Name, kat.ID_KATEGOR);

            return View(iTEMS_SHOPs);
        }

     //   [Authorize]
        public IActionResult ItemsBuy(string ID_ITEMS_SHOP)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");

            ViewBag._ID_ITEMS_SHOP = ID_ITEMS_SHOP;

            var ItemsBuy = ITEMS_BUY.GetITEMS_BUYs(ID_ITEMS_SHOP);

            return View(ItemsBuy);
        }


        //удаление товара
        //[Authorize]
        public IActionResult DeleteItem(string ID_ITEMS_BUY)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");
            // удаление итема с ID_ITEMS_BUY
            //и далее редирект на адрес, откуда пришли
            ITEMS_BUY.Delete(ID_ITEMS_BUY);

            var referrer = Request.Headers["Referer"];

            return Redirect(referrer);
        }

        // изменение товара
       // [Authorize]
        [HttpGet]
        public IActionResult EditItem(string ID_ITEMS_BUY)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");
            // запрос итема с ID_ITEMS_BUY

            var IB = ITEMS_BUY.GetItem(ID_ITEMS_BUY);


            return View(IB);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> EditItem(ITEMS_BUY IB)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");
            // запрос итема с ID_ITEMS_BUY
            if (ModelState.IsValid)
            {
                // проверка на загрузку изоображений
                if (IB.IMG_URL_F != null)
                {
                    string nameItem = Guid.NewGuid().ToString();
                    // путь к папке Files
                    string path = "/images/itemsbuy/" + nameItem + ".jpeg";
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await IB.IMG_URL_F.CopyToAsync(fileStream);
                    }
                    IB.IMG_URL = CONSTANT.UrlHost + path;
                }
                IB.Update();
            }


            return Redirect("ItemsBuy?ID_ITEMS_SHOP=" + IB.ID_ITEMS_SHOP);
        }

        //[Authorize]
        [HttpGet]
        public IActionResult ItemAdd(string ID_ITEMS_SHOP)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");
            return View();
        }
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> ItemAdd(ITEMS_BUY iTEMS_BUY)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!checkCookiesKategor())// если куки не равны категории то редиеркт на услуги
                return RedirectToAction("Uslug");
            if (ModelState.IsValid)
            {
                // проверка на загрузку изоображений
                if (iTEMS_BUY.IMG_URL_F != null)
                {
                    string nameItem = Guid.NewGuid().ToString();
                    // путь к папке Files
                    string path = "/images/itemsbuy/" + nameItem + ".jpeg";
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await iTEMS_BUY.IMG_URL_F.CopyToAsync(fileStream);
                    }
                    iTEMS_BUY.IMG_URL = CONSTANT.UrlHost + path;
                }
                iTEMS_BUY.CreateItemsBuy();
            }


            return Redirect("ItemsBuy?ID_ITEMS_SHOP=" + iTEMS_BUY.ID_ITEMS_SHOP);
        }
        #endregion

        /////////////////////////
        // Учетная запись магазина
        /////////////////////////
        #region
        //[Authorize]
        [HttpGet]
        public IActionResult AccountShop()
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            List<City> cities = City.GetCities();
            Shop shop = new Shop(User.Identity.Name);
            List<Metro> metros = Metro.GetCities(shop.City.ID_City);
            List<TimeWayMetroClass> timeWayMetros = TimeWayMetroClass.GetTimewayMetro();
            List<IMG_Shop> imgsShop = IMG_Shop.GetShopIMGs(shop.ID_shop);

            // предустановка комбо боксов, в первом списке будут текущие значения
            cities.Insert(0, shop.City);
            timeWayMetros.Insert(0, new TimeWayMetroClass() { minutes = shop.TimeWayMetro, Comment = shop.TimeWayMetro.ToString() });
            if (shop.Metro != null) metros.Insert(0, shop.Metro);

            AccountShopVM accountShop = new AccountShopVM()
            {
                Shop = shop,
                Citys = cities,
                Metros = metros,
                timeWayMetros = timeWayMetros,
                iMG_Shops = imgsShop
            };


            return View(accountShop);
        }

       // [Authorize]
        [HttpPost]
        public async Task<IActionResult> AccountShop(AccountShopVM accountShopVM)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (ModelState.IsValid)
            {
                // проверка на загрузку изоображений
                if (accountShopVM.Shop.IMG_LOGO_f != null)
                {
                    string nameLogo = Guid.NewGuid().ToString();
                    // путь к папке Files
                    string path = "/images/logo/" + nameLogo + ".jpeg";
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await accountShopVM.Shop.IMG_LOGO_f.CopyToAsync(fileStream);
                    }
                    accountShopVM.Shop.IMG_LOGO = CONSTANT.UrlHost + path;
                }

                if (accountShopVM.Shop.IMG_DRIVE_TO_f != null)
                {
                    string nameDriveTo = Guid.NewGuid().ToString();
                    // путь к папке Files
                    string path = "/images/driveto/" + nameDriveTo + ".jpeg";
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await accountShopVM.Shop.IMG_DRIVE_TO_f.CopyToAsync(fileStream);
                    }
                    accountShopVM.Shop.IMG_DRIVE_TO = CONSTANT.UrlHost + path;
                }

                if (accountShopVM.Shop.IMG_MAP_f != null)
                {
                    string nameMap = Guid.NewGuid().ToString();
                    // путь к папке Files
                    string path = "/images/map/" + nameMap + ".jpeg";
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await accountShopVM.Shop.IMG_MAP_f.CopyToAsync(fileStream);
                    }
                    accountShopVM.Shop.IMG_MAP = CONSTANT.UrlHost + path;
                }





                // апдейтнуть магазин и время работы
                accountShopVM.Shop.UpdateShop();
                accountShopVM.Shop.TimeWork.UpdateTimeWork();
            }

            return RedirectToAction("Index", "Home");

        }

        // Проверка уч записи        
        private bool checkCookiesKategor()
        {
            // проверка что куки относятся к категории товаров
            if (HttpContext.Request.Cookies.ContainsKey("typeshop") &&
                ((HttpContext.Request.Cookies["typeshop"] == "340eb5f2-0ffd-411b-9cf2-318a60b22604"
                      ||
                  HttpContext.Request.Cookies["typeshop"] == "350eb5f2-0ffd-411b-9cf2-318a60b22604"))
                )
                return true;

            return false;
        }

        private bool checkCookiesUslug()
        {
            // проверка что куки относятся к категории товаров
            if (HttpContext.Request.Cookies.ContainsKey("typeshop") &&
                HttpContext.Request.Cookies["typeshop"] == "360eb5f2-0ffd-411b-9cf2-318a60b22604"
                )
                return true;

            return false;
        }
        #endregion


        /////////////////////////
        // Чат
        /////////////////////////
       // [Authorize]
        public IActionResult chatView(string ID_Client)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            string id_shop = "";

            if (HttpContext.Request.Cookies.ContainsKey("iduser"))
                id_shop = HttpContext.Request.Cookies["iduser"];

            ChatViewModel chatViewModel = new ChatViewModel()
            {
                ID_Shop = id_shop
            };

            if (ID_Client != null)
            {
                chatViewModel = new ChatViewModel(id_shop, ID_Client);
            }

            return View(chatViewModel);
        }


        /////////////////////////
        // Загрузка фото магазина
        /////////////////////////

        [HttpGet]
        public IActionResult ImgsShop()
        {


            Shop shop = new Shop(User.Identity.Name);

            ImgsShopVM VM = new ImgsShopVM(shop.ID_shop);



            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> ImgsShop(ImgsShopVM VM)
        {


            if (ModelState.IsValid)
            {
                string URL_foto = await LoadIMG("vitrina", VM.IMG_formFile);

                if (!String.IsNullOrEmpty(URL_foto))
                {
                    IMG_Shop iMG_Shop = new IMG_Shop
                    {
                        ID_SHOP = VM.ID_SHOP,
                        Type = "VITRINA",
                        Url = URL_foto
                    };

                    iMG_Shop.Create();
                }


            }

            return RedirectToAction("ImgsShop");
        }


        [HttpGet]
        public IActionResult ImgsShopDelete(long ID_IMG)
        {
            Shop shop = new Shop(User.Identity.Name);

            IMG_Shop.Delete(ID_IMG, shop.ID_shop);


            return RedirectToAction("ImgsShop");
        }




        /////////////////
        // ЗАГРУЗКА ИЗООБРАЖЕНИЙ

        private async Task<string> LoadIMG(string path, IFormFile IMG_FormFile)
        {
            //shop_interyer
            if (IMG_FormFile != null)
            {
                string name = Guid.NewGuid().ToString();
                // путь к папке Files
                string _path = $"/images/{path}/" + name + ".jpeg";

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + _path, FileMode.Create))
                {
                    await IMG_FormFile.CopyToAsync(fileStream);
                }
                return CONSTANT.UrlHost + _path;
            }
            else
            {
                return "";
            }
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

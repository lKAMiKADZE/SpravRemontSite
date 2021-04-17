using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpravRemontSite.DataObject;
using SpravRemontSite.Models;

namespace SpravRemontSite.Controllers
{
    //[Authorize]
    public class admpanelController : Controller
    {

        IHostingEnvironment _appEnvironment;

        public admpanelController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

               
        public IActionResult admIndex()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


            return View();
        }


        ///////////////////////
        // Категории
        ///////////////////////
        #region
        public IActionResult Kategor()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            var kat = KATEGOR.GetKATEGORs(); 

            return View(kat);
        }

        [HttpGet]
        public IActionResult EditKategor(string ID_KATEGOR, string NAME_KATEGOR)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            var kategor = new KATEGOR()
            {
                ID_KATEGOR=ID_KATEGOR,
                NAME_KATEGOR=NAME_KATEGOR
            };

            return View(kategor);
        }
        [HttpPost]
        public IActionResult EditKategor(KATEGOR kat)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            kat.Update();

            return RedirectToAction("Kategor", "admpanel");
        }

        public IActionResult DeleteKategor(string ID_KATEGOR)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            KATEGOR.Delete(ID_KATEGOR);

            return RedirectToAction("Kategor", "admpanel");
        }

        [HttpPost]
        public IActionResult AddKategor(string NAME_KATEGOR)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            KATEGOR.Create(NAME_KATEGOR);

            return RedirectToAction("Kategor", "admpanel");
        }

        public IActionResult DetailKategor(string ID_KATEGOR, string NAME_KATEGOR)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            ViewBag.NAME_KATEGOR = NAME_KATEGOR;
            ViewBag.ID_KATEGOR = ID_KATEGOR;

            var it = ITEM_KATEGOR.GetITEM_KATEGORs_ADMIN(ID_KATEGOR);

            return View(it);
        }

        public IActionResult DeleteItem(string ID_ITEM_KATEGOR, string ID_ITEM)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            ITEM_KATEGOR.Delete(ID_ITEM_KATEGOR, ID_ITEM);

            var referrer = Request.Headers["Referer"];

            return Redirect(referrer);
        }
        [HttpGet]
        public IActionResult EditItem(string ID_ITEM_KATEGOR, string ID_ITEM)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


            var it = ITEM_KATEGOR.GetITEM_ITEM_KATEGOR(ID_ITEM_KATEGOR);

            return View(it);
        }
        [HttpPost]
        public async Task<IActionResult> EditItem(ITEM_KATEGOR it)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            
                // проверка на загрузку изоображений
                if (it.ITEM.IMG_URL_F != null)
                {
                    string nameItem = Guid.NewGuid().ToString();
                    // путь к папке Files
                    string path = "/images/item/" + nameItem + ".jpeg";
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await it.ITEM.IMG_URL_F.CopyToAsync(fileStream);
                    }
                    it.ITEM.IMG_URL = CONSTANT.UrlHost + path;
                }
                ITEM_KATEGOR.Update(it.ITEM);
            


            return Redirect("DetailKategor?ID_KATEGOR="+it.KATEGOR.ID_KATEGOR);
        }
        [HttpGet]
        public IActionResult CreateItem(string ID_KATEGOR)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


            var it = new ITEM_KATEGOR() {
                ITEM = new ITEM(),
                KATEGOR = new KATEGOR() { ID_KATEGOR = ID_KATEGOR }
            };


            return View(it);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(ITEM_KATEGOR it)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


            // проверка на загрузку изоображений
            if (it.ITEM.IMG_URL_F != null)
            {
                string nameItem = Guid.NewGuid().ToString();
                // путь к папке Files
                string path = "/images/item/" + nameItem + ".jpeg";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await it.ITEM.IMG_URL_F.CopyToAsync(fileStream);
                }
                it.ITEM.IMG_URL = CONSTANT.UrlHost + path;
            }

            it.Create();



            return Redirect("DetailKategor?ID_KATEGOR=" + it.KATEGOR.ID_KATEGOR);
        }



        #endregion




        ///////////////////////
        // Услуги
        ///////////////////////
        #region
        public IActionResult Uslug()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            var uslugs = USLUG.GetUSLUGs();

            return View(uslugs);
        }

        [HttpGet]
        public IActionResult EditUslug(string ID_USLUG)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            var uslug = USLUG.GetItem(ID_USLUG);

            return View(uslug);
        }
        [HttpPost]
        public async Task<IActionResult> EditUslug(USLUG us)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            // проверка на загрузку изоображений
            if (us.IMG_URL_F != null)
            {
                string nameItem = Guid.NewGuid().ToString();
                // путь к папке Files
                string path = "/images/item/" + nameItem + ".jpeg";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await us.IMG_URL_F.CopyToAsync(fileStream);
                }
                us.IMG_URL = CONSTANT.UrlHost + path;
            }            

            us.Update();

            return Redirect("Uslug");
        }
        [HttpGet]
        public IActionResult CreateUslug()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");
            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUslug(USLUG us)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            // проверка на загрузку изоображений
            if (us.IMG_URL_F != null)
            {
                string nameItem = Guid.NewGuid().ToString();
                // путь к папке Files
                string path = "/images/item/" + nameItem + ".jpeg";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await us.IMG_URL_F.CopyToAsync(fileStream);
                }
                us.IMG_URL = CONSTANT.UrlHost + path;
            }

            us.Create();

            return Redirect("Uslug");
        }
        public IActionResult DeleteUslug(string ID_USLUG)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            USLUG.Delete(ID_USLUG);

            var referrer = Request.Headers["Referer"];

            return Redirect(referrer);

        }


        #endregion



        ///////////////////////
        // магазины
        ///////////////////////
        #region
        public IActionResult Shops()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            var sh = Shop.GetShops();

            return View(sh);
        }

        [HttpGet]
        public IActionResult AccountShop(string ID_Shop)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            List<City> cities = City.GetCities();
            Shop shop = Shop.GetItemAdmin(ID_Shop);
            List<Metro> metros = Metro.GetCities(shop.City.ID_City);
            List<TimeWayMetroClass> timeWayMetros = TimeWayMetroClass.GetTimewayMetro();

            List<IMG_Shop> imgsShop = IMG_Shop.GetShopIMGs(ID_Shop);

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
        [HttpPost]
        public async Task<IActionResult> AccountShop(AccountShopVM accountShopVM)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


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

            return RedirectToAction("Shops");
        }

        

        public IActionResult ActiveShop(string ID_Shop)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            Shop.Active(ID_Shop);

            var referrer = Request.Headers["Referer"];
            return Redirect(referrer);

        }
        public IActionResult DeactivatedShop(string ID_Shop)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            Shop.Deactivated(ID_Shop);

            var referrer = Request.Headers["Referer"];
            return Redirect(referrer);

        }

        #endregion

        ///////////////////////
        // реклама
        ///////////////////////
        #region
        public IActionResult Reklama()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            var reks=DataObject.Reklama.GetReklams();

            return View(reks);
        }

        [HttpGet]
        public IActionResult ReklamaShop(string ID_Reklama)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


            string url_img = DataObject.Reklama.GetReklama(ID_Reklama);

            var rek = new Reklama() { ID_Reklama = ID_Reklama, IMG_URL= url_img };

            return View(rek); 
        }
        [HttpPost]
        public async Task<IActionResult> ReklamaShop(Reklama rek)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            // проверка на загрузку изоображений
            if (rek.IMG_URL_F != null)
            {
                string nameItem = Guid.NewGuid().ToString();
                // путь к папке Files
                string path = "/images/reklama/" + nameItem + ".jpeg";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await rek.IMG_URL_F.CopyToAsync(fileStream);
                }
                rek.IMG_URL = CONSTANT.UrlHost + path;
                rek.Update();
            }

            return RedirectToAction("Reklama");
        }
        [HttpGet]
        public IActionResult ReklamaSprav(string ID_Reklama)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            string url_img = DataObject.Reklama.GetReklama(ID_Reklama);

            var rek = new Reklama() { ID_Reklama = ID_Reklama, IMG_URL = url_img };

            return View(rek); 
        }
        [HttpPost]
        public async Task<IActionResult> ReklamaSprav(Reklama rek)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            // проверка на загрузку изоображений
            if (rek.IMG_URL_F != null)
            {
                string nameItem = Guid.NewGuid().ToString();
                // путь к папке Files
                string path = "/images/reklama/" + nameItem + ".jpeg";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await rek.IMG_URL_F.CopyToAsync(fileStream);
                }
                rek.IMG_URL = CONSTANT.UrlHost + path;

                rek.Update();
            }

            return RedirectToAction("Reklama");
        }

        #endregion


        ///////////////////////
        // загрузка метро
        ///////////////////////
        #region

        public IActionResult LoadMetro(string token)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");

            if (token != "ghjgamewar") 
                return RedirectToAction("index", "Home");

            // Metro.loadMetroMoscow();




            return RedirectToAction("admIndex", "admpanel"); ;
        }

        #endregion

        /////////////////////////
        // Загрузка фото магазина
        /////////////////////////

        [HttpGet]
        public IActionResult ImgsShop(string ID_SHOP)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



            //Shop shop = new Shop(User.Identity.Name);

            ImgsShopVM VM = new ImgsShopVM(ID_SHOP);



            return View(VM);
        }
        [HttpPost]
        public async Task<IActionResult> ImgsShop(ImgsShopVM VM)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");



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

            var referrer = Request.Headers["Referer"];
            return Redirect(referrer);
            //return RedirectToAction("ImgsShop", "admpanel");
        }


        [HttpGet]
        public IActionResult ImgsShopDelete(long ID_IMG, string ID_SHOP)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            //Shop shop = new Shop(User.Identity.Name);

            IMG_Shop.Delete(ID_IMG, ID_SHOP);

            var referrer = Request.Headers["Referer"];
            return Redirect(referrer);
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




        public IActionResult Setings()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (!CheckAdmin())
                return RedirectToAction("index", "Home");


            return View();
        }

        private bool CheckAdmin()
        {
            if (User.Identity.Name == "1adm" || User.Identity.Name== "Igor.zenin500@yandex.ru")
                return true;

            return false;
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using SpravRemontSite.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SpravRemontSite.DataObject;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace SpravRemontSite.Controllers
{
    public class AccountController : Controller
    {
        IHostingEnvironment _appEnvironment;

        public AccountController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            //createDir();
        }



        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //HttpContext.User.Identity.Name //логин
            //HttpContext.User.Identity.IsAuthenticated // авторизован ли

            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            model.Password = GethashPassword(model.Password);
            
            if (ModelState.IsValid)
            {
                User user = new User();
                // получаем тип учетной записи и определяем, есть ли такой пользователь
                //string TypeShop = user.LoginUserGetTypeShop(model.Email, model.Password);
                user.LoginUserGetTypeShop(model.Email, model.Password);

                if (user.type_shop!="")
                {
                    await Authenticate(model.Email, user.type_shop, user.ID_user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }


            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            RegisterModel regModel = new RegisterModel();

            return View(regModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                User user = new User();
                model.Password = GethashPassword(model.Password);



                if (!user.GetUser(model.Email))
                {
                    if (user.RegisterUser(model))
                    {
                        await Authenticate(model.Email, model.typeShop, user.ID_user); // аутентификация
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                                
            }
            

            return View(model);
        }


        private string GethashPassword(string password)
        {
            string salt = "";// тут соль 

            var valueBytes = KeyDerivation.Pbkdf2(
                             password: password,
                             salt: Encoding.UTF8.GetBytes(salt),
                             prf: KeyDerivationPrf.HMACSHA512,
                             iterationCount: 1, // итераций 10000
                             numBytesRequested: 256 / 8);
            
            return Convert.ToBase64String(valueBytes);



        }

        private async Task Authenticate(string userName, string TypeShop, string id_user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));


            if (HttpContext.Request.Cookies.ContainsKey("typeshop"))
            {
                HttpContext.Response.Cookies.Delete("typeshop");
                HttpContext.Response.Cookies.Append("typeshop", TypeShop);
            }
            else
                HttpContext.Response.Cookies.Append("typeshop", TypeShop);

            if (HttpContext.Request.Cookies.ContainsKey("iduser"))
            {
                HttpContext.Response.Cookies.Delete("iduser");
                HttpContext.Response.Cookies.Append("iduser", id_user);
            }
            else
                HttpContext.Response.Cookies.Append("iduser", id_user);

        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (HttpContext.Request.Cookies.ContainsKey("typeshop"))
                HttpContext.Response.Cookies.Delete("typeshop");

            return RedirectToAction("Login", "Account");
        }


        ////////////////////////
        //загрузка изображений        

        [HttpPost]
        public async Task<string> LoadImgView()
        {
            string imgURL_return = "Ошибка загрузки";
            //if (!ControllerContext.HttpContext.Request.Headers.ContainsKey("tokenMobile"))
            //    return imgURL_return;

            //if (ControllerContext.HttpContext.Request.Headers["tokenMobile"] != "51558244f76c53b6aeda52c8a337f2c37")
            //    return imgURL_return;

            var files = ControllerContext.HttpContext.Request.Form.Files;

            IFormFile lOADimg = files[0];


            // запрос итема с ID_ITEMS_BUY

            // проверка на загрузку изоображений
            if (lOADimg != null)
            {
                string nameItem = Guid.NewGuid().ToString();
                // путь к папке Files
                string path = "/images/chat/" + nameItem + ".jpeg";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await lOADimg.CopyToAsync(fileStream);
                }
                imgURL_return = CONSTANT.UrlHost + path;

            }



            return imgURL_return;
        }

    }
}
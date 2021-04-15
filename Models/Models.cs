using SpravRemontSite.DataObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.Models
{
    public enum TypeShop { SHOP,BREAK,USLUG}

    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан тип организации")]
        public string typeShop { get; set; }
        

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }



        public List<TYPE_SHOP> TYPE_SHOPs { get; set; }

        public RegisterModel()
        {
            TYPE_SHOPs = TYPE_SHOP.GetListTP();
        }


    }

}

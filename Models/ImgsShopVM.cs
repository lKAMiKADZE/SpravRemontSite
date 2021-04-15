using Microsoft.AspNetCore.Http;
using SpravRemontSite.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.Models
{
    public class ImgsShopVM
    {
        public List<IMG_Shop> iMG_Shops { get; set; }
        public string ID_SHOP { get; set; }
        //public IMG_Shop iMG_Shop { get; set; }

        public IFormFile IMG_formFile { get; set; }


        public ImgsShopVM()
        {

        }
        public ImgsShopVM( string ID_SHOP)
        {
            iMG_Shops = IMG_Shop.GetShopIMGs(ID_SHOP);
            this.ID_SHOP = ID_SHOP;
        }


    }
}

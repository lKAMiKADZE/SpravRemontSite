using SpravRemontSite.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.Models
{
    public class AccountShopVM
    {
        public Shop Shop { get; set; }
        public List<Metro> Metros { get; set; }
        public List<City> Citys { get; set; }
        public  List<TimeWayMetroClass> timeWayMetros { get; set; }
        public List<IMG_Shop> iMG_Shops { get; set; }

        public AccountShopVM()
        {            
        }
    }
}

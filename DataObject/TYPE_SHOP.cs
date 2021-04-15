using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class TYPE_SHOP
    {
        public string ID_TYPE_SHOP { get; set; }
        public string NAME_TYPE { get; set; }
       // private List<TYPE_SHOP> TYPE_SHOP_LIST { get; set; }

        public static List<TYPE_SHOP> GetListTP()
        {
            List<TYPE_SHOP> TYPE_SHOP_LIST = new List<TYPE_SHOP>()
            {
                new TYPE_SHOP(){ID_TYPE_SHOP="340eb5f2-0ffd-411b-9cf2-318a60b22604", NAME_TYPE="Магазин"},
                new TYPE_SHOP(){ID_TYPE_SHOP="350eb5f2-0ffd-411b-9cf2-318a60b22604", NAME_TYPE="Разборки"},
                new TYPE_SHOP(){ID_TYPE_SHOP="360eb5f2-0ffd-411b-9cf2-318a60b22604", NAME_TYPE="Услуги"}
            };
            return TYPE_SHOP_LIST;
        }

        public static TYPE_SHOP GetName(string ID_TYPE_SHOP)
        {
            List<TYPE_SHOP> TYPE_SHOP_LIST = GetListTP();

            for (int i = 0; i < TYPE_SHOP_LIST.Count; i++)
            {
                if (ID_TYPE_SHOP == TYPE_SHOP_LIST[i].ID_TYPE_SHOP)
                    return TYPE_SHOP_LIST[i];
            }

            return null;
        }


    }
}
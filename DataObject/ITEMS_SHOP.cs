using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class ITEMS_SHOP
    {
        public string ID_ITEMS_SHOP { get; set; }
        public string ID_SHOP { get; set; }
        public ITEM_KATEGOR ITEM_KATEGOR { get; set; }


        // При входе в пункт категории или подкатегории, идет проверка, все ли категории имеются в БД у этого класса
        public static void Check(string Email_User)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                // чекаем существование записи
                string sqlExpression = @"
                SELECT ik.ID_item_kategor, 
                        (SELECT u.ID_USER FROM USERs u
                            WHERE u.email='"+ Email_User + @"') AS id_shop

                FROM SPAVREMONT.ITEM_KATEGOR ik
                LEFT JOIN SPAVREMONT.ITEMS_SHOP its 
                    ON its.ID_item_kategor= ik.ID_item_kategor
                     AND its.id_shop=(
                        SELECT s.ID_SHOP FROM SPAVREMONT.Shop s
                            JOIN USERs u ON u.id_user=s.id_shop
                            WHERE u.email='"+ Email_User + @"'
                     )
                WHERE its.id_items_shop is null
                
   
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_item_kategor_index = reader.GetOrdinal("ID_item_kategor");
                    int id_shop_index = reader.GetOrdinal("id_shop");


                    while (reader.Read()) // построчно считываем данные
                    {
                        Create(
                            reader.GetString(id_shop_index),
                            reader.GetString(ID_item_kategor_index)
                            );
                    }
                }
                reader.Close();
            }
                       
        }

        public static bool Create(string ID_shop, string ID_ITEM_KATEGOR)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string _ID_ITEMS_SHOP = Guid.NewGuid().ToString();

                string sqlExpression = @"
                  INSERT INTO SPAVREMONT.ITEMS_SHOP (
                    ID_ITEMS_SHOP,
                    ID_SHOP,
                    ID_ITEM_KATEGOR,
                    MIN_PRICE,
                    MAX_PRICE
                    )
                    VALUES (
                    '"+ _ID_ITEMS_SHOP + @"',
                    '"+ID_shop+@"',
                    '"+ID_ITEM_KATEGOR+@"',
                    0,
                    0
                    )

                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numbInsUser = command.ExecuteNonQuery();

                // если успешно вставились данные
                if (numbInsUser > 0)
                {
                    return true;
                }
            }


            return false;
        }
        
        public static List<ITEMS_SHOP> GetITEMS_SHOPs(string email_user,string ID_KATEGOR)
        {
            List<ITEMS_SHOP> itemKATEGORs = new List<ITEMS_SHOP>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                its.id_items_shop,
                ik.ID_ITEM_KATEGOR, 
                it.NAME_ITEM,
                it.IMG_URL
                
                 FROM SPAVREMONT.ITEMS_SHOP its
                 JOIN SPAVREMONT.ITEM_KATEGOR ik ON its.id_item_kategor=ik.id_item_kategor
                 JOIN SPAVREMONT.ITEM it ON it.ID_item=ik.id_item
                 JOIN SPAVREMONT.Shop s ON its.ID_shop=s.id_shop
                 JOIN USERs u ON u.id_user=s.id_shop
                
                 WHERE ID_KATEGOR='" + ID_KATEGOR + @"'
                    AND u.email='"+ email_user + @"'
                 ORDER BY  NAME_ITEM ASC
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int id_items_shop_Index = reader.GetOrdinal("id_items_shop");
                    int ID_ITEM_KATEGOR_Index = reader.GetOrdinal("ID_ITEM_KATEGOR");
                    int NAME_ITEM_Index = reader.GetOrdinal("NAME_ITEM");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");


                    while (reader.Read()) // построчно считываем данные
                    {
                        itemKATEGORs.Add(
                            new ITEMS_SHOP()
                            {
                                ITEM_KATEGOR= new ITEM_KATEGOR() {
                                    ID_ITEM_KATEGOR = reader.GetString(ID_ITEM_KATEGOR_Index),
                                    ITEM = new ITEM() {
                                        NAME_ITEM = reader.GetString(NAME_ITEM_Index),
                                        IMG_URL = reader.IsDBNull(IMG_URL_Index) ? "" : reader.GetString(IMG_URL_Index)
                                    }
                                },
                                ID_ITEMS_SHOP= reader.GetString(id_items_shop_Index)
                            });
                    }
                }
                reader.Close();
            }


            return itemKATEGORs;
        }


    }
}
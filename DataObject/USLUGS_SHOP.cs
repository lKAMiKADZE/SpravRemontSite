using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class USLUGS_SHOP
    {
        public string ID_USLUGS_SHOP { get; set; }
        public string ID_SHOP { get; set; }
        public USLUG Uslug { get; set; }
        [Required(ErrorMessage = "Укажите цену")]
        public int PRICE { get; set; }
        public string NOTE_USLUGS_SHOP { get; set; }


        private static List<USLUGS_SHOP> GetUslugItems(string id_uslug, string email_shop)
        {
            List<USLUGS_SHOP> uslugItems = new List<USLUGS_SHOP>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT  
                   ussh.ID_USLUGS_SHOP
                  ,ussh.ID_USLUG
                  ,ussh.PRICE
              FROM SPAVREMONT.USLUGS_SHOP ussh
              JOIN SPAVREMONT.Shop sh ON sh.id_shop=ussh.ID_SHOP
              JOIN USERS users ON users.id_user=sh.id_shop
              WHERE ussh.ID_USLUG='"+ id_uslug + @"'
                AND users.Email='"+ email_shop + @"'

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_USLUGS_SHOP_index = reader.GetOrdinal("ID_USLUGS_SHOP");
                    int ID_USLUG_Index = reader.GetOrdinal("ID_USLUG");
                    int PRICE_Index = reader.GetOrdinal("PRICE");


                    while (reader.Read()) // построчно считываем данные
                    {
                        uslugItems.Add(
                            new USLUGS_SHOP()
                            {
                                //ID_USLUG = reader.GetString(ID_ID_USLUG_index),
                                //NAME_USLUG = reader.GetString(NAME_USLUG_Index)
                            });
                    }
                }
                reader.Close();
            }


            return uslugItems;
        }


        public static USLUGS_SHOP GetUslugItem(string id_uslug, string email_shop)
        {
            USLUGS_SHOP uslugItem = new USLUGS_SHOP();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT  
                   ussh.ID_USLUGS_SHOP
                  ,ussh.ID_USLUG
                  ,ussh.PRICE
                  ,ussh.NOTE_USLUGS_SHOP
                  ,usl.NAME_USLUG
                  ,usl.IMG_URL
              FROM SPAVREMONT.USLUGS_SHOP ussh
              JOIN SPAVREMONT.USLUG usl ON usl.ID_USLUG=ussh.ID_USLUG
              JOIN SPAVREMONT.Shop sh ON sh.id_shop=ussh.ID_SHOP
              JOIN USERS users ON users.id_user=sh.id_shop
              WHERE ussh.ID_USLUG='" + id_uslug + @"'
                AND users.Email='" + email_shop + @"'
                ORDER BY ID_USLUGS_SHOP ASC

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_USLUGS_SHOP_index = reader.GetOrdinal("ID_USLUGS_SHOP");
                    int ID_USLUG_Index = reader.GetOrdinal("ID_USLUG");
                    int PRICE_Index = reader.GetOrdinal("PRICE");
                    int NOTE_USLUGS_SHOP_Index = reader.GetOrdinal("NOTE_USLUGS_SHOP");
                    int NAME_USLUG_Index = reader.GetOrdinal("NAME_USLUG");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");



                    while (reader.Read()) // построчно считываем данные
                    {
                        uslugItem =
                            new USLUGS_SHOP()
                            {
                                ID_USLUGS_SHOP= reader.GetString(ID_USLUGS_SHOP_index),
                                PRICE= reader.GetInt32(PRICE_Index),
                                NOTE_USLUGS_SHOP= reader.IsDBNull(NOTE_USLUGS_SHOP_Index) ? "": reader.GetString(NOTE_USLUGS_SHOP_Index),
                                Uslug = new USLUG()
                                {
                                    ID_USLUG= reader.GetString(ID_USLUG_Index),
                                    IMG_URL= reader.GetString(IMG_URL_Index),
                                    NAME_USLUG= reader.GetString(NAME_USLUG_Index)
                                }
                            };
                        break;
                    }
                }
                reader.Close();
            }


            return uslugItem;
        }

        public bool CreateUslugShop()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  INSERT INTO [SPAVREMONT].[USLUGS_SHOP]
                   (
                      [ID_USLUGS_SHOP]
                     ,[ID_SHOP]
                     ,[ID_USLUG]
                     ,[PRICE]
                     ,[NOTE_USLUGS_SHOP]
                   )
                   VALUES(
                      '" + Guid.NewGuid().ToString() + @"'--[ID_USLUGS_SHOP]
                     ,'" + ID_SHOP + @"'  --[ID_SHOP]
                     ,'" + Uslug.ID_USLUG + @"'  --[ID_USLUG]
                     ," + PRICE.ToString() + @"--[PRICE]
                     ,'"+ NOTE_USLUGS_SHOP + @"' --[NOTE_USLUGS_SHOP]
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


        public static bool Delete(string ID_USLUGS_SHOP)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  DELETE [SPAVREMONT].[USLUGS_SHOP]
                    WHERE [ID_USLUGS_SHOP]='" + ID_USLUGS_SHOP + @"'
                   
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numb = command.ExecuteNonQuery();

                // если успешно вставились данные
                if (numb > 0)
                {
                    return true;
                }
            }


            return false;
        }


        public bool Update()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  UPDATE [SPAVREMONT].[USLUGS_SHOP] SET                
                      [ID_SHOP]              ='" + ID_SHOP + @"'
                     ,[ID_USLUG]             ='" + Uslug.ID_USLUG + @"'
                     ,[PRICE]                =" + PRICE.ToString() + @"
                     ,[NOTE_USLUGS_SHOP]     ='" + NOTE_USLUGS_SHOP + @"'      
                 
                 
                    WHERE [ID_USLUGS_SHOP]='" + ID_USLUGS_SHOP + @"'
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numb = command.ExecuteNonQuery();

                // если успешно вставились данные
                if (numb > 0)
                {
                    return true;
                }
            }


            return false;
        }


        public static USLUGS_SHOP GetUslug(string ID_USLUGS_SHOP)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT 
                  [ID_USLUGS_SHOP]
                 ,[ID_SHOP]
                 ,[ID_USLUG]
                 ,[PRICE]
                 ,[NOTE_USLUGS_SHOP]
             FROM [SPAVREMONT].[USLUGS_SHOP]
             WHERE [ID_USLUGS_SHOP]='" + ID_USLUGS_SHOP +@"'
                ";
               


        connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_USLUGS_SHOP_Index = reader.GetOrdinal("ID_USLUGS_SHOP");
                    int ID_SHOP_Index = reader.GetOrdinal("ID_SHOP");
                    int ID_USLUG_Index = reader.GetOrdinal("ID_USLUG");
                    int PRICE_Index = reader.GetOrdinal("PRICE");
                    int NOTE_USLUGS_SHOP_Index = reader.GetOrdinal("NOTE_USLUGS_SHOP");


                    while (reader.Read()) // построчно считываем данные
                    {
                        return
                            new USLUGS_SHOP()
                            {
                                
                                ID_SHOP=reader.GetString(ID_SHOP_Index),
                                ID_USLUGS_SHOP = reader.GetString(ID_USLUGS_SHOP_Index),
                                NOTE_USLUGS_SHOP = reader.GetString(NOTE_USLUGS_SHOP_Index),
                                Uslug= new USLUG() { ID_USLUG = reader.GetString(ID_USLUG_Index) },
                                PRICE = reader.IsDBNull(PRICE_Index) ? 0 : reader.GetInt32(PRICE_Index)
                            };
                    }
                }
                reader.Close();
            }


            return null;
        }

    }
}
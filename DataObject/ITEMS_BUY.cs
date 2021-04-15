using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class ITEMS_BUY
    {
        public string ID_ITEMS_BUY { get; set; }        
        public string ID_ITEMS_SHOP { get; set; }
        [Required(ErrorMessage ="Введите название товара" )]
        public string NAME_IB { get; set; }        
        public string NOTE_IB { get; set; }        
        public string IMG_URL { get; set; }
        [Required(ErrorMessage = "Не указана цена")]
        public int PRICE { get; set; }

        public IFormFile IMG_URL_F { get; set; }


        public static List<ITEMS_BUY> GetITEMS_BUYs(string ID_ITEMS_SHOP)
        {
            List<ITEMS_BUY> iTEMS_BUYs = new List<ITEMS_BUY>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"  SELECT 
                ID_ITEMS_BUY,	
                ID_ITEMS_SHOP	,
                NAME_IB	,
                NOTE_IB	,
                IMG_URL	,
                PRICE 
                FROM SPAVREMONT.items_buy
                WHERE id_items_shop='" + ID_ITEMS_SHOP+@"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ITEMS_BUY_Index = reader.GetOrdinal("ID_ITEMS_BUY");
                    int ID_ITEMS_SHOP_Index = reader.GetOrdinal("ID_ITEMS_SHOP"); 
                    int NAME_IB_Index = reader.GetOrdinal("NAME_IB"); 
                    int NOTE_IB_Index = reader.GetOrdinal("NOTE_IB"); 
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");
                    int PRICE_Index = reader.GetOrdinal("PRICE");


                    while (reader.Read()) // построчно считываем данные
                    {
                        iTEMS_BUYs.Add(
                            new ITEMS_BUY()
                            {
                                ID_ITEMS_BUY=reader.GetString(ID_ITEMS_BUY_Index),
                                ID_ITEMS_SHOP = reader.GetString(ID_ITEMS_SHOP_Index),
                                IMG_URL =reader.IsDBNull(IMG_URL_Index) ? "" :  reader.GetString(IMG_URL_Index),
                                NAME_IB = reader.IsDBNull(NAME_IB_Index) ? "" : reader.GetString(NAME_IB_Index),
                                NOTE_IB = reader.IsDBNull(NOTE_IB_Index) ? "" : reader.GetString(NOTE_IB_Index),
                                PRICE = reader.IsDBNull(PRICE_Index) ? 0 : reader.GetInt32(PRICE_Index)
                            });
                    }
                }
                reader.Close();
            }


            return iTEMS_BUYs;
        }


        public bool CreateItemsBuy()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  INSERT INTO [SPAVREMONT].[ITEMS_BUY]
                       (
                       [ID_ITEMS_BUY]
                      ,[ID_ITEMS_SHOP]
                      ,[NAME_IB]
                      ,[NOTE_IB]
                      ,[IMG_URL]
                      ,[PRICE]
                      )
                  VALUES (
                       '" + Guid.NewGuid().ToString() + @"' --[ID_ITEMS_BUY]
                      ,'" + ID_ITEMS_SHOP + @"' --[ID_ITEMS_SHOP]
                      ,'" + NAME_IB + @"' --[NAME_IB]
                      ,'" + NOTE_IB + @"' --[NOTE_IB]
                      ,'" + IMG_URL + @"' --[IMG_URL]
                      ,"  + PRICE.ToString() + @" --[PRICE]
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


        public static bool Delete(string ID_ITEMS_BUY)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  DELETE [SPAVREMONT].[ITEMS_BUY]
                    WHERE [ID_ITEMS_BUY]='"+ ID_ITEMS_BUY + @"'
                   
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
                  UPDATE [SPAVREMONT].[ITEMS_BUY] SET
                                          
                   [ID_ITEMS_SHOP]  ='" + ID_ITEMS_SHOP + @"' 
                   ,[NAME_IB]  ='" + NAME_IB + @"'       
                   ,[NOTE_IB]  ='" + NOTE_IB + @"'       
                   ,[IMG_URL]  ='" + IMG_URL + @"'       
                   ,[PRICE]  =" + PRICE.ToString() + @"
                 
                    WHERE [ID_ITEMS_BUY]='" + ID_ITEMS_BUY + @"'
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


        public static ITEMS_BUY GetItem(string ID_ITEMS_BUY)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT 
                ID_ITEMS_BUY,	
                ID_ITEMS_SHOP	,
                NAME_IB	,
                NOTE_IB	,
                IMG_URL	,
                PRICE 
                FROM SPAVREMONT.items_buy
                WHERE ID_ITEMS_BUY='" + ID_ITEMS_BUY + @"'
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ITEMS_BUY_Index = reader.GetOrdinal("ID_ITEMS_BUY");
                    int ID_ITEMS_SHOP_Index = reader.GetOrdinal("ID_ITEMS_SHOP");
                    int NAME_IB_Index = reader.GetOrdinal("NAME_IB");
                    int NOTE_IB_Index = reader.GetOrdinal("NOTE_IB");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");
                    int PRICE_Index = reader.GetOrdinal("PRICE");


                    while (reader.Read()) // построчно считываем данные
                    {
                        return
                            new ITEMS_BUY()
                            {
                                ID_ITEMS_BUY = reader.GetString(ID_ITEMS_BUY_Index),
                                ID_ITEMS_SHOP = reader.GetString(ID_ITEMS_SHOP_Index),
                                IMG_URL = reader.IsDBNull(IMG_URL_Index) ? "" : reader.GetString(IMG_URL_Index),
                                NAME_IB = reader.IsDBNull(NAME_IB_Index) ? "" : reader.GetString(NAME_IB_Index),
                                NOTE_IB = reader.IsDBNull(NOTE_IB_Index) ? "" : reader.GetString(NOTE_IB_Index),
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
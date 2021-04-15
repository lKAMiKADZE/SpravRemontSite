using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class ITEM
    {
        public string ID_ITEM { get; set; }

        [Required(ErrorMessage = "Укажите название типа товара")]
        public string NAME_ITEM { get; set; }
        //public string NOTE_ITEM { get; set; }
        public string IMG_URL { get; set; }
        public IFormFile IMG_URL_F { get; set; }


        public bool Create()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                ID_ITEM = Guid.NewGuid().ToString();
                string sqlExpression = @"
                  INSERT INTO [SPAVREMONT].[ITEM]
                       (
                       [ID_ITEM]
                      ,[NAME_ITEM]
                      ,[NOTE_ITEM]
                      ,[IMG_URL]                     
                  )
                    VALUES
                    (
                    '"+ ID_ITEM + @"'
                    ,'"+ NAME_ITEM + @"'
                    ,''
                    ,'"+ IMG_URL + @"'
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

        public static bool Delete(string ID_ITEM)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  DELETE [SPAVREMONT].[ITEM]
                    WHERE [ID_ITEM]='" + ID_ITEM + @"'
                   
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
                  UPDATE [SPAVREMONT].[ITEM] SET
                       [NAME_ITEM]=  '"+ NAME_ITEM + @"'

                      ,[IMG_URL] =   '"+IMG_URL + @"'
                    WHERE [ID_ITEM]='" + ID_ITEM + @"'
                                          
                 
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

        public static ITEM GetItem(string ID_ITEM)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT 
                ID_ITEM,	
                NAME_ITEM,
                IMG_URL

                FROM SPAVREMONT.ITEM
                WHERE ID_ITEM='" + ID_ITEM + @"'
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ITEM_Index = reader.GetOrdinal("ID_ITEM");
                    int NAME_ITEM_SHOP_Index = reader.GetOrdinal("NAME_ITEM");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");


                    while (reader.Read()) // построчно считываем данные
                    {
                        return
                            new ITEM()
                            {
                                ID_ITEM = reader.GetString(ID_ITEM_Index),
                                NAME_ITEM = reader.GetString(NAME_ITEM_SHOP_Index),
                                IMG_URL = reader.GetString(IMG_URL_Index)
                            };
                    }
                }
                reader.Close();
            }


            return null;
        }


    }
}
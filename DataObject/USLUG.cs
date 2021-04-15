using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class USLUG
    {
        public string ID_USLUG   { get; set; }
        public string NAME_USLUG { get; set; }
        //public string NOTE_USLUG { get; set; }
        public string IMG_URL    { get; set; }
        public IFormFile IMG_URL_F { get; set; }

        public static List<USLUG> GetUSLUGs()
        {
            List<USLUG> uslugs = new List<USLUG>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    ID_USLUG,
                    NAME_USLUG,
                    IMG_URL
                     FROM SPAVREMONT.USLUG

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ID_USLUG_index = reader.GetOrdinal("ID_USLUG");
                    int NAME_USLUG_Index = reader.GetOrdinal("NAME_USLUG");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");


                    while (reader.Read()) // построчно считываем данные
                    {
                        uslugs.Add(
                            new USLUG()
                            {
                                ID_USLUG = reader.GetString(ID_ID_USLUG_index),
                                NAME_USLUG = reader.GetString(NAME_USLUG_Index),
                                IMG_URL = reader.IsDBNull(IMG_URL_Index) ? "" : reader.GetString(IMG_URL_Index)
                            });
                    }
                }
                reader.Close();
            }


            return uslugs;
        }


        public bool Create()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                ID_USLUG = Guid.NewGuid().ToString();
                string sqlExpression = @"
                  INSERT INTO [SPAVREMONT].[USLUG]
                       (
                       [ID_USLUG]
                      ,[NAME_USLUG]
                      ,[NOTE_USLUG]
                      ,[IMG_URL]                     
                  )
                    VALUES
                    (
                    '" + ID_USLUG + @"'
                    ,'" + NAME_USLUG + @"'
                    ,''
                    ,'" + IMG_URL + @"'
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

        public static bool Delete(string ID_USLUG)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  DELETE [SPAVREMONT].[USLUG]
                    WHERE [ID_USLUG]='" + ID_USLUG + @"'
                   
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
                  UPDATE [SPAVREMONT].[USLUG] SET                       
                        NAME_USLUG   ='" +NAME_USLUG+@"'
                        ,IMG_URL      ='"+ IMG_URL + @"'

                    WHERE [ID_USLUG]='" + ID_USLUG + @"' 
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

        public static USLUG GetItem(string ID_USLUG)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT 
                ID_USLUG,	
                NAME_USLUG,
                IMG_URL

                FROM SPAVREMONT.USLUG
                WHERE ID_USLUG='" + ID_USLUG + @"'
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_USLUG_Index = reader.GetOrdinal("ID_USLUG");
                    int NAME_USLUG_Index = reader.GetOrdinal("NAME_USLUG");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");


                    while (reader.Read()) // построчно считываем данные
                    {
                        return
                            new USLUG()
                            {
                                ID_USLUG = reader.GetString(ID_USLUG_Index),
                                NAME_USLUG  = reader.GetString(NAME_USLUG_Index),
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
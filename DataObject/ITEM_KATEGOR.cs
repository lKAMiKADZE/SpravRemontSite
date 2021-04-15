using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class ITEM_KATEGOR
    {
        public string ID_ITEM_KATEGOR { get; set; }
        public ITEM ITEM { get; set; }
        public KATEGOR KATEGOR { get; set; }


        private static List<ITEM_KATEGOR> GetITEM_KATEGORs(string ID_KATEGOR) // OLD
        {
            List<ITEM_KATEGOR> itemKATEGORs = new List<ITEM_KATEGOR>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    k.ID_ITEM_KATEGOR,	
                    it.NAME_ITEM
                    
                     FROM SPAVREMONT.ITEM_KATEGOR k
                     JOIN SPAVREMONT.ITEM it ON it.ID_item=k.id_item
                     WHERE ID_KATEGOR='" + ID_KATEGOR + @"'
                ";
                
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ITEM_KATEGOR_index = reader.GetOrdinal("ID_ITEM_KATEGOR");
                    int NAME_ITEM_Index = reader.GetOrdinal("NAME_ITEM");


                    while (reader.Read()) // построчно считываем данные
                    {
                        itemKATEGORs.Add(
                            new ITEM_KATEGOR()
                            {
                                ID_ITEM_KATEGOR = reader.GetString(ID_ITEM_KATEGOR_index),
                                ITEM = new ITEM()
                                {
                                    NAME_ITEM = reader.GetString(NAME_ITEM_Index)
                                    
                                }
                            });
                    }
                }
                reader.Close();
            }


            return itemKATEGORs;
        }
        
        public static List<ITEM_KATEGOR> GetITEM_KATEGORs_ADMIN(string ID_KATEGOR) 
        {
            List<ITEM_KATEGOR> itemKATEGORs = new List<ITEM_KATEGOR>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    k.ID_ITEM_KATEGOR,	    
                    k.ID_KATEGOR,
                    it.ID_ITEM,                
                    it.NAME_ITEM,
                    it.IMG_URL

                     FROM SPAVREMONT.ITEM_KATEGOR k
                     JOIN SPAVREMONT.ITEM it ON it.ID_item=k.id_item
                     WHERE ID_KATEGOR='" + ID_KATEGOR + @"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ITEM_KATEGOR_index = reader.GetOrdinal("ID_ITEM_KATEGOR");
                    int ID_KATEGORIndex = reader.GetOrdinal("ID_KATEGOR");
                    int ID_ITEM_Index = reader.GetOrdinal("ID_ITEM");
                    int NAME_ITEM_Index = reader.GetOrdinal("NAME_ITEM");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");


                    while (reader.Read()) // построчно считываем данные
                    {
                        itemKATEGORs.Add(
                            new ITEM_KATEGOR()
                            {
                                ID_ITEM_KATEGOR = reader.GetString(ID_ITEM_KATEGOR_index),
                                KATEGOR= new KATEGOR()
                                {
                                    ID_KATEGOR = reader.GetString(ID_KATEGORIndex)
                                },
                                ITEM = new ITEM()
                                {
                                    NAME_ITEM = reader.GetString(NAME_ITEM_Index),
                                    ID_ITEM = reader.GetString(ID_ITEM_Index),
                                    IMG_URL = reader.GetString(IMG_URL_Index)

                                }
                            });
                    }
                }
                reader.Close();
            }


            return itemKATEGORs;
        }

        public bool Create()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {

                ITEM.Create();

                ID_ITEM_KATEGOR = Guid.NewGuid().ToString();
                string sqlExpression = @"
                  INSERT INTO [SPAVREMONT].[ITEM_KATEGOR]
                       (
                       [ID_ITEM_KATEGOR]
                      ,[ID_ITEM]
                      ,[ID_KATEGOR]                     
                  )
                    VALUES
                    (
                      '"+ ID_ITEM_KATEGOR + @"',
                      '"+ITEM.ID_ITEM+@"',
                      '"+KATEGOR.ID_KATEGOR+@"'
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


        public static bool Delete(string ID_ITEM_KATEGOR, string ID_ITEM)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  DELETE [SPAVREMONT].[ITEM_KATEGOR]
                    WHERE [ID_ITEM_KATEGOR]='" + ID_ITEM_KATEGOR + @"'
                   
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numb = command.ExecuteNonQuery();

                // если успешно вставились данные
                if (numb > 0)
                {
                    ITEM.Delete(ID_ITEM);
                    return true;
                }
            }


            return false;
        }


        public static bool Update(ITEM item)
        {
            if (item.Update())
                return true;


            return false;
        }

        public static ITEM_KATEGOR GetITEM_ITEM_KATEGOR(string ID_ITEM_KATEGOR)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT                 
                       itkat.ID_ITEM_KATEGOR
                      ,itkat.ID_ITEM
                      ,itkat.ID_KATEGOR

                      ,it.NAME_ITEM
                      ,it.IMG_URL
                FROM SPAVREMONT.ITEM_KATEGOR itkat
                JOIN SPAVREMONT.ITEM it ON it.ID_ITEM= itkat.ID_ITEM
                WHERE itkat.ID_ITEM_KATEGOR='" + ID_ITEM_KATEGOR + @"'
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_ITEM_KATEGOR_Index = reader.GetOrdinal("ID_ITEM_KATEGOR");
                    int ID_ITEM_Index = reader.GetOrdinal("ID_ITEM");
                    int ID_KATEGOR_Index = reader.GetOrdinal("ID_KATEGOR");

                    int NAME_ITEM_Index = reader.GetOrdinal("NAME_ITEM");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");


                    while (reader.Read()) // построчно считываем данные
                    {
                        return
                            new ITEM_KATEGOR()
                            {
                                ID_ITEM_KATEGOR = reader.GetString(ID_ITEM_KATEGOR_Index),
                                ITEM = new ITEM() {
                                    ID_ITEM = reader.GetString(ID_ITEM_Index),
                                    IMG_URL = reader.GetString(IMG_URL_Index),
                                    NAME_ITEM= reader.GetString(NAME_ITEM_Index)
                                },
                                KATEGOR = new KATEGOR(){ID_KATEGOR = reader.GetString(ID_KATEGOR_Index)}
                            };
                    }
                }
                reader.Close();
            }


            return null;
        }// OLD


    }
}
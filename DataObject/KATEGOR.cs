using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class KATEGOR
    {
        public string ID_KATEGOR { get; set; }

        [Required(ErrorMessage = "Название категории пусто")]
        public string NAME_KATEGOR { get; set; }


        public static List<KATEGOR> GetKATEGORs()
        {
            List<KATEGOR> kATEGORs = new List<KATEGOR>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    ID_KATEGOR,	
                    NAME_KATEGOR
                     FROM SPAVREMONT.KATEGOR
                    ORDER BY NAME_KATEGOR ASC

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_KATEGOR_index = reader.GetOrdinal("ID_KATEGOR");
                    int NAME_KATEGOR_Index = reader.GetOrdinal("NAME_KATEGOR");


                    while (reader.Read()) // построчно считываем данные
                    {
                        kATEGORs.Add(
                            new KATEGOR()
                            {
                                ID_KATEGOR= reader.GetString(ID_KATEGOR_index),
                                NAME_KATEGOR= reader.GetString(NAME_KATEGOR_Index)
                            });
                    }
                }
                reader.Close();
            }


            return kATEGORs;
        }


        public static bool Create(string NAME_KATEGOR)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                NAME_KATEGOR = NAME_KATEGOR.Trim();

                string sqlExpression = @"
                  INSERT INTO [SPAVREMONT].[KATEGOR]
                       (
                       [ID_KATEGOR]
                      ,[NAME_KATEGOR]                      
                      )
                  VALUES
                     (
                       '" + Guid.NewGuid().ToString() + @"' --[ID_KATEGOR]
                      ,'" + NAME_KATEGOR + @"' --[NAME_KATEGOR]                      
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

        public bool Update()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  UPDATE [SPAVREMONT].[KATEGOR] SET                                          
                   [NAME_KATEGOR]  ='" + NAME_KATEGOR + @"'    
                    WHERE [ID_KATEGOR]='" + ID_KATEGOR + @"'
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



        public static bool Delete(string ID_KATEGOR)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  DELETE [SPAVREMONT].[KATEGOR]
                    WHERE [ID_KATEGOR]='" + ID_KATEGOR + @"'
                   
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


    }
}
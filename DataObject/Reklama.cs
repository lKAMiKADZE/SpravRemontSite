using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class Reklama
    {
        public string ID_Reklama { get; set; }
        public string IMG_URL { get; set; }
        public IFormFile IMG_URL_F { get; set; }


        public static List<Reklama> GetReklams()
        {
            List<Reklama> uslugs = new List<Reklama>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    ID_Reklama,
                    IMG_URL
                     FROM SPAVREMONT.Reklama
                ORDER BY ID_Reklama ASC

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    int ID_Reklama_index = reader.GetOrdinal("ID_Reklama");
                    int IMG_URL_Index = reader.GetOrdinal("IMG_URL");
                    
                    while (reader.Read()) // построчно считываем данные
                    {
                        uslugs.Add(
                            new Reklama()
                            {
                                ID_Reklama = reader.GetString(ID_Reklama_index),                                
                                IMG_URL = reader.IsDBNull(IMG_URL_Index) ? "" : reader.GetString(IMG_URL_Index)
                            });
                    }
                }
                reader.Close();
            }


            return uslugs;
        }



        public static string GetReklama(string ID_Reklama)
        {


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"ID_Reklama",SqlDbType.NVarChar) { Value =ID_Reklama ?? ""}

            };

            #region sql

            string sqlText = $@"
SELECT [ID_Reklama]
      ,[IMG_URL]
  FROM [SPAVREMONT].[Reklama]
  WHERE ID_Reklama=@ID_Reklama


";

            #endregion


            DataTable dt = new DataTable();// при наличии данных
            // получаем данные из запроса
            dt = ExecuteSqlGetDataTableStatic(sqlText, parameters);


            foreach (DataRow row in dt.Rows)
            {
                return (string)row["IMG_URL"];               
            }

            return "";
        }



        public bool Update()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  UPDATE [SPAVREMONT].[Reklama] SET            
                        IMG_URL      ='" + IMG_URL + @"'
                    WHERE [ID_Reklama]='" + ID_Reklama + @"' 
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



        ///////////////
        // Методы SQL
        ////////////////
        private async static void ExecuteSqlStatic(string sqlText, SqlParameter[] parameters = null)
        {

            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    // перехват ошибок и выполнение запроса
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception e) { }

                    command.Parameters.Clear();
                }

                connection.Close();
            }
        }

        private static DataTable ExecuteSqlGetDataTableStatic(string sqlText, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Load(reader);
                    }
                    catch (Exception ex)
                    {

                    }

                    command.Parameters.Clear();


                }

                connection.Close();
            }
            return dt;

        }


    }
}

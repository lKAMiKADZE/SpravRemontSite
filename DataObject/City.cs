using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.DataObject
{
    public class City
    {
        public string ID_City { get; set; }
        public string NAME_City { get; set; }

        public static List<City> GetCities()
        {
            List<City> Cities = new List<City>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                 ID_City,	
                 NAME_City
                FROM SPAVREMONT.City
                ORDER BY NAME_City ASC
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_City_index = reader.GetOrdinal("ID_City");
                    int NAME_City_Index = reader.GetOrdinal("NAME_City");


                    while (reader.Read()) // построчно считываем данные
                    {
                        Cities.Add(
                            new City(){
                                ID_City = reader.GetString(ID_City_index),
                                NAME_City = reader.GetString(NAME_City_Index)
                            });                               
                    }
                }
                reader.Close();
            }


            return Cities;
        }


    }
}

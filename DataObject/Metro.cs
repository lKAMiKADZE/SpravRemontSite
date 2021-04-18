using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Newtonsoft.Json;
using System.Data.SqlClient;

namespace SpravRemontSite.DataObject
{
    public class Metro
    {
        public string ID_metro   { get; set; }
        public string Name_line { get; set; }
        public string Station   { get; set; }
        public Geo    Geo       { get; set; }
        public string Color_Hex { get; set; }
        public string ID_City   { get; set; }

        public static List<Metro> GetCities(string ID_City)
        {
            List<Metro> metros = new List<Metro>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    id_metro,
                    station
                     FROM SPAVREMONT.metro
                WHERE id_city='"+ID_City+@"'
                     ORDER BY station ASC
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int id_metro_index = reader.GetOrdinal("id_metro");
                    int station_Index = reader.GetOrdinal("station");


                    while (reader.Read()) // построчно считываем данные
                    {
                        metros.Add(
                            new Metro()
                            {
                                ID_metro=reader.GetString(id_metro_index),
                                Station= reader.GetString(station_Index)
                            });
                    }
                }
                reader.Close();
            }


            return metros;
        }


        public bool Create()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                ID_metro = Guid.NewGuid().ToString();

                if (Station.Length > 50)
                    Station=Station.Substring(0, 50).Trim();

                Name_line = Name_line.Trim();
                Station = Station.Trim();
                Geo.ID_Geo = Geo.ID_Geo.Trim();
                Color_Hex = Color_Hex.Trim();
                ID_City = ID_City.Trim();


                string sqlExpression = @"
                  INSERT INTO SPAVREMONT.Metro
                        (
                        ID_metro,
                        Name_line,
                        Station,
                        ID_Geo,
                        Color_Hex,
                        ID_City
                        )
                        VALUES(
                       '" + ID_metro + @"',
                       '" + Name_line + @"',
                       '" + Station + @"',
                       '" + Geo.ID_Geo + @"',
                       '" + Color_Hex + @"',
                       '" + ID_City + @"'
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


        public static void loadMetroMoscow()
        {
            List<Metro> metros = new List<Metro>();

            //metros.Add( new Metro() { Station = "", Name_line = "", Color_Hex = "", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" }  );


            
            metros.Add(new Metro() { Station = "Арбатская  (Арбатско-Покровская линия) ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бауманская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Волоколамская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Измайловская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Киевская  (Арбатско-Покровская линия) ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Крылатское ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кунцевская  (Арбатско-Покровская линия) ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Курская  (Арбатско-Покровская линия) ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Митино ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Молодежная ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Мякинино ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Парк Победы  (Арбатско-Покровская линия) ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Партизанская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Первомайская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Площадь Революции ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Пятницкое шоссе ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Семеновская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Славянский бульвар ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Смоленская  (Арбатско-Покровская линия) ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Строгино ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Щелковская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Электрозаводская ", Name_line = "Арбатско-Покровская", Color_Hex = "#0078BE", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Деловой центр  (Большая кольцевая линия) ", Name_line = "Большая кольцевая линия", Color_Hex = "#26A69A", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Петровский парк  (Большая кольцевая линия) ", Name_line = "Большая кольцевая линия", Color_Hex = "#26A69A", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Савеловская  (Большая кольцевая линия) ", Name_line = "Большая кольцевая линия", Color_Hex = "#26A69A", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Хорошёвская  (Большая кольцевая линия) ", Name_line = "Большая кольцевая линия", Color_Hex = "#26A69A", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "ЦСКА  (Большая кольцевая линия) ", Name_line = "Большая кольцевая линия", Color_Hex = "#26A69A", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Шелепиха  (Большая кольцевая линия) ", Name_line = "Большая кольцевая линия", Color_Hex = "#26A69A", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Битцевский парк ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бульвар адмирала Ушакова ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бунинская Аллея ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Лесопарковая ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Улица Горчакова ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Улица Скобелевская ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Улица Старокачаловская ", Name_line = "Бутовская", Color_Hex = "#A1B3D4", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Автозаводская  (Замоскворецкая линия) ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Алма-Атинская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Аэропорт ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Беломорская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Белорусская  (Замоскворецкая линия) ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Водный стадион ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Войковская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Динамо ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Домодедовская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кантемировская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Каширская  (Замоскворецкая линия) ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Коломенская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Красногвардейская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Маяковская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новокузнецкая ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Орехово ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Павелецкая  (Замоскворецкая линия) ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Речной вокзал ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Сокол ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Тверская ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Театральная ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Технопарк ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Ховрино ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Царицыно ", Name_line = "Замоскворецкая", Color_Hex = "#2DBE2C", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Авиамоторная ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Боровское шоссе ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Говорово ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Ломоносовский проспект ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Марксистская ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Минская ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Мичуринский проспект ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новогиреево ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новокосино ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новопеределкино ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Озёрная ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Парк Победы  (Калининская линия) ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Перово ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Площадь Ильича ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Раменки ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Рассказовка ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Солнцево ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Третьяковская  (Калининская линия) ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Шоссе Энтузиастов  (Калининская линия) ", Name_line = "Калининская", Color_Hex = "#FFD702", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Академическая ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Алексеевская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бабушкинская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add( new Metro() { Station = "Беляево ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" }  );
            metros.Add(new Metro() { Station = "Ботанический сад  (Калужско-Рижская линия) ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add( new Metro() { Station = "ВДНХ ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" }  );
            metros.Add(new Metro() { Station = "Калужская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Китай-город  (Калужско-Рижская линия) ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Коньково ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Ленинский проспект ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Медведково ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новоясеневская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новые Черёмушки ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Октябрьская  (Калужско-Рижская линия) ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Проспект Мира  (Калужско-Рижская линия) ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Профсоюзная ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add( new Metro() { Station = "Рижская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" }  );
            metros.Add(new Metro() { Station = "Свиблово ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Сухаревская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Теплый стан ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Третьяковская  (Калужско-Рижская линия) ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Тургеневская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Шаболовская ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Ясенево ", Name_line = "Калужско-Рижская", Color_Hex = "#FFA500", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Варшавская ", Name_line = "Каховская", Color_Hex = "#a0bedc", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Каховская ", Name_line = "Каховская", Color_Hex = "#a0bedc", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Каширская  (Каховская линия) ", Name_line = "Каховская", Color_Hex = "#a0bedc", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Белорусская  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Добрынинская ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Киевская  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Комсомольская  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Краснопресненская ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Курская  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новослободская ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Октябрьская  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Павелецкая  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Парк Культуры  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Проспект Мира  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Таганская  (Кольцевая линия) ", Name_line = "Кольцевая", Color_Hex = "#8D5B2D", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Борисово ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Братиславская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бутырская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Верхние Лихоборы ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Волжская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Достоевская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Дубровка  (Люблинско-Дмитровская линия) ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Зябликово ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кожуховская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Крестьянская застава ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Люблино ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Марьина роща ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Марьино ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Окружная  (Люблинско-Дмитровская линия) ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Петровско-Разумовская  (Люблинско-Дмитровская линия) ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Печатники ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Римская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Селигерская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Сретенский бульвар ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Трубная ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Фонвизинская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Чкаловская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Шипиловская ", Name_line = "Люблинско-Дмитровская", Color_Hex = "#99CC00", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Автозаводская  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Андроновка ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Балтийская ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Белокаменная ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Ботанический сад  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бульвар Рокоссовского  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Верхние Котлы ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Владыкино  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Деловой центр  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Дубровка  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "ЗИЛ ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Зорге ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Измайлово ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Коптево ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Крымская ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кутузовская  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Лихоборы ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Локомотив ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Лужники ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Нижегородская ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Новохохловская ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Окружная  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Панфиловская ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Площадь Гагарина ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Ростокино ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Соколиная Гора ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Стрешнево ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Угрешская ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Хорошёво ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Шелепиха  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Шоссе Энтузиастов  (Московское центральное кольцо линия) ", Name_line = "Московское центральное кольцо", Color_Hex = "#FFCDD2", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Алтуфьево ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Аннино ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бибирево ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Боровицкая ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бульвар Дмитрия Донского ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Владыкино  (Серпуховско-Тимирязевская линия) ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Дмитровская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Менделеевская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Нагатинская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Нагорная ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Нахимовский Проспект ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Отрадное ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Петровско-Разумовская  (Серпуховско-Тимирязевская линия) ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Полянка ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Пражская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Савеловская  (Серпуховско-Тимирязевская линия) ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Севастопольская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Серпуховская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Тимирязевская  (Серпуховско-Тимирязевская линия) ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Тульская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Улица академика Янгеля ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Цветной бульвар ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Чертановская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Чеховская ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Южная ", Name_line = "Серпуховско-Тимирязевская", Color_Hex = "#999999", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Библиотека имени Ленина ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Бульвар Рокоссовского  (Сокольническая линия) ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Воробьевы горы ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add( new Metro() { Station = "Комсомольская  (Сокольническая линия) ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" }  );
            metros.Add(new Metro() { Station = "Красносельская ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Красные ворота ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кропоткинская ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Лубянка ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Охотный ряд ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Парк Культуры  (Сокольническая линия) ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Преображенская площадь ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Проспект Вернадского ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Румянцево ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Саларьево ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Сокольники ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Спортивная ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Тропарёво ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Университет ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Фрунзенская ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Черкизовская ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Чистые пруды ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Юго-Западная ", Name_line = "Сокольническая", Color_Hex = "#ff0000", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Баррикадная ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Беговая ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Волгоградский проспект ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Выхино ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Жулебино ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Китай-город  (Таганско-Краснопресненская линия) ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Котельники ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кузнецкий мост ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кузьминки ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Лермонтовский проспект ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Октябрьское поле ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Планерная ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Полежаевская ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Пролетарская ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Пушкинская ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Рязанский проспект ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Спартак ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Сходненская ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Таганская  (Таганско-Краснопресненская линия) ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Текстильщики ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Тушинская ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Улица 1905 года ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Щукинская ", Name_line = "Таганско-Краснопресненская", Color_Hex = "#800080", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Александровский сад ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Арбатская  (Филевская линия) ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Багратионовская ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Выставочная ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Киевская  (Филевская линия) ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кунцевская  (Филевская линия) ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Кутузовская  (Филевская линия) ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Международная ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Пионерская ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Смоленская  (Филевская линия) ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Студенческая ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Филевский парк ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });
            metros.Add(new Metro() { Station = "Фили ", Name_line = "Филевская", Color_Hex = "#00BFFF", Geo = new Geo() { ID_Geo = "6d0eb5f2-0ffd-411b-9cf2-118a60b22604" }, ID_City = "6d0eb5f2-01fd-411b-9cf2-318a60b22604" });







            foreach (Metro item in metros)
            {
                item.Create();
            }        
        }


    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SpravRemontSite.DataObject
{
    public class Shop
    {
        //[JsonProperty("ID_shop")]
        public string ID_shop { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public Metro Metro { get; set; }
        public Geo Geo { get; set; }
        public DateTime DateAdd { get; set; }
        public TimeWork TimeWork { get; set; }
        public string Note { get; set; }
        public bool BuyCard { get; set; }
        public int TimeWayMetro { get; set; }

        public City City { get; set; }

        public TYPE_SHOP TYPE_SHOP { get; set; }

        public double AVG_Star { get; set; }// рейтинг магазина по комментариям, дополнительный запрос
        public int Count_feedback { get; set; }// кол-во оставленных отзывов

        public string IMG_LOGO { get; set; }// URL link img
        public string IMG_DRIVE_TO { get; set; }
        public string IMG_MAP { get; set; }

        public IFormFile IMG_LOGO_f { get; set; }
        public IFormFile IMG_DRIVE_TO_f { get; set; }
        public IFormFile IMG_MAP_f { get; set; }

        public string DISCONT_NOTE { get; set; }
        public bool DISCONT_CARD { get; set; }

        public bool New_shop { get; set; }
        public bool VISIBLE { get; set; }
        public bool ADMIN_SHOP { get; set; }
        public string Descrip { get; set; }
        



        public Shop() { }

        public Shop(string emailLogin)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {


                #region sql
                string sqlExpression = @"SELECT
                s.ID_shop,
                s.Type,
                s.Name,
                s.Phone,
                s.Adress,
                s.Email,
                s.Site,
                s.Id_Geo,
                s.DateAdd,
                s.TimeWork,
                s.Note,
                s.Admin_shop,
                
                s.id_city,
                c.name_city,

                m.ID_Metro,
                m.Station,
                m.Color_hex,
                t.ID_timeWork ,
                t.MondayStart,
                t.MondayEnd ,
                t.TuesdayStart ,
                t.TuesdayEnd ,
                t.WednesdayStart,
                t.WednesdayEnd ,
                t.ThursdayStart ,
                t.ThursdayEnd ,
                t.FridayStart ,
                t.FridayEnd ,
                t.SaturdayStart ,
                t.SaturdayEnd ,
                t.SundayStart ,
                t.SundayEnd,

                s.IMG_LOGO ,
                s.IMG_DRIVE_TO,
                s.IMG_MAP,

                s.DISCONT_NOTE,
                s.DISCONT_CARD,

                u.email,
                u.Type_shop,
                tp.name_type,
                s.TimeWayMetro,
                
                s.Descrip


                
                FROM SPAVREMONT.Shop S
                JOIN users u ON u.ID_user=s.id_shop
                JOIN SPAVREMONT.Type_shop tp ON s.id_type_shop=tp.id_type_shop
                LEFT JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                LEFT JOIN SPAVREMONT.Timework t ON t.ID_timework=s.timework
                JOIN SPAVREMONT.City c ON c.id_city=s.id_city
                WHERE 1=1
                    AND u.email='" + emailLogin + @"'


                    ";

                #endregion


                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    int sID_shopIndex = reader.GetOrdinal("ID_shop");
                    int sTypeIndex = reader.GetOrdinal("Type");
                    int sNameIndex = reader.GetOrdinal("Name");
                    int sPhoneIndex = reader.GetOrdinal("Phone");
                    int sAdressIndex = reader.GetOrdinal("Adress");
                    int sEmailIndex = reader.GetOrdinal("Email");
                    int sSiteIndex = reader.GetOrdinal("Site");
                    int sId_GeoIndex = reader.GetOrdinal("Id_Geo");
                    int sDateAddIndex = reader.GetOrdinal("DateAdd");
                    int sTimeWorkIndex = reader.GetOrdinal("TimeWork");
                    int sNoteIndex = reader.GetOrdinal("Note");
                    int mID_MetroIndex = reader.GetOrdinal("ID_Metro");
                    int mStationIndex = reader.GetOrdinal("Station");
                    int mColor_hexIndex = reader.GetOrdinal("Color_hex");
                    int tID_timeWorkIndex = reader.GetOrdinal("ID_timeWork");
                    int tMondayStartIndex = reader.GetOrdinal("MondayStart");
                    int tMondayEndIndex = reader.GetOrdinal("MondayEnd");
                    int tTuesdayStartIndex = reader.GetOrdinal("TuesdayStart");
                    int tTuesdayEndIndex = reader.GetOrdinal("TuesdayEnd");
                    int tWednesdayStartIndex = reader.GetOrdinal("WednesdayStart");
                    int tWednesdayEndIndex = reader.GetOrdinal("WednesdayEnd");
                    int tThursdayStartIndex = reader.GetOrdinal("ThursdayStart");
                    int tThursdayEndIndex = reader.GetOrdinal("ThursdayEnd");
                    int tFridayStartIndex = reader.GetOrdinal("FridayStart");
                    int tFridayEndIndex = reader.GetOrdinal("FridayEnd");
                    int tSaturdayStartIndex = reader.GetOrdinal("SaturdayStart");
                    int tSaturdayEndIndex = reader.GetOrdinal("SaturdayEnd");
                    int tSundayStartIndex = reader.GetOrdinal("SundayStart");
                    int tSundayEndIndex = reader.GetOrdinal("SundayEnd");

                    int sIMG_LOGO_index = reader.GetOrdinal("IMG_LOGO");
                    int sIMG_DRIVE_TO_index = reader.GetOrdinal("IMG_DRIVE_TO");
                    int sIMG_MAP_index = reader.GetOrdinal("IMG_MAP");

                    int sDISCONT_NOTE_index = reader.GetOrdinal("DISCONT_NOTE");
                    int sDISCONT_CARD_index = reader.GetOrdinal("DISCONT_CARD");

                    int sid_city_index = reader.GetOrdinal("id_city");
                    int cname_city_index = reader.GetOrdinal("name_city");

                    int tpName_type_index = reader.GetOrdinal("name_type");
                    int sTimeWayMetro_index = reader.GetOrdinal("TimeWayMetro");
                    int Descrip_index = reader.GetOrdinal("Descrip");



                    while (reader.Read()) // построчно считываем данные
                    {
                        Metro metro = new Metro
                        {
                            ID_metro = reader.IsDBNull(mID_MetroIndex) ? "" : reader.GetString(mID_MetroIndex),
                            Color_Hex = reader.IsDBNull(mColor_hexIndex) ? "" : reader.GetString(mColor_hexIndex),
                            Station = reader.IsDBNull(mStationIndex) ? "" : reader.GetString(mStationIndex)
                        };

                        TimeWork timework = new TimeWork
                        {
                            ID_timeWork = reader.GetString(tID_timeWorkIndex),
                            FridayEnd = reader.GetString(tFridayEndIndex),
                            FridayStart = reader.GetString(tFridayStartIndex),
                            MondayEnd = reader.GetString(tMondayEndIndex),
                            MondayStart = reader.GetString(tMondayStartIndex),
                            SaturdayEnd = reader.GetString(tSaturdayEndIndex),
                            SaturdayStart = reader.GetString(tSaturdayStartIndex),
                            SundayEnd = reader.GetString(tSundayEndIndex),
                            SundayStart = reader.GetString(tSundayStartIndex),
                            ThursdayEnd = reader.GetString(tThursdayEndIndex),
                            ThursdayStart = reader.GetString(tThursdayStartIndex),
                            TuesdayEnd = reader.GetString(tTuesdayEndIndex),
                            TuesdayStart = reader.GetString(tTuesdayStartIndex),
                            WednesdayEnd = reader.GetString(tWednesdayEndIndex),
                            WednesdayStart = reader.GetString(tWednesdayStartIndex)
                        };

                        //sh = new Shop
                        //{
                        ID_shop = reader.GetString(sID_shopIndex);
                        Type = reader.GetString(sTypeIndex);
                        Name = reader.GetString(sNameIndex);
                        Phone = reader.IsDBNull(sPhoneIndex) ? "" : reader.GetString(sPhoneIndex);
                        Adress = reader.IsDBNull(sAdressIndex) ? "" : reader.GetString(sAdressIndex);
                        Metro = metro;
                        DateAdd = reader.GetDateTime(sDateAddIndex);
                        TimeWork = timework;
                        Email = reader.IsDBNull(sEmailIndex) ? "" : reader.GetString(sEmailIndex);
                        Note = reader.IsDBNull(sNoteIndex) ? "" : reader.GetString(sNoteIndex);
                        Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex);
                        IMG_LOGO = reader.IsDBNull(sIMG_LOGO_index) ? "" : reader.GetString(sIMG_LOGO_index);
                        IMG_DRIVE_TO = reader.IsDBNull(sIMG_DRIVE_TO_index) ? "" : reader.GetString(sIMG_DRIVE_TO_index);
                        IMG_MAP = reader.IsDBNull(sIMG_MAP_index) ? "" : reader.GetString(sIMG_MAP_index);
                        DISCONT_CARD = reader.IsDBNull(sDISCONT_CARD_index) ? false : reader.GetBoolean(sDISCONT_CARD_index);
                        DISCONT_NOTE = reader.IsDBNull(sDISCONT_NOTE_index) ? "" : reader.GetString(sDISCONT_NOTE_index);
                        TYPE_SHOP = new TYPE_SHOP() { NAME_TYPE = reader.GetString(tpName_type_index) };
                        City = new City() { ID_City = reader.GetString(sid_city_index), NAME_City = reader.GetString(cname_city_index) };
                        TimeWayMetro = reader.GetInt32(sTimeWayMetro_index);
                        Descrip = reader.GetString(Descrip_index);

                        //};
                    }
                }

                reader.Close();// закрытие потока




            }// using db
        }

        public static bool CreateShop(string ID_shop, TYPE_SHOP Type_shop, string ID_time_work)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  INSERT  INTO SPAVREMONT.Shop (
                       [ID_shop]
                      ,[Type]
                      ,[Name]
                      ,[Phone]
                      ,[Adress]
                      ,[Email]
                      ,[Site]
                      ,[ID_Metro]
                      ,[Id_Geo]
                      ,[DateAdd]
                      ,[TimeWork]
                      ,[Note]
                      ,[ID_TYPE_SHOP]
                      ,[BuyCard]
                      ,[TimeWayMetro]
                      ,[ID_City]
                      ,[IMG_LOGO]
                      ,[IMG_DRIVE_TO]
                      ,[IMG_MAP]
                      ,[DISCONT_NOTE]
                      ,[DISCONT_CARD]
                      ,[VISIBLE]
                      ,[New_shop]
                      ,[ADMIN_SHOP]
                      ,[Descrip]
)
                VALUES (
                       '" + ID_shop + @"'--[ID_shop]
                      ,'" + Type_shop.NAME_TYPE + @"'--[Type]
                      ,''--[Name]
                      ,''--[Phone]
                      ,''--[Adress]
                      ,''--[Email]
                      ,''--[Site]
                      ,null--[ID_Metro]
                      ,null--[Id_Geo]
                      ,CURRENT_TIMESTAMP--[DateAdd]
                      ,'" + ID_time_work + @"'--[TimeWork]
                      ,''--[Note]
                      ,'" + Type_shop.ID_TYPE_SHOP + @"'--[ID_TYPE_SHOP]
                      ,0--[BuyCard]
                      ,0--[TimeWayMetro]
                      ,'6d0eb5f2-01fd-411b-9cf2-318a60b22604'--[ID_City] -- Москва 
                      ,''--[IMG_LOGO]
                      ,''--[IMG_DRIVE_TO]
                      ,''--[IMG_MAP]
                      ,''--[DISCONT_NOTE]
                      ,0--[DISCONT_CARD]
                      ,0--[VISIBLE]
                      ,1--[New_shop]
                      ,0--[ADMIN_SHOP]
                      ,''
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

        public bool UpdateShop()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string id_metro = "null";
                if (Metro.ID_metro != null)
                    id_metro = "'" + Metro.ID_metro + "'";

                string sqlExpression = @" UPDATE[SPAVREMONT].[Shop] SET
                     [Name]          ='" + Name + @"'
                     ,[Phone]         ='" + Phone + @"'
                     ,[Adress]        ='" + Adress + @"'
                     ,[Email]         ='" + Email + @"'
                     ,[Site]          ='" + Site + @"'
                     ,[ID_Metro]      =" + id_metro + @"
                     --,[Id_Geo]      =''
                     ,[Note]          ='" + Note + @"'
                     ,[BuyCard]       =" + Convert.ToInt32(BuyCard) + @"
                     ,[TimeWayMetro]  =" + TimeWayMetro + @"
                     ,[ID_City]       ='" + City.ID_City + @"'
                     ,[IMG_LOGO]      ='" + IMG_LOGO + @"'
                     ,[IMG_DRIVE_TO]  ='" + IMG_DRIVE_TO + @"'
                     ,[IMG_MAP]       ='" + IMG_MAP + @"'
                     ,[DISCONT_NOTE]  ='" + DISCONT_NOTE + @"'
                     ,[DISCONT_CARD]  =" + Convert.ToInt32(DISCONT_CARD) + @"                     
                     ,[New_shop]=   " + Convert.ToInt32(New_shop) + @"
                     ,[ADMIN_SHOP]= " + Convert.ToInt32(ADMIN_SHOP) + @"
                     ,[Descrip]       ='" + Descrip + @"'
                WHERE [ID_shop] = '" + ID_shop + @"'
                ";
                //,[VISIBLE]=    "+ Convert.ToInt32(VISIBLE) + @"

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    return true;
                }
                reader.Close();
            }
            return false;
        }


        public static List<Shop> GetShops()
        {
            List<Shop> shops = new List<Shop>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT 
                    ID_shop,
                    IMG_LOGO,
                    Name,
                    DateAdd,
                    Phone,
                    Type,                    
                    New_shop,
                    VISIBLE
                     FROM SPAVREMONT.Shop
                ORDER BY New_shop DESC, NAME ASC --, DateAdd ASC

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int IMG_LOGO_index = reader.GetOrdinal("IMG_LOGO");
                    int Name_Index = reader.GetOrdinal("Name");
                    int DateAdd_Index = reader.GetOrdinal("DateAdd");
                    int Phone_Index = reader.GetOrdinal("Phone");
                    int Type_Index = reader.GetOrdinal("Type");
                    int New_shop_Index = reader.GetOrdinal("New_shop");

                    int ID_shop_Index = reader.GetOrdinal("ID_shop");
                    int VISIBLE_Index = reader.GetOrdinal("VISIBLE");


                    while (reader.Read()) // построчно считываем данные
                    {
                        shops.Add(
                            new Shop()
                            {
                                ID_shop= reader.GetString(ID_shop_Index),
                                IMG_LOGO = reader.GetString(IMG_LOGO_index),
                                Name = reader.GetString(Name_Index),
                                DateAdd = reader.GetDateTime(DateAdd_Index).ToUniversalTime(),
                                Phone = reader.GetString(Phone_Index),
                                Type = reader.GetString(Type_Index),
                                New_shop = reader.GetBoolean(New_shop_Index),
                                VISIBLE = reader.GetBoolean(VISIBLE_Index)
                            });
                    }
                }
                reader.Close();
            }


            return shops;
        }

        

        public static bool Deactivated(string ID_shop)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                   UPDATE [SPAVREMONT].[Shop] SET
                        VISIBLE=0
                    WHERE [ID_shop]='" + ID_shop + @"'
                   
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
        
        public static bool Active(string ID_shop)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                   UPDATE [SPAVREMONT].[Shop] SET
                        VISIBLE=1,New_shop=0
                    WHERE [ID_shop]='" + ID_shop + @"'
                   
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

        public static Shop GetItemAdmin(string ID_Shop)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {                
                #region sql
                string sqlExpression = @"SELECT
                s.ID_shop,
                s.Type,
                s.Name,
                s.Phone,
                s.Adress,
                s.Email,
                s.Site,
                s.Id_Geo,
                s.DateAdd,
                s.TimeWork,
                s.Note,
                s.Admin_shop,
                
                s.id_city,
                c.name_city,

                m.ID_Metro,
                m.Station,
                m.Color_hex,
                t.ID_timeWork ,
                t.MondayStart,
                t.MondayEnd ,
                t.TuesdayStart ,
                t.TuesdayEnd ,
                t.WednesdayStart,
                t.WednesdayEnd ,
                t.ThursdayStart ,
                t.ThursdayEnd ,
                t.FridayStart ,
                t.FridayEnd ,
                t.SaturdayStart ,
                t.SaturdayEnd ,
                t.SundayStart ,
                t.SundayEnd,

                s.IMG_LOGO ,
                s.IMG_DRIVE_TO,
                s.IMG_MAP,

                s.DISCONT_NOTE,
                s.DISCONT_CARD,

                u.email,
                u.Type_shop,
                tp.name_type,
                s.TimeWayMetro,
                
                s.New_shop,
                s.VISIBLE,
                s.BuyCard,
                s.ADMIN_SHOP,
                s.Descrip
                
                FROM SPAVREMONT.Shop S
                JOIN users u ON u.ID_user=s.id_shop
                JOIN SPAVREMONT.Type_shop tp ON s.id_type_shop=tp.id_type_shop
                LEFT JOIN SPAVREMONT.Metro m ON s.ID_Metro=m.ID_Metro
                LEFT JOIN SPAVREMONT.Timework t ON t.ID_timework=s.timework
                JOIN SPAVREMONT.City c ON c.id_city=s.id_city
                WHERE 1=1
                    AND S.ID_Shop='" + ID_Shop + @"'

                    ";

                #endregion

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    int sID_shopIndex = reader.GetOrdinal("ID_shop");
                    int sTypeIndex = reader.GetOrdinal("Type");
                    int sNameIndex = reader.GetOrdinal("Name");
                    int sPhoneIndex = reader.GetOrdinal("Phone");
                    int sAdressIndex = reader.GetOrdinal("Adress");
                    int sEmailIndex = reader.GetOrdinal("Email");
                    int sSiteIndex = reader.GetOrdinal("Site");
                    int sId_GeoIndex = reader.GetOrdinal("Id_Geo");
                    int sDateAddIndex = reader.GetOrdinal("DateAdd");
                    int sTimeWorkIndex = reader.GetOrdinal("TimeWork");
                    int sNoteIndex = reader.GetOrdinal("Note");
                    int mID_MetroIndex = reader.GetOrdinal("ID_Metro");
                    int mStationIndex = reader.GetOrdinal("Station");
                    int mColor_hexIndex = reader.GetOrdinal("Color_hex");
                    int tID_timeWorkIndex = reader.GetOrdinal("ID_timeWork");
                    int tMondayStartIndex = reader.GetOrdinal("MondayStart");
                    int tMondayEndIndex = reader.GetOrdinal("MondayEnd");
                    int tTuesdayStartIndex = reader.GetOrdinal("TuesdayStart");
                    int tTuesdayEndIndex = reader.GetOrdinal("TuesdayEnd");
                    int tWednesdayStartIndex = reader.GetOrdinal("WednesdayStart");
                    int tWednesdayEndIndex = reader.GetOrdinal("WednesdayEnd");
                    int tThursdayStartIndex = reader.GetOrdinal("ThursdayStart");
                    int tThursdayEndIndex = reader.GetOrdinal("ThursdayEnd");
                    int tFridayStartIndex = reader.GetOrdinal("FridayStart");
                    int tFridayEndIndex = reader.GetOrdinal("FridayEnd");
                    int tSaturdayStartIndex = reader.GetOrdinal("SaturdayStart");
                    int tSaturdayEndIndex = reader.GetOrdinal("SaturdayEnd");
                    int tSundayStartIndex = reader.GetOrdinal("SundayStart");
                    int tSundayEndIndex = reader.GetOrdinal("SundayEnd");

                    int sIMG_LOGO_index = reader.GetOrdinal("IMG_LOGO");
                    int sIMG_DRIVE_TO_index = reader.GetOrdinal("IMG_DRIVE_TO");
                    int sIMG_MAP_index = reader.GetOrdinal("IMG_MAP");

                    int sDISCONT_NOTE_index = reader.GetOrdinal("DISCONT_NOTE");
                    int sDISCONT_CARD_index = reader.GetOrdinal("DISCONT_CARD");

                    int sid_city_index = reader.GetOrdinal("id_city");
                    int cname_city_index = reader.GetOrdinal("name_city");

                    int tpName_type_index = reader.GetOrdinal("name_type");
                    int sTimeWayMetro_index = reader.GetOrdinal("TimeWayMetro");

                    int New_shop_index = reader.GetOrdinal("New_shop");
                    int VISIBLE_index = reader.GetOrdinal("VISIBLE");
                    int BuyCard_index = reader.GetOrdinal("BuyCard");
                    int ADMIN_SHOP_index = reader.GetOrdinal("ADMIN_SHOP");
                    int Descrip_index = reader.GetOrdinal("Descrip");

                    

                    



                    while (reader.Read()) // построчно считываем данные
                    {
                        Metro metro = new Metro
                        {
                            ID_metro = reader.IsDBNull(mID_MetroIndex) ? "" : reader.GetString(mID_MetroIndex),
                            Color_Hex = reader.IsDBNull(mColor_hexIndex) ? "" : reader.GetString(mColor_hexIndex),
                            Station = reader.IsDBNull(mStationIndex) ? "" : reader.GetString(mStationIndex)
                        };

                        TimeWork timework = new TimeWork
                        {
                            ID_timeWork = reader.GetString(tID_timeWorkIndex),
                            FridayEnd = reader.GetString(tFridayEndIndex),
                            FridayStart = reader.GetString(tFridayStartIndex),
                            MondayEnd = reader.GetString(tMondayEndIndex),
                            MondayStart = reader.GetString(tMondayStartIndex),
                            SaturdayEnd = reader.GetString(tSaturdayEndIndex),
                            SaturdayStart = reader.GetString(tSaturdayStartIndex),
                            SundayEnd = reader.GetString(tSundayEndIndex),
                            SundayStart = reader.GetString(tSundayStartIndex),
                            ThursdayEnd = reader.GetString(tThursdayEndIndex),
                            ThursdayStart = reader.GetString(tThursdayStartIndex),
                            TuesdayEnd = reader.GetString(tTuesdayEndIndex),
                            TuesdayStart = reader.GetString(tTuesdayStartIndex),
                            WednesdayEnd = reader.GetString(tWednesdayEndIndex),
                            WednesdayStart = reader.GetString(tWednesdayStartIndex)
                        };

                        return new Shop
                        {
                            ID_shop = reader.GetString(sID_shopIndex),
                            Type = reader.GetString(sTypeIndex),
                            Name = reader.GetString(sNameIndex),
                            Phone = reader.IsDBNull(sPhoneIndex) ? "" : reader.GetString(sPhoneIndex),
                            Adress = reader.IsDBNull(sAdressIndex) ? "" : reader.GetString(sAdressIndex),
                            Metro = metro,
                            DateAdd = reader.GetDateTime(sDateAddIndex),
                            TimeWork = timework,
                            Email = reader.IsDBNull(sEmailIndex) ? "" : reader.GetString(sEmailIndex),
                            Note = reader.IsDBNull(sNoteIndex) ? "" : reader.GetString(sNoteIndex),
                            Site = reader.IsDBNull(sSiteIndex) ? "" : reader.GetString(sSiteIndex),
                            IMG_LOGO = reader.IsDBNull(sIMG_LOGO_index) ? "" : reader.GetString(sIMG_LOGO_index),
                            IMG_DRIVE_TO = reader.IsDBNull(sIMG_DRIVE_TO_index) ? "" : reader.GetString(sIMG_DRIVE_TO_index),
                            IMG_MAP = reader.IsDBNull(sIMG_MAP_index) ? "" : reader.GetString(sIMG_MAP_index),
                            DISCONT_CARD = reader.IsDBNull(sDISCONT_CARD_index) ? false : reader.GetBoolean(sDISCONT_CARD_index),
                            DISCONT_NOTE = reader.IsDBNull(sDISCONT_NOTE_index) ? "" : reader.GetString(sDISCONT_NOTE_index),
                            TYPE_SHOP = new TYPE_SHOP() { NAME_TYPE = reader.GetString(tpName_type_index) },
                            City = new City() { ID_City = reader.GetString(sid_city_index), NAME_City = reader.GetString(cname_city_index) },
                            TimeWayMetro = reader.GetInt32(sTimeWayMetro_index),
                            VISIBLE= reader.GetBoolean(VISIBLE_index),
                            New_shop = reader.GetBoolean(New_shop_index),
                            BuyCard = reader.GetBoolean(BuyCard_index),
                            ADMIN_SHOP = reader.GetBoolean(ADMIN_SHOP_index),
                            Descrip= reader.GetString(Descrip_index)
                            

                        };
                    }
                }

                reader.Close();// закрытие потока




            }// using db


            return null;
        }

        
        


    }//
}
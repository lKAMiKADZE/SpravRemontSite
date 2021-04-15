using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.DataObject
{
    public class IMG_Shop
    {
        public long ID_IMG_Shop { get; set; }
        public string ID_SHOP { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public bool Deleted { get; set; }
        public DateTime Date_add { get; set; }

        // доп парам
        public IFormFile IMG_formFile{ get; set; }



        public static void Delete(long ID_IMG_Shop, string ID_SHOP)
        {


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"ID_IMG_Shop",SqlDbType.BigInt) { Value =ID_IMG_Shop },
                new SqlParameter(@"ID_SHOP",SqlDbType.NVarChar) { Value =ID_SHOP }
            };

            #region sql

            string sqlText = $@"
UPDATE [SPAVREMONT].[IMG_Shop] set Deleted=1
WHERE 1=1
	AND ID_IMG_SHOP=@ID_IMG_SHOP
	AND ID_SHOP=@ID_SHOP



";

            #endregion

            // получаем данные из запроса
            ExecuteSqlStatic(sqlText, parameters);




        }

        public void Create()
        {


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"ID_SHOP",SqlDbType.NVarChar) { Value =ID_SHOP ?? ""},
                new SqlParameter(@"Url",SqlDbType.NVarChar) { Value =Url ?? ""},
                new SqlParameter(@"Type",SqlDbType.NVarChar) { Value =Type ?? ""}

            };

            #region sql

            string sqlText = $@"
INSERT INTO [SPAVREMONT].[IMG_Shop]
           ([ID_SHOP]
           ,[Url]
           ,[Type]
           ,[Deleted]
           ,[Date_add])
     VALUES
           (
			@ID_SHOP		--<ID_SHOP, nvarchar(50),>
           ,@Url			--<Url, nvarchar(500),>
           ,@Type			--<Type, nvarchar(50),>
           ,0				--<Deleted, bit,>
           ,CURRENT_TIMESTAMP	--<Date_add, datetime,>
		   
		   )


";

            #endregion

            // получаем данные из запроса
            ExecuteSqlStatic(sqlText, parameters);




        }


        public static List<IMG_Shop> GetShopIMGs(string ID_SHOP)
        {
            List<IMG_Shop> iMG_Shops = new List<IMG_Shop>();


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"ID_SHOP",SqlDbType.NVarChar) { Value =ID_SHOP }
            };

            #region sql

            string sqlText = $@"
SELECT TOP(10)
       [ID_IMG_SHOP]
      ,[ID_SHOP]
      ,[Url]
      ,[Type]
      ,[Deleted]
      ,[Date_add]
  FROM [SPAVREMONT].[IMG_Shop]
  WHERE 1=1
	AND Deleted=0
	AND ID_SHOP=@ID_SHOP
    AND Type='VITRINA'

	ORDER BY Date_add DESC

";

            #endregion

            DataTable dt = new DataTable();// при наличии данных
            // получаем данные из запроса
            dt = ExecuteSqlGetDataTableStatic(sqlText, parameters);


            foreach (DataRow row in dt.Rows)
            {

                // попали в цикл, значит авторизовались, т.к. такой пользователь существует
                IMG_Shop img = new IMG_Shop
                {
                    Date_add= (DateTime)row["Date_add"],
                    Deleted = (bool)row["Deleted"],
                    ID_IMG_Shop = (long)row["ID_IMG_Shop"],
                    ID_SHOP = (string)row["ID_SHOP"],
                    Type = (string)row["Type"],
                    Url = (string)row["Url"]
                };

                iMG_Shops.Add(img);
            }


            return iMG_Shops;
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

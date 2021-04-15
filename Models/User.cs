using SpravRemontSite.DataObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.Models
{
    public class User
    {
        public string ID_user { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string type_shop { get; set; }
        public TYPE_SHOP Type_shop { get; set; }

        public static string GetIDUser(string email)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT
                    email, id_user, type_shop 
                FROM USERS
                WHERE email='" + email + @"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    //int email_index = reader.GetOrdinal("email");
                    int id_user_Index = reader.GetOrdinal("id_user");
                    //int type_shop_Index = reader.GetOrdinal("type_shop"); 

                    while (reader.Read()) // построчно считываем данные
                    {
                        return reader.IsDBNull(id_user_Index) ? "" : reader.GetString(id_user_Index);
                    }
                }
                reader.Close();
            }


            return "";
        }

        public bool GetUser(string email)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT
                    email, id_user, type_shop 
                FROM USERS
                WHERE email='"+email+@"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    return true;
                    //int email_index = reader.GetOrdinal("email");
                    //int id_user_Index = reader.GetOrdinal("id_user");
                    //int type_shop_Index = reader.GetOrdinal("type_shop"); 

                    while (reader.Read()) // построчно считываем данные
                    {

                        //ITEM Item = new ITEM
                        //{
                        //    ID_ITEM = reader.GetString(itmID_ITEMIndex),                            
                        //};                      

                        
                    }
                }
                reader.Close();                
            }


            return false;
        }

        public void LoginUserGetTypeShop(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT
                    email, id_user, type_shop 
                FROM USERS
                WHERE email='" + email + @"'
                    AND Password ='"+password+@"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int email_index = reader.GetOrdinal("email");
                    int id_user_Index = reader.GetOrdinal("id_user");
                    int type_shop_Index = reader.GetOrdinal("type_shop"); 

                    while (reader.Read()) // построчно считываем данные
                    {
                        Email = reader.GetString(email_index);
                        ID_user = reader.GetString(id_user_Index);
                        type_shop = reader.GetString(type_shop_Index);
                    }
                }
                reader.Close();
            }


            
        }

        public bool RegisterUser(RegisterModel model)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {

                ID_user = Guid.NewGuid().ToString();

                //model.typeShop = "340eb5f2-0ffd-411b-9cf2-318a60b22604";

                Type_shop = TYPE_SHOP.GetName(model.typeShop);
                model.Email = model.Email.Trim();

                string sqlExpression = @"INSERT INTO USERS
                    (
                    ID_user,
                    Type_shop,
                    Email,
                    Password 
                    )
                    VALUES(
                    '"+ ID_user + @"',
                    '"+model.typeShop+@"',
                    '"+model.Email+@"',
                    '"+model.Password+@"'
                    )
                ";

                

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numbInsUser =command.ExecuteNonQuery();
                
                // если успешно вставились данные
                if (numbInsUser > 0)
                {
                    //создание времени работы
                    string id_TimeWork = TimeWork.CreateTimeWork();

                    if (id_TimeWork != "")
                    { 
                        if (Shop.CreateShop(ID_user, Type_shop, id_TimeWork))
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
        }


        





    }   //
}

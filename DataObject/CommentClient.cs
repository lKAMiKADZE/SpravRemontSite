using SpravRemontSite;
using SpravRemontSite.DataObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class CommentClient
    {
        public string ID_comment_client { get; set; }
        public string ID_shop { get; set; }
        public CommentShop CommentShop { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Count_star { get; set; }
        public DateTime Date_add { get; set; }

        public bool Admin_shop { get; set; }



        public static List<CommentClient> GetComments(string email_shop)
        {
            List<CommentClient> commentClients = new List<CommentClient>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                SELECT TOP(100)
                   cc.ID_comment_client
                  ,cc.ID_shop
                  ,cc.Email
                  ,cc.Name
                  ,cc.Comment
                  ,cc.Count_star
                  ,cc.Date_add
                  ,cc.Visible
                  ,cs.Comment AS commentShop
                  ,cs.ID_comment_shop
                  ,sh.ADMIN_SHOP
              FROM SPAVREMONT.Comment_Client cc
              JOIN SPAVREMONT.SHOP sh ON sh.ID_shop= cc.ID_shop
              JOIN users us ON us.ID_user=sh.ID_shop
              LEFT join SPAVREMONT.Comment_Shop cs ON cs.ID_comment_shop=cc.ID_comment_shop
              WHERE us.Email='"+email_shop+ @"'
                    AND cc.Visible=1
                    AND cc.Deleted=0
              ORDER BY cc.Date_add DESC

                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    
                    int ID_comment_client_Index = reader.GetOrdinal("ID_comment_client");
                    int ID_shop_Index = reader.GetOrdinal("ID_shop");
                    int ID_comment_shop_Index = reader.GetOrdinal("ID_comment_shop");
                    int Email_Index = reader.GetOrdinal("Email");
                    int Name_Index = reader.GetOrdinal("Name");
                    int Comment_Index = reader.GetOrdinal("Comment");
                    int Count_star_Index = reader.GetOrdinal("Count_star");
                    int Date_add_Index = reader.GetOrdinal("Date_add");
                    int Visible_Index = reader.GetOrdinal("Visible");
                    int commentShop_Index = reader.GetOrdinal("commentShop");
                    int ADMIN_SHOP_Index = reader.GetOrdinal("ADMIN_SHOP");

                    

                    while (reader.Read()) // построчно считываем данные
                    {
                        CommentShop CS = new CommentShop();

                        if (!reader.IsDBNull(ID_comment_shop_Index))
                        {
                            CS.Comment_shop = reader.GetString(commentShop_Index);
                            CS.ID_comment_shop= reader.GetString(ID_comment_shop_Index);
                        }

                        
                        commentClients.Add(
                            new CommentClient()
                            {
                                Admin_shop= reader.GetBoolean(ADMIN_SHOP_Index),
                                Comment=reader.IsDBNull(Comment_Index) ?"": reader.GetString(Comment_Index),
                                Count_star = reader.IsDBNull(Count_star_Index) ? 0 : reader.GetByte(Count_star_Index),
                                Date_add = reader.GetDateTime(Date_add_Index),
                                Email = reader.IsDBNull(Email_Index) ? "" : reader.GetString(Email_Index),
                                ID_comment_client = reader.IsDBNull(ID_comment_client_Index) ? "" : reader.GetString(ID_comment_client_Index),
                                Name = reader.IsDBNull(Name_Index) ? "" : reader.GetString(Name_Index),
                                CommentShop=CS
                            });
                    }
                }
                reader.Close();
            }


            return commentClients;
        }


        public static bool DeleteVisible(string ID_comment_client)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  UPDATE [SPAVREMONT].[Comment_Client] SET Visible=0,Deleted=1
                    WHERE [ID_comment_client]='" + ID_comment_client + @"'
                   
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

        public static CommentClient GetComment(string ID_comment_client)
        {
            CommentClient CC = new CommentClient();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @" SELECT
                   [ID_comment_client]
                  ,[ID_shop]
                  ,[ID_comment_shop]
                  ,[Email]
                  ,[Name]
                  ,[Comment]
                  ,[Count_star]
             FROM [SPAVREMONT].[Comment_Client]
            WHERE [ID_comment_client]='"+ID_comment_client+@"'
                ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_comment_client_Index = reader.GetOrdinal("ID_comment_client");
                    //int ID_shop_Index = reader.GetOrdinal("ID_shop");
                    //int ID_comment_shop_Index = reader.GetOrdinal("ID_comment_shop");
                    int Email_Index = reader.GetOrdinal("Email");
                    int Name_Index = reader.GetOrdinal("Name");
                    int Comment_Index = reader.GetOrdinal("Comment");
                    int Count_star_Index = reader.GetOrdinal("Count_star");



                    while (reader.Read()) // построчно считываем данные
                    {
                        CC = new CommentClient()
                        {
                            Comment = reader.IsDBNull(Comment_Index) ? "" : reader.GetString(Comment_Index),
                            Count_star = reader.IsDBNull(Count_star_Index) ? 0 : reader.GetByte(Count_star_Index),
                            Email = reader.IsDBNull(Email_Index) ? "" : reader.GetString(Email_Index),
                            ID_comment_client = reader.IsDBNull(ID_comment_client_Index) ? "" : reader.GetString(ID_comment_client_Index),
                            Name = reader.IsDBNull(Name_Index) ? "" : reader.GetString(Name_Index)

                        };
                    }
                }
                reader.Close();
            }


            return CC;
        }

    }
}
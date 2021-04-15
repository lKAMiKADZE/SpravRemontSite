using SpravRemontSite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpravRemontSite.DataObject
{
    public class CommentShop
    {
        public string ID_comment_shop { get; set; }
        public string Comment_shop { get; set; }
        public DateTime Date_add_shop { get; set; }



        public static bool Delete(string ID_comment_shop, string ID_comment_client)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                // т.к. комментарий магазина удаляется, сначала надо его удалить у комментариев клиентов, а после уже удалить из таблицы ответов магазина

                string sqlExpression = @"
                    UPDATE [SPAVREMONT].[Comment_Client] SET ID_comment_shop=null
                    WHERE ID_comment_client='" + ID_comment_client + @"'
                    ";

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numb = command.ExecuteNonQuery();

                if (numb > 0)
                {
                    sqlExpression = @"
                        DELETE [SPAVREMONT].[Comment_Shop]
                          WHERE [ID_comment_shop]='" + ID_comment_shop + @"'                   
                      ";
                    command.CommandText = sqlExpression;
                    int numbCC = command.ExecuteNonQuery();

                    if (numbCC > 0)
                        return true;
                }
            }
            return false;
        }



        public static bool Create(string ID_comment_client_answer, string Comment_shop)
        {
            // помимо создания комментария магазина, еще необходимо его присоединить путем апдейта, к комментарию клиента
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string ID_comment_shop = Guid.NewGuid().ToString();
                string sqlExpression = @"
                   INSERT INTO [SPAVREMONT].[Comment_Shop]    (
                    [ID_comment_shop]
                    ,[Comment]
                    ,[Date_add]
                    ,[Visible]
                    )
                    VALUES
                    (
                     '" + ID_comment_shop + @"' --[ID_comment_shop]
                    ,'"+ Comment_shop + @"' --[Comment]
                    ,CURRENT_TIMESTAMP    --[Date_add]
                    ,1  --[Visible]
                    )
                ";


                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                int numb = command.ExecuteNonQuery();

                // если успешно вставились данные
                if (numb > 0)
                {
                    sqlExpression = @"
                    UPDATE [SPAVREMONT].[Comment_Client] SET ID_comment_shop='" + ID_comment_shop + @"'
                    WHERE ID_comment_client='" + ID_comment_client_answer + @"'
                    ";
                    command.CommandText = sqlExpression;
                    int numbCC = command.ExecuteNonQuery();

                    if (numbCC > 0)
                        return true;
                }
            }


            return false;
        }
    }
}
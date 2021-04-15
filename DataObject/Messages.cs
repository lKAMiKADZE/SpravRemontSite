using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.DataObject
{
    public class Messages
    {
        public string ID_message { get; set; }
        public string ID_from { get; set; }
        public string ID_to { get; set; }
        public string Message { get; set; }
        public byte ID_type_message { get; set; }
        public DateTime Date_send { get; set; }
        public bool new_message { get; set; }



        public  string Create()
        {

            string s="";
            try
            {
                using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
                {
                    ID_message = Guid.NewGuid().ToString();

                    // 20/05/2019 11:13:24
                    string str_date = Date_send.ToString("dd/MM/yyyy HH:mm:ss");
                    string sqlExpression = @"
                  INSERT INTO Messages
                    (
                    ID_message,
                    ID_from,
                    ID_to,
                    Message,
                    ID_type_message,
                    Date_send,
                    new_message
                    
                    )
                    VALUES(
                    '" + ID_message + @"',
                    '" + ID_from + @"',
                    '" + ID_to + @"',
                    '" + Message + @"',
                    " + ID_type_message.ToString() + @",                    
                    convert(datetime, '" + str_date+@"', 104),  
                    " + Convert.ToInt32(new_message) + @"
                    )



                ";
                    s = sqlExpression;

                    //--CURRENT_TIMESTAMP,
                    // CONVERT(DATETIME, '" + Date_send.ToShortDateString() + @"', 13),
                    // 02/01/2004 14:31:21
                    //convert(datetime, '"+Date_send.ToString("DD/MM/YYYY hh:mm:ss") +@"', 104),

                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = sqlExpression;
                    command.Connection = connection;
                    //await command.ExecuteNonQueryAsync();
                     command.ExecuteNonQuery();

                    // если успешно вставились данные
                    //if (numbInsUser > 0)
                    //{
                    //    return true;
                    //}
                }
            }
            catch (Exception e) { s += e.Message; }
            return s;
        }

        public static void UpdateReadMessages(string ID_FROM,string ID_TO)
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                  UPDATE Messages SET new_message=0
                    WHERE ID_From='"+ ID_FROM + @"'
                        AND ID_to='" + ID_TO + @"'
                ";
                               
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                command.ExecuteNonQuery();

                // если успешно вставились данные
                //if (numbInsUser > 0)
                //{
                //    return true;
                //}
            }

            //return false;
        }
        
        public static async Task<List<Messages>> GetClientsAsync(string ID_shop)
        {
            List<Messages> mess = new List<Messages>();

            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"
                    SELECT ID_FROM, MAX(date_send) AS ddate_send,
                        (
                        SELECT new_message FROM messages m2
                        WHERE ID_to='" + ID_shop + @"'
                             AND date_send > getdate() - 1
                             AND new_message=1
                             AND m1.id_from=m2.id_from                           
                        GROUP BY m2.new_message
                        ) AS new_msg
                     FROM messages m1
                    WHERE ID_to='" + ID_shop + @"'
                     AND date_send > getdate() - 1
                    GROUP BY ID_FROM
                    ORDER BY new_msg DESC, ddate_send DESC 
                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {

                    int ID_FROM_Index = reader.GetOrdinal("ID_FROM");
                    int ddate_send_Index = reader.GetOrdinal("ddate_send");
                    int new_msg_Index = reader.GetOrdinal("new_msg");


                    while (reader.Read()) // построчно считываем данные
                    {
                        mess.Add(
                            new Messages()
                            {
                                ID_from = reader.GetString(ID_FROM_Index),
                                Date_send = reader.GetDateTime(ddate_send_Index),
                                new_message = reader.IsDBNull(new_msg_Index)? false : reader.GetBoolean(new_msg_Index)

                            }
                            );
                    }
                }
                reader.Close();
            }


            return mess;

        }

        public static async Task<List<Messages>> GetLastMessagesAsync(string ID_SHOP, string ID_CLIENT)
        {

           // UpdateReadMessages(ID_SHOP);

            List<Messages> messagesList = new List<Messages>();
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT 
                    ID_message,
                    ID_from,
                    ID_to,
                    Message,
                    ID_type_message,
                    Date_send,
                    new_message  
                     FROM messages 
                    WHERE ((ID_from='" + ID_CLIENT + @"' AND ID_TO='" + ID_SHOP + @"') OR
                    (ID_from='" + ID_SHOP + @"' AND ID_TO='" + ID_CLIENT + @"'))
                     AND date_send > DATEADD(hour, -15, getdate()) 

                    ORDER BY date_send ASC -- сортируем от меньшего к большему

                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows) // если есть данные
                {

                    int ID_message_Index = reader.GetOrdinal("ID_message");
                    int ID_FROM_Index = reader.GetOrdinal("ID_FROM");
                    int ID_to_Index = reader.GetOrdinal("ID_to");
                    int Message_Index = reader.GetOrdinal("Message");
                    int ID_type_message_Index = reader.GetOrdinal("ID_type_message");
                    int Date_send_Index = reader.GetOrdinal("Date_send");
                    int new_message_Index = reader.GetOrdinal("new_message");



                    while (reader.Read()) // построчно считываем данные
                    {
                        messagesList.Add(
                            new Messages() {
                                 ID_message= reader.GetString(ID_message_Index),
                                 ID_from = reader.GetString(ID_FROM_Index),
                                 ID_to = reader.GetString(ID_to_Index),
                                 Message = reader.GetString(Message_Index),
                                 ID_type_message = reader.GetByte(ID_type_message_Index),
                                 Date_send = reader.GetDateTime(Date_send_Index),
                                 new_message= reader.GetBoolean(new_message_Index)
                            }
                            );
                    }
                }
                reader.Close();
            }


            return messagesList;
        }

        public static List<Messages> GetLastMessages(string ID_SHOP, string ID_CLIENT)
        {
            UpdateReadMessages( ID_CLIENT, ID_SHOP);

            List<Messages> messagesList = new List<Messages>();
            
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                string sqlExpression = @"SELECT 
                    ID_message,
                    ID_from,
                    ID_to,
                    Message,
                    ID_type_message,
                    Date_send,
                    new_message  
                     FROM messages 
                    WHERE ((ID_from='" + ID_CLIENT + @"' AND ID_TO='" + ID_SHOP + @"') OR
                    (ID_from='" + ID_SHOP + @"' AND ID_TO='" + ID_CLIENT + @"'))
                     AND date_send > DATEADD(hour, -15, getdate()) 

                    ORDER BY date_send ASC -- сортируем от меньшего к большему

                ";



                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlExpression;
                command.Connection = connection;
                SqlDataReader reader =  command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    int ID_message_Index = reader.GetOrdinal("ID_message");
                    int ID_FROM_Index = reader.GetOrdinal("ID_FROM");
                    int ID_to_Index = reader.GetOrdinal("ID_to");
                    int Message_Index = reader.GetOrdinal("Message");
                    int ID_type_message_Index = reader.GetOrdinal("ID_type_message");
                    int Date_send_Index = reader.GetOrdinal("Date_send");
                    int new_message_Index = reader.GetOrdinal("new_message");



                    while (reader.Read()) // построчно считываем данные
                    {
                        messagesList.Add(
                            new Messages()
                            {
                                ID_message = reader.GetString(ID_message_Index),
                                ID_from = reader.GetString(ID_FROM_Index),
                                ID_to = reader.GetString(ID_to_Index),
                                Message = reader.GetString(Message_Index),
                                ID_type_message = reader.GetByte(ID_type_message_Index),
                                Date_send = reader.GetDateTime(Date_send_Index),
                                new_message = reader.GetBoolean(new_message_Index)
                            }
                            );
                    }
                }
                reader.Close();
            }


            return messagesList;
        }
    }
}

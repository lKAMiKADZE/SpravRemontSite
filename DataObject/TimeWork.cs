using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Data.SqlClient;

namespace SpravRemontSite.DataObject
{
    public class TimeWork
    {
        public string ID_timeWork { get; set; }
        public DateTime DateAdd { get; set; }
        public string MondayStart { get; set; }//ToShortTimeString
        public string MondayEnd { get; set; }
        public string TuesdayStart { get; set; }
        public string TuesdayEnd { get; set; }
        public string WednesdayStart { get; set; }
        public string WednesdayEnd { get; set; }
        public string ThursdayStart { get; set; }
        public string ThursdayEnd { get; set; }
        public string FridayStart { get; set; }
        public string FridayEnd { get; set; }
        public string SaturdayStart { get; set; }
        public string SaturdayEnd { get; set; }
        public string SundayStart { get; set; }
        public string SundayEnd { get; set; }

        public static string CreateTimeWork()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {

                string ID_timeWork = Guid.NewGuid().ToString();

                
                string sqlExpression = @"
                INSERT INTO SPAVREMONT.TimeWork
                (
                ID_timeWork ,
                DateAdd ,
                MondayStart,
                MondayEnd ,
                TuesdayStart ,
                TuesdayEnd ,
                WednesdayStart,
                WednesdayEnd ,
                ThursdayStart ,
                ThursdayEnd ,
                FridayStart ,
                FridayEnd ,
                SaturdayStart ,
                SaturdayEnd ,
                SundayStart ,
                SundayEnd
                )
                VALUES(
                '"+ID_timeWork+@"',
                CURRENT_TIMESTAMP,
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00', 
                '00:00'
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
                    return ID_timeWork;
                }
            }


            return "";
        }


        public bool UpdateTimeWork()
        {
            using (SqlConnection connection = new SqlConnection(CONSTANT.connectBD))
            {
                
                string sqlExpression = @"
                UPDATE [SPAVREMONT].[TimeWork] SET
                      [MondayStart]    ='"+MondayStart  +@"' 
                     ,[MondayEnd]      ='"+MondayEnd    +@"'
                     ,[TuesdayStart]   ='"+TuesdayStart +@"'
                     ,[TuesdayEnd]     ='"+TuesdayEnd   +@"'
                     ,[WednesdayStart] ='"+WednesdayStart+@"'
                     ,[WednesdayEnd]   ='"+WednesdayEnd +@"'
                     ,[ThursdayStart]  ='"+ThursdayStart+@"'
                     ,[ThursdayEnd]    ='"+ThursdayEnd +@"'
                     ,[FridayStart]    ='"+FridayStart  +@"'
                     ,[FridayEnd]      ='"+FridayEnd    +@"'
                     ,[SaturdayStart]  ='"+SaturdayStart+@"'
                     ,[SaturdayEnd]    ='"+SaturdayEnd  +@"'
                     ,[SundayStart]    ='"+SundayStart  +@"'
                     ,[SundayEnd]      ='"+ SundayEnd    +@"'
                  
                 WHERE  [ID_timeWork]='" +ID_timeWork+@"'

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



    }
}
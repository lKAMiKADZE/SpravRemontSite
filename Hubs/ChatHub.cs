//using Microsoft.AspNetCore.SignalR;
using SpravRemontSite.DataObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpravRemontSite.Hubs
{
    public class UserChat
    {
        public string ID_User { get; set; }
        public string Connection_id { get; set; }
        
    }

    /*
    public class ChatHub : Hub
    {
        static List<UserChat> UsersChat = new List<UserChat>();

        public async Task Send(string username, string message)
        {
            await this.Clients.All.SendAsync("Receive", username, message);
        }

        public async Task SendTo(string username, string userTo, string message, string typeMSG, string typeSiteClient)
        {
            //typeMSG 0- текст 1 картинка
            //typeSiteClient 0- сайт 1 клиент(андроид)

            bool new_msg = true;
            if (typeSiteClient == "0")
                new_msg = false;

            DateTime dt = DateTime.Now;
            dt = dt.AddHours(3);

            //DateTime _dt = new DateTime(
            //    dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second
            //    );



            List<Messages> allUsers = null;
            UserChat uc = new UserChat() { Connection_id = Context.ConnectionId, ID_User = username };
            Messages mes = new Messages()
            {
                Date_send = dt,
                ID_from = username,
                ID_to = userTo,
                ID_type_message = Convert.ToByte(typeMSG),
                Message = message,
                new_message=new_msg                
            };
            mes.Create();

            //message+= mes.Create();

            string timeHHmm = mes.Date_send.ToString("HH:mm");

            // доп проверка, существует ли пользователь в списке
            if (!UsersChat.Any(x => x.Connection_id == Context.ConnectionId))
            {
                UsersChat.Add(new UserChat { Connection_id = Context.ConnectionId, ID_User = username });                
            }           

            // если отправляемый пользователь существует и пишет магазину, то добавим магазину нового пользователя
            allUsers = await Messages.GetClientsAsync(userTo);

            //Проверка, если получаемый пользователь, онлайн, то посылаем сообщение только ему
            var item_shop = UsersChat.FirstOrDefault(x => x.ID_User == userTo);
            if (item_shop != null)
            {
                // получаем айди сессии из списка, для дальнейшей отправки сообщения
                int ind = UsersChat.IndexOf(item_shop);
                string conect_id = UsersChat[ind].Connection_id;

                await this.Clients.Client(conect_id).SendAsync("ReceiveTo", username, userTo, message, typeMSG, typeSiteClient, timeHHmm);// отправляем сообщение кому оно было адресовано

                if (allUsers != null)
                    await this.Clients.Client(conect_id).SendAsync("Connectlist", userTo, allUsers);// данный метод слушают только магазины

                
            }


            // отправляем сообщение этому же пользователю, который инициировал сообщение
            await this.Clients.Caller.SendAsync("ReceiveTo", username, username, message, typeMSG, typeSiteClient, timeHHmm);


        }

       
        public async Task Connect_shop(string userName)
        {
            var id = Context.ConnectionId;

            if (!UsersChat.Any(x => x.Connection_id == id))
            {
                UsersChat.Add(new UserChat { Connection_id = id, ID_User = userName });

                List<Messages> allUsers = await Messages.GetClientsAsync(userName);// список клиентов, которые общались менее 24 часа назад

                
                //// Посылаем сообщение текущему пользователю
                //Clients.Caller.onConnected(id, userName, UsersChat);
                await this.Clients.Caller.SendAsync("Connectlist", userName, allUsers);// 
                
                //// Посылаем сообщение всем пользователям, кроме текущего
                //Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        

        // Отключение пользователя
        public override Task OnDisconnectedAsync(Exception exception)
        {          
            var item = UsersChat.FirstOrDefault(x => x.Connection_id == Context.ConnectionId);
            if (item != null)
            {
                UsersChat.Remove(item);
                //var id = Context.ConnectionId;
                //Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnectedAsync(exception);
        }

    }

    */
}

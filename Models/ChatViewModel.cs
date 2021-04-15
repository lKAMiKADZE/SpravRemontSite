using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpravRemontSite.DataObject;

namespace SpravRemontSite.Models
{
    public class ChatViewModel
    {
        public string ID_Shop { get; set; }
        public string ID_Client { get; set; }
        public string ID_ClientShort { get; set; }
        public List<Messages> MessagesChat { get; set; }

        public ChatViewModel() { }

        public ChatViewModel(string _ID_Shop, string _ID_Client)
        {
            ID_Shop = _ID_Shop;
            ID_Client = _ID_Client;
            //GetMessages(_ID_Shop, _ID_Client);
            MessagesChat=Messages.GetLastMessages(_ID_Shop, _ID_Client);

            string ID_Client_tmp = _ID_Client.Substring(0, 8);
            ID_ClientShort = "#" + ID_Client_tmp;
        }

        private async void GetMessages(string _ID_Shop, string _ID_Client)
        {
            MessagesChat =await Messages.GetLastMessagesAsync(_ID_Shop, _ID_Client);
        }

    }
}

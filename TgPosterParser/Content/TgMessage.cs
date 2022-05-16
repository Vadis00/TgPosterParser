using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgPosterParser.Content;
using TgPosterParser.DB;
using TL;
using WTelegram;

namespace TgPosterParser.Telegram.WTelegramClient
{
   // class TgMessage : MessageOptions, IMessage
    class TgMessage : DB.Message
    {

       public TgMessage(TL.Message message, WTelegramClient client, Accaunt accaunt)
        {
            this.message = message;

            this.client = client;

            
            MsgId = message.ID;
            Folder = $@"{accaunt.Chats[message.Peer.ID].Folder}\{message.ID}";
            Date =  message.Date.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
            Text = message.message;
            Groupedid = message.grouped_id;
            ChannelId = ChannelIdToDataBaseId(message.Peer.ID);
            ForwardFrom =2340;
            //ForwardFrom = message.fwd_from.from_id.ID;
        }

        readonly WTelegramClient client;
        readonly TL.Message message;
        

        public void Show()
        {
            bool ForwardFrom = false;

            long FromID = 0;
            long? PeerID = message.Peer.ID;
            if (message.fwd_from != null)
                FromID = message.fwd_from.from_id.ID;



            GlobalData.Log.Report($"ID: {Id}" +
                $"Date: {Date}" +
                $"Text: {Text}" +
                $"Groupedid: {Groupedid}" +
                $"PeerID {PeerID}" +
                $"FromID {FromID}");

            // message.fwd_from.from_id
        }
        public async Task DownloadMedia()
        {
            //    var path = GetNewFilePath();
            var path = "";

            switch (message.media)
            {
                case MessageMediaDocument document:
                    await client.DownloadFille(document, path);
                    break;
                case MessageMediaPhoto photo:
                    await client.DownloadFille(photo, path);
                    break;
                case MessageMediaInvoice TLMessage:
                    break;

                case MessageMediaGame TLMessage:

                    break;

                case MessageMediaVenue TLMessage:

                    break;

                case MessageMediaWebPage TLMessage:

                    break;

                case MessageMediaGeo TLMessage:

                    break;

            }


        }
        private int ChannelIdToDataBaseId(long ChannelId)
        {
            TelegaPosterContext DataBase = new();

            foreach (var channel in DataBase.Channels)
            {
                if (channel.ChannelsId == ChannelId)
                    return channel.Id;
            }             
            return -1;

        }
         
        private int GetMsgFileCount()
        {
            var msgfiles = Directory.GetFiles(Folder);

            if (msgfiles != null)
                return msgfiles.Length;

            return 0;
        }
        protected string GetNewFilePath()
        {
            var MsgFileCount = GetMsgFileCount();
            MsgFileCount++;

            var path = $@"{Folder}\{MsgFileCount}";

            return path;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgPosterParser.Content;
using TL;
using WTelegram;

namespace TgPosterParser.Telegram.WTelegramClient
{
    class TgMessage : Content.Message
    {

        public TgMessage(TL.Message message, WTelegramClient client) : base(message.ID, message.Peer.ID, client.Accaunt.Chats[message.Peer.ID].Folder)
        {
            this.message = message;
            this.client = client;

            Date = message.Date;
            Caption = message.message;
            Groupedid = message.grouped_id;

        }

        WTelegramClient client;
        TL.Message message;
        long Groupedid;


        public void Show()
        {
            bool ForwardFrom = false;

            long FromID = 0;
            long? PeerID = message.Peer.ID;
            if (message.fwd_from != null)
                FromID = message.fwd_from.from_id.ID;



            GlobalData.Log.Report($"ID: {ID}" +
                $"Date: {Date}" +
                $"Text: {Caption}" +
                $"Groupedid: {Groupedid}" +
                $"PeerID {PeerID}" +
                $"FromID {FromID}");

            // message.fwd_from.from_id
        }
        public async Task Save()
        {
            var path = GetNewFilePath();

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
    }
}

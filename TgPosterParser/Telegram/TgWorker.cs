using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgPosterParser.Telegram.WTelegramClient;
using WTelegram;
using TL;
using System.Windows;
using TgPosterParser.DB;
using Microsoft.EntityFrameworkCore;

namespace TgPosterParser.Telegram
{
    public class TgWorker
    {

        public TgWorker(string phone)
        {

        }
 
        public TgWorker(DB.Accaunt accaunt)
        {
            Phone = accaunt.Phone;
          //  accaunt.TgWorker = this;
            this.Accaunt = accaunt;
          
        }

        public string Phone { get; set; }
        public int MessagesReceivedCount;
        public int MessagesSavedCount;

        public DB.Accaunt Accaunt;

        public WTelegramClient.WTelegramClient Client;

        public delegate void AccountHandler();
        static public event AccountHandler? Notify;

        bool isLogIn = false;

        public async Task LogIn()
        {
            Client = await WTelegramClient.WTelegramClient.WTelegramClientAsync(this);
            isLogIn = true;
        }
        public async Task ListenUpdate()
        {
            if (isLogIn)
                await Client.ListenUpdate();
        }
        public async Task NewMessage(TL.Message msg)
        {
            MessagesReceivedCount++;
            Notify?.Invoke();            

            if (Accaunt.Chats != null)
            {
                GlobalData.Log.Report(Accaunt.Chats[msg.Peer.ID].GetInfo());
            }
            await new TgMessage(msg, Client, Accaunt).Save();

            new TgMessage(msg, Client, Accaunt).Show();
        }
        public void UpdateChannelsList()
        {
            //    var needSubscribes = accaunt.Channels; // Список каналов за которыми ДОЛЖЕН следить аккаунт

            //var alreadySubscribes = (IEnumerable<Channel>)Chats; // Список каналов за которыми УЖЕ следит аккаунт

            // IEnumerable<Channels> differenceQuery = needSubscribes.Except(alreadySubscribes);

            // foreach (Channels s in differenceQuery)
            //      MessageBox.Show(s.UserName);


            // с ДБ получаем список каналов за которыми должен следить аккаунт
            // получаем список каналов за которыми уже следит аккаунт
            // Подписываемся или отписываемся на каналы

        }
        public void ShowAllChats()
        {
            foreach (var chat in Accaunt.Chats.Values)
            {
                GlobalData.Log.Report($"{chat.ChannelsId} {chat.UserName}");
            }
        }

        public static ObservableCollection<TgWorker> GetTgWorkers(DbSet<Accaunt> Accaunts)
        {
            ObservableCollection<TgWorker> tgWorkers = new();

            foreach (var accaunt in Accaunts)
            {
                tgWorkers.Add(new TgWorker(accaunt));
            }
            return tgWorkers;
        }

    }
}

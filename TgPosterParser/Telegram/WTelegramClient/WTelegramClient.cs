using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WTelegram;
using System.Diagnostics;
using TL;
using System.IO;


namespace TgPosterParser.Telegram.WTelegramClient
{
   public class WTelegramClient  
	{

		private TgWorker worker;
		ListenUpdates updates;
		Client Client;
	    User My;
		 
		public delegate string AccountHandler(string message);
		public event AccountHandler Notify;
		private WTelegramClient(TgWorker worker)
		{
			this.worker = worker;

			Client = new Client(what => Config(what, "session"));
		}

		//Асинхронный вариант конструктора WTelegramClient
		public static async Task<WTelegramClient> WTelegramClientAsync(TgWorker worker)
		{
			WTelegramClient Client = new WTelegramClient(worker);
			await Client.isAuthorization();

			return Client;
		}

		public async Task isAuthorization()
		{
			My = await Client.LoginUserIfNeeded();
		}
		private string Config(string what, string session_pathname)
		{

			if (what == "api_id") return worker.Accaunt.ClientId;
			if (what == "api_hash") return worker.Accaunt.ClientHash;
			if (what == "phone_number") return worker.Accaunt.Phone;
			if (what == "server_address") return "149.154.167.50:443";
			if (what == "verification_code") return Notify?.Invoke("verification_code");
			if (what == "session_pathname") return worker.Accaunt.Phone;

			return null;
		}
		// Слушаем обновлеия всех чатов на которые подписан юзер
		public async Task ListenUpdate()
		{
			updates = new(Client, worker, My);

			await updates.Start();
		}

		//Получаем список всех каналов на которые подписан аккаунт
		public async Task GetAllDialogs()
		{			 
			var chats = await Client.Messages_GetAllChats();

			foreach ((long id, ChatBase chat) in chats.chats)
			{
				worker.Accaunt.Chats = new();

				worker.Accaunt.Chats.Add(id, new DB.Channel(chat));
				//MainWindow.log.Report(new Channels(chat).GetInfo());
			}

			 
		}
		public async Task ChannelSubscription()
		{ 
		}
		public async Task ChannelUnsubscribe()
		{ 
		}

		public async Task DownloadFille(MessageMediaPhoto media, string path)
		{
			var img = (Photo)media.photo;

			using (FileStream fs = File.Create($@"{path}.jpg"))
			{
				var buffer = await Client.DownloadFileAsync(img, fs);

			}
		}
		public async Task DownloadFille(MessageMediaDocument media, string path)
		{ 
			var document = (Document)media.document;

			using (FileStream fs = File.Create($@"{path}.mp4"))
			{
				var buffer = await Client.DownloadFileAsync(document, fs);

			}
		}
	}
}

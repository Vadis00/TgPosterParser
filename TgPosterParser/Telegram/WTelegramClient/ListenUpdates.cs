using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TgPosterParser.DB;
using TL;
using WTelegram;

namespace TgPosterParser.Telegram.WTelegramClient
{
	class ListenUpdates 
	{

		public ListenUpdates(Client client, Accaunt accaunt, User user)
		{
			this.Client = client;
			this.Accaunt = accaunt;
			My = user;
			 
		}

		private Client Client;
		private User My;
		private Accaunt Accaunt;



		static readonly Dictionary<long, User> Users = new();
		static readonly Dictionary<long, ChatBase> Chats = new();

		public async Task Start()
		{
			Client.Update += Client_Update;

			Users[My.id] = My;

			var dialogs = await Client.Messages_GetAllDialogs(); // dialogs = groups/channels/users

			dialogs.CollectUsersChats(Users, Chats);
		}

		private void Client_Update(IObject arg)
		{
			if (arg is not UpdatesBase updates) return;
			updates.CollectUsersChats(Users, Chats);
			foreach (var update in updates.UpdateList)
				switch (update)
				{
					case UpdateNewMessage unm: NewMessage(unm.message); break;
					//case UpdateEditMessage uem: DisplayMessage(uem.message, true); break;
					case UpdateDeleteChannelMessages udcm: Console.WriteLine($"{udcm.messages.Length} message(s) deleted in {Chat(udcm.channel_id)}"); break;
					case UpdateDeleteMessages udm: Console.WriteLine($"{udm.messages.Length} message(s) deleted"); break;
					case UpdateUserTyping uut: Console.WriteLine($"{User(uut.user_id)} is {uut.action}"); break;
					case UpdateChatUserTyping ucut: Console.WriteLine($"{Peer(ucut.from_id)} is {ucut.action} in {Chat(ucut.chat_id)}"); break;
					case UpdateChannelUserTyping ucut2: Console.WriteLine($"{Peer(ucut2.from_id)} is {ucut2.action} in {Chat(ucut2.channel_id)}"); break;
					case UpdateChatParticipants { participants: ChatParticipants cp }: Console.WriteLine($"{cp.participants.Length} participants in {Chat(cp.chat_id)}"); break;
					case UpdateUserStatus uus: Console.WriteLine($"{User(uus.user_id)} is now {uus.status.GetType().Name[10..]}"); break;
					case UpdateUserName uun: Console.WriteLine($"{User(uun.user_id)} has changed profile name: @{uun.username} {uun.first_name} {uun.last_name}"); break;
					case UpdateUserPhoto uup: Console.WriteLine($"{User(uup.user_id)} has changed profile photo"); break;
					default: Console.WriteLine(update.GetType().Name); break; // there are much more update types than the above cases
				}
		}

		private async Task NewMessage(MessageBase messageBase)
		{
			switch (messageBase)
			{
				case TL.Message m: await Accaunt.TgWorker.NewMessage(m); break;
					//	case MessageService ms: MessageBox.Show($"{Peer(ms.from_id)} in {Peer(ms.peer_id)} [{ms.action.GetType().Name[13..]}]"); break;
			}
		}

		private static string User(long id) => Users.TryGetValue(id, out var user) ? user.ToString() : $"User {id}";
		private static string Chat(long id) => Chats.TryGetValue(id, out var chat) ? chat.ToString() : $"Chat {id}";
		private static string Peer(Peer peer) => peer is null ? null : peer is PeerUser user ? User(user.user_id)
			: peer is PeerChat or PeerChannel ? Chat(peer.ID) : $"Peer {peer.ID}";

	}
}

using System;
using System.Collections.Generic;
using System.IO;
using TL;

#nullable disable

namespace TgPosterParser.DB
{
    public partial class Channel
    {
        public Channel()
        {
            Messages = new HashSet<Message>();
        }

        public Channel(ChatBase chat)
        {
            Chat = chat;
            Title = chat.Title;
            ChannelsId = chat.ID;

            switch (chat) // example of downcasting to their real classes:
            {
                case TL.Chat smallgroup when smallgroup.IsActive:
                    UserName = "NoName";
                    break;
                case TL.Channel group when group.IsGroup:
                    UserName = group.username;
                    break;
                case TL.Channel channel:
                    UserName = channel.username;
                    break;
            }

            Folder = $@"{GlobalData.ContentFolder}\{ChannelsId}";
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public float ChannelsId { get; set; }
        public string Title { get; set; }

        private string folder;
        public string Folder
        {
            get { return folder; }
            set
            {
                if (!Directory.Exists(value))
                    Directory.CreateDirectory(value);

                folder = value;
            }
        }
        public ChatBase Chat { get; }
        public virtual Accaunt Account { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public string GetInfo()
        {
            return $"Title: {Title} Username: {UserName} ID: {ChannelsId}";
        }
    }
}

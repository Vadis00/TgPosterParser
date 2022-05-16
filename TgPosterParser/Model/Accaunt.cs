using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using TgPosterParser.Telegram;
using TgPosterParser.Telegram.WTelegramClient;

#nullable disable

namespace TgPosterParser.DB
{
    public partial class Accaunt
    {
        public Accaunt()
        {
            Channels = new HashSet<Channel>();
            Path = GlobalData.TgAccountsFolder + Phone;
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string ClientId { get; set; }
        public string ClientHash { get; set; }
        public string Path { get; }

        [NotMapped]
        public string AuthCode { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }

        [NotMapped]
        public Dictionary<long, Channel> Chats { get; set; }

         
    }
}

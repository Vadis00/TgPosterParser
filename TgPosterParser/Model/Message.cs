using System;
using System.Collections.Generic;
using System.IO;
using TgPosterParser.Content;

#nullable disable

namespace TgPosterParser.DB
{
    public partial class Message : IMessage
    {
        public int Id { get; set; }
        public int MsgId { get; set; }
        public int ChannelId { get; set; }
        public string Text { get; set; }

        private string messageFolder;
        public string Folder
        {
            get { return messageFolder; }
            set
            {
                if (!Directory.Exists(value))
                    Directory.CreateDirectory(value);

                messageFolder = value;
            }
        }
        public long? Groupedid { get; set; }
        public long? ForwardFrom { get; set; }
        public string Date { get; set; }
        public bool isMedia { get; set; }
        public virtual Channel Channel { get; set; }
        
    }
}

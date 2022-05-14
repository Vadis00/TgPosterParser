using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgPosterParser.Content
{
    class Message : IMessage
    {
        public Message(long id, long source, string path)
        {
            ID = id;
            Source = source;
            ProjectFolder = path;
            MessageFolder = $@"{ProjectFolder}\{ID}";
        }        
        public long ID { get; protected set; }
        public long Source { get; }
        public string ProjectFolder { get; set; }
        private string messageFolder;
        public string MessageFolder
        {
            get { return messageFolder; }
            private set
            {
                if (!Directory.Exists(value))
                    Directory.CreateDirectory(value);

                messageFolder = value;
            }
        }
        public string Name => throw new NotImplementedException();
        public DateTime Date { get; protected set; }
        public string Caption { get; protected set; }
        public Content Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private int GetMsgFileCount()
        {
            var msgfiles = Directory.GetFiles(MessageFolder);

            if (msgfiles != null)
                return msgfiles.Length;

            return 0;
        }
        protected string GetNewFilePath()
        {
            var MsgFileCount = GetMsgFileCount();
            MsgFileCount++;

            var path = $@"{MessageFolder}\{MsgFileCount}";

            return path;
        }
    }
}

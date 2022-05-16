using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgPosterParser.Content
{
    class MessageOptions 
    {
        public MessageOptions(long id, long source, string path)
        {
            Source = source;
            ProjectFolder = path;
            Folder = $@"{ProjectFolder}\{id}";
            System.Windows.MessageBox.Show(Folder);
        }        
        public long Source { get; }
        public string ProjectFolder { get; set; }

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
        public Content Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgPosterParser.Content
{
    interface IMessage
    {
        int MsgId { get; set; }
        int ChannelId { get; set; }
        string Text { get; set; }
        string Folder { get; set; }
        long? Groupedid { get; set; }
        long? ForwardFrom { get; set; }
        string Date { get; set; }
        bool isMedia { get; set; }

    //    public string ProjectFolder { get;   }
    //    public string MessageFolder { get;   }
    //    Content Content { get; set; }

    }
}

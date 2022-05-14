using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgPosterParser.Content
{
    interface IMessage
    {
        long ID { get; }
        long Source { get; }
        string Name { get; }
        DateTime Date { get; }
        string Caption { get; }
        public string ProjectFolder { get;   }
        public string MessageFolder { get;   }
        Content Content { get; set; }

    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace TgPosterParser.DB
{
    public partial class Message
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string Text { get; set; }
        public float? Groupedid { get; set; }
        public float? ForwardFrom { get; set; }

        public virtual Channel Channel { get; set; }
    }
}

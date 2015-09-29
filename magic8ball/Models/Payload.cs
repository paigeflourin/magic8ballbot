using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace magic8ball.Models
{
    public class Payload
    {
        [JsonProperty("attachments")]
        public Attachments Attachment { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon_emoji")]
        public string Emoji { get; set; }

        [JsonProperty("icon_url")]
        public string Icon { get; set; }


    }
}
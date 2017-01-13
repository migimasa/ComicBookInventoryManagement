using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ComicBookInventoryAPI.Models.Comics
{
    public class ComicBookIssueDTO
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "issueNumber")]
        public int IssueNumber { get; set; }
        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }
        [JsonProperty(PropertyName = "inker")]
        public string Inker { get; set; }
        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }
        [JsonProperty(PropertyName = "review")]
        public float Review { get; set; }
    }
}
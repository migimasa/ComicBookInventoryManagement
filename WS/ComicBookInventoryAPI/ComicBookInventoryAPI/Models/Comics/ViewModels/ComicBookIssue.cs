using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ComicBookInventoryAPI.Models.Comics.ViewModels
{
    public class ComicBookIssue
    {
        [JsonProperty(PropertyName = "id")]
        public int ComicBookIssueId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string SeriesTitle { get; set; }
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
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
    }
}
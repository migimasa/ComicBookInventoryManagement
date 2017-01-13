using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ComicBookInventoryAPI.Models.Comics
{
    /// <summary>
    /// Comic Book Issue
    /// </summary>
    public class ComicBookIssueViewModel
    {
        /// <summary>
        /// Comic Book Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int ComicBookIssueId { get; set; }

        /// <summary>
        /// Title of the comic book Series
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string SeriesTitle { get; set; }

        /// <summary>
        /// Issue Number
        /// </summary>
        [JsonProperty(PropertyName = "issueNumber")]
        public int IssueNumber { get; set; }

        /// <summary>
        /// Writer
        /// </summary>
        [JsonProperty(PropertyName = "writer")]
        public string Writer { get; set; }

        /// <summary>
        /// Inker
        /// </summary>
        [JsonProperty(PropertyName = "inker")]
        public string Inker { get; set; }

        /// <summary>
        /// Publisher
        /// </summary>
        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// Review
        /// </summary>
        [JsonProperty(PropertyName = "review")]
        public float Review { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }
    }
}
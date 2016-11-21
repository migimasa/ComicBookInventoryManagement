using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookInventory.Data.Models
{
    public class ComicBookIssue
    {
        public int ComicBookIssueId { get; set; }
        public string SeriesTitle { get; set; }
        public int IssueNumber { get; set; }
        public string Writer { get; set; }
        public string Inker { get; set; }
        public string Publisher { get; set; }
        public float Review { get; set; }
        public Guid UserId { get; set; }
    }
}

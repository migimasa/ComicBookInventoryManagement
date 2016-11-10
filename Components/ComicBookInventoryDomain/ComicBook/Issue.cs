using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookInventory.Data.Models;

namespace ComicBookInventory.Domain.ComicBook
{
    public class Issue
    {
        public Guid ComicBookIssueId { get; set; }
        public string SeriesTitle { get; set; }
        public int IssueNumber { get; set; }
        public string Writer { get; set; }
        public string Inker { get; set; }
        public string Publisher { get; set; }
        public float Review { get; set; }
        public Guid UserId { get; set; }

        public Issue()
        {
            
        }
    }
}

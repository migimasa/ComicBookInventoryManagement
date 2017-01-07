using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookInventory.Domain.ComicBook;
using Migi.Framework.Models;

namespace ComicBookInventory.Domain.Abstract
{
    public interface IComicBook
    {
        List<Issue> GetComicBookIssuesForUser(Guid userId);

        Issue GetComicBookIssue(int id, Guid userId);

        ChangeResult SaveComicBook(Issue issueToSave);

        ChangeResult DeleteComicBook(int comicBookIssueId, Guid userId);
    }
}

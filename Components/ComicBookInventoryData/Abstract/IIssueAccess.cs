using ComicBookInventory.Data.Models;
using System.Collections.Generic;
using System;

namespace ComicBookInventory.Data.Abstract
{
    public interface IIssueAccess
    {
        List<ComicBookIssue> GetComicBookIssuesForUser(Guid userId);

        bool SaveComicBookIssue(ComicBookIssue issue);

        ComicBookIssue GetComicBookIssue(int issueId);

        int RemoveComicBookIssue(Guid comicBookIssueId);
    }
}

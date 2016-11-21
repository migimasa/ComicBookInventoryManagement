using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookInventory.Domain.ComicBook;

namespace ComicBookInventory.Domain.Abstract
{
    public interface IComicBook
    {
        List<Issue> GetComicBookIssuesForUser(Guid userId);
    }
}

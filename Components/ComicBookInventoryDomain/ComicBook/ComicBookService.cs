using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookInventory.Data.Abstract;
using ComicBookInventory.Data.Models;
using AutoMapper;
using ComicBookInventory.Domain.Abstract;
using Migi.Framework.Models;


namespace ComicBookInventory.Domain.ComicBook
{
    public class ComicBookService : IComicBook
    {
        private IIssueAccess _issueAccess;
        private MapperConfiguration mapConfig;

        public ComicBookService(IIssueAccess access)
        {
            _issueAccess = access;

            InitializeMapConfig();
        }

        private void InitializeMapConfig()
        {
            mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComicBookIssue, Issue>();
                cfg.CreateMap<Issue, ComicBookIssue>();

            });
        }

        public List<Issue> GetComicBookIssuesForUser(Guid userId)
        {
            IMapper mapper = mapConfig.CreateMapper();

            return mapper.Map<List<ComicBookIssue>, List<Issue>>(_issueAccess.GetComicBookIssuesForUser(userId)).ToList();
        }

        public Issue GetComicBookIssue(int issueId, Guid userId)
        {
            IMapper mapper = mapConfig.CreateMapper();

            return mapper.Map<ComicBookIssue, Issue>(_issueAccess.GetComicBookIssue(issueId, userId));
        }

        public ChangeResult SaveComicBook(Issue issueToSave)
        {
            ChangeResult result = new ChangeResult();

            IMapper mapper = mapConfig.CreateMapper();
            bool isSaved = _issueAccess.SaveComicBookIssue(mapper.Map<Issue, ComicBookIssue>(issueToSave));

            if (!isSaved)
                result.AddErrorMessage("Could not save comic book.");

            return result;
        }

        public ChangeResult DeleteComicBook(int comicBookIssueId, Guid userId)
        {
            ChangeResult result = new ChangeResult();

            bool isDeleted = _issueAccess.RemoveComicBookIssue(comicBookIssueId, userId);

            if (!isDeleted)
                result.AddErrorMessage("Could not delete comic book.");

            return result;
        }
    }
}

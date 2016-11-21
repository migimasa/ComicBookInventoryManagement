using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookInventory.Data.Abstract;
using ComicBookInventory.Data.Models;
using AutoMapper;
using ComicBookInventory.Domain.Abstract;


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
            });
        }

        public List<Issue> GetComicBookIssuesForUser(Guid userId)
        {
            IMapper mapper = mapConfig.CreateMapper();

            return mapper.Map<List<ComicBookIssue>, List<Issue>>(_issueAccess.GetComicBookIssuesForUser(userId)).ToList();
        }
    }
}

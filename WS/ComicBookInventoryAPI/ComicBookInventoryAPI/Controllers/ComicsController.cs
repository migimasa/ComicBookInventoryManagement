using ComicBookInventory.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ComicBookInventoryAPI.Models.Comics.ViewModels;
using ComicBookInventory.Domain.ComicBook;

namespace ComicBookInventoryAPI.Controllers
{
    public class ComicsController : ApiController
    {
        private IComicBook comicBookAccess;
        private MapperConfiguration mapConfig;

        private Guid? UserId { get { return GetUserIdFromHeader(); } }


        public ComicsController(IComicBook comicBookAccess)
        {
            this.comicBookAccess = comicBookAccess;

            InitializeMapConfig();
        }

        [NonAction]
        private void InitializeMapConfig()
        {
            mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Issue, ComicBookIssue>();
                cfg.CreateMap<Models.Comics.DTO.ComicBookIssue, Issue>()
                   .ForMember(dest => dest.SeriesTitle,
                              opts => opts.MapFrom(src => src.Title));
            });
        }

        [NonAction]
        private Guid? GetUserIdFromHeader()
        {
            string userIdHeaderValue = Request.Headers.GetValues("user-id").FirstOrDefault();

            return Migi.Framework.Helper.Types.GetNullableGuid(userIdHeaderValue);
        }

        // GET: api/Comics
        public IHttpActionResult Get()
        {
            IMapper mapper = mapConfig.CreateMapper();

            if (UserId.HasValue)
                return Ok(mapper.Map<List<Issue>, List<ComicBookIssue>>(comicBookAccess.GetComicBookIssuesForUser(UserId.Value)));
            else
                return BadRequest("user-id missing from header");
        }

        // GET: api/Comics/5
        public IHttpActionResult Get(int id)
        {
            IMapper mapper = mapConfig.CreateMapper();

            Issue comicBook = comicBookAccess.GetComicBookIssue(id);

            if (comicBook != null)
                return Ok(mapper.Map<Issue, ComicBookIssue>(comicBook));
            return NotFound();
        }

        // POST: api/Comics
        public IHttpActionResult Post([FromBody]Models.Comics.DTO.ComicBookIssue comicBookIssue)
        {
            if (UserId.HasValue)
            {
                IMapper mapper = mapConfig.CreateMapper();
                Issue issueToSave = mapper.Map<Models.Comics.DTO.ComicBookIssue, Issue>(comicBookIssue);
                issueToSave.UserId = UserId.Value;
                var saveResult = comicBookAccess.SaveComicBook(issueToSave);

                if (saveResult.IsSuccess)
                    return Ok();
                else
                    return InternalServerError();
            }

            return BadRequest("user-id missing from header");
        }

        // PUT: api/Comics/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Comics/5
        public void Delete(int id)
        {
        }
    }
}

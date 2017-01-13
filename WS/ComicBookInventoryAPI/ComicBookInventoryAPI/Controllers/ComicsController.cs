using ComicBookInventory.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ComicBookInventoryAPI.Models.Comics;
using ComicBookInventory.Domain.ComicBook;
using Migi.Framework.Models;
using System.Web.Http.Description;

namespace ComicBookInventoryAPI.Controllers
{
    /// <summary>
    /// Actions for managing comic books
    /// </summary>
    public class ComicsController : ApiController
    {
        private IComicBook comicBookService;
        private MapperConfiguration mapConfig;

        private Guid? UserId { get { return GetUserIdFromHeader(); } }

        /// <summary>
        /// Initialize a new comic book controller instance
        /// </summary>
        /// <param name="comicBookService">A service for handling comic book actions</param>
        public ComicsController(IComicBook comicBookService)
        {
            this.comicBookService = comicBookService;

            InitializeMapConfig();
        }

        [NonAction]
        private void InitializeMapConfig()
        {
            mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Issue, ComicBookIssueViewModel>();
                cfg.CreateMap<ComicBookIssueDTO, Issue>()
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
        /// <summary>
        /// Get the comic books for the user.
        /// </summary>
        /// <remarks>Get all comic books for user</remarks>
        /// <returns>A list of comic books for user id supplied on header</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [ResponseType(typeof(IEnumerable<ComicBookIssueViewModel>))]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                IMapper mapper = mapConfig.CreateMapper();

                if (UserId.HasValue)
                    return Ok(mapper.Map<List<Issue>, List<ComicBookIssueViewModel>>(comicBookService.GetComicBookIssuesForUser(UserId.Value)));
                else
                    return BadRequest("user-id missing from header");
            }
            catch (Exception ex)
            {
                //TODO: Add Logging
                return InternalServerError(ex);
            }
        }

        // GET: api/Comics/5
        /// <summary>
        /// Get a single comic book.
        /// </summary>
        /// <param name="id">The comic book id of the desired comic book</param>
        /// <remarks>Get the comic book for the supplied id</remarks>
        /// <returns>A comic book for the id</returns>
        /// <response code="200">Everything is normal</response>
        /// <response code="400">Missing user-id</response>
        /// <response code="404">No comic book found</response>
        /// <response code="500">An exception occured</response>
        [ResponseType(typeof(ComicBookIssueViewModel))]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (UserId.HasValue)
                {
                    IMapper mapper = mapConfig.CreateMapper();

                    Issue comicBook = comicBookService.GetComicBookIssue(id, UserId.Value);

                    if (comicBook != null)
                        return Ok(mapper.Map<Issue, ComicBookIssueViewModel>(comicBook));
                    return NotFound();
                }
                return BadRequest("user-id missing from header");
            }
            catch (Exception ex)
            {
                //TODO: Add Logging
                return InternalServerError(ex);
            }
        }

        // POST: api/Comics
        /// <summary>
        /// Add a new comic book
        /// </summary>
        /// <remarks>Saves a new comic book</remarks>
        /// <param name="comicBookIssue">The comic book to add</param>
        /// <returns>Http Result</returns>
        /// <response code="200">The issue has been saved.</response>
        /// <response code="400">Missing user-id</response>
        /// <response code="409">Validation of new comic book failed</response>
        /// <response code="500">An exception occured.</response>
        [HttpPost]
        public IHttpActionResult Post([FromBody]ComicBookIssueDTO comicBookIssue)
        {
            try
            {
                if (UserId.HasValue)
                {
                    IMapper mapper = mapConfig.CreateMapper();
                    Issue issueToSave = mapper.Map<ComicBookIssueDTO, Issue>(comicBookIssue);
                    issueToSave.UserId = UserId.Value;

                    return GetHttpResponseForChangeResult(comicBookService.SaveComicBook(issueToSave));
                }
                return BadRequest("user-id missing from header");
            }
            catch (Exception ex)
            {
                //TODO: Add Logging
                return InternalServerError(ex);
            }
        }

        // PUT: api/Comics/5
        /// <summary>
        /// Update an existing comic book
        /// </summary>
        /// <remarks>Save changes to an existing comic book</remarks>
        /// <param name="id">The id of the requested comic book</param>
        /// <param name="comicBookIssue">The comic book to update</param>
        /// <returns>Http Response</returns>
        /// <response code="200">The comic book has been updated</response>
        /// <response code="400">Missing user-id</response>
        /// <response code="409">Validation failed for comic book to save</response>
        /// <response code="500">An exception has occured</response>
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ComicBookIssueDTO comicBookIssue)
        {
            try
            {
                if (UserId.HasValue)
                {
                    IMapper mapper = mapConfig.CreateMapper();
                    Issue issueToSave = mapper.Map<ComicBookIssueDTO, Issue>(comicBookIssue);
                    issueToSave.UserId = UserId.Value;
                    issueToSave.ComicBookIssueId = id;

                    return GetHttpResponseForChangeResult(comicBookService.SaveComicBook(issueToSave));
                }
                return BadRequest("user-id missing from header");
            }
            catch (Exception ex)
            {
                //TODO: Add Logging
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Comics/5
        /// <summary>
        /// Delete comic book
        /// </summary>
        /// <remarks>Delete the desired comic book</remarks>
        /// <param name="id">The id of the comic book to delete</param>
        /// <returns>Http Response</returns>
        /// <response code="200">The comic book has been deleted</response>
        /// <response code="400">Missing user-id</response>
        /// <response code="409">Business Logic failure</response>
        /// <response code="500">An exception has occured</response>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (UserId.HasValue)
                    return GetHttpResponseForChangeResult(comicBookService.DeleteComicBook(id, UserId.Value));

                return BadRequest("user-id missing from header");
            }
            catch (Exception ex)
            {
                //TODO: Add Logging
                return InternalServerError(ex);
            }
        }

        private IHttpActionResult GetHttpResponseForChangeResult(ChangeResult result)
        {
            if (result.IsSuccess)
                return Ok();
            else
            {
                return Conflict();
            }
        }
    }
}

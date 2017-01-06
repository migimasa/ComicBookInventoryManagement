using ComicBookInventory.Domain.ComicBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ComicBookInventory.Domain.Abstract;

namespace ComicBookInventoryAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private IComicBook comicBookAccess;
        public ValuesController(IComicBook comicBookAccess)
        {
            this.comicBookAccess = comicBookAccess;
        }

        // GET api/values
        public IHttpActionResult Get()
        {
            var comics = comicBookAccess.GetComicBookIssuesForUser(new Guid("CACE14BC-AB05-4081-964C-0D75C8C3E8FD"));

            return Ok(comics);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

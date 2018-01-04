using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hw_service_try2.Controllers
{
    public class GroupsController : ApiController
    {
        private IGroupBusinessLayer bll;

        public GroupsController(IGroupBusinessLayer groupBusinessLayer)
        {
            bll = groupBusinessLayer;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var g = bll.Get(id);
            if (g == null) return NotFound();
            else return Ok(g);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var l = bll.GetAll();
            if (l == null) return NotFound();
            else return Ok(l);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] Group g)
        {
            bll.Add(g);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult UpdateName(int id, string newName)
        {
            bll.UpdateName(id, newName);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bll.Delete(id);
            return Ok();
        }
    }
}

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
        [ActionName("all")]
        public IHttpActionResult GetAll()
        {
            var l = bll.GetAll();
            if (l == null) return NotFound();
            else return Ok(l);
        }

        [HttpGet]
        [ActionName("list")]
        public IHttpActionResult List()
        {
            var l = bll.List();
            if (l != null) return Ok(l);
            else return NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post(string name)
        {
            try
            {
                var g = bll.Add(name);
                if (g != null) return Created($"api/groups/{g.ID}",g);
                else return NotFound();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Group group)
        {
            try
            {
                if (bll.Update(id, group)) return Ok();
                else return NotFound();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (bll.Delete(id)) return Ok();
            else return NotFound();
        }
    }
}

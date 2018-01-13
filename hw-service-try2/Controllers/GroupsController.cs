using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

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
        [ActionName("get")]
        [SwaggerResponse(HttpStatusCode.OK,"Group by Id.",typeof(Group))]
        [SwaggerResponse(HttpStatusCode.NotFound,"Group does not exist or DB is not accessible.")]
        public IHttpActionResult Get(int id)
        {
            var g = bll.Get(id);
            if (g == null) return NotFound();
            else return Ok(g);
        }

        [HttpGet]
        [ActionName("all")]
        [SwaggerResponse(HttpStatusCode.OK,"All groups.",typeof(List<Group>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,"DB is not accessible.")]
        public IHttpActionResult GetAll()
        {
            var l = bll.GetAll();
            if (l == null) return InternalServerError();
            else return Ok(l);
        }

        [HttpGet]
        [ActionName("list")]
        [SwaggerResponse(HttpStatusCode.OK,"List of group ids.", typeof(List<int>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "DB is not accessible.")]
        public IHttpActionResult List()
        {
            var l = bll.List();
            if (l != null) return Ok(l);
            else return InternalServerError();
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created,"Create group.",typeof(Group))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,"DB is not accessible.")]
        [SwaggerResponse(HttpStatusCode.BadRequest,"Invalid name for a group.")]
        public IHttpActionResult Post(string name)
        {
            try
            {
                var g = bll.Add(name);
                if (g != null) return Created($"api/groups/{g.ID}",g);
                else return InternalServerError();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, "Update group.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Group does not exist or DB is not accessible.")]
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
        [SwaggerResponse(HttpStatusCode.OK, "Delete group.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Group does not exist or DB is not accessible.")]
        public IHttpActionResult Delete(int id)
        {
            if (bll.Delete(id)) return Ok();
            else return NotFound();
        }
    }
}

using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace hw_service_try2.Controllers
{
    public class CardsController : ApiController
    {
        private ICardBusinessLayer bll;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public CardsController(ICardBusinessLayer bll)
        {
            this.bll = bll;
        }

        [HttpGet]
        [ActionName("get")]
        [SwaggerResponse(HttpStatusCode.OK, "Returns Card with requsted ID.",typeof(Card))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Requested ID was not found or DB is not accessible.")]
        public IHttpActionResult Get(int id)
        {
            var card = bll.Get(id);
            if (card != null) return Ok(card);
            else return NotFound();
        }

        [HttpGet]
        [ActionName("group")]
        [SwaggerResponse(HttpStatusCode.OK, "Returns all cards from requested group. Empty set if group doesn't exist or doesn't have cards in it.", typeof(List<Card>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "DB is not accessible.")]
        public IHttpActionResult GetGroup(int id)
        {
            var group = bll.GetGroup(id);

            if (group != null) return Ok(group);
            else return InternalServerError();
        }

        [HttpGet]
        [ActionName("all")]
        [SwaggerResponse(HttpStatusCode.OK,"Returns all the cards. Empty set if no cards in DB.",typeof(List<Card>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "DB is not accessible.")]
        public IHttpActionResult GetAll()
        {
            var list = bll.GetAll();
            if (list != null) return Ok(list);
            else return InternalServerError();
        }

        [HttpGet]
        [ActionName("list")]
        [SwaggerResponse(HttpStatusCode.OK, "Returns list of card ids. Empty set if no cards in DB.",typeof(List<int>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "DB is not accessible.")]
        public IHttpActionResult List()
        {
            var list = bll.List();
            if (list != null) return Ok(list);
            else return InternalServerError();
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, "Creates card in the DB.",typeof(Card))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "DB is not accesible.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrect value passed for one or more arguments.")]
        public IHttpActionResult Post(string rus, string eng, int? groupId = default)
        {
            try
            {
                var card = bll.Add(rus, eng, groupId);

                if (card != null) return Created($"/api/cards/{card.ID}", card);
                else return InternalServerError();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK,"Deletes the card from DB by id.")]
        [SwaggerResponse(HttpStatusCode.NotFound,"Card doesn't exist or DB is not accessible.")]
        public IHttpActionResult Delete(int id)
        {
            if (bll.Delete(id))
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK,"Updates the card in the DB.")]
        [SwaggerResponse(HttpStatusCode.NotFound,"Card doesn't exist or DB is not accessible.")]
        [SwaggerResponse(HttpStatusCode.BadRequest,"Incorrect value provided for one or more fields.")]
        public IHttpActionResult Put([FromBody] Card card)
        {
            try
            {
                if (bll.Update(card))
                    return Ok();
                else
                    return NotFound();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

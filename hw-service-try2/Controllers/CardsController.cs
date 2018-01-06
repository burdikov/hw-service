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
        public IHttpActionResult Get(int id)
        {
            var card = bll.Get(id);
            if (card != null) return Ok(card);
            else return NotFound();
        }

        [HttpGet]
        [ActionName("group")]
        public IHttpActionResult GetGroup(int id)
        {
            var group = bll.GetGroup(id);

            if (group != null) return Ok(group);
            else return NotFound();
        }

        [HttpGet]
        [ActionName("all")]
        public IHttpActionResult GetAll()
        {
            var list = bll.GetAll();
            if (list != null) return Ok(list);
            else return NotFound();
        }

        [HttpGet]
        [ActionName("list")]
        public IHttpActionResult List()
        {
            var list = bll.List();
            if (list != null) return Ok(list);
            else return NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post(string rus, string eng, int? groupId = default)
        {
            try
            {
                var card = bll.Add(rus, eng, groupId);

                if (card != null) return Created($"/api/cards/{card.ID}", card);
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
            if (bll.Delete(id))
                return Ok();
            else
                return NotFound();
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Card card)
        {
            try
            {
                if (bll.Update(id, card))
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

using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using NLog;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult Get(int id)
        {
            var card = bll.Get(id);
            if (card != null) return Ok(card);
            else return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var list = bll.GetAll();
            if (list != null) return Ok(list);
            else return NotFound();
        }

        [HttpPost]
        public IHttpActionResult Post(string rus, string eng, int? groupId = default)
        {
            var card = bll.Add(rus,eng,groupId);

            if (card != null) return Created($"/api/cards/{card.ID}", card);
            else return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bll.Delete(id);
            return Ok();
        }
    }
}

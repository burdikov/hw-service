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
        private ICardRepository repository;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public CardsController(ICardRepository cardRepository)
        {
            this.repository = cardRepository;
        }

        [HttpGet]
        public Card Get(int id)
        {
            return repository.Get(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public IEnumerable<Card> GetAll()
        {
            return repository.GetAll(); 
        }

        [HttpPost]
        public void Post([FromBody] Card card)
        {
            repository.Add(card);
        }

        [HttpGet]
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}

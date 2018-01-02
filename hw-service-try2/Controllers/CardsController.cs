using hw_service_try2.Dal.Interfaces;
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
        ICardRepository _cardRepository;

        public CardsController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        public string Get(int id) => _cardRepository.GetGuid().ToString();
    }
}

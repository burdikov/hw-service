using hw_service_try2.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hw_service_try2.Controllers
{
    public class TranslateController : ApiController
    {
        private ICardTranslator cardTranslator;
        public TranslateController(ICardTranslator cardTranslator)
        {
            this.cardTranslator = cardTranslator;
        }

        [HttpGet]
        [ActionName("ru-en")]
        public IHttpActionResult RuEn(string word)
        {
            var list = cardTranslator.Translate(word, Common.TranslateDirection.ToEnglish);
            if (list != null) return Ok(list);
            else return NotFound();
        }

        [HttpGet]
        [ActionName("en-ru")]
        public IHttpActionResult EnRu(string word)
        {
            var list = cardTranslator.Translate(word, Common.TranslateDirection.ToRussian);
            if (list != null) return Ok(list);
            else return NotFound();
        }
    }
}

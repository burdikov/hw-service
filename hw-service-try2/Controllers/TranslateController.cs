using hw_service_try2.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

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
        [SwaggerResponse(HttpStatusCode.OK,"Translates the word from russian to english using DB cards. Empty set if translation is not found.",typeof(List<string>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,"DB is not accessible.")]
        public IHttpActionResult RuEn(string word)
        {
            var list = cardTranslator.Translate(word, Common.TranslateDirection.ToEnglish);
            if (list != null) return Ok(list);
            else return InternalServerError();
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,"Translates the word from russian to english using DB cards. Empty set if translation is not found.",typeof(List<string>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError,"DB is not accessible.")]
        public IHttpActionResult EnRu(string word)
        {
            var list = cardTranslator.Translate(word, Common.TranslateDirection.ToRussian);
            if (list != null) return Ok(list);
            else return NotFound();
        }
    }
}

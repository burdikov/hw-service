using hw_service_try2.Bl.Interfaces;
using hw_service_try2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using hw_service_try2.Models;
using Swashbuckle.Swagger.Annotations;

namespace hw_service_try2.Controllers
{
    public class TestController : ApiController
    {
        private ICardTester tester;

        public TestController(ICardTester cardTester)
        {
            tester = cardTester;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,"Returns a test for a requested card.",typeof(WordTest))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal error occured or DB is not accessible.")]
        [SwaggerResponse(HttpStatusCode.BadRequest,"Card with requested ID does not exist or incorrect arguments provided.")]
        public IHttpActionResult Get(int id, string originLang)
        {
            try
            {
                Lang l = (Lang)Enum.Parse(typeof(Lang), originLang);
                var wordtest = tester.Create(id, l);

                if (wordtest != null) return Ok(wordtest);
                else return InternalServerError();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Checks the test and return the result. NotChecked if wrong ID or DB is inaccessible.",typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest,"Incorrect body.")]
        public IHttpActionResult Check([FromBody] WordTest wordTest)
        {
            try
            {
                tester.Check(wordTest);
                return Ok(wordTest.Result.ToString());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

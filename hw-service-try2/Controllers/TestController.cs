using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public IHttpActionResult Get(int id, string originLang)
        {
            try
            {
                Lang l = (Lang)Enum.Parse(typeof(Lang), originLang);
                var wordtest = tester.CreateWordTest(id, l);

                if (wordtest != null) return Ok(wordtest);
                else return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

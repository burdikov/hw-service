using System;
using hw_service_try2.Bl;
using hw_service_try2.Controllers;
using hw_service_try2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;

namespace hw_service_try2.Tests
{
    [TestClass]
    public class CardsControllerTests
    {
        [TestMethod]
        public void Cards_AddCard_ShouldCreate()
        {
            // Arrange
            var bl = new CardBusinessLayer(new MockCardRepository());
            var target = new CardsController(bl);
            string rus = "Слово", eng = "word";
            int? groupId = 3;

            // Act
            var res = target.Post(rus, eng, groupId);

            // Assert
            Assert.IsInstanceOfType(res, typeof(CreatedNegotiatedContentResult<Card>));

            var created = (CreatedNegotiatedContentResult<Card>)res;

            Assert.AreEqual(rus, created.Content.Rus);
            Assert.AreEqual(eng, created.Content.Eng);
            Assert.AreEqual(groupId, created.Content.GroupID);
        }

        [TestMethod]
        public void Cards_AddCardWithNullField_ShouldNotCreate()
        {
            // A
            var bl = new CardBusinessLayer(new MockCardRepository());
            var target = new CardsController(bl);
            string rus = null, eng = null;
            int? groupId = null;

            // A
            var res = target.Post(rus, eng, groupId);

            // A
            Assert.IsInstanceOfType(res, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Cards_AddCardWithWrongField_ShouldNotCreate()
        {
            // A
            var bl = new CardBusinessLayer(new MockCardRepository());
            var target = new CardsController(bl);
            string rus = "rus", eng = "енг";
            int? groupId = null;

            // A
            var res = target.Post(rus, eng, groupId);

            // A
            Assert.IsInstanceOfType(res, typeof(BadRequestErrorMessageResult));
        }
    }
}

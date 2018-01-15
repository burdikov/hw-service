using System;
using hw_service_try2.Bl;
using hw_service_try2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hw_service_try2.Tests
{
    [TestClass]
    public class CardsBusinessLayerTests
    {
        [TestMethod]
        public void CardBl_AddCard_ShouldCreate()
        {
            // Arrange
            var target = new CardBusinessLayer(new MockCardRepository());
            string rus = "Слово", eng = "word";
            int? groupId = 3;

            // Act
            Card res = target.Add(rus, eng, groupId);

            // Assert
            Assert.AreEqual(rus, res.Rus);
            Assert.AreEqual(eng, res.Eng);
            Assert.AreEqual(groupId, res.GroupID);
        }

        [TestMethod]
        public void CardBl_AddCardWithNullField_ShouldNotCreate()
        {
            // A
            var target = new CardBusinessLayer(new MockCardRepository());
            string rus = null, eng = null;
            Exception ex = null;
            Card res = null;

            // A
            try
            {
                res = target.Add(rus, eng, null);
            }
            catch (Exception e)
            {
                ex = e;
            }

            // A
            Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            Assert.IsNull(res);
        }

        [TestMethod]
        public void CardBl_AddCardWithWrongField_ShouldNotCreate()
        {
            // A
            var target = new CardBusinessLayer(new MockCardRepository());
            string rus = "eng", eng = "рус";
            Exception ex = null;
            Card res = null;

            // A
            try
            {
                res = target.Add(rus, eng, null);
            }
            catch (Exception e)
            {
                ex = e;
            }

            // A
            Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            Assert.IsNull(res);
        }
    }
}

using hw_service_try2.Bl.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Tests
{
    class MockCardBl : ICardBusinessLayer
    {
        public Card Add(string rus, string eng, int? groupId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Card Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> Get(int[] ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(Card card)
        {
            throw new NotImplementedException();
        }
    }
}

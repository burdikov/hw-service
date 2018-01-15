using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Tests
{
    class MockCardRepository : ICardRepository
    {
        public List<Card> db = new List<Card>();
        int identity = 0;

        public Card Create(string rus, string eng, int? groupId)
        {
            var res = new Card() { ID = identity++, Rus = rus, Eng = eng, GroupID = groupId };
            db.Add(res);
            return res;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> List()
        {
            throw new NotImplementedException();
        }

        public Card Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> Read(int[] ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> ReadAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> ReadGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public int Update(Card card)
        {
            throw new NotImplementedException();
        }
    }
}

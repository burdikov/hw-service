using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Dal.Interfaces
{
    public interface ICardRepository
    {
        void Add(Card card);
        void Delete(int id);
        Card Get(int id);
        IEnumerable<Card> GetAll();
    }
}

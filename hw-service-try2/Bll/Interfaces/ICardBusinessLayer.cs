using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Bll.Interfaces
{
    public interface ICardBusinessLayer
    {
        Card Add(string rus, string eng, int? groupId);
        bool Delete(int id);
        bool Update(Card card);
        Card Get(int id);
        IEnumerable<int> List();
        IEnumerable<Card> Get(int[] ids);
        IEnumerable<Card> GetAll();
        IEnumerable<Card> GetGroup(int groupId);
    }
}

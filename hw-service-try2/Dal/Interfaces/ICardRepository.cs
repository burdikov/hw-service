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
        Card Create(string rus, string eng, int? groupId);
        Card Read(int id);
        IEnumerable<int> List();
        IEnumerable<Card> Read(int[] ids);
        IEnumerable<Card> ReadGroup(int groupId);
        IEnumerable<Card> ReadAll();
        int Update(Card card);
        int Delete(int id);
    }
}

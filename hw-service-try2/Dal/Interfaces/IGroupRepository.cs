using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Dal.Interfaces
{
    public interface IGroupRepository
    {
        void Add(Group group);
        void UpdateName(int id, string newName);
        void Delete(int id);
        IEnumerable<Card> Get(int groupId);
        IEnumerable<Group> GetAll();
    }
}

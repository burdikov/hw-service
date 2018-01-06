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
        Group Create(string name);
        Group Read(int groupId);
        IEnumerable<Group> ReadAll();
        IEnumerable<int> List();
        int Update(int id, Group group);
        int Delete(int id);
    }
}

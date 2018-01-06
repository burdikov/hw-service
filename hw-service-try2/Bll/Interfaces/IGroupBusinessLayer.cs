using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Bll.Interfaces
{
    public interface IGroupBusinessLayer
    {
        Group Add(string name);
        bool Update(int id, Group group);
        bool Delete(int id);
        Group Get(int groupId);
        IEnumerable<int> List();
        IEnumerable<Group> GetAll();
    }
}

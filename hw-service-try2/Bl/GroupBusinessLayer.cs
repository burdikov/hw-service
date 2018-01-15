using hw_service_try2.Bl.Interfaces;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hw_service_try2.Bl
{
    public class GroupBusinessLayer : IGroupBusinessLayer
    {
        IGroupRepository repository;

        public GroupBusinessLayer(IGroupRepository repository)
        {
            this.repository = repository;
        }

        public Group Add(string name)
        {
            if (name == null) throw new ArgumentNullException();
            if (name.Length > 50 || name.Length == 0)
                throw new ArgumentException("Invalid name length.");

            return repository.Create(name);
        }

        public bool Delete(int id) => repository.Delete(id) == 1 ? true : false;

        public Group Get(int groupId) => repository.Read(groupId);

        public IEnumerable<Group> GetAll() => repository.ReadAll();

        public IEnumerable<int> List() => repository.List();

        public bool Update(int id, Group group)
        {
            if (group == null) throw new ArgumentNullException();
            if (group.Name.Length == 0 || group.Name.Length > 50)
                throw new ArgumentException("Invalid name length.");

            return repository.Update(id, group) == 1 ? true : false;
        }
    }
}
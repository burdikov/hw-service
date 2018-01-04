using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hw_service_try2.Bll
{
    public class GroupBusinessLayer : IGroupBusinessLayer
    {
        IGroupRepository repository;

        public GroupBusinessLayer(IGroupRepository repository)
        {
            this.repository = repository;
        }

        public void Add(Group group)
        {
            if (group == null) throw new ArgumentNullException();
            if (group.Name.Length > 50 || group.Name.Length == 0)
                throw new ArgumentException("Invalid name length.");
            repository.Add(group);
        }

        public void Delete(int id) => repository.Delete(id);

        public IEnumerable<Card> Get(int groupId) => repository.Get(groupId);

        public IEnumerable<Group> GetAll() => repository.GetAll();

        public void UpdateName(int id, string newName)
        {
            if (newName == null) throw new ArgumentNullException();
            if (newName.Length == 0 || newName.Length > 50)
                throw new ArgumentException("Invalid name length.");
            repository.UpdateName(id, newName);
        }
    }
}
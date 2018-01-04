﻿using hw_service_try2.Models;
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
        void Update(int id, Card card);
        void Delete(int id);
        IEnumerable<Card> ReadAll();
    }
}

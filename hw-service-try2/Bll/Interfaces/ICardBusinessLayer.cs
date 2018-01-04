﻿using hw_service_try2.Models;
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
        void Delete(int id);
        void Update(int id, Card card);
        Card Get(int id);
        IEnumerable<Card> GetAll();
    }
}

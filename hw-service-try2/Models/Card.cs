using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hw_service_try2.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string Rus { get; set; }
        public string Eng { get; set; }
        public int? GroupID { get; set; }
    }
}
using hw_service_try2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hw_service_try2.Models
{
    public class WordTest
    {
        public int CardId { get; set; }
        public Lang OriginLang { get; set; }
        public string Word { get; set; }
        public string ChosenOption { get; set; }
        public List<string> Options { get; set; }
    }
}
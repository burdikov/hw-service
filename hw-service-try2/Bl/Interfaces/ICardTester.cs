using hw_service_try2.Common;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Bl.Interfaces
{
    public interface ICardTester
    {
        WordTest Create(int cardId, Lang originLang);
        void Check(WordTest test);
    }
}

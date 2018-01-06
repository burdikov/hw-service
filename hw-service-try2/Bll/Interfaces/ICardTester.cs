using hw_service_try2.Common;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Bll.Interfaces
{
    public interface ICardTester
    {
        WordTest CreateWordTest(int id, Lang originLang);
        bool IsWordTestPassed(WordTest test);
    }
}

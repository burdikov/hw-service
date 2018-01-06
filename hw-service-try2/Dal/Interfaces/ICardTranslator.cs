using hw_service_try2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_service_try2.Dal.Interfaces
{
    public interface ICardTranslator
    {
        IEnumerable<string> Translate(string word, TranslateDirection translateDirection);
    }
}

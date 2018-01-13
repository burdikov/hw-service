using hw_service_try2.Common;
using hw_service_try2.Dal.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace hw_service_try2.Dal
{
    public class DbCardTranslator : SqlDbRepository, ICardTranslator
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DbCardTranslator() : base(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString) { }

        public DbCardTranslator(string connectionString) : base(connectionString) { }

        public IEnumerable<string> Translate(string word, TranslateDirection translateDirection)
        {
            try
            {
                string commandText = default;
                switch (translateDirection)
                {
                    case TranslateDirection.ToRussian:
                        commandText = $"select [rus] from [card] where upper(eng) = '{word.ToUpper()}'";
                        break;
                    case TranslateDirection.ToEnglish:
                        commandText = $"select [eng] from [card] where upper(rus) = '{word.ToUpper()}'";
                        break;
                }

                var reader = ExecuteSingleSetReader(commandText);

                var list = new List<string>();
                foreach (object[] row in reader)
                {
                    list.Add(row[0].ToString());
                }

                return list;
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }
    }
}
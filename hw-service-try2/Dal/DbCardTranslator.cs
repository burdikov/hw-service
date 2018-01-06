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
    public class DbCardTranslator : ICardTranslator
    {
        private string ConnectionString { get; }
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DbCardTranslator()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
        }
        public IEnumerable<string> Translate(string word, TranslateDirection translateDirection)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    switch (translateDirection)
                    {
                        case TranslateDirection.ToRussian:
                            cmd.CommandText = $"select [rus] from [card] where upper(eng) = '{word.ToUpper()}'";
                            break;
                        case TranslateDirection.ToEnglish:
                            cmd.CommandText = $"select [eng] from [card] where upper(rus) = '{word.ToUpper()}'";
                            break;
                    }

                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var list = new List<string>();
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }

                        return list;
                    }
                    else
                        return null;
                }
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }
    }
}
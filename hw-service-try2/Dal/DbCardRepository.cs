using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using NLog;

namespace hw_service_try2.Dal
{
    public class DbCardRepository : SqlDbRepository, ICardRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DbCardRepository() : base(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)
        {
        }

        public DbCardRepository(string connectionString) : base (connectionString)
        {
        }
        
        /// <summary>
        /// Insert a new record into Card table.
        /// </summary>
        public Card Create(string rus, string eng, int? groupId)
        {
            try
            {
                var commandText = $"insert into [Card] " +
                        $"values ('{rus}','{eng}'," +
                        $"{ groupId?.ToString() ?? "null" });" +
                        $"select SCOPE_IDENTITY()";

                var s = ExecuteScalar(commandText).ToString();
                int.TryParse(s, out int id);

                return new Card() { ID = id, Rus = rus, Eng = eng, GroupID = groupId };
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        public int Delete(int id)
        {
            try
            {
                return ExecuteNonQuery("delete from [dbo].[Card] where id = " + id);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return 0;
            }
        }

        public Card Read(int id)
        {
            try
            {
                var commandText = $"select * from [dbo].[Card] where id = {id}";

                List<object[]> list = ExecuteSingleSetReader(commandText);

                return ConvertToCards(list).ElementAtOrDefault(0);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        public IEnumerable<Card> ReadAll()
        {
            try
            {
                var commandText = "select * from [Card]";
                List<object[]> list = ExecuteSingleSetReader(commandText);

                return ConvertToCards(list);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        public int Update(Card card)
        {
            try
            {
                var cmd = $"update [Card] " +
                    $"set rus = '{card.Rus}', eng = '{card.Eng}', " +
                    $"groupid = {card.GroupID?.ToString() ?? "null"} " +
                    $"where id = {card.ID}";

                return ExecuteNonQuery(cmd);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return 0;
            }
        }

        public IEnumerable<Card> ReadGroup(int groupId)
        {
            try
            {
                var commandText = $"select * from [card] where [groupid] = {groupId}";

                List<object[]> list = ExecuteSingleSetReader(commandText);

                return ConvertToCards(list);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        private List<Card> ConvertToCards(List<object[]> list)
        {
            var result = new List<Card>();
            for (int i = 0; i < list.Count(); i++)
            {
                result.Add(new Card()
                {
                    ID = (int)(list[i][0]),
                    Rus = (string)(list[i][1]),
                    Eng = (string)(list[i][2]),
                    GroupID = (int?)(list[i][3])
                });
            }
            return result;
        }

        public IEnumerable<int> List()
        {
            try
            {
                var cmd = "select [id] from [Card]";
                var list = ExecuteSingleSetReader(cmd);
                return list.Select(x => (int)x[0]).ToArray();
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        public IEnumerable<Card> Read(int[] ids)
        {
            try
            {
                var cmd = $"select * from [card] where [id] in " +
                    $"({ids.Select(x => x.ToString()).Aggregate((x, y) => x + ',' + y)})";
                return ConvertToCards(ExecuteSingleSetReader(cmd));
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }
    }
}
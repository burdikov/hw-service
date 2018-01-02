using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using NLog;

namespace hw_service_try2.Dal
{
    public class DbCardRepository : ICardRepository
    {
        private string ConnectionString { get; }
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DbCardRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
        }

        public DbCardRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private void ExecuteNonQuery(string commandText)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = commandText;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Insert a new card record into Card table. ID field of paramater is ignored.
        /// </summary>
        /// <param name="card">Card to add into the DB.</param>
        public void Add(Card card)
        {
            try
            {
                ExecuteNonQuery(
                    $"insert into Card (rus, eng, groupid) " +
                    $"values ('{card.Rus}', '{card.Eng}', {card.GroupID})");
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                ExecuteNonQuery("delete from [dbo].[Card] where id = " + id);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }

        public Card Get(int id)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from [dbo].[Card] where id = {id}";

                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                        return new Card()
                        {
                            ID = (int)reader[0],
                            Rus = (string)reader[1],
                            Eng = (string)reader[2],
                            GroupID = reader.IsDBNull(3) ? null : (int?)reader[3]
                        };
                    else return null;
                }
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }

        public IEnumerable<Card> GetAll()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from [dbo].[Card]";
                    List<Card> list = new List<Card>();

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var x = reader[3];
                        list.Add(new Card()
                        {
                            ID = reader.GetInt32(0),
                            Rus = reader.GetString(1),
                            Eng = reader.GetString(2),
                            GroupID = reader.IsDBNull(3) ? null : (int?)reader[3]
                        });
                    }
                    return list;
                }
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }
    }
}
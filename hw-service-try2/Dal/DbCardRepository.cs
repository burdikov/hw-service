﻿using System;
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
        /// Insert a new record into Card table.
        /// </summary>
        public Card Create(string rus, string eng, int? groupId)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();

                    cmd.CommandText = $"insert into [Card] " +
                        $"values ('{rus}','{eng}'," +
                        $"{ groupId?.ToString() ?? "null" });" +
                        $"select SCOPE_IDENTITY()";

                    var s = cmd.ExecuteScalar().ToString();
                    int.TryParse(s, out int id);
                    
                    return new Card() { ID = id, Rus = rus, Eng = eng, GroupID = groupId };
                }
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

        public Card Read(int id)
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

        public IEnumerable<Card> ReadAll()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from [dbo].[Card]";

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<Card> list = new List<Card>();
                        while (reader.Read())
                        {
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
                    else return null;
                }
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }

        public void Update(int id, Card card)
        {
            try
            {
                var cmd = $"update [Card] " +
                    $"set rus = '{card.Rus}', eng = '{card.Eng}', " +
                    $"groupid = {card.GroupID?.ToString() ?? "null"} " +
                    $"where id = {id}";

                ExecuteNonQuery(cmd);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }
    }
}
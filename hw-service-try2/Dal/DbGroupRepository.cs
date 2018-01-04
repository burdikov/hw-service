using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace hw_service_try2.Dal
{
    public class DbGroupRepository : IGroupRepository
    {
        private string ConnectionString { get; }
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DbGroupRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
        }

        public DbGroupRepository(string connectionString)
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

        public void Add(Group group)
        {
            try
            {
                var cmd = $"insert into Group values ('{group.Name}')";

                ExecuteNonQuery(cmd);
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
                var cmd = $"delete from [Group] where id = {id}";

                ExecuteNonQuery(cmd);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                throw;
            }
        }

        public IEnumerable<Card> Get(int groupId)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"select * from Card where groupid = {groupId}";

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

        public IEnumerable<Group> GetAll()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from [Group]";

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        var list = new List<Group>();
                        while (reader.Read())
                        {
                            list.Add(new Group()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1)
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

        public void UpdateName(int id, string newName)
        {
            try
            {
                var cmd = $"update [Group] set name = '{newName}' where id = {id}";
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
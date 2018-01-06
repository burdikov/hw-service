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
    public class DbGroupRepository : SqlDbRepository, IGroupRepository
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public DbGroupRepository() : base(ConfigurationManager.ConnectionStrings["mssql"].ConnectionString)
        {
        }

        public DbGroupRepository(string connectionString) : base(connectionString)
        {
        }

        public Group Create(string name)
        {
            try
            {
                var commandText = $"insert into [Group] values ('{name}'); select scope_identity()";
                var s = ExecuteScalar(commandText).ToString();
                int.TryParse(s, out int id);

                return new Group() { ID = id, Name = name };
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
                var cmd = $"delete from [Group] where id = {id}";

                return ExecuteNonQuery(cmd);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return 0;
            }
        }

        public Group Read(int groupId)
        {
            try
            {
                var commandText = $"select * from [Group] where id = {groupId}";

                return ConvertToGroups(ExecuteSingleSetReader(commandText)).ElementAtOrDefault(0);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        public IEnumerable<Group> ReadAll()
        {
            try
            {
                var commandText = "select * from [Group]";
                return ConvertToGroups(ExecuteSingleSetReader(commandText));
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        public int Update(int id, Group group)
        {
            try
            {
                var cmd = $"update [Group] set name = '{group.Name}' where id = {id}";
                return ExecuteNonQuery(cmd);
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return 0;
            }
        }

        public IEnumerable<int> List()
        {
            try
            {
                // list of rows (object[] represents row) with single value
                List<object[]> idList = ExecuteSingleSetReader("select [id] from [group]");
                return idList.Select(x => (int)x[0]).ToArray();
            }
            catch (SqlException e)
            {
                logger.Error(e);
                return null;
            }
        }

        private List<Group> ConvertToGroups(List<object[]> list)
        {
            var result = new List<Group>();
            for (int i = 0; i < list.Count(); i++)
            {
                result.Add(new Group()
                {
                    ID = (int)(list[i][0]),
                    Name = (string)(list[i][1])
                });
            }
            return result;
        }
    }
}
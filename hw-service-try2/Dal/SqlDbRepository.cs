using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace hw_service_try2.Dal
{
    public abstract class SqlDbRepository
    {
        protected virtual string ConnectionString { get; }

        protected SqlDbRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected virtual T RunFunc<T>(Func<SqlCommand, T> func)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                return func(cmd);
            }
        }

        protected virtual object ExecuteScalar(string commandText) =>
            RunFunc((SqlCommand cmd) =>
            {
                cmd.CommandText = commandText;
                return cmd.ExecuteScalar();
            });

        protected virtual int ExecuteNonQuery(string commandText) =>
            RunFunc((SqlCommand cmd) =>
            {
                cmd.CommandText = commandText;
                return cmd.ExecuteNonQuery();
            });

        /// <summary>
        /// Works like DbCommand.ExecuteReader, but returns list of rows instead of 
        /// reader (empty list if query returned no rows). Checks for DBNull.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected virtual List<object[]> ExecuteSingleSetReader(string commandText)
        {
            List<object[]> func (SqlCommand cmd)
            {
                cmd.CommandText = commandText;

                var r = cmd.ExecuteReader();

                var result = new List<object[]>();
                while (r.Read())
                {
                    var o = new object[r.FieldCount];
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        o[i] = r.IsDBNull(i) ? null : r.GetValue(i);
                    }
                    result.Add(o);
                }
                return result;
            }

            return RunFunc(func);
        }
    }
}
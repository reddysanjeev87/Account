using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Account.Persisstent.SqlServer
{
    public static class SqlQueryExtensions
    {
        public static async Task<List<T>> SqlQueryAsync<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                return await db2.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
            }
        }

        public static List<T> SqlQuery<T>(this DbContext db, string sql, params object[] parameters) where T : class
        {
            using (var db2 = new ContextForQueryType<T>(db.Database.GetDbConnection()))
            {
                return db2.Set<T>().FromSqlRaw(sql, parameters).ToList();
            }
        }
        public static async Task<List<T>> ExecuteQuery<T>(this DbContext dbContext, string query, object[] parameters = null)
        {
            using (var dbConnection = dbContext.Database.GetDbConnection())
            {
                if(dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    if (parameters != null && parameters.Any())
                        command.Parameters.AddRange(parameters);

                    using (var result = await command.ExecuteReaderAsync())
                    {
                        return result.ToList<T>();
                    }
                }
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly DbConnection connection;

            public ContextForQueryType(DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }
        public static List<T> ToList<T>(this IDataReader dataReader)
        {
            if (dataReader == null) return null;

            List<string> readerColumns = new List<string>();
            for (int index = 0; index < dataReader.FieldCount; index++)
                readerColumns.Add(dataReader.GetName(index));

            List<T> list = new List<T>();
            var obj = Activator.CreateInstance<T>();
            var props = obj.GetType().GetProperties().Where(p => readerColumns.Any(c => c.ToLower() == p.Name.ToLower()));

            while (dataReader.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in props)
                {
                    if (!object.Equals(dataReader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dataReader[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            dataReader.Close();
            dataReader.Dispose();
            dataReader = null;
            return list;
        }
    }

}

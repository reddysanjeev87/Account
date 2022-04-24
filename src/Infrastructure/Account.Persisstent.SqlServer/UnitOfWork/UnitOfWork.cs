using Account.Application;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;



namespace Account.Persisstent.SqlServer
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IUnitOfWork"/> and <see cref="IUnitOfWork{TContext}"/> interface.
    /// </summary>
    /// <typeparam name="TContext">The type of the db context.</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private bool disposed = false;
        private readonly IRepositoryResolver repositoryResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(TContext context, IRepositoryResolver repositoryResolver)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.repositoryResolver = repositoryResolver;
        }

        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        public TContext DbContext => _context;

        /// <summary>
        /// Changes the database name. This require the databases in the same machine. NOTE: This only work for MySQL right now.
        /// </summary>
        /// <param name="database">The database name.</param>
        /// <remarks>
        /// This only been used for supporting multiple databases in the same model. This require the databases in the same machine.
        /// </remarks>
        public void ChangeDatabase(string database)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(database);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }

            // Following code only working for mysql.
            var items = _context.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item is IConventionEntityType entityType)
                {
                    entityType.SetSchema(database);
                }
            }
        }
        public TRepo GetChildRepository<TRepo>() where TRepo : class
        {
            return this.repositoryResolver.ChildResolve<TRepo>();
        }

        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomRepository"><c>True</c> if providing custom repositry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/> interface.</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var repository = this.repositoryResolver.BaseResolve<TEntity>();
            return repository;
        }

        public async Task<List<TEntity>> GetFromRawSqlAsync<TEntity>(string query, object[] parameters = null) where TEntity : class
        {
            var dbConnection = _context.Database.GetDbConnection();

            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }
            var command = dbConnection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            if (parameters != null && parameters.Any())
                command.Parameters.AddRange(parameters);

            var result = await command.ExecuteReaderAsync();

            return result.ToList<TEntity>();

        }
        public async Task<List<TEntity>> GetFromStoredProcedureAsync<TEntity>(string spName, object[] parameters = null) where TEntity : class
        {
            var dbConnection = _context.Database.GetDbConnection();

            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }
            var command = dbConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = spName;
            if (parameters != null && parameters.Any())
                command.Parameters.AddRange(parameters);

            var result = await command.ExecuteReaderAsync();

            return result.ToList<TEntity>();

        }
        public async Task<IEnumerable<dynamic>> GetDynamicFromRawSqlAsync(string query, object[] parameters = null)
        {
            var data = new List<dynamic>();
            using (var dbConnection = _context.Database.GetDbConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
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
                        while (result.Read())
                        {
                            data.Add(SqlDataReaderToExpando(result));
                        }
                    }
                }
            }
            return data;
        }
        private dynamic SqlDataReaderToExpando(DbDataReader reader)
        {
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;

            for (var i = 0; i < reader.FieldCount; i++)
                expandoObject.Add(reader.GetName(i), reader[i]);

            return expandoObject;
        }

        /// <summary>
        /// Executes the specified raw SQL command.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The number of state entities written to database.</returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters) => _context.Database.ExecuteSqlRaw(sql, parameters);

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{T}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class => _context.Set<TEntity>().FromSqlRaw(sql, parameters);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            //if (ensureAutoHistory)
            //{
            //    _context.EnsureAutoHistory();
            //}

            return _context.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            //if (ensureAutoHistory)
            //{
            //    _context.EnsureAutoHistory();
            //}

            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves all changes made in this context to the database with distributed transaction.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <param name="unitOfWorks">An optional <see cref="IUnitOfWork"/> array.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var count = 0;
                foreach (var unitOfWork in unitOfWorks)
                {
                    count += await unitOfWork.SaveChangesAsync(ensureAutoHistory).ConfigureAwait(false);
                }

                count += await SaveChangesAsync(ensureAutoHistory);

                ts.Complete();

                return count;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // dispose the db context.
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            _context.ChangeTracker.TrackGraph(rootEntity, callback);
        }
    }
}

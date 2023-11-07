using App.DvdRental.Data.DbContext;
using App.DvdRental.Data.Extentions;
using App.DvdRental.Data.Models;
using App.DvdRental.Domain.Interfaces;
using Dapper;
using System.Data.Common;

namespace App.DvdRental.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DapperContext _dapperContext;


        public Repository(DapperContext dapperContext)
        {
            _dapperContext=dapperContext;
        }

        protected QueryCommannd Query { get; set; }
  
        public async Task<QueryResult<IEnumerable<T>>> GetAllAsync()
        {
            var result = new QueryResult<IEnumerable<T>>();
            try
            {
                using var conn = _dapperContext.CreateConnection();
                var data = await conn.QueryAsync<T>(Query.CommandText, Query.Parameters);
                result.WrappSuccessResult(data);
            }
            catch (InvalidOperationException ex)
            {
                result.WrappFailedResult(ex);
            }
            catch (DbException ex)
            {
               result.WrappFailedResult(ex);
            }

            return result;

        }

        public async Task<QueryResult<T>> GetByIdAsync<TKey>(TKey Id)
        {
            var result = new QueryResult<T>();
            try
            {
                using var conn = _dapperContext.CreateConnection();
                var data = await conn.QuerySingleAsync<T>(Query.CommandText, Query.Parameters);
                result.WrappSuccessResult(data);
            }
            catch (InvalidOperationException ex)
            {
                result.WrappFailedResult(ex);
            }
            catch (DbException ex)
            {
                result.WrappFailedResult(ex);
            }

            return result;

        }
    }
}

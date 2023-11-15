using App.DvdRental.Data.DbContext;
using App.DvdRental.Data.Extentions;
using App.DvdRental.Data.Models;
using Dapper;
using System.Data.Common;

namespace App.DvdRental.Data.Repository
{
    public interface IDapperRepository<T>
    {
        Task<QueryResult<IEnumerable<T>>> GetAllAsync(QueryCommannd query);
        Task<QueryResult<T>> GetByIdAsync<TKey>(TKey Id, QueryCommannd query);
    }


    public  class Repository<T> : IDapperRepository<T> where T : class
    {
        protected readonly DapperContext _dapperContext;


        public Repository(DapperContext dapperContext)
        {
            _dapperContext=dapperContext;
        }

        public  async Task<QueryResult<IEnumerable<T>>> GetAllAsync(QueryCommannd query)
        {
            var result = new QueryResult<IEnumerable<T>>();
            try
            {
                using var conn = _dapperContext.CreateConnection();
                var data = await conn.QueryAsync<T>(query.CommandText,query.Parameters);
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

        public  async Task<QueryResult<T>> GetByIdAsync<TKey>(TKey Id, QueryCommannd query)
        {
            var result = new QueryResult<T>();
            try
            {
                using var conn = _dapperContext.CreateConnection();
                var data = await conn.QuerySingleAsync<T>(query.CommandText, query.Parameters);
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

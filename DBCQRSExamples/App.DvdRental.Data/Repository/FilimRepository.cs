using App.DvdRental.Data.DbContext;
using App.DvdRental.Data.Models;
using App.DvdRental.Domain.Models.Entity;
using Dapper;

namespace App.DvdRental.Data.Repository
{
    public class FilimRepository : Repository<Film>
    {
        public FilimRepository(DapperContext dapperContext)
            : base(dapperContext)
        {
          
        }

        public async  Task<QueryResult<IEnumerable<Film>>> GetAllAsync()
        {
            var query = @"select 
                            film_id
                            ,title
                            ,description
                            ,length
                            ,rating
                            from public.film
                            ";
            var command = new QueryCommannd { CommandText = query };
            return await GetAllAsync(command);
        }

        public async Task<QueryResult<Film>> GetByIdAsync(int id)
        {
            var command = new QueryCommannd { CommandText = @"select 
                                                        film_id
                                                        ,title
                                                        ,description
                                                        ,length
                                                        ,rating
                                                     from public.film
                                                     where  film_id = @film_id ",
            };
            var template = new Film{ film_id = id };
            command.Parameters = new DynamicParameters(template);
            return await GetByIdAsync(id, command);
        }

    }
}

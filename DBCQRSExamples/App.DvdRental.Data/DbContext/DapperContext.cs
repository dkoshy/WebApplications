using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace App.DvdRental.Data.DbContext
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString=configuration.GetConnectionString("DVDRentalConnection")!;
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}

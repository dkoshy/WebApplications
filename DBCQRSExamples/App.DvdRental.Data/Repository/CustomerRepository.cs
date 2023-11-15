using App.DvdRental.Data.DbContext;
using App.DvdRental.Data.Extentions;
using App.DvdRental.Data.Models;
using App.DvdRental.Domain.Models.Entity;
using Dapper;
using System.Data;
using System.Data.Common;

namespace App.DvdRental.Data.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(DapperContext dapperContext)
            : base(dapperContext)
        {
        }

        //Muti Mapp Example
        public async Task<QueryResult<IEnumerable<Customer>>> GetAllAsync()
        {
            var result = new QueryResult<IEnumerable<Customer>>();
            try
            {
                var query = @"select 
                            c.customer_id
                            ,c.first_name
                            ,c.last_name
                            ,c.email
                            ,a.address_id
                            ,a.address
                            ,a.address2
                            ,a.district
                            ,a.postal_code
                            ,a.phone
                    from public.customer c 
                    inner join public.address a on c.address_id = a.address_id
                        ";
                var command = new QueryCommannd { CommandText = query };
                using var conn = _dapperContext.CreateConnection();
                var data = await conn.QueryAsync<Customer, ContactDetails, Customer>(command.CommandText
                      , (c, ad) => { c.ContactDetails = ad; return c; }
                      , command.Parameters,commandType:CommandType.Text , splitOn: "address_id");
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

using App.DvdRental.Data.DbContext;
using App.DvdRental.Domain.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DvdRental.Data.Repository
{
    public class FilimRepository : Repository<Filim>
    {
        public FilimRepository(DapperContext dapperContext)
            : base(dapperContext)
        {
        }

    }
}

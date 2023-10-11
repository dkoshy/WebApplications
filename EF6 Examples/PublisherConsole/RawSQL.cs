using Microsoft.EntityFrameworkCore;
using PublisherData;

namespace PublisherConsole
{
    public  class RawSQL
    {
        private readonly PublisherDBContext _ctx;

        public RawSQL()
        {
                _ctx = new PublisherDBContext();
        }
        public void  FormattedWithInterpolated_Safe()
        {
            var lastnamestart = "L";

            var authors = _ctx.Authors
                            .FromSqlInterpolated($"select * from authors where lastname LIKE '{lastnamestart}%'")
                            .OrderBy(a => a.LastName)
                            .TagWith("InterPolated_Query")
                            .ToList();
        }

        public void FormattedWithInterpolated_StringFromInterpolated_StillUnsafe()
        {
            var lastnamestart = "L";
            
            var authors = _ctx.Authors
                            .FromSqlRaw($"select * from authors where lastname LIKE '{lastnamestart}%'")
                            .OrderBy(a => a.LastName)
                            .TagWith("InterPolated_Query")
                            .ToList();
        }

        public void FormattedWithInterpolated_StringFromInterpolated_SafeMode()
        {
            var lastnamestart = "L";

            var authors = _ctx.Authors
                            .FromSqlRaw($"select * from authors where lastname LIKE '{0}%'",lastnamestart)
                            .OrderBy(a => a.LastName)
                            .TagWith("InterPolated_Query")
                            .ToList();
        }

        public void InterpolatedSqlStoredProc() 
        {
            int start = 2010;
            int end = 2015;
            var authors = _ctx.Authors
            .FromSqlInterpolated($"AuthorsPublishedinYearRange {start}, {end}")
            .ToList();
        } 
        public void RawSqlStoredProc()
        {
            var authors = _ctx.Authors
                .FromSqlRaw("AuthorsPublishedinYearRange {0}, {1}", 2010, 2015)
                .ToList();
        }

        public void DeleCover()
        {
            var affectedRows = _ctx.Database.ExecuteSqlRaw("Delete from Cover where id = 3");
        }
    }
}

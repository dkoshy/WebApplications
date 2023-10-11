using PublisherDomain;

namespace PublisherData.Repo
{
    public class CoverRepository : Repository<Cover>
    {
        public CoverRepository(PublisherDBContext dBContext) : base(dBContext)
        {
        }
    }
}


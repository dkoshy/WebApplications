using PublisherDomain;

namespace PublisherData.Repo
{
    public partial class AuthorRepository
    {
        public class ArtistRepository : Repository<Artist>
        {
            public ArtistRepository(PublisherDBContext dBContext) : base(dBContext)
            {
            }
        }

    }
}


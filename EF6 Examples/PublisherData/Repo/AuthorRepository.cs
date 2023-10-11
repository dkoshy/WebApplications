using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublisherData.DataMapper;
using PublisherDomain;
using System.Runtime.InteropServices;

namespace PublisherData.Repo
{
    public partial class AuthorRepository : Repository<Author>
    {
        private readonly IMapper _mapper;

        public AuthorRepository(PublisherDBContext dBContext) 
            : base(dBContext)
        {
            _mapper = EntityMapper.Instance;
        }

       public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
           var authors =   await GetAll().OrderBy(a => a.FirstName)
                       .ThenBy(a => a.LastName).ToListAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);

        }

        public async Task<AuthorDto> FindAsync<Tkey>(Tkey id)
        {
           var author =  await FindAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}


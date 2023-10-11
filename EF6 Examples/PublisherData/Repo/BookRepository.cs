using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublisherData.DataMapper;
using PublisherDomain;

namespace PublisherData.Repo
{
    public class BookRepository : Repository<Book>
    {
        private readonly IMapper _mapper;

        public BookRepository(PublisherDBContext dBContext) : base(dBContext)
        {
            _mapper = EntityMapper.Instance;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var result = await GetAll().OrderBy(b=>b.Title).ToListAsync();
            return _mapper.Map<IEnumerable<BookDto>>(result);
        }
        public async Task<IEnumerable<BookAuthorDto>> GetAllBooksByAuthor(int AuthorId)
        {
            var authors = await  _dbCtx.Authors.Include(author => author.Books)
                         .Where(b => b.AuthorId == AuthorId)
                         .ToListAsync();

          return _mapper.Map<List<BookAuthorDto>>(authors);
        }
        public async Task<BookCoverDto> GetBookWithCoverDetailsAsync(int bookId)
        {
            var result = await _dbCtx.Books.Include(b => b.Cover)
                 .ThenInclude(c => c.Artists)
                 .FirstOrDefaultAsync(b => b.BookId == bookId);
            return _mapper.Map<BookCoverDto>(result);
        }
    }
}


using AutoMapper;
using PublisherDomain;

namespace PublisherData.DataMapper
{
    public class PubDataMappProfile : Profile
    {
        public PubDataMappProfile()
        {
            CreateMap<IEnumerable<Author>, IEnumerable<BookAuthorDto>>()
                .ConvertUsing((src, desc) => src.SelectMany(a => a.Books,
                (a, b) => new BookAuthorDto
                {
                    BookTitle = b.Title,
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    PublishDate = b.PublishDate,
                    BookPrice =b.BasePrice
                }).ToList());

            CreateMap<Author, AuthorDto>()
                .ForMember(a => a.Name, op => op.MapFrom(src => $"{src.FirstName}{src.LastName}"));

            CreateMap<Book, BookDto>()
                .ForMember(b => b.BookTitle, op => op.MapFrom(src => src.Title))
                .ForMember(b => b.BasePrice, op => op.MapFrom(src => src.BasePrice))
                .ForMember(b => b.PublishDate, op => op.MapFrom(src => src.PublishDate));

            CreateMap<Book, BookCoverDto>()
                .ForMember(b => b.Book, op => op.MapFrom(src => src))
                .ForMember(b => b.CoverTitle, op => op.MapFrom(src => src.Cover.DesignIdeas ))
                .ForMember(b => b.Artist, op => op.MapFrom(src => src.Cover.Artists.Select(a => $"{a.FirstName} {a.LastName}")));
        }
    }           
}


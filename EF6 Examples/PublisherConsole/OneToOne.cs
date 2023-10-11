using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public  class OneToOne
    {
        private readonly PublisherDBContext _ctx;

        public OneToOne()
        {
           
            _ctx = new PublisherDBContext();

            
        }

       public void  GetAllBooksWithTheirCovers()
        {
           var bookswithCover =   _ctx.Books.Include(b=>b.Cover).ToList();
        }

        public void GetAllBooksWithNoCover()
        {
            var bookwithoutcover = _ctx.Books
                                        .Where(b=> b.Cover == null)
                                        .ToList();

            var boostoExcmpt = new string[]{ "In God's Ear", "A Tale For the Time Being" };

            var bookswithout1 = _ctx.Books
                                   .Select(b=>b)
                                   .ToList() 
                                   .ExceptBy(boostoExcmpt, b=>b.Title)
                                   .ToList();
        }

        public void MultiLevelInclude()
        {
            var data = _ctx.Authors.AsNoTracking()
                            .Include(a => a.Books)
                            .ThenInclude(b => b.Cover)
                            .ThenInclude(c => c.Artists)
                            .FirstOrDefault(a => a.AuthorId == 1);

        }

        public void DeleteCoverFromBook()
        {
            var book = _ctx.Books.Include(_ => _.Cover)
                             .FirstOrDefault(b => b.BookId == 1);
           book.Cover = null;
           _ctx.ChangeTracker.DetectChanges();
            var shortView = _ctx.ChangeTracker.DebugView.ShortView;
        }

        public void MoveCoverFromOneBookToAnother()
        {
            var books = _ctx.Books
                         .Include(_ => _.Cover)
                         .Where(_=> (new int [] { 3,5}).Contains(_.BookId))
                         .ToList();
            books.First(books => books.Cover == null)
                      .Cover = books.Find(b => b.BookId == 3)?.Cover;

            _ctx.ChangeTracker.DetectChanges();
            var view = _ctx.ChangeTracker.DebugView.ShortView;
        }

        public void AddCoverToExistingBookWithTrackedCover()
        {
            var book = _ctx.Books.Include(b => b.Cover)
                             .FirstOrDefault(b => b.BookId == 3);
            book.Cover = new Cover { DesignIdeas="Spyraly spyral" };
            _ctx.ChangeTracker.DetectChanges();
            var view = _ctx.ChangeTracker.DebugView.ShortView;
        }

        public void PointThatGenerateInnerJoin()
        {
            var book = _ctx.Books.Include(b => b.Cover)
                                   .Where(b=> new int[] {1,2,3,4,5}.Contains(b.BookId))
                                .ToList();
            
        }

        public void EntityFunctions()
        {
            var data = _ctx.Books
                          .Where(_=> EF.Functions.Like(_.Title , "%book%") )
                          .ToList();
        }
    }
}

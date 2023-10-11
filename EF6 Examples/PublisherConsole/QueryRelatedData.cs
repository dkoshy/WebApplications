using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class QueryRelatedData
    {
        private readonly PublisherDBContext _ctx;
        public QueryRelatedData()
        {
            _ctx = new PublisherDBContext();
        }

        public void CascadeDeleteInActionWhenTracked()
        {
            var author = _ctx.Authors.Include(b => b.Books)
                .FirstOrDefault(a => a.AuthorId == 2);
            if (author != null && author.Books.Count > 0)
            {
                _ctx.Books.Remove(author.Books[0]);
                _ctx.ChangeTracker.DetectChanges();
                var debugView = _ctx.ChangeTracker.DebugView.ShortView;

                //_ctx.SaveChanges();
            }

        }

        public void ModifyingRelatedDataWhenTracked()
        {
            var author = _ctx.Authors.Include(_ => _.Books)
                            .First(a => a.AuthorId == 2);
            author.Books[0].BasePrice = 12.75m;

            _ctx.ChangeTracker.DetectChanges();
            var status = _ctx.ChangeTracker.DebugView.ShortView;
        }


        public void ModifyingRelatedDataWhenNotTracked()
        {
            var author = _ctx.Authors.Include(a => a.Books)
                        .First(a => a.AuthorId == 2);
            author.Books[0].BasePrice = (decimal)12.00;
            updateDisconnected(author);
        }

        private void updateDisconnected(Author author)
        {
            using var _ctx1 = new PublisherDBContext();
            //_ctx1.Books.Update(author.Books[0]);
            _ctx1.Entry(author.Books[0]).State= EntityState.Modified;
            var status = _ctx1.ChangeTracker.DebugView.ShortView;   
        }

        public void FilterUsingRelatedData()
        {
            var recentAuthors = _ctx.Authors
                  .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2000))
                  .ToList();

        }

        //Projections - Loading Related Data

       public void Projections()
        {
            var author = _ctx.Authors
                .Select(a => new
                {
                    id = a.AuthorId,
                    Name = $"{a.LastName},{a.FirstName}",
                    a.Books,
                }).ToList();

            var status = _ctx.ChangeTracker.DebugView.ShortView;
        }

        public void ExplicitLoadCollections()
        {
            var author = _ctx.Authors
                          .Find(2) ?? new Author() { AuthorId=-1};
            _ctx.Authors.Attach(author);
            _ctx.Entry(author).Collection(a => a.Books).Load();
        }


        //if navigation property is there We also load Parent
        public void ExplicitLoadReference()
        {
            var book = _ctx.Books.FirstOrDefault() ?? new Book() { BookId =-1};
            _ctx.Books.Attach(book);
           //_ctx.Entry(book).Reference(b => b.Author).OriginalValue;
        }

    }
}


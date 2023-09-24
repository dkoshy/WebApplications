using Microsoft.EntityFrameworkCore.ChangeTracking;
using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class TrackingExample
    {
        private readonly PublisherDBContext _context;
        public TrackingExample()
        {
            _context = new PublisherDBContext();
        }

       public void Coordinator()
        {
            //InsertAuthor();
            //RetrieveAndUpdateAuthor();
            //DeleteAnAuthor();
            RetrieveAndUpdateMultipleAuthors();
            CoordinatedRetrieveAndUpdateAuthor();
            VariousOperations();
            InsertMultipleAuthors();
        }

        private void InsertMultipleAuthors()
        {
            var newAuthors = new Author[]{
                   new Author { FirstName = "Ruth", LastName = "Ozeki" },
                   new Author { FirstName = "Sofia", LastName = "Segovia" },
                   new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
                   new Author { FirstName = "Hugh", LastName = "Howey" },
                   new Author { FirstName = "Isabelle", LastName = "Allende" }
                };
            _context.Authors.AddRange(newAuthors);
            _context.SaveChanges();
        }

        private void VariousOperations()
        {
            var author = _context.Authors.Find(6);
            if(author != null) 
                  author.LastName = "Newfoundland";

            var newauthor = new Author { LastName = "Appleman", FirstName = "Dan" };
            _context.Authors.Add(newauthor);
            _context.SaveChanges();
        }

        private void CoordinatedRetrieveAndUpdateAuthor()
        {
            //Disconnected approch and updating author
            var findauthor = findThatAuthor(3);
            if (findauthor != null)
                updateThatAuthor(findauthor);
        }

        private void updateThatAuthor(Author findauthor)
        {
            using var shortContext = new PublisherDBContext();
            findauthor.LastName = "Molamootil";
            shortContext.Authors.Update(findauthor);
            shortContext.SaveChanges();
        }

        private Author? findThatAuthor(int v)
        {
            using var shortContext = new PublisherDBContext();
            return shortContext.Authors.Find(v);

        }

        private void RetrieveAndUpdateMultipleAuthors()
        {
            var name = "koshy";
            var authors = _context.Authors.Where(a=>a.LastName == name)
                                .ToList();
            foreach(var author in authors)
            {
                author.LastName = "K";
                
            }
            Console.WriteLine($"Before + {_context.ChangeTracker.DebugView.ShortView}");
            _context.ChangeTracker.DetectChanges(); //By default savechanges will invoke detect changes.
            Console.WriteLine($"After + {_context.ChangeTracker.DebugView.ShortView}");
            _context.SaveChanges();
        }

        private void DeleteAnAuthor()
        {
            var don = _context.Authors.Find(5);
            if (don != null)
                _context.Authors.Remove(don);
            _context.SaveChanges();
        }

        private void RetrieveAndUpdateAuthor()
        {
            var deepu = _context.Authors.Find(1);
            if (deepu != null)
                deepu.FirstName = "Deepak";
            _context.SaveChanges();
        }

        private void InsertAuthor()
        {
            var author = new Author { FirstName = "Frank", LastName="Herbert" };
            _context.Add(author);
            _context.SaveChanges();
        }
    }
}

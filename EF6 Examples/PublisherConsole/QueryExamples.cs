using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class QueryExamples
    {

        public void Coordinator()
        {
            PublisherDBContext dBContext = new PublisherDBContext();

            //QueryFilters(dBContext);
            //FindIt(dBContext);
            // AddSomeMoreAuthors(dBContext);
            //SkipAndTakeAuthors(dBContext);
            //SortAuthors(dBContext);
            //QueryAggregate(dBContext);
        }

        private void QueryAggregate(PublisherDBContext dBContext)
        {
           var author = dBContext.Authors.FirstOrDefault(a=> a.LastName == "Lerman");
            Console.WriteLine($"{author?.FirstName},{author?.LastName}");
            author = dBContext.Authors
                        .OrderBy(a => a.LastName)
                        .LastOrDefault(a=> EF.Functions.Like(a.FirstName,"d%"));
            Console.WriteLine(author?.FirstName);
        }

        private void SortAuthors(PublisherDBContext dBContext)
        {
            var authors = dBContext.Authors
                             .OrderBy(a => a.LastName)
                             .ThenByDescending(a => a.FirstName)
                             .ToList();
            foreach(var author in authors)
            {
                Console.WriteLine($"{author.LastName},{author.FirstName}");
            }
        }

        private void SkipAndTakeAuthors(PublisherDBContext dBContext)
        {
            var authors = dBContext.Authors
                             .Skip(2).Take(5);
            foreach(var auth in authors)
            {
                Console.WriteLine(auth.FirstName);
            }
        }

        private void AddSomeMoreAuthors(PublisherDBContext dBContext)
        {
            dBContext.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
            dBContext.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
            dBContext.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
            dBContext.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
            dBContext.SaveChanges();
        }

        private void FindIt(PublisherDBContext dBContext)
        {
            var author = dBContext.Authors.Find(2);
            Console.WriteLine(author?.FirstName);
        }

        private void QueryFilters(PublisherDBContext dBContext)
        {
            var filter = "d";
            /* var authors = dBContext.Authors
                              .Where(a => a.FirstName != null &&  a.FirstName.StartsWith(filter))
                              .ToList();*/
            filter="k%";
           var authors = dBContext.Authors
                                    .Where(a=>EF.Functions.Like(a.FirstName,filter))
                                    .ToList();

            foreach (var a in authors)
            {
                Console.WriteLine($"{a.LastName},{a.FirstName}");
            }
        }
    }
}

using PublisherData;

namespace PublisherConsole
{
    public  class OneToManyExample
    {
       
        public  void  ReadRelatedData()
        {
           using var ctx = new PublisherDBContext();

            var authorwithBooks = ctx.Authors
                                     .Where(a => a.Books.Any())
                                     .ToList();
            foreach (var author in authorwithBooks)
            {
                Console.WriteLine($"{author.FirstName} , {author.LastName}");
            }
        }
    }
}

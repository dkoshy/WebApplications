using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using (var dbContext = new PublisherDBContext())
{
    dbContext.Database.EnsureCreated();
}

//GetAuthors();
//AddAuthor();
//GetAuthors();
//AddAuthorWithBook();
GetAuthorsWithBooks();


void GetAuthors()
{
    var ctx = new PublisherDBContext();
    var query = ctx.Authors;
    foreach(var author in query)
    {
        Console.WriteLine($"{author.FirstName}, {author.LastName}");
    }
}

void AddAuthor()
{
    var ctx = new PublisherDBContext();
    ctx.Authors.Add(new Author { FirstName="Deepak", LastName="Koshy" });      
    ctx.SaveChanges();
}


void AddAuthorWithBook()
{
    var ctx = new PublisherDBContext();
    ctx.Authors.Add(new Author { FirstName="Thomas", LastName="Koshy", Books=new List<Book>() { new Book { Title="History of Thazyayill." } } });
    ctx.SaveChanges();
}


void GetAuthorsWithBooks()
{
    var ctx = new PublisherDBContext();
    var authors = ctx.Authors.Include(a => a.Books).ToList();

    foreach(var author in authors)
    {
        if(author.Books.Count > 0)
        Console.WriteLine($"{author.FirstName}, {author.LastName}");
        foreach(var book in author.Books)
        {
            Console.WriteLine($"-- {book.Title} --");
        }
       
    }

}


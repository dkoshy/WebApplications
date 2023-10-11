using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData
{
    public class PublisherDBContext : DbContext
    {
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Cover> Covers { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<ArtistCover> ArtistCovers { get; set; } = null!;

        public PublisherDBContext()
        {
            
        }
        public PublisherDBContext(DbContextOptions<PublisherDBContext> options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                          "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
                          )
                          .LogTo(Console.WriteLine
                          , new[] { DbLoggerCategory.Database.Command.Name }
                          , LogLevel.Information)
                         .EnableSensitiveDataLogging();
            }
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .Property(b => b.AuthorFK)
                        .HasColumnName("AuthorId");
            modelBuilder.Entity<Book>()
                        .Property(b => b.BasePrice)
                        .HasPrecision(10, 2);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne()
                .HasForeignKey(b => b.AuthorFK)
                .IsRequired(false);

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Covers)
                .WithMany(c => c.Artists)
                .UsingEntity<ArtistCover>(
                 p => p.Property(ac => ac.DateCreated).HasDefaultValueSql("GetDate()")); 
            
            modelBuilder.Entity<Author>().HasData(new Author { AuthorId =1, FirstName="Roshny", LastName="Koshy" });

            var authors = new List<Author>()
            {
                new Author {AuthorId = 2, FirstName = "Ruth", LastName = "Ozeki" },
                new Author {AuthorId = 3, FirstName = "Sofia", LastName = "Segovia" },
                new Author {AuthorId = 4, FirstName = "Ursula K.", LastName = "LeGuin" },
                new Author {AuthorId = 5, FirstName = "Hugh", LastName = "Howey" },
                new Author {AuthorId = 6, FirstName = "Isabelle", LastName = "Allende" }
            };

            modelBuilder.Entity<Author>().HasData(authors);

            var someBooks = new Book[]{
                new Book {BookId = 1, AuthorFK=1, Title = "In God's Ear",
                    PublishDate= new DateTime(1989,3,1) },
                new Book {BookId = 2, AuthorFK=2, Title = "A Tale For the Time Being",
                PublishDate = new DateTime(2013,12,31) },
                new Book {BookId = 3, AuthorFK=3, Title = "The Left Hand of Darkness",
                PublishDate=(DateTime)new DateTime(1969,3,1)} };


            modelBuilder.Entity<Book>().HasData(someBooks);

            var someArtists = new Artist[]{
                new Artist {ArtistId = 1, FirstName = "Pablo", LastName="Picasso"},
                new Artist {ArtistId = 2, FirstName = "Dee", LastName="Bell"},
                new Artist {ArtistId = 3, FirstName ="Katharine", LastName="Kuharic"} };
            modelBuilder.Entity<Artist>().HasData(someArtists);

            var someCovers = new Cover[]{
                new Cover {CoverId = 1, BookId=1,DesignIdeas="How about a left hand in the dark?", DigitalOnly=false},
                new Cover {CoverId = 2, BookId =2, DesignIdeas= "Should we put a clock?", DigitalOnly=true},
                new Cover {CoverId = 3, BookId =3,DesignIdeas="A big ear in the clouds?", DigitalOnly = false}};
            modelBuilder.Entity<Cover>().HasData(someCovers);

        }
    }
}

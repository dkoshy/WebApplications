using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class ManyToMany
    {
        private readonly PublisherDBContext _ctx;
        public ManyToMany()
        {
            _ctx = new PublisherDBContext();
        }

        public void CreateNewCoverAndArtistTogether()
        {
            var artist = new Artist { FirstName = "Kir", LastName = "Talmage" };
            var newCover = new Cover { DesignIdeas = "We like birds!" };

            _ctx.Artists.Add(artist);
            _ctx.Covers.Add(newCover);
            _ctx.SaveChanges();
        }

        public void CreateNewCoverWithExistingArtist()
        {
            var artist = _ctx.Artists.First();
            artist.Covers.Add(new Cover { DesignIdeas = "Author has provided a photo" });
            _ctx.ChangeTracker.DetectChanges();
            var status = _ctx.ChangeTracker.DebugView.ShortView;
            _ctx.SaveChanges();
        }


        public void ConnectExistingArtistAndCoverObjects()
        {
            var cover = _ctx.Covers.Find(1);
            var artistA = _ctx.Artists.Find(1);
            var artistB = _ctx.Artists.Find(2);

            artistA.Covers.Add(cover);
            artistB.Covers.Add(cover);

            _ctx.SaveChanges();
        }

        public void RetrieveAllArtistsWhoHaveCovers()
        {
            var artists = _ctx.Artists.Include(b => b.Covers)
                                    .ToList();
            foreach (var a in artists)
            {
                Console.WriteLine($"{a.FirstName} {a.LastName}, Designs to work on:");
                var primaryArtist = a.ArtistId;

                if (a.Covers.Count == 0)
                {
                    Console.WriteLine("No cover.");
                }
                else
                {
                    foreach (var c in a.Covers)
                    {
                        string collaborators = "";
                        var Associates = c.Artists.Where(a => a.ArtistId != primaryArtist).ToList();

                        Associates.ForEach(a => collaborators += $"{a.FirstName} {a.LastName}");
                        if (collaborators.Length > 0)
                        {
                            collaborators = $"with {collaborators}";
                        }
                            Console.WriteLine($"  *{c.DesignIdeas} {collaborators}");
                        

                    }
                }
            }
        }

        public void RetrieveACoverWithItsArtists()
        {
            var coverWithArtists = _ctx.Covers.Include(c => c.Artists)
                                        .FirstOrDefault(c => c.CoverId == 1);
        }

        public void RetrieveAnArtistWithTheirCovers()
        {
            var artists = _ctx.Artists.Include(a => a.Covers)
                                   .FirstOrDefault(a => a.ArtistId == 1);
        }

        public void ReassignACover()
        {
            var artist1 = _ctx.Artists
                            .Include(a => a.Covers.Where(c => c.CoverId == 1))
                            .FirstOrDefault(a => a.ArtistId == 1);
            var artist2 = _ctx.Artists.Find(3);
            var cover = artist1.Covers.First();
            artist1.Covers.RemoveAt(0);
            artist2.Covers.Add(cover);

            _ctx.ChangeTracker.DetectChanges();
            var status = _ctx.ChangeTracker.DebugView.ShortView;

            _ctx.SaveChanges();
        }

        public void RemovingAllCoverOfExistingartist()
        {
            var artist = _ctx.Artists.Include(a => a.Covers)
                             .FirstOrDefault(a => a.ArtistId == 1);
            artist.Covers=null;
            _ctx.SaveChanges();
        }

        public void RemovingAllCovverOfExsistingWithSJoinClass()
        {
            var artist = _ctx.Artists.Find(1);
            var artistCover = _ctx.ArtistCovers.Where(a => a.ArtistId == artist.ArtistId)
                                    .ToList();
            _ctx.ArtistCovers.RemoveRange(artistCover);

            _ctx.SaveChanges(); 
        }

        //DeleteAnObjectThatsInARelationship();
        public void DeleteAnObjectThatsInARelationship()
        {
            var cover = _ctx.Covers.Find(1);
            _ctx.Covers.Remove(cover);
            _ctx.SaveChanges();
        }

        public void UnAssignAnArtistFromACover()
        {
            var artist = _ctx.Artists.Include(a => a.Covers)
                                .FirstOrDefault(a=> a.ArtistId == 3);
            artist.Covers.Remove(artist.Covers[0]);
            _ctx.ChangeTracker.DetectChanges();

            var status = _ctx.ChangeTracker.DebugView.ShortView;
        }
    }
}
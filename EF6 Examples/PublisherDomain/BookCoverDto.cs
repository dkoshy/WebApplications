using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherDomain
{
    public class BookCoverDto
    {
        public BookCoverDto()
        {
            Artist = new List<string>();
        }
        public BookDto Book { get; set; }= null!;
        public string CoverTitle { get; set; } = string.Empty;
        public List<string> Artist { get;}
    }
}

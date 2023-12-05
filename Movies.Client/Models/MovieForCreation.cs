using System.Text;
using Movies.Client.Helpers;

namespace Movies.Client.Models;

public class MovieForCreation
{

  public string Title { get; set; }

  public string Description { get; set; }

  public string Genre { get; set; }

  public DateTime? ReleaseDate { get; set; }

  public Guid? DirectorId { get; set; }

  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.Append("class MovieForCreation {\n");
    sb.Append("  Title: ").Append(Title).Append("\n");
    sb.Append("  Description: ").Append(Description).Append("\n");
    sb.Append("  Genre: ").Append(Genre).Append("\n");
    sb.Append("  ReleaseDate: ").Append(ReleaseDate).Append("\n");
    sb.Append("  DirectorId: ").Append(DirectorId).Append("\n");
    sb.Append("}\n");
    return sb.ToString();
  }

  public string ToJson()
  {
    return this.ToJsonData();
  }

}


using System.Text;
using Movies.Client.Helpers;

namespace Movies.Client.Models;
public class Movie
{
  /// <summary>
  /// Gets or Sets Id
  /// </summary>
  public Guid? Id { get; set; }

  /// <summary>
  /// Gets or Sets Title
  /// </summary>
  public string Title { get; set; }

  /// <summary>
  /// Gets or Sets Description
  /// </summary>
  public string Description { get; set; }

  /// <summary>
  /// Gets or Sets Genre
  /// </summary>
  public string Genre { get; set; }

  /// <summary>
  /// Gets or Sets ReleaseDate
  /// </summary>

  public DateTime? ReleaseDate { get; set; }

  /// <summary>
  /// Gets or Sets Director
  /// </summary>

  public string Director { get; set; }


  /// <summary>
  /// Get the string presentation of the object
  /// </summary>
  /// <returns>String presentation of the object</returns>
  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.Append("class Movie {\n");
    sb.Append("  Id: ").Append(Id).Append("\n");
    sb.Append("  Title: ").Append(Title).Append("\n");
    sb.Append("  Description: ").Append(Description).Append("\n");
    sb.Append("  Genre: ").Append(Genre).Append("\n");
    sb.Append("  ReleaseDate: ").Append(ReleaseDate).Append("\n");
    sb.Append("  Director: ").Append(Director).Append("\n");
    sb.Append("}\n");
    return sb.ToString();
  }

  /// <summary>
  /// Get the JSON string presentation of the object
  /// </summary>
  /// <returns>JSON string presentation of the object</returns>
  public string ToJson()
  {
    return this.ToJsonData();
  }

}


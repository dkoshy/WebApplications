using System.Text;
using Movies.Client.Helpers;

namespace Movies.Client.Models;


public class Trailer
{

  public Guid? Id { get; set; }

  public Guid? MovieId { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public byte[] Bytes { get; set; }


  /// <summary>
  /// Get the string presentation of the object
  /// </summary>
  /// <returns>String presentation of the object</returns>
  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.Append("class Trailer {\n");
    sb.Append("  Id: ").Append(Id).Append("\n");
    sb.Append("  MovieId: ").Append(MovieId).Append("\n");
    sb.Append("  Name: ").Append(Name).Append("\n");
    sb.Append("  Description: ").Append(Description).Append("\n");
    sb.Append("  Bytes: ").Append(Bytes).Append("\n");
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


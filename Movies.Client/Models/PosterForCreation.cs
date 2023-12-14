using System.Text;
using Movies.Client.Helpers;

namespace Movies.Client.Models;

public class PosterForCreation {
   
    public string Name { get; set; }

    public byte[] Bytes { get; set; }

    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class PosterForCreation {\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Bytes: ").Append(Bytes).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    public string ToJson() {
      return this.ToJsonData();
    }

}


using System.Dynamic;

namespace App.DvdRental.Data.Models
{
    public class QueryCommannd
    {
        public string CommandText { get; set; } =  string.Empty;
        public ExpandoObject? Parameters { get; set; }
    }
}

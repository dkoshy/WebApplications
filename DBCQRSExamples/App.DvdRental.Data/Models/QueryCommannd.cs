using Dapper;

namespace App.DvdRental.Data.Models
{
    public class QueryCommannd
    {
        public string CommandText { get; set; } =  string.Empty;
        public DynamicParameters? Parameters { get; set; }
    }
}

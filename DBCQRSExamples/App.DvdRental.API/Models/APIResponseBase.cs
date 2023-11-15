using System.Dynamic;

namespace App.DvdRental.API.Models
{
    public class APIResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public ExpandoObject Metadata { get; set; } = new ExpandoObject();
    }
}
using System.Dynamic;

namespace App.DvdRental.API.Models
{

    public class APIResponse<T> : APIResponseBase
    {
        public T? ResultSet { get; set; }
    }
}

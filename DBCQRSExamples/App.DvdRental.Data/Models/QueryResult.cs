namespace App.DvdRental.Data.Models
{
    public class QueryResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

    }
}

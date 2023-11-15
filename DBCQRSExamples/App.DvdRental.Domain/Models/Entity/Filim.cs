namespace App.DvdRental.Domain.Models.Entity
{
    public class Film
    {
        public int film_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int length { get; set; }
        public string rating { get; set; }
    }
}

﻿namespace App.DvdRental.Domain.Models.Entity
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }
}

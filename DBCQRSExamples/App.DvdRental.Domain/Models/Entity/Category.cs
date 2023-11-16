using System.ComponentModel.DataAnnotations;

namespace App.DvdRental.Domain.Models.Entity;
public class Category
{
    [Key]
    public int Category_Id { get; set; }

    public string Name { get; set; }

    public DateTime Last_Update { get; set; }

}

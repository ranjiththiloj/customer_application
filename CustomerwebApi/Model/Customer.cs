using System.ComponentModel.DataAnnotations;
namespace MyApp.Model;

public class Customer
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Address { get; set; }

}
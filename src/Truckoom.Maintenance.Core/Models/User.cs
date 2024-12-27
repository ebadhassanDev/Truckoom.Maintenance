namespace Truckoom.Maintenance.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    public int UserId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    public string FirstName {get;set;}
    public string LastName {get;set;}
    [NotMapped]
    public string? Token { get; set; }
}

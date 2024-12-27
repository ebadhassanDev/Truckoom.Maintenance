namespace Truckoom.Maintenance.Core.Models;

using System.ComponentModel.DataAnnotations;

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
}
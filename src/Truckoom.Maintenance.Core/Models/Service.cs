namespace Truckoom.Maintenance.Core.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Service
{
    [Key] public int ServiceId { get; set; }
    [Required][MaxLength(100)] public string ServiceName { get; set; }
    [Required] public DateTime ServiceDate { get; set; }
    [NotMapped] public List<ServiceTasks> ServiceTasks { get; set; } = [];
}



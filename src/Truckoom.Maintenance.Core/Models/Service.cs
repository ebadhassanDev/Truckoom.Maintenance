namespace Truckoom.Maintenance.Core
{
    using System.ComponentModel.DataAnnotations;

    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }
        [Required]
        public DateTime ServiceDate { get; set; }
        public List<Task> Tasks { get; set; } = [];
    }
}


namespace Truckoom.Maintenance.Core
{
    using System.ComponentModel.DataAnnotations;

    public class ServiceTasks
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }

    }
}
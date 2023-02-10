using System.ComponentModel.DataAnnotations;

namespace ProjectoFinal_LR.Models
{
    public class Priority
    {
        [Key]
        public int IdPriority { get; set; }
        public string Type { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}

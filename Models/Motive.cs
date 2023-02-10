using System.ComponentModel.DataAnnotations;

namespace ProjectoFinal_LR.Models
{
    public class Motive
    {
        [Key]
        public int IdMotive { get; set; }
        public string Description { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}

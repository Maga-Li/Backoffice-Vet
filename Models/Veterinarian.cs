using System.ComponentModel.DataAnnotations;

namespace ProjectoFinal_LR.Models
{
    public class Veterinarian
    {
        [Key]
        public int IdVeterinarian { get; set; }
        public string Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}

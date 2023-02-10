using System.ComponentModel.DataAnnotations;

namespace ProjectoFinal_LR.Models
{
    public class Specie
    {
        [Key]
        public int IdSpecie { get; set; }
        public string Name { get; set; }
        public ICollection<Animal> Animals { get; set; }

        public ICollection<Breed> Breeds { get; set; }
    }
}

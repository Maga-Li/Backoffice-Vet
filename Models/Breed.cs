using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoFinal_LR.Models
{
    public class Breed
    {
        [Key]
        public int IdBreed { get; set; }
        public string Name { get; set; }
        public int IdSpecie { get; set; }
        [ForeignKey("IdSpecie")]
        public Specie Specie { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoFinal_LR.Models
{
    public class Animal
    {
        [Key]
        public int IdAnimal { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Género")]
        public int Gender { get; set; }  // 0 - Masculino 1 - Feminino
        [Display(Name = "Dono")]
        public int IdClient { get; set; }
        [ForeignKey("IdClient")]
        [Display(Name = "Cliente")]
        public Client Client { get; set; }
        [Display(Name = "Espécie")]
        public int IdSpecie { get; set; }
        [ForeignKey("IdSpecie")]
        [Display(Name = "Espécie")]
        public Specie Specie { get; set; }
        [Display(Name = "Raça")]
        public int IdBreed { get; set; }
        [ForeignKey("IdBreed")]
        [Display(Name = "Raça")]
        public Breed Breed { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
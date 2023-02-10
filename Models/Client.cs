

using System.ComponentModel.DataAnnotations;

namespace ProjectoFinal_LR.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Morada")]
        public string Address { get; set; }
        [Display(Name = "Localidade")]
        public string City { get; set; }
        [Display(Name = "Telemóvel")]
        public int Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "NIF")]
        public int TaxNumber { get; set; }
        public ICollection<Animal> Animals { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectoFinal_LR.Models
{
    public class Appointment
    {
        [Key]
        public int IdAppointment { get; set; }
		[Display(Name = "Tempo de espera")]
		public DateTime WaitingTime { get; set; }
		[Display(Name = "Data/Hora da consulta")]
		public DateTime Schedule { get; set; }
		[Display(Name = "Observações")]
		public string Observations { get; set; }
		[Display(Name = "Preço base")]
		public decimal Price { get; set; }
		[Display(Name = "Animal")]
		public int IdAnimal { get; set; }
        [ForeignKey("IdAnimal")]
        public Animal Animal { get; set; }
		[Display(Name = "Médico Veterinário")]
		public int IdVeterinarian { get; set; }
        [ForeignKey("IdVeterinarian")]
        public Veterinarian Veterinarian { get; set; }
		[Display(Name = "Motivo")]
		public int IdMotive { get; set; }
        [ForeignKey("IdMotive")]
        public Motive Motive { get; set; }
		[Display(Name = "Prioridade")]
		public int IdPriority { get; set; }
        [ForeignKey("IdPriority")]
        public Priority Priority { get; set; }


    }
    public class AppointmentEvents
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public bool AllDay { get; set; }
    }

}
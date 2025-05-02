using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionApp.Models
{
    public class Adoption
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime AdoptionDate { get; set; }
        
        // Relación con Pet (una adopción tiene una mascota)
        [Required]
        public int PetId { get; set; }
        
        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
        
        // Relación con Adopter (una adopción tiene un adoptante)
        [Required]
        public int AdopterId { get; set; }
        
        [ForeignKey("AdopterId")]
        public virtual Adopter Adopter { get; set; }
    }
}

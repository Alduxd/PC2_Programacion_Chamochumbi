using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoptionApp.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [Range(0, 30)]
        public int Age { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        
        [Required]
        public bool IsAdopted { get; set; } = false;
        
        // Relación con Adoption (una mascota puede tener una adopción)
        public virtual Adoption Adoption { get; set; }
    }
}

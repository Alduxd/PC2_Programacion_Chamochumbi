using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionApp.Models
{
    public class Adopter
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
        
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        
        // Relación con Adoption (un adoptante puede tener muchas adopciones)
        public virtual ICollection<Adoption> Adoptions { get; set; }
    }
}

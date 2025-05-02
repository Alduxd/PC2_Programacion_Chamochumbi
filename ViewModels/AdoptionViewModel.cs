using System;
using System.ComponentModel.DataAnnotations;

namespace PetAdoptionApp.ViewModels
{
    public class AdoptionViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar una mascota")]
        [Display(Name = "Mascota")]
        public int PetId { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar un adoptante")]
        [Display(Name = "Adoptante")]
        public int AdopterId { get; set; }
    }
}

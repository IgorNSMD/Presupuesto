using System.ComponentModel.DataAnnotations;

namespace Presupuesto.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido ")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {1} es requerido ")]
        public string Password { get; set; }
    }
}

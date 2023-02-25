using Microsoft.AspNetCore.Mvc;
using Presupuesto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Presupuesto.Models
{
    public class TipoCuenta //: IValidatableObject
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Ingrese valor campo {0}")]
        //[StringLength(maximumLength:50, MinimumLength =5, ErrorMessage = "Longitud del campo {0} debe ser entre 5 y 50 caracteres..")]
        //[Display(Name = "Nombre del tipo de cuenta")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VerificarExisteTipoCuenta", controller:"TiposCuentas", AdditionalFields = nameof(id))]

        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nombre != null && Nombre.Length > 0)
        //    {
        //        var primeraLetra = Nombre[0].ToString();
        //        if (primeraLetra != primeraLetra.ToUpper())
        //        {
        //            yield return new ValidationResult("La primera letra debe ser Mayúscula..",
        //                new[] { nameof(Nombre) }
        //            );
        //        }
        //    }

        //    //throw new NotImplementedException();
        //}
    }
}

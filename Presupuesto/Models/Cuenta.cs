using Presupuesto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Presupuesto.Models
{
    public class Cuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requierido")]
        [StringLength(maximumLength:50)]
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }

        [Display(Name ="Tipo de Cuenta")]
        public int TipoCuentaId { get; set; }

        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [StringLength(maximumLength: 1000)]
        public string Descripcion { get; set; }
        public string TipoCuenta { get; set; }

    }
}

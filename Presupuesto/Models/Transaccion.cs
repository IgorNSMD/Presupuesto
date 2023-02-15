using System.ComponentModel.DataAnnotations;

namespace Presupuesto.Models
{
    public class Transaccion
    {
        public int id { get; set; }
        public int UsuarioId { get; set; }

        [Display(Name = "Fecha Transacción")]
        [DataType(DataType.DateTime)]
        public DateTime FechaTransaccion { get; set; } = DateTime.Parse(DateTime.Now.ToString("g"));  // DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:MM tt")); // DateTime.Now;

        public decimal Monto { get; set; }

        [Range(1,maximum:int.MaxValue, ErrorMessage = "Debe seleccionar una categoria")]
        [Display(Name = "Categoria")]   
        public int CategoriaId { get; set; }

        [StringLength(maximumLength:1000, ErrorMessage = "La nota {1} no puede pasar de 1000 caracteres")]
        public string Nota { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
        [Display(Name = "Cuenta")]
        public int CuentaId { get; set; }



    }
}

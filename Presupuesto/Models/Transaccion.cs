using System.ComponentModel.DataAnnotations;

namespace Presupuesto.Models
{
    public class Transaccion
    {
        public int id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;

        public decimal Monto { get; set; }

        [Range(1,maximum:int.MaxValue, ErrorMessage = "Debe seleccionar una categoria")]
        public int CategoriaId { get; set; }

        [StringLength(maximumLength:1000, ErrorMessage = "La nota {1} no puede pasar de 1000 caracteres")]
        public string Nota { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
        public int CuentaId { get; set; }



    }
}

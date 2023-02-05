namespace Presupuesto.Models
{
    public class IndiceCuentasViewModel
    {
        public string TipoCuenta { get; set; }
        public IEnumerable<Cuenta> Cuentas { get; set; }

        public Decimal Balance => Cuentas.Sum(c => c.Balance);
    }
}

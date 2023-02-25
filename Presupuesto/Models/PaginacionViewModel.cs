namespace Presupuesto.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;
        private int recorsPorPagina = 5;
        private readonly int cantidadMaximaRecordsPorPagina = 50;

        public int RecordsPorPagina
        {
            get
            {
                return recorsPorPagina;
            }
            set
            {
                recorsPorPagina = 
                    (value > cantidadMaximaRecordsPorPagina) ? cantidadMaximaRecordsPorPagina : value;
            }
        }

        public int RecordsASaltar => recorsPorPagina * (Pagina - 1);
    }
}

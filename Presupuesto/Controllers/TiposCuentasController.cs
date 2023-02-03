using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Presupuesto.Models;
using Presupuesto.Servicios;

namespace Presupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {
        //private readonly string connectionString;

        //public TiposCuentasController(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetConnectionString("DefaultConecction");
        //}

        public readonly IRepositorioTipoCuentas repositorioTipoCuentas;

        public TiposCuentasController(IRepositorioTipoCuentas repositorioTipoCuentas)
        {
            this.repositorioTipoCuentas = repositorioTipoCuentas;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = 1;
            var tiposCuentas = await repositorioTipoCuentas.Obtener(usuarioId);
            return View(tiposCuentas);

        }

        public IActionResult Crear()
        {
            //string aux = "";


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta) //TipoCuenta tipoCuenta
        {

            //string aux = "1";


            //using (var connection = new SqlConnection(connectionString))
            //{
            //    var query = connection.Query("SELECT 1").FirstOrDefault();
            //}

            if (!ModelState.IsValid)
            {
                return View();
            }
            tipoCuenta.UsuarioId = 1;

            var existeTipoCuenta = await repositorioTipoCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (existeTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe.. ");
                return View(tipoCuenta);
            }

            await repositorioTipoCuentas.Crear(tipoCuenta);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;
            var existeTipoCuenta = await repositorioTipoCuentas.Existe(nombre, usuarioId);

            if (existeTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe..");
            }

            return Json(true);
        }
    }
}

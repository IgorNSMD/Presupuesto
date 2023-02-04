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
        private readonly IServicioUsuarios servicioUsuarios;

        public TiposCuentasController(IRepositorioTipoCuentas repositorioTipoCuentas,
                                      IServicioUsuarios servicioUsuarios )
        {
            this.repositorioTipoCuentas = repositorioTipoCuentas;
            this.servicioUsuarios = servicioUsuarios;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
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
            tipoCuenta.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

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
        public async Task<ActionResult> Editar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTipoCuentas.ObtenerPorId(id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(tipoCuenta);

        }

        [HttpPost]
        public async Task<ActionResult> Editar(TipoCuenta tipoCuenta)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuentaExiste = await repositorioTipoCuentas.ObtenerPorId(tipoCuenta.id, usuarioId);

            if (tipoCuentaExiste is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTipoCuentas.Actualizar(tipoCuenta);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Eliminar(int Id)
        {

            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTipoCuentas.ObtenerPorId(Id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(tipoCuenta);

        }

        [HttpPost]
        public async Task<IActionResult> EliminarTipoCuenta(int Id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTipoCuentas.ObtenerPorId(Id, usuarioId);

            if (tipoCuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioTipoCuentas.Eliminar(Id);
            return RedirectToAction("index");

        }

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var existeTipoCuenta = await repositorioTipoCuentas.Existe(nombre, usuarioId);

            if (existeTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe..");
            }

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Ordenar([FromBody] int[] ids)
        {
            return Ok();
        }
    }
}

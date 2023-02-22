using Microsoft.AspNetCore.Mvc;
using Presupuesto.Models;

namespace Presupuesto.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            return RedirectToAction("Index", "Transacciones");
        }
    }
}

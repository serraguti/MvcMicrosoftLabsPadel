using Microsoft.AspNetCore.Mvc;
using MvcMicrosoftLabsPadel.Models;
using MvcMicrosoftLabsPadel.Repositories;

namespace MvcMicrosoftLabsPadel.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Usuario> usuarios = this.repo.GetUsuarios();
            return View(usuarios);
        }

        public IActionResult Details(int iduser)
        {
            Usuario user = this.repo.FindUsuario(iduser);
            return View(user);
        }

        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(Usuario usuario)
        {
            await this.repo.AddUserAsync(usuario.Nombre
                , usuario.Apellidos, usuario.Email, usuario.FechaNacimiento);
            return RedirectToAction("Index");
        }
    }
}

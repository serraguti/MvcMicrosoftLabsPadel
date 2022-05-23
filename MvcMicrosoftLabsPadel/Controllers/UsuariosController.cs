using Microsoft.AspNetCore.Mvc;
using MvcMicrosoftLabsPadel.Models;
using MvcMicrosoftLabsPadel.Repositories;
using MvcMicrosoftLabsPadel.Services;

namespace MvcMicrosoftLabsPadel.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;
        private ServiceAzureStorage serviceAzureStorage;

        public UsuariosController
            (RepositoryUsuarios repo,
            ServiceAzureStorage serviceAzureStorage)
        {
            this.repo = repo;
            this.serviceAzureStorage = serviceAzureStorage;
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
        public async Task<IActionResult> NewUser
            (Usuario usuario, IFormFile file)
        {
            string fileName = file.FileName;
            //RECUPERAMOS EL ID DEL NEW USER INSERTADO
            //imagen1.png
            int idNewUser = await this.repo.AddUserAsync
                (usuario.Nombre, 
                usuario.Apellidos, 
                usuario.Email,
                fileName, 
                usuario.FechaNacimiento);
            //SUBIMOS EL STREAM A AZURE
            using (Stream stream = file.OpenReadStream())
            {
                await this.serviceAzureStorage.UploadBlobAsync
                    (idNewUser + "_" + fileName, stream);
            }
            return RedirectToAction("Index");
        }
    }
}

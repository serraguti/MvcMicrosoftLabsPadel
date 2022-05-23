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
        private ServiceLogicApps serviceLogicApps;

        public UsuariosController
            (RepositoryUsuarios repo,
            ServiceAzureStorage serviceAzureStorage,
            ServiceLogicApps serviceLogicApps)
        {
            this.repo = repo;
            this.serviceAzureStorage = serviceAzureStorage;
            this.serviceLogicApps = serviceLogicApps;
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
            //ADEMAS DEL MAIL, ENVIAMOS UN ENLACE PARA QUE PUEDA PULSAR Y ACTIVAR SU CUENTA
            //RECUPERAMOS NUESTRO HOST PARA QUE PULSE EL USUARIO EN NUESTRA WEB
            string host = "https://"
                + Request.Host + "/usuarios/ActiveAccount/" + idNewUser;
            string body = "Bienvenido a nuestra página de Microsoft LABS";
            body += "Debe pulsar en el siguiente enlace para activar su cuenta: <br/>";
            body += "<a href='" + host + "'>" + host + "</a><br/>";
            body += "<h1>Welcome to the Jungle!!!</h1>";
            await this.serviceLogicApps.SendMailNewUserAsync(usuario.Email, "Alta realizada correctamente"
                , body);
            return RedirectToAction("Index");
        }

        public IActionResult ActiveAccount(int id)
        {
            //MEDIANTE EL REPOSITORIO O LO QUE DESEARAMOS
            //CAMBIAMOS EL ESTADO DEL USER DE ACTIVO --> false
            //A true
            return View();
        }
    }
}

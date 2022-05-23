using MvcMicrosoftLabsPadel.Data;
using MvcMicrosoftLabsPadel.Models;

namespace MvcMicrosoftLabsPadel.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;

        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        //METODO PARA RECUPERAR TODOS LOS USUARIOS
        public List<Usuario> GetUsuarios()
        {
            return this.context.Usuarios.ToList();
        }

        //METODO PARA BUSCAR UN USUARIO PARA LOS DETALLES
        public Usuario FindUsuario(int idUser)
        {
            return this.context.Usuarios.FirstOrDefault(x => x.IdUsuario == idUser);
        }

        //METODO PRIVADO PARA RECUPERAR EL MAXIMO ID
        //DEL USUARIO AL INSERTAR
        private int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(x => x.IdUsuario) + 1;
            }
        }

        //METODO PARA CREAR NUEVOS USUARIOS
        //Y DEVOLVERA EL ID DEL NUEVO USUARIO INSERTADO
        //PARA FUTURAS ACCIONES
        public async Task<int> AddUserAsync(string nombre
            , string apellidos
            , string email
            , string imagen,
            DateTime fechaNacimiento)
        {
            Usuario user = new Usuario();
            user.IdUsuario = this.GetMaxIdUsuario();
            user.Nombre = nombre;
            user.Apellidos = apellidos;
            user.Email = email;
            //1_imagen1.png
            user.Imagen = user.IdUsuario + "_" + imagen;
            user.FechaNacimiento = fechaNacimiento;
            user.Activo = false;
            await this.context.Usuarios.AddAsync(user);
            await this.context.SaveChangesAsync();
            return user.IdUsuario;
        }
    }
}

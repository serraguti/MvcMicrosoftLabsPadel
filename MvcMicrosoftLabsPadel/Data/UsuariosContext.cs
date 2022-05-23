using Microsoft.EntityFrameworkCore;
using MvcMicrosoftLabsPadel.Models;

namespace MvcMicrosoftLabsPadel.Data
{
    public class UsuariosContext: DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options)
            : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMicrosoftLabsPadel.Models
{
    [Table("USUARIOSPADEL")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string? Nombre { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("FECHA_NACIMIENTO")]
        public DateTime FechaNacimiento { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("ACTIVO")]
        public bool Activo { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaVirtual.MVVM.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [Unique]
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
    }
}

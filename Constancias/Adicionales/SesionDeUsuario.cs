using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constancias.Adicionales {
    public class SesionDeUsuario {
        private string nombreUsuario;
        private string correo;

        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }
        public string Correo { get { return correo;  } set { correo = value;  } }
    }
}

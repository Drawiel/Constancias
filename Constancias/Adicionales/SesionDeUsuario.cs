using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constancias.Adicionales {
    public class SesionDeUsuario {
        private string nombreUsuario;
        private string numeroPersonal;

        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }
        public string NumeroPersonal { get { return numeroPersonal;  } set { numeroPersonal = value;  } }
    }
}

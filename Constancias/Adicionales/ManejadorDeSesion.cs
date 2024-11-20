using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constancias.Adicionales {
    public class ManejadorDeSesion {
        private static ManejadorDeSesion instancia;
        private SesionDeUsuario usuario;

        public static ManejadorDeSesion GetInstancia() {
            if (instancia == null) {
                instancia = new ManejadorDeSesion();
            }
            return instancia;
        }

        public void IniciarSesion(SesionDeUsuario usuario) {
            this.usuario = usuario;
        }

        public SesionDeUsuario GetUsuario() {
            return this.usuario;
        }

        public void SalirDeSesion() {
            this.usuario = null;
        }
    }
}

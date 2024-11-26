using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Clases {
    public class ParticipacionDTO {
        public int IdProyectoCampo { get; set; }
        public string Nombre { get; set; }
        public int IdPeriodo { get; set; }
        public int IdTipoParticipacion { get; set; }
        public int IdProgramaEducativo { get; set; }
        public int IdAcademico { get; set; }
    }
}

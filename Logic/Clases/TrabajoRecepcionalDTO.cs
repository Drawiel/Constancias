using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Clases {
    public class TrabajoRecepcionalDTO {
        public int IdTrabajoRecepcional { get; set; }
        public string NombreEstudiante { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int IdAcademico { get; set; }
        public int IdTipoTrabajo { get; set; }
        public int IdRolAcademico { get; set; }
    }
}

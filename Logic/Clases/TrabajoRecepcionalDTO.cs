using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Clases {
    public class TrabajoRecepcionalDTO {
        public int IdTrabajoRecepcional { get; set; }
        public string NombreEstudiante { get; set; }
        public string Titulo {  get; set; }
        public string FechaPublicacion { get; set; }
        public int IdAcademico { get; set; }
        public string TipoTrabajo { get; set; }
        public string RolAcademico { get; set; }
    }
}

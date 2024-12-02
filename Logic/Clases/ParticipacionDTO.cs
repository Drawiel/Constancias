using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Clases {
    public class ParticipacionDTO {
        public int IdParticipacion { get; set; }
        public string Nombre { get; set; }
        public string PeriodoParticipacion { get; set; }
        public string TipoParticipacion { get; set; }
        public int IdProgramaEducativo { get; set; }
        public int IdAcademico { get; set; }
        public string nombreAcademico { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class participacion
    {
        public int idProyectoCampo { get; set; }
        public string nombre { get; set; }
        public Nullable<int> idPeriodo { get; set; }
        public Nullable<int> idTipoParticipacion { get; set; }
        public Nullable<int> idProgramaEducativo { get; set; }
        public Nullable<int> idAcademico { get; set; }
    
        public virtual academico academico { get; set; }
        public virtual periodo periodo { get; set; }
        public virtual programaeducativo programaeducativo { get; set; }
        public virtual tipoparticipacion tipoparticipacion { get; set; }
    }
}

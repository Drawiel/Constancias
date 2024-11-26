using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Clases {
    public class ProductoAcademicoDTO {
        public int IdProducto { get; set; }
        public string Titulo { get; set; }
        public string FechaPublicacion { get; set; }
        public string TipoProducto { get; set; }
        public string TipoPublicacion { get; set; }
        public int IdAcademico { get; set; }
    }
}

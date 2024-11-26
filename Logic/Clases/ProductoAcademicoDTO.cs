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
        public int IdTipoProducto { get; set; }
        public int IdTipoPublicacion { get; set; }
        public int IdAcademico { get; set; }
    }
}

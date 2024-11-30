using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Constancias.Adicionales
{
    public class MensajesError
    {
        public void ObtenerMensajeErrorAcademico(int resultado)
        {
            switch (resultado)
            {
                case 1:
                    MessageBox.Show("Académico agregado correctamente.");
                    break;
                case -3:
                    MessageBox.Show("Error: Ya existe un académico con el mismo número personal.");
                    break;
                case -1:
                    MessageBox.Show("Error en la base de datos al agregar el académico.");
                    break;
                case -2:
                    MessageBox.Show("Error general al agregar el académico.");
                    break;
                default:
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                    break;
            }
        }

        public void ObtenerMensajeErrorExperienciaEducativa(int resultado) 
        {
            switch (resultado)
            {
                case 1:
                    MessageBox.Show("Experiencia educativa agregada correctamente.");
                    break;
                case -3:
                    MessageBox.Show("Error: Ya existe una experiencia educativa con el mismo nombre.");
                    break;
                case -1:
                    MessageBox.Show("Error en la base de datos al agregar la experiencia educativa.");
                    break;
                case -2:
                    MessageBox.Show("Error general al agregar la experiencia educativa.");
                    break;
                default:
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                    break;
            }
        }

        public void ObtenerMensajeErrorParticipacion(int resultado) 
        {
            switch (resultado)
            {
                case 1:
                    MessageBox.Show("Participación agregada correctamente.");
                    break;
                case -3:
                    MessageBox.Show("Error: Ya existe una participación con datos duplicados.");
                    break;
                case -1:
                    MessageBox.Show("Error en la base de datos al agregar la participación.");
                    break;
                case -2:
                    MessageBox.Show("Error general al agregar la participación.");
                    break;
                default:
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                    break;
            }
        }

        public void ObtenerMensajeErrorProductoAcademico(int resultado) 
        {
            switch (resultado)
            {
                case 1:
                    MessageBox.Show("Producto académico agregado correctamente.");
                    break;
                case -3:
                    MessageBox.Show("Error: Ya existe un producto académico con datos duplicados.");
                    break;
                case -1:
                    MessageBox.Show("Error en la base de datos al agregar el producto académico.");
                    break;
                case -2:
                    MessageBox.Show("Error general al agregar el producto académico.");
                    break;
                default:
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                    break;
            }
        }

        
        public void ObtenerMensajeErrorProyectoCampo(int resultado) 
        {
            switch (resultado)
            {
                case 1:
                    MessageBox.Show("Proyecto de campo agregado correctamente.");
                    break;
                case -3:
                    MessageBox.Show("Error: Ya existe un proyecto de campo con datos duplicados.");
                    break;
                case -1:
                    MessageBox.Show("Error en la base de datos al agregar el proyecto de campo.");
                    break;
                case -2:
                    MessageBox.Show("Error general al agregar el proyecto de campo.");
                    break;
                default:
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                    break;
            }
        }

        public void ObtenerMensajeErrorTrabajoRecepcional(int resultado) 
        {
            switch (resultado)
            {
                case 1:
                    MessageBox.Show("Trabajo recepcional agregado correctamente.");
                    break;
                case -3:
                    MessageBox.Show("Error: Ya existe un trabajo recepcional con datos duplicados.");
                    break;
                case -1:
                    MessageBox.Show("Error en la base de datos al agregar el trabajo recepcional.");
                    break;
                case -2:
                    MessageBox.Show("Error general al agregar el trabajo recepcional.");
                    break;
                default:
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                    break;
            }
        }



    }

}


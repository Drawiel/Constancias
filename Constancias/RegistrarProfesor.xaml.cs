using Constancias.Adicionales;
using Logic.Clases;
using Logic.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Constancias
{
    /// <summary>
    /// Lógica de interacción para RegistrarProfesor.xaml
    /// </summary>
    public partial class RegistrarProfesor : Window
    {
        List<ExperienciaEducativaDTO> eeList = new List<ExperienciaEducativaDTO>();
        List<ParticipacionDTO> participacionList = new List<ParticipacionDTO>();
        List<TrabajoRecepcionalDTO> trabajoRecepcionalList = new List<TrabajoRecepcionalDTO>();
        List<ProductoAcademicoDTO> productoAcademicoList = new List<ProductoAcademicoDTO>();
        List<ProyectoDeCampoDTO> proyectoCampoList = new List<ProyectoDeCampoDTO>();

        public RegistrarProfesor()
        {
            InitializeComponent();
        }

        private void ToggleEducationVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(stackPannelProgramasEducativosActualizados);
        }

        private void ToggleCertificationVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(stackPanelCertificacion);
        }

        private void ToggleProductVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(stackPanelProductoAcademico);
        }

        private void ToggleProjectVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(stackPanelProyectoCampo);
        }

        private void ToggleThesisVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(stackPanelTrabajoRecepcional);
        }

        // Helper method to toggle visibility and collapse others
        private void ToggleVisibility(StackPanel panel)
        {
            // Collapse all panels first
            stackPannelProgramasEducativosActualizados.Visibility = Visibility.Collapsed;
            stackPanelCertificacion.Visibility = Visibility.Collapsed;
            stackPanelProductoAcademico.Visibility = Visibility.Collapsed;
            stackPanelProyectoCampo.Visibility = Visibility.Collapsed;
            stackPanelTrabajoRecepcional.Visibility = Visibility.Collapsed;

            // Toggle visibility of the selected panel
            panel.Visibility = panel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        


        private int AgregarAcademico()
        {
            string nombreAcademico = textBoxNombreAcademico.Text;
            var seletedTipoContratacion = comboBoxTipoContratacion.SelectedItem as ComboBoxItem;
            string tipoContratacion = seletedTipoContratacion.Content.ToString();
            var areaAcademicaSeleccionada = comboBoxArea.SelectedItem as ComboBoxItem;
            string areaAcademica = areaAcademicaSeleccionada.Content.ToString();
            string fechaContratacion = datePickerFechaContratacion.SelectedDate?.ToString("yyyy-MM-dd");
            var programaEducativoSeleccionado = comboBoxPrograma.SelectedItem as ComboBoxItem;
            string programaEducativo = programaEducativoSeleccionado.Content.ToString();

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(nombreAcademico, tipoContratacion, areaAcademica, fechaContratacion, programaEducativo))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return -1;
            }
            AcademicoDAO academico = new AcademicoDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();
            //int idAcademico = (int) academico.ObtenerUltimoIdAcademico();

            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = nombreAcademico,
                NumeroPersonal = textBoxNumeroPersonal.Text,
                TipoContratacion = tipoContratacion,
                FechaContratacion = fechaContratacion,
                AreaAcademica = areaAcademica,
                IdPrograma = (int) programaEducativoDAO.ObtenerIdProgramaPorNombre(programaEducativo)
            };

            AcademicoDAO academicoDAO = new AcademicoDAO();
            int resultado = academicoDAO.ValidarYRegistrarAcademico(academicoDTO);

            MensajesError mensajesError = new MensajesError();
            mensajesError.ObtenerMensajeErrorAcademico(resultado);
            return resultado;
        }

        private void AgregarAsignatura(object sender, RoutedEventArgs e)
        {
            string nombreEE = NombreAsignaturaTextBox.Text;
            var nombreProgramaSeleccionado = comboBoxProgramaEducativo.SelectedItem as ComboBoxItem;
            string nombrePrograma = nombreProgramaSeleccionado?.Content.ToString();
            Console.WriteLine(nombrePrograma);

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(nombreEE, nombrePrograma))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ExperienciaEducativaDAO eeDAO = new ExperienciaEducativaDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            

            int idProgramaEducativo = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(nombrePrograma);
            Console.WriteLine("Id programaL " + idProgramaEducativo);

            ExperienciaEducativaDTO experienciaEducativaDTO = new ExperienciaEducativaDTO
            {
                IdProgramaEducativo = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(nombrePrograma),
                Nombre = nombreEE
            };
            
            eeList.Add(experienciaEducativaDTO);

            


            int resultado = eeDAO.ObtenerORegistrarExperiencia(experienciaEducativaDTO);

            MensajesError mensajesError = new MensajesError();
            mensajesError.ObtenerMensajeErrorExperienciaEducativa(resultado);
        }

        private void RegistrarTrabajo(object sender, RoutedEventArgs e)
        {
            string numeroPersonal = textBoxNumeroPersonal.Text;
            var tipoTrabajoSeleccionado = comboBoxTipoTrabajo.SelectedItem as ComboBoxItem;
            string tipoTrabajo = tipoTrabajoSeleccionado?.Content.ToString();
            string titulo = textBoxTituloTrabajo.Text;
            var rolAcademicoSeleccionado = comboBoxRolAcademicoTrabajo.SelectedItem as ComboBoxItem;
            string rolAcademico = rolAcademicoSeleccionado?.Content.ToString();
            string nombreEstudiante = tetxBoxNombreEstudianteTrabajoRecepcional.Text;
            string fechaPresentacion = datePickerFechaPresentacionTrabajo.SelectedDate?.ToString("yyyy-MM-dd");

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(numeroPersonal, tipoTrabajo, titulo, rolAcademico, nombreEstudiante, fechaPresentacion))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validación explícita adicional
            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(nombreEstudiante))
            {
                MessageBox.Show("El título del trabajo y el nombre del estudiante son obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AcademicoDAO academicoDAO = new AcademicoDAO();
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();

            // Crear el objeto DTO
            TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
            {
                Titulo = titulo,
                NombreEstudiante = nombreEstudiante,
                FechaPublicacion = fechaPresentacion,
                RolAcademico = rolAcademico,
                TipoTrabajo = tipoTrabajo
            };

            // Depuración para verificar los valores
            Console.WriteLine($"Titulo: {trabajoRecepcionalDTO.Titulo}, NombreEstudiante: {trabajoRecepcionalDTO.NombreEstudiante}, FechaPublicacion: {trabajoRecepcionalDTO.FechaPublicacion}");

            // Guardar en la lista (o base de datos si aplica)
            trabajoRecepcionalList.Add(trabajoRecepcionalDTO);
            stackPanelTrabajoRecepcional.Visibility = Visibility.Hidden;
           
        }


        private void RegistrarProducto(object sender, RoutedEventArgs e)
        {
            string tituloProducto = textBoxTituloProducto.Text;
            var tipoProductoSeleccionado = comboBoxTipoProducto.SelectedItem as ComboBoxItem;
            string tipoProducto = tipoProductoSeleccionado.Content.ToString();
            string fechaPublicacion = datePickerFechaPublicacion.SelectedDate?.ToString("yyyy-MM-dd");
            var tipoPublicacionSeleccionad = comboBoxTipoPublicaiconProducto.SelectedItem as ComboBoxItem;
            string tipoPublicacion = tipoPublicacionSeleccionad.Content.ToString();
            
            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(tituloProducto, tipoProducto, fechaPublicacion, tipoPublicacion))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();

            ProductoAcademicoDTO productoAcademicoDTO = new ProductoAcademicoDTO
            {
                Titulo = tituloProducto,
                FechaPublicacion = fechaPublicacion,
                TipoProducto = tipoProducto,
                TipoPublicacion = tipoPublicacion
            };

            productoAcademicoList.Add(productoAcademicoDTO);
            stackPanelProductoAcademico.Visibility = Visibility.Hidden;
        }

        private void RegistrarProyectoCampo(object sender, RoutedEventArgs e)
        {
            string nombreProyecto = textBoxNombreProyecto.Text;
            var rolSeleccionado = comboBoxRolAcademicoProyecto.SelectedItem as ComboBoxItem;
            string rol = rolSeleccionado.Content.ToString();
            string lugarRealizacion = textBoxPeriodoRealizacion.Text;
            string periodo = $"{datePickerPeriodoRealizacionProyecto.SelectedDate?.ToString("yyyy-MM-dd")} - {datePickerOtroPeriodoRealizacion.SelectedDate?.ToString("yyyy-MM-dd")}";
            string numeroPersonal = textBoxNumeroPersonal.Text;

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(nombreProyecto, rol, lugarRealizacion, periodo, numeroPersonal))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AcademicoDAO academicoDAO = new AcademicoDAO();
            ProyectoDeCampoDAO proyectoCampoDAO = new ProyectoDeCampoDAO();

            

            ProyectoDeCampoDTO proyectoDTO = new ProyectoDeCampoDTO
            {
                Nombre = nombreProyecto,
                LugarRealizacion = lugarRealizacion,
                Periodo = periodo,
                RolAcademico = rol,
            };

            proyectoCampoList.Add(proyectoDTO);
            stackPanelProyectoCampo.Visibility = Visibility.Hidden;
        }


        


        private void AbrirPopupAsignatura(object sender, RoutedEventArgs e)
        {
            PopupIngresarAsignatura.IsOpen = true; // Abre el Popup
        }


        private void AsignarExperienciaEducativa()
        {
            // Recuperar el número personal desde la interfaz
            string numeroPersonal = textBoxNumeroPersonal.Text;

            // Crear instancias de DAO
            AcademicoDAO academicoDAO = new AcademicoDAO();
            ExperienciaEducativaDAO experienciaEducativaDAO = new ExperienciaEducativaDAO();

            // Obtener el ID del académico a partir del número personal
            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                Console.WriteLine("No se encontró el académico.");
                return; // Terminar si el académico no existe
            }

            // Lista de nombres de experiencias educativas ingresadas
            List<string> eeNombres = eeList.Select(ee => ee.Nombre).ToList();

            // Obtener IDs de experiencias educativas
            List<int> eeIds = ObtenerIdsExperiencias(eeNombres);

            if (eeIds.Any())
            {
                Console.WriteLine("Asignando experiencias educativas al académico...");
                // Lógica para asignar las experiencias educativas al académico
                foreach (var idExperiencia in eeIds)
                {
                    experienciaEducativaDAO.RegistrarRelacionAcademicoExperiencia(idAcademico, idExperiencia);
                    Console.WriteLine($"Asignando experiencia educativa con ID: {idExperiencia} al académico con ID: {idAcademico}");
                    // Aquí podrías llamar al método que guarda en la base de datos
                }
            }
            else
            {
                Console.WriteLine("No se encontraron experiencias educativas para asignar.");
            }
        }


        public List<int> ObtenerIdsExperiencias(List<string> nombresExperiencias)
        {
            var listaIds = new List<int>();
            ExperienciaEducativaDAO experienciaEducativaDAO = new ExperienciaEducativaDAO();

            foreach (var nombre in nombresExperiencias)
            {
                // Obtener el ID por nombre
                var id = experienciaEducativaDAO.ObtenerIdExperienciaPorNombre(nombre);

                // Si el ID no es null, agregarlo a la lista
                if (id.HasValue)
                {
                    listaIds.Add(id.Value);
                }
                else
                {
                    Console.WriteLine($"No se encontró la experiencia educativa con nombre: {nombre}");
                }
            }

            return listaIds;
        }

        private void RegistrarActualizacion(Object sender, RoutedEventArgs e)
        {
            // Recuperar datos de los controles
            var programaEducativoSeleccionado = comboBoxProgramaEducativoActualizado.SelectedItem as ComboBoxItem;
            string programaEducativo = programaEducativoSeleccionado.Content.ToString();
            string tipoParticipacion = "Actualización";
            string periodoInicio = actualzacionYearPicker.SelectedDate?.ToString("yyyy");
            string periodoFin = actualizaciónAnotherYear.SelectedDate?.ToString("yyyy");
            string numeroPersonal = textBoxNumeroPersonal.Text;

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(programaEducativo, periodoInicio, periodoFin, numeroPersonal))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Instanciar DAOs
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();
            AcademicoDAO academicoDAO = new AcademicoDAO();
            ParticipacionDAO participacionDAO = new ParticipacionDAO();

            int nuevoIdParticipacion = (int)(participacionDAO.ObtenerUltimoIdParticipacion() ?? 0) + 1;
            ParticipacionDTO participacionDTO = new ParticipacionDTO
            {
                IdParticipacion = nuevoIdParticipacion,
                TipoParticipacion = tipoParticipacion,
                PeriodoParticipacion = $"{periodoInicio}-{periodoFin}",
                IdProgramaEducativo = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(programaEducativo)
            };

            // Agregar la participación a la lista temporal
            participacionList.Add(participacionDTO);
            stackPannelProgramasEducativosActualizados.Visibility = Visibility.Hidden;


            

        
        }


        private void RegistrarCertificacion(object sender, RoutedEventArgs e)
        {
            // Recuperar datos de los controles
            var programaEducativoSeleccionado = comboBoxProgramaEducativoCertificado.SelectedItem as ComboBoxItem;
            string programaEducativo = programaEducativoSeleccionado.Content.ToString();
            string tipoParticipacion = "Certificación";
            string periodoInicio = certificacionYearPicker.SelectedDate?.ToString("yyyy");
            string periodoFin = certificacionAnotherYear.SelectedDate?.ToString("yyyy");
            string numeroPersonal = textBoxNumeroPersonal.Text;

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(programaEducativo, periodoInicio, periodoFin, numeroPersonal))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Instanciar DAOs
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();
            AcademicoDAO academicoDAO = new AcademicoDAO();
            ParticipacionDAO participacionDAO = new ParticipacionDAO();

            // Obtener IDs relacionados
            int idPrograma = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(programaEducativo);
            

            int nuevoIdParticipacion = (int)(participacionDAO.ObtenerUltimoIdParticipacion() ?? 0) + 1;

            // Crear DTO de participación
            ParticipacionDTO participacionDTO = new ParticipacionDTO
            {
                IdParticipacion = nuevoIdParticipacion,
                TipoParticipacion = tipoParticipacion,
                PeriodoParticipacion = $"{periodoInicio}-{periodoFin}",
                IdProgramaEducativo = idPrograma
            };

            participacionList.Add(participacionDTO);
            stackPanelCertificacion.Visibility = Visibility.Hidden;

        }



        private void AsignarParticipacion()
        {
            // Recuperar el número personal desde la interfaz
            string numeroPersonal = textBoxNumeroPersonal.Text;

            // Crear instancias de DAO
            AcademicoDAO academicoDAO = new AcademicoDAO();
            ParticipacionDAO participacionDAO = new ParticipacionDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            // Obtener el ID del académico a partir del número personal
            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                Console.WriteLine("No se encontró el académico.");
                return; // Terminar si el académico no existe
            }

            // Lista de nombres de programas educativos ingresados
            List<string> programaNombres = participacionList.Select(a => a.Nombre).ToList();

            // Obtener IDs de programas educativos

            if (participacionList.Any())
            {
                Console.WriteLine("Asignando participaciones al académico...");
                // Lógica para asignar las actualizaciones al académico
                foreach (var participacion in participacionList)
                {
                    Console.WriteLine(participacion.TipoParticipacion);
                    // Crear el DTO de Participacion
                    ParticipacionDTO participacionDTO = new ParticipacionDTO
                    {
                        TipoParticipacion = participacion.TipoParticipacion,
                        PeriodoParticipacion = participacion.PeriodoParticipacion,
                        IdProgramaEducativo = participacion.IdParticipacion,
                        IdAcademico = idAcademico
                    };

                    // Registrar la participación en la base de datos
                    int resultado = participacionDAO.AgregarParticipacion(participacionDTO);

                    if (resultado == 1)
                    {
                        Console.WriteLine($"Asignando actualización con ID de programa:  al académico con ID: {idAcademico}");
                    }
                    else if (resultado == -3)
                    {
                        Console.WriteLine($"Ya existe una actualización para el programa con y el académico con ID: {idAcademico}");
                    }
                    else
                    {
                        Console.WriteLine($"Error al asignar actualización para el programa con ID: ");
                    }
                }
            }
            else
            {
                Console.WriteLine("No se encontraron participaciones para asignar.");
            }
        }

        
        

        private void AsignarTrabajoRecepcional()
        {
            // Recuperar el número personal desde la interfaz
            string numeroPersonal = textBoxNumeroPersonal.Text;

            // Crear instancias de DAO
            AcademicoDAO academicoDAO = new AcademicoDAO();
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();

            // Obtener el ID del académico a partir del número personal
            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                Console.WriteLine("No se encontró el académico.");
                return; // Terminar si el académico no existe
            }

            // Crear una lista para almacenar los trabajos recepcionales
            List<TrabajoRecepcionalDTO> trabajosRecepcionales = new List<TrabajoRecepcionalDTO>();

            // Recorrer los trabajos recepcionales que se van a asignar
            foreach (var trabajo in trabajoRecepcionalList)
            {
                // Crear el DTO del trabajo recepcional
                TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
                {
                    Titulo = trabajo.Titulo,
                    NombreEstudiante = trabajo.NombreEstudiante,
                    FechaPublicacion = trabajo.FechaPublicacion,
                    IdAcademico = idAcademico,
                    TipoTrabajo = trabajo.TipoTrabajo,
                    RolAcademico = trabajo.RolAcademico
                };

                // Agregar el trabajo a la base de datos
                int resultado = trabajoRecepcionalDAO.AgregarTrabajoRecepcional(trabajoRecepcionalDTO);

                MensajesError mensajesError = new MensajesError();
                mensajesError.ObtenerMensajeErrorTrabajoRecepcional(resultado);
                Console.WriteLine(trabajoRecepcionalDTO.IdAcademico + trabajoRecepcionalDTO.NombreEstudiante + trabajoRecepcionalDTO.FechaPublicacion + trabajoRecepcionalDTO.Titulo + trabajoRecepcionalDTO.Titulo + trabajoRecepcionalDTO.RolAcademico);

                if (resultado == 1)
                {
                    Console.WriteLine("Trabajo recepcional registrado exitosamente.");
                }
                else if (resultado == -3)
                {
                    Console.WriteLine("El trabajo recepcional ya existe.");
                }
                else
                {
                    Console.WriteLine("Error al registrar el trabajo recepcional.");
                }
            }
        }

        

        private void AsignarProductoAcademico()
        {
            try
            {
                // Recuperar el número personal del académico desde la interfaz
                string numeroPersonal = textBoxNumeroPersonal.Text;

                // Crear instancias de DAO
                AcademicoDAO academicoDAO = new AcademicoDAO();
                ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();

                // Obtener el ID del académico a partir del número personal
                int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

                if (idAcademico == 0)
                {
                    MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Terminar si el académico no existe
                }

                if (productoAcademicoList.Any())
                {
                    MessageBox.Show("Asignando productos académicos al académico...", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Recorrer la lista de productos académicos
                    foreach (var producto in productoAcademicoList)
                    {
                        ProductoAcademicoDTO productoAcademico = new ProductoAcademicoDTO()
                        {
                            IdAcademico = idAcademico,
                            TipoProducto = producto.TipoProducto,
                            Titulo = producto.Titulo,
                            TipoPublicacion = producto.TipoPublicacion,
                            FechaPublicacion = producto.FechaPublicacion
                        };
                    
                        int resultado = productoAcademicoDAO.AgregarProductoAcademico(productoAcademico);

                        MensajesError mensajesError = new MensajesError();
                        mensajesError.ObtenerMensajeErrorProductoAcademico(resultado);
                        Console.WriteLine(productoAcademico.IdAcademico + productoAcademico.TipoProducto + producto.TipoPublicacion + productoAcademico.FechaPublicacion);
                    }
                }
                else
                {
                    MessageBox.Show("No hay productos académicos para asignar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al asignar productos académicos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        

        

        private void AsignarProyectoCampo()
        {
            try
            {
                // Recuperar el número personal del académico desde la interfaz
                string numeroPersonal = textBoxNumeroPersonal.Text;

                // Crear instancias de DAO
                AcademicoDAO academicoDAO = new AcademicoDAO();
                ProyectoDeCampoDAO proyectoCampoDAO = new ProyectoDeCampoDAO();

                // Obtener el ID del académico a partir del número personal
                int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

                if (idAcademico == 0)
                {
                    MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Terminar si el académico no existe
                }

                if (proyectoCampoList.Any())
                {
                    MessageBox.Show("Asignando proyectos de campo al académico...", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Recorrer la lista de proyectos de campo
                    foreach (var proyecto in proyectoCampoList)
                    {
                        // Asignar el ID del académico al proyecto
                        proyecto.IdAcademico = idAcademico;

                        // Guardar el proyecto de campo en la base de datos
                        int resultado = proyectoCampoDAO.AgregarProyectoCampo(proyecto);

                        MensajesError mensajesError = new MensajesError();
                        mensajesError.ObtenerMensajeErrorProyectoCampo(resultado);
                    }
                }
                else
                {
                    MessageBox.Show("No hay proyectos de campo para asignar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al asignar proyectos de campo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void RegistrarAcademico(object sender, RoutedEventArgs e) 
        {
            if (AgregarAcademico() == 1)
            {
                
                AsignarExperienciaEducativa();
                AsignarParticipacion();
                AsignarTrabajoRecepcional();
                AsignarProductoAcademico();
                AsignarProyectoCampo();

            }
            
        }







    }
}

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

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                var partTextBox = (DatePickerTextBox)datePicker.Template.FindName("PART_TextBox", datePicker);
                if (partTextBox != null)
                {
                    partTextBox.PreviewKeyDown += PartTextBox_PreviewKeyDown;
                    partTextBox.TextChanged += PartTextBox_TextChanged;
                }

                var popup = (Popup)datePicker.Template.FindName("PART_Popup", datePicker);
                if (popup != null && popup.Child is Calendar calendar)
                {
                    calendar.DisplayMode = CalendarMode.Decade;
                    calendar.DisplayModeChanged += (s, a) =>
                    {
                        if (calendar.DisplayMode != CalendarMode.Decade)
                            calendar.DisplayMode = CalendarMode.Decade;
                    };
                }
            }
        }

        private void PartTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void PartTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && DateTime.TryParse(textBox.Text, out DateTime newDate))
            {
                textBox.Text = newDate.ToString("yyyy");
            }
        }

        private void AgregarAcademico()
        {
            string nombreAcademico = textBoxNombreAcademico.Text;
            string tipoContratacion = comboBoxTipoContratacion.SelectedItem?.ToString();
            string areaAcademica = comboBoxArea.SelectedItem?.ToString();
            string fechaContratacion = datePickerFechaContratacion.SelectedDate?.ToString("yyyy-MM-dd");

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(nombreAcademico, tipoContratacion, areaAcademica, fechaContratacion))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = nombreAcademico,
                NumeroPersonal = textBoxNumeroPersonal.Text,
                TipoContratacion = tipoContratacion,
                FechaContratacion = fechaContratacion,
                AreaAcademica = areaAcademica
            };

            AcademicoDAO academicoDAO = new AcademicoDAO();
            int resultado = academicoDAO.ValidarYRegistrarAcademico(academicoDTO);

            MensajesError mensajesError = new MensajesError();
            mensajesError.ObtenerMensajeErrorAcademico(resultado);
        }

        private void AgregarAsignatura(object sender, RoutedEventArgs e)
        {
            string nombreEE = NombreAsignaturaTextBox.Text;
            string nombrePrograma = comboBoxProgramaEducativo.SelectedItem?.ToString();

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(nombreEE, nombrePrograma))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ExperienciaEducativaDAO eeDAO = new ExperienciaEducativaDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            int? idExistente = eeDAO.ObtenerIdExperienciaPorNombre(nombreEE);
            if (idExistente.HasValue)
            {
                MessageBox.Show("La asignatura ya existe en el sistema.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int nuevoIdAsignatura = (int)(eeDAO.ObtenerUltimoIdExperienciaEducativa() ?? 0) + 1;
            int idProgramaEducativo = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(nombrePrograma);

            ExperienciaEducativaDTO experienciaEducativaDTO = new ExperienciaEducativaDTO
            {
                IdExperienciaEducativa = nuevoIdAsignatura,
                IdProgramaEducativo = idProgramaEducativo,
                Nombre = nombreEE
            };

            eeList.Add(experienciaEducativaDTO);
            listBoxAsignaturas.ItemsSource = null; // Refrescar el ListBox
            listBoxAsignaturas.ItemsSource = eeList;

            int resultado = eeDAO.AgregarExperiencia(experienciaEducativaDTO);

            MensajesError mensajesError = new MensajesError();
            mensajesError.ObtenerMensajeErrorExperienciaEducativa(resultado);
        }

        private void RegistrarTrabajo(object sender, RoutedEventArgs e)
        {
            string numeroPersonal = textBoxNumeroPersonal.Text;
            string tipoTrabajo = comboBoxTipoTrabajo.SelectedItem?.ToString();
            string titulo = textBoxTituloTrabajo.Text;
            string rolAcademico = comboBoxRolAcademicoTrabajo.SelectedItem?.ToString();
            string nombreEstudiante = tetxBoxNombreEstudianteTrabajoRecepcional.Text;
            string fechaPresentacion = datePickerFechaPresentacionTrabajo.SelectedDate?.ToString("yyyy-MM-dd");

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(numeroPersonal, tipoTrabajo, titulo, rolAcademico, nombreEstudiante, fechaPresentacion))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AcademicoDAO academicoDAO = new AcademicoDAO();
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();

            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;
            if (idAcademico == 0)
            {
                MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
            {
                Titulo = titulo,
                NombreEstudiante = nombreEstudiante,
                FechaPublicacion = fechaPresentacion,
                IdAcademico = idAcademico,
                RolAcademico = rolAcademico,
                TipoTrabajo = tipoTrabajo
            };

            trabajoRecepcionalList.Add(trabajoRecepcionalDTO);
        }

        private void RegistrarProducto(object sender, RoutedEventArgs e)
        {
            string tituloProducto = textBoxTituloProducto.Text;
            string tipoProducto = comboBoxTipoProducto.SelectedItem?.ToString();
            string fechaPublicacion = datePickerFechaPublicacion.SelectedDate?.ToString("yyyy-MM-dd");
            string tipoPublicacion = GetSelectedPublicationType();

            // Validar campos vacíos
            if (ValidadorCampos.EstanVacios(tituloProducto, tipoProducto, tipoPublicacion))
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
        }

        private void RegistrarProyectoCampo(object sender, RoutedEventArgs e)
        {
            string nombreProyecto = textBoxNombreProyecto.Text;
            string rol = comboBoxRolAcademicoProyecto.SelectedItem?.ToString();
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

            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;
            if (idAcademico == 0)
            {
                MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProyectoDeCampoDTO proyectoDTO = new ProyectoDeCampoDTO
            {
                Nombre = nombreProyecto,
                LugarRealizacion = lugarRealizacion,
                Periodo = periodo,
                RolAcademico = rol,
                IdAcademico = idAcademico
            };

            proyectoCampoList.Add(proyectoDTO);
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
            string programaEducativo = comboBoxProgramaEducativoActualizado.SelectedItem?.ToString();
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

            // Obtener IDs relacionados
            int idPrograma = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(programaEducativo);
            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int nuevoIdParticipacion = (int)(participacionDAO.ObtenerUltimoIdParticipacion() ?? 0) + 1;

            // Crear DTO de participación
            ParticipacionDTO participacionDTO = new ParticipacionDTO
            {
                IdParticipacion = nuevoIdParticipacion,
                TipoParticipacion = tipoParticipacion,
                PeriodoParticipacion = $"{periodoInicio}-{periodoFin}",
                IdProgramaEducativo = idPrograma,
                IdAcademico = idAcademico
            };

            // Agregar la participación a la lista temporal
            participacionList.Add(participacionDTO);

            // Refrescar el ListBox
            listBoxActualizaciones.ItemsSource = null;
            listBoxActualizaciones.ItemsSource = participacionList;

            // Guardar la participación en la base de datos
            int resultado = participacionDAO.AgregarParticipacion(participacionDTO);

            // Mostrar mensaje según el resultado
            MensajesError mensajesError = new MensajesError();
            mensajesError.ObtenerMensajeErrorParticipacion(resultado);
        }


        private void RegistrarCertificacion(object sender, RoutedEventArgs e)
        {
            // Recuperar datos de los controles
            string programaEducativo = comboBoxProgramaEducativoCertificado.SelectedItem?.ToString();
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
            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int nuevoIdParticipacion = (int)(participacionDAO.ObtenerUltimoIdParticipacion() ?? 0) + 1;

            // Crear DTO de participación
            ParticipacionDTO participacionDTO = new ParticipacionDTO
            {
                IdParticipacion = nuevoIdParticipacion,
                TipoParticipacion = tipoParticipacion,
                PeriodoParticipacion = $"{periodoInicio}-{periodoFin}",
                IdProgramaEducativo = idPrograma,
                IdAcademico = idAcademico
            };

            // Guardar la participación en la base de datos
            int resultado = participacionDAO.AgregarParticipacion(participacionDTO);

            // Mostrar mensaje según el resultado
            MensajesError mensajesError = new MensajesError();
            mensajesError.ObtenerMensajeErrorParticipacion(resultado);
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
            List<int> programaIds = programaEducativoDAO.ObtenerIdsProgramas(programaNombres);

            if (programaIds.Any())
            {
                Console.WriteLine("Asignando actualizaciones al académico...");
                // Lógica para asignar las actualizaciones al académico
                foreach (var idPrograma in programaIds)
                {
                    // Crear el DTO de Participacion
                    ParticipacionDTO participacionDTO = new ParticipacionDTO
                    {
                        IdParticipacion = participacionDAO.ObtenerUltimoIdParticipacion() ?? 0 + 1,
                        TipoParticipacion = "Actualizacion",
                        PeriodoParticipacion = $"{actualzacionYearPicker.SelectedDate?.Year}-{actualizaciónAnotherYear.SelectedDate?.Year}",
                        IdProgramaEducativo = idPrograma,
                        IdAcademico = idAcademico
                    };

                    // Registrar la participación en la base de datos
                    int resultado = participacionDAO.AgregarParticipacion(participacionDTO);

                    if (resultado == 1)
                    {
                        Console.WriteLine($"Asignando actualización con ID de programa: {idPrograma} al académico con ID: {idAcademico}");
                    }
                    else if (resultado == -3)
                    {
                        Console.WriteLine($"Ya existe una actualización para el programa con ID: {idPrograma} y el académico con ID: {idAcademico}");
                    }
                    else
                    {
                        Console.WriteLine($"Error al asignar actualización para el programa con ID: {idPrograma}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No se encontraron programas educativos para asignar.");
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
                        // Asignar el ID del académico al producto
                        producto.IdAcademico = idAcademico;

                        // Guardar el producto académico en la base de datos
                        int resultado = productoAcademicoDAO.AgregarProductoAcademico(producto);

                        MensajesError mensajesError = new MensajesError();
                        mensajesError.ObtenerMensajeErrorProductoAcademico(resultado);
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


        // Método para obtener el tipo de publicación seleccionado (RadioButton)
        private string GetSelectedPublicationType()
        {
            // Buscar los RadioButtons por nombre
            RadioButton revista = FindName("RadioButtonRevista") as RadioButton;
            RadioButton editorial = FindName("RadioButtonEditorial") as RadioButton;

            // Verificar cuál está seleccionado
            if (revista != null && revista.IsChecked == true)
            {
                return "Revista";
            }
            else if (editorial != null && editorial.IsChecked == true)
            {
                return "Editorial";
            }

            return string.Empty; // Retornar vacío si no hay ninguno seleccionado
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
            AgregarAcademico();
            AsignarExperienciaEducativa();
            AsignarParticipacion();
            AsignarTrabajoRecepcional();
            AsignarProductoAcademico();
            AsignarProyectoCampo();
        }







    }
}

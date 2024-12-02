using Constancias.Adicionales;
using Logic.Clases;
using Logic.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
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

        private void ToggleVisibility(StackPanel panel)
        {
            stackPannelProgramasEducativosActualizados.Visibility = Visibility.Collapsed;
            stackPanelCertificacion.Visibility = Visibility.Collapsed;
            stackPanelProductoAcademico.Visibility = Visibility.Collapsed;
            stackPanelProyectoCampo.Visibility = Visibility.Collapsed;
            stackPanelTrabajoRecepcional.Visibility = Visibility.Collapsed;

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

            if (ValidadorCampos.EstanVacios(nombreAcademico, tipoContratacion, areaAcademica, fechaContratacion, programaEducativo))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return -1;
            }
            AcademicoDAO academico = new AcademicoDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

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

            if (ValidadorCampos.EstanVacios(nombreEE, nombrePrograma))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ExperienciaEducativaDAO eeDAO = new ExperienciaEducativaDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            int idProgramaEducativo = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(nombrePrograma);

            ExperienciaEducativaDTO experienciaEducativaDTO = new ExperienciaEducativaDTO
            {
                IdProgramaEducativo = idProgramaEducativo,
                Nombre = nombreEE,
            };

            eeList.Add(experienciaEducativaDTO);

            int resultado = eeDAO.ObtenerORegistrarExperiencia(experienciaEducativaDTO);

            if (resultado == 1)
            {
                MessageBox.Show("Experiencia educativa registrada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (resultado == 2)
            {
                MessageBox.Show("La experiencia educativa ya está registrada.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MensajesError mensajesError = new MensajesError();
                mensajesError.ObtenerMensajeErrorExperienciaEducativa(resultado);
            }
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

            if (ValidadorCampos.EstanVacios(numeroPersonal, tipoTrabajo, titulo, rolAcademico, nombreEstudiante, fechaPresentacion))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(nombreEstudiante))
            {
                MessageBox.Show("El título del trabajo y el nombre del estudiante son obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AcademicoDAO academicoDAO = new AcademicoDAO();
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();

            TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
            {
                Titulo = titulo,
                NombreEstudiante = nombreEstudiante,
                FechaPublicacion = fechaPresentacion,
                RolAcademico = rolAcademico,
                TipoTrabajo = tipoTrabajo
            };

            Console.WriteLine($"Titulo: {trabajoRecepcionalDTO.Titulo}, NombreEstudiante: {trabajoRecepcionalDTO.NombreEstudiante}, FechaPublicacion: {trabajoRecepcionalDTO.FechaPublicacion}");

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
            PopupIngresarAsignatura.IsOpen = true; 
        }


        private void AsignarExperienciaEducativa()
        {
            string numeroPersonal = textBoxNumeroPersonal.Text;

            AcademicoDAO academicoDAO = new AcademicoDAO();
            ExperienciaEducativaDAO experienciaEducativaDAO = new ExperienciaEducativaDAO();

            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                Console.WriteLine("No se encontró el académico.");
                return;
            }

            List<string> eeNombres = eeList.Select(ee => ee.Nombre).ToList();

            List<int> eeIds = ObtenerIdsExperiencias(eeNombres);

            if (eeIds.Any())
            {
                Console.WriteLine("Asignando experiencias educativas al académico...");
                foreach (var idExperiencia in eeIds)
                {
                    experienciaEducativaDAO.RegistrarRelacionAcademicoExperiencia(idAcademico, idExperiencia);
                    Console.WriteLine($"Asignando experiencia educativa con ID: {idExperiencia} al académico con ID: {idAcademico}");
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
                var id = experienciaEducativaDAO.ObtenerIdExperienciaPorNombre(nombre);

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
            var programaEducativoSeleccionado = comboBoxProgramaEducativoActualizado.SelectedItem as ComboBoxItem;
            string programaEducativo = programaEducativoSeleccionado.Content.ToString();
            string tipoParticipacion = "Actualización";
            string periodoInicio = actualzacionYearPicker.SelectedDate?.ToString("yyyy");
            string periodoFin = actualizaciónAnotherYear.SelectedDate?.ToString("yyyy");
            string numeroPersonal = textBoxNumeroPersonal.Text;

            if (ValidadorCampos.EstanVacios(programaEducativo, periodoInicio, periodoFin, numeroPersonal))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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

            participacionList.Add(participacionDTO);
            stackPannelProgramasEducativosActualizados.Visibility = Visibility.Hidden;


            

        
        }


        private void RegistrarCertificacion(object sender, RoutedEventArgs e)
        {
            var programaEducativoSeleccionado = comboBoxProgramaEducativoCertificado.SelectedItem as ComboBoxItem;
            string programaEducativo = programaEducativoSeleccionado.Content.ToString();
            string tipoParticipacion = "Certificación";
            string periodoInicio = certificacionYearPicker.SelectedDate?.ToString("yyyy");
            string periodoFin = certificacionAnotherYear.SelectedDate?.ToString("yyyy");
            string numeroPersonal = textBoxNumeroPersonal.Text;

            if (ValidadorCampos.EstanVacios(programaEducativo, periodoInicio, periodoFin, numeroPersonal))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Campos Vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();
            AcademicoDAO academicoDAO = new AcademicoDAO();
            ParticipacionDAO participacionDAO = new ParticipacionDAO();

            int idPrograma = (int)programaEducativoDAO.ObtenerIdProgramaPorNombre(programaEducativo);
            

            int nuevoIdParticipacion = (int)(participacionDAO.ObtenerUltimoIdParticipacion() ?? 0) + 1;

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
            string numeroPersonal = textBoxNumeroPersonal.Text;

            AcademicoDAO academicoDAO = new AcademicoDAO();
            ParticipacionDAO participacionDAO = new ParticipacionDAO();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                Console.WriteLine("No se encontró el académico.");
                return; 
            }

            List<string> programaNombres = participacionList.Select(a => a.Nombre).ToList();


            if (participacionList.Any())
            {
                Console.WriteLine("Asignando participaciones al académico...");
                foreach (var participacion in participacionList)
                {
                    Console.WriteLine(participacion.TipoParticipacion);
                    ParticipacionDTO participacionDTO = new ParticipacionDTO
                    {
                        TipoParticipacion = participacion.TipoParticipacion,
                        PeriodoParticipacion = participacion.PeriodoParticipacion,
                        IdProgramaEducativo = participacion.IdParticipacion,
                        IdAcademico = idAcademico
                    };

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
            string numeroPersonal = textBoxNumeroPersonal.Text;

            AcademicoDAO academicoDAO = new AcademicoDAO();
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();

            int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

            if (idAcademico == 0)
            {
                Console.WriteLine("No se encontró el académico.");
                return; 
            }

            List<TrabajoRecepcionalDTO> trabajosRecepcionales = new List<TrabajoRecepcionalDTO>();

            foreach (var trabajo in trabajoRecepcionalList)
            {
                TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
                {
                    Titulo = trabajo.Titulo,
                    NombreEstudiante = trabajo.NombreEstudiante,
                    FechaPublicacion = trabajo.FechaPublicacion,
                    IdAcademico = idAcademico,
                    TipoTrabajo = trabajo.TipoTrabajo,
                    RolAcademico = trabajo.RolAcademico
                };

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
                string numeroPersonal = textBoxNumeroPersonal.Text;

                AcademicoDAO academicoDAO = new AcademicoDAO();
                ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();

                int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

                if (idAcademico == 0)
                {
                    MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (productoAcademicoList.Any())
                {
                    MessageBox.Show("Asignando productos académicos al académico...", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

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
                string numeroPersonal = textBoxNumeroPersonal.Text;

                AcademicoDAO academicoDAO = new AcademicoDAO();
                ProyectoDeCampoDAO proyectoCampoDAO = new ProyectoDeCampoDAO();

                int idAcademico = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal) ?? 0;

                if (idAcademico == 0)
                {
                    MessageBox.Show("No se encontró el académico.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; 
                }

                if (proyectoCampoList.Any())
                {
                    MessageBox.Show("Asignando proyectos de campo al académico...", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                    foreach (var proyecto in proyectoCampoList)
                    {
                        proyecto.IdAcademico = idAcademico;

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
                InicioAdministrativo inicioAdministrativo = new InicioAdministrativo();
                inicioAdministrativo.Show();
                this.Close();

            }
            
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            PopupIngresarAsignatura.Visibility = Visibility.Collapsed;
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            InicioAdministrativo inicioAdministrativo = new InicioAdministrativo();
            inicioAdministrativo.Show();
            this.Close();
        }





    }
}

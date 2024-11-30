using Constancias.Adicionales;
using MaterialDesignColors;
using Logic.Clases;
using Logic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Constancias {
    /// <summary>
    /// Interaction logic for SolicitarConstancia.xaml
    /// </summary>
    public partial class SolicitarConstancia : Window {
        private string numPersonal;
        private string nombreAcademico;

        public SolicitarConstancia() {
            InitializeComponent();
            InitializeMaterialDesign();
            SesionDeUsuario sesionDeUsuario = new SesionDeUsuario();
            sesionDeUsuario = ManejadorDeSesion.GetInstancia().GetUsuario();
            nombreAcademico = sesionDeUsuario.NombreUsuario.ToString();
            numPersonal = sesionDeUsuario.NumeroPersonal.ToString();
            textBlockNombreSolicitante.Text = sesionDeUsuario.NombreUsuario.ToString();
        }

        private void InitializeMaterialDesign()
        {
            var card = new MaterialDesignThemes.Wpf.Card();
            var hue = new Hue("Dummy", Colors.Black, Colors.White);
        }

        private void ClickCancelar(object sender, RoutedEventArgs e) {
            ManejadorDeSesion.GetInstancia().SalirDeSesion();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ClickSolicitar(object sender, RoutedEventArgs e) {
            if (!String.IsNullOrEmpty(comboBoxOpcionesParticipacion.Text)) {
                AcademicoDAO academicoDAO = new AcademicoDAO();
                int idAcademico = (int)academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numPersonal);
                ConstanciaDTO constanciaDTO = new ConstanciaDTO {
                    FechaExpedicion = "NO EXPEDIDA",
                    IdAcademico = idAcademico,
                    TipoConstancia = comboBoxConstancias.SelectedItem.ToString(),
                    Solicitante = nombreAcademico,
                };
                ConstanciaDAO constanciaDAO = new ConstanciaDAO();
                constanciaDAO.SolicitarConstancia(constanciaDTO);
                MessageBox.Show("Constancia generada exitosamente");
            } else {
                MessageBox.Show("No ha elegido una opción de participación valida");
            }
        }

        private void SetComboBoxOpciones(object sender, SelectionChangedEventArgs e) {
            AcademicoDAO academicoDAO = new AcademicoDAO();
            ParticipacionDAO participacionDAO = new ParticipacionDAO();

            int idAcademico = (int)academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numPersonal);
            switch (comboBoxConstancias.SelectedIndex) {
                case 0: //Generacion producto académico
                    comboBoxOpcionesParticipacion.Items.Clear();

                    ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();
                    var listaProductosAcademicos = productoAcademicoDAO.ObtenerProductosAcademicosDeAcademico(idAcademico);

                    foreach(var productoAcademico in listaProductosAcademicos) {
                        comboBoxOpcionesParticipacion.Items.Add(productoAcademico);
                    }

                    break;
                case 1: //Participación proceso de actualización de programas de estudio
                    comboBoxOpcionesParticipacion.Items.Clear();
                    ProgramaEducativoDAO programaEducativoActualizacionDAO = new ProgramaEducativoDAO();
                    var listaParticipaciones = participacionDAO.ObtenerIdParticipacionesActualizacionPorAcademico(idAcademico);
                    foreach (var participacion in listaParticipaciones) {
                        string opcion = programaEducativoActualizacionDAO.ObtenerNombreProgramaPorIdPrograma((int)participacion);
                        comboBoxOpcionesParticipacion.Items.Add(opcion);
                    }

                    break;
                case 2: //Participación en procesos de certificación de programas de estudio
                    comboBoxOpcionesParticipacion.Items.Clear();
                    ProgramaEducativoDAO programaEducativoCertificacionDAO = new ProgramaEducativoDAO();
                    var listaCertificaciones = participacionDAO.ObtenerIdParticipacionesCertificacionPorAcademico(idAcademico);
                    foreach (var participacion in listaCertificaciones) {
                        string opcion = programaEducativoCertificacionDAO.ObtenerNombreProgramaPorIdPrograma((int)participacion);
                        comboBoxOpcionesParticipacion.Items.Add(opcion);
                    }

                    break;
                case 3: //Participación trabajos recepcionales
                    comboBoxOpcionesParticipacion.Items.Clear();

                    TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();
                    var listaTrabajosRecepcionales = trabajoRecepcionalDAO.ObtenerTrabajosRecepcionalesDeAcademico(idAcademico);
                    foreach(var trabajoRecepcional in listaTrabajosRecepcionales) {
                        comboBoxOpcionesParticipacion.Items.Add(trabajoRecepcional);
                    }

                    break;

                case 4: //Participacion proyecto de campo y/o gestión académica o artística 
                    comboBoxOpcionesParticipacion.Items.Clear();

                    ProyectoDeCampoDAO proyectoDeCampoDAO = new ProyectoDeCampoDAO();
                    var listaProyectosDeCampo = proyectoDeCampoDAO.ObtenerProyectoDeCampoDeAcademico(idAcademico);

                    foreach(var proyectosDeCampo in listaProyectosDeCampo) {
                        comboBoxOpcionesParticipacion.Items.Add(proyectosDeCampo);
                    }

                    break;

                case 5: //Experiencia docente   
                    comboBoxOpcionesParticipacion.Items.Clear();

                    ExperienciaEducativaDAO experienciaEducativaDAO = new ExperienciaEducativaDAO();
                    var idPrograma = academicoDAO.ObtenerIdProgramaDeAcademicoPorIdAcademico(idAcademico);
                    var listaExperienciasEducativas = experienciaEducativaDAO.ObtenerNombreExperienciaEducativaPorIdPrograma((int)idPrograma);

                    foreach (var experienciaEducativa in listaExperienciasEducativas) {
                        comboBoxOpcionesParticipacion.Items.Add(experienciaEducativa);
                    }
                    break;
            }
            
        }
    }
}

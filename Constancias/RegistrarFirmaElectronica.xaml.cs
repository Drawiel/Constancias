using Logic.Clases;
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

namespace Constancias
{
    /// <summary>
    /// Interaction logic for RegistrarFirmaElectronica.xaml
    /// </summary>
    public partial class RegistrarFirmaElectronica : Window
    {
        public RegistrarFirmaElectronica()
        {
            InitializeComponent();
            inicializarTabla();
        }

        private void inicializarTabla()
        {
            lvParticipantes.Items.Clear();
            List<ParticipacionDTO> listaParticipaciones =  recuperarRegistros();
            lvParticipantes.ItemsSource = listaParticipaciones;
        }

        private List<ParticipacionDTO> recuperarRegistros()
        {
            List<ParticipacionDTO> participacionDTOs = new List<ParticipacionDTO>
            {
                new ParticipacionDTO {IdParticipacion = 1, Nombre= "Modelado de sistemas con arquitectura de microservicios", PeriodoParticipacion = "22/04/2021", TipoParticipacion = "Trabajo recepcional", nombreAcademico = "Jorge Octavío Ocharan Hernandez"},
                 new ParticipacionDTO {IdParticipacion = 2, Nombre= "Guía para la  pureba de API's web", PeriodoParticipacion = "22/04/2023", TipoParticipacion = "Trabajo recepcional", nombreAcademico = "Jorge Octavío Ocharan Hernandez"},
                  new ParticipacionDTO {IdParticipacion = 3, Nombre= "Principios de diseño de software", PeriodoParticipacion = "17/07/2023", TipoParticipacion = "Experiencia educativa", nombreAcademico = "María Karen Cortés Verdín"},
                   new ParticipacionDTO {IdParticipacion = 4, Nombre= "Prueba de software", PeriodoParticipacion = "04/06/2024", TipoParticipacion = "Experiencia educativa", nombreAcademico = "Elizabeth Murrieta Sangabriel"},
            };
            return participacionDTOs;
        }

        private void registrarFirma_clic(object sender, RoutedEventArgs e)
        {
            if (txtb_Firma.Text.Equals(""))
            {
                MessageBox.Show("Por favor, ingrese la firma antes de continuar", "Sin texto");
                txtb_Firma.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFFF0000");
            }
            else
            {
                if (lvParticipantes.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, seleccione una participación", "Sin selección");
                    lvParticipantes.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFFF0000");
                }
                else
                {
                    MessageBox.Show("Se ha registrado la firma para la constancia", "Registro exitoso");
                    InicioAdministrativo inicioAdministrativo = new InicioAdministrativo();
                    inicioAdministrativo.Show();
                    this.Close();
                }
            }
        }

        private void cancelar_clic(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea cancelar el registro de firma electronica?",
                "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                InicioAdministrativo inicioAdministrativo = new InicioAdministrativo();
                inicioAdministrativo.Show();
                this.Close();
            }
        }

        private void txtb_Firma_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtb_Firma.BorderBrush = null;
        }
    }

}

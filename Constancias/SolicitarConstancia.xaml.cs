using Constancias.Adicionales;
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
        public SolicitarConstancia() {
            InitializeComponent();
            SesionDeUsuario sesionDeUsuario = new SesionDeUsuario();
            sesionDeUsuario = ManejadorDeSesion.GetInstancia().GetUsuario();
            textBlockNombreSolicitante.Text = sesionDeUsuario.NombreUsuario.ToString();
        }

        private void ClickCancelar(object sender, RoutedEventArgs e) {
            ManejadorDeSesion.GetInstancia().SalirDeSesion();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ClickSolicitar(object sender, RoutedEventArgs e) {
            MessageBox.Show("Constancia Solicitada");
        }

        private void SetComboBoxOpciones(object sender, SelectionChangedEventArgs e) {
            switch (comboBoxConstancias.SelectedIndex) {
                case 0:
                    comboBoxOpcionesParticipacion.Items.Clear();
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 1 para producto académico");
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 2 para producto académico");
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 3 para producto académico");
                    break;
                case 1:
                    comboBoxOpcionesParticipacion.Items.Clear();
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 1 para actualización programa de estudio");
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 2 para actualización programa de estudio");
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 3 para actualización programa de estudio");

                    break;
                case 2:
                    comboBoxOpcionesParticipacion.Items.Clear();
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 1 para certificación programa de estudio");
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 2 para certificación programa de estudio");

                    break;
                case 3:
                    comboBoxOpcionesParticipacion.Items.Clear();
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 1 para participación trabajos recepcionales");

                    break;

                case 4:
                    comboBoxOpcionesParticipacion.Items.Clear();
                    comboBoxOpcionesParticipacion.Items.Add("Opcion participación 1 para participación proyecto de campo");

                    break;
            }
            
        }
    }
}

using Constancias.Adicionales;
using MaterialDesignColors;
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
            InitializeMaterialDesign();
            SesionDeUsuario sesionDeUsuario = new SesionDeUsuario();
            sesionDeUsuario = ManejadorDeSesion.GetInstancia().GetUsuario();
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
            MessageBox.Show("Constancia Solicitada");
        }
    }
}

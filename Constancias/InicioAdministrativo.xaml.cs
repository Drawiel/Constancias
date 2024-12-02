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
    /// Interaction logic for InicioAdministrativo.xaml
    /// </summary>
    public partial class InicioAdministrativo : Window {
        public InicioAdministrativo() {
            InitializeComponent();
            lbMensajeBienvenida.Visibility = Visibility.Visible;
            esconderTodo();
        }

        private void irGenerarConstancia(object sender, RoutedEventArgs e)
        {
            GenerarConstancia generarConstancia = new GenerarConstancia();
            generarConstancia.Show();
            this.Close();
        }

        private void mostrarMiniMenuAcademicos(object sender, RoutedEventArgs e)
        {
            esconderTodo();
            btn_irRegistrarProfesor.Visibility = Visibility.Visible;

        }

        private void esconderTodo()
        {
            lbMensajeBienvenida.Visibility = Visibility.Hidden;
            btn_irGenerarConstancia.Visibility = Visibility.Hidden;
            btn_irRegistrarProfesor.Visibility = Visibility.Hidden;
            btn_irRegistrarFirma.Visibility = Visibility.Hidden;
        }

        private void mostrarMiniMenuConstancias(object sender, RoutedEventArgs e)
        {
            esconderTodo();
            btn_irGenerarConstancia.Visibility = Visibility.Visible;
            btn_irRegistrarFirma.Visibility = Visibility.Visible;
        }
        private void mostrarMiniMenuAjustes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("La siguiente caracteristica se encuentra en desarrollo", "Proximamente");
        }

        private void cerrarSesion(object sender, RoutedEventArgs e)
        {
            ManejadorDeSesion.GetInstancia().SalirDeSesion();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void irRegistrarProfesor(object sender, RoutedEventArgs e)
        {
            RegistrarProfesor registrarProfesor = new RegistrarProfesor();
            registrarProfesor.Show();
            this.Close();
        }

        private void irRegistrarFirma(object sender, RoutedEventArgs e)
        {
            RegistrarFirmaElectronica registrarFirma = new RegistrarFirmaElectronica();
            registrarFirma.Show();
            this.Close();
        }
    }
}

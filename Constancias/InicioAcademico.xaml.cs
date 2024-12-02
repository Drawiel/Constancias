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

namespace Constancias
{
    /// <summary>
    /// Interaction logic for InicioAcademico.xaml
    /// </summary>
    public partial class InicioAcademico : Window
    {
        public InicioAcademico()
        {
            InitializeComponent();
            lbMensajeBienvenida.Visibility = Visibility.Visible;
            btn_irSolicitarConstancia.Visibility = Visibility.Hidden;
        }

        private void irSolicitarConstancia(object sender, RoutedEventArgs e)
        {
            SolicitarConstancia solicitarConstancia = new SolicitarConstancia();
            solicitarConstancia.Show();
            this.Close();
        }

        private void mostrarMiniMenuAcademicos(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("La siguiente caracteristica se encuentra en desarrollo", "Proximamente");
        }

        private void mostrarMiniMenuConstancias(object sender, RoutedEventArgs e)
        {
            lbMensajeBienvenida.Visibility = Visibility.Hidden;
            btn_irSolicitarConstancia.Visibility = Visibility.Visible;
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
    }
}

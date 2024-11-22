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
    /// Interaction logic for GenerarConstancia.xaml
    /// </summary>
    public partial class GenerarConstancia : Window {
        public GenerarConstancia() {
            InitializeComponent();
        }

        private void ClickCancelar(object sender, RoutedEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ClickAceptar(object sender, RoutedEventArgs e) {
            MessageBox.Show("Se ha aceptado la constancia");
        }
    }
}

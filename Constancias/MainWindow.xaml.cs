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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Constancias {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        public void ClickIniciarSesion(object sender, RoutedEventArgs e) { 
            string numeroPersonal = textBoxNumeroPersonal.Text;

            if (EsAcademico(numeroPersonal)) {

                SesionDeUsuario sesionDeUsuario = new SesionDeUsuario() {
                    NombreUsuario = "Daniel Alejandre",
                    NumeroPersonal = numeroPersonal,
                };

                ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                var inicioAcademico = new InicioAcademico();
                inicioAcademico.Show();
                this.Close();

            } else if (EsAdministrativo(numeroPersonal)) {

                SesionDeUsuario sesionDeUsuario = new SesionDeUsuario() {
                    NombreUsuario = "Jorge Octavio",
                    NumeroPersonal = numeroPersonal,
                };

                ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                var inicioAdministrativo = new InicioAdministrativo();
                inicioAdministrativo.Show();
                this.Close();

            } else {
                MessageBox.Show("No ha ingresado una cuenta valida");
            }
            
        }

        public bool EsAcademico(string numeroPersonal) {

            if(numeroPersonal == "070604") {
                return true;
            } else {
                return false;
            }
        }

        public bool EsAdministrativo(string numeroPersonal) {

            if (numeroPersonal == "121254") {
                return true;
            } else {
                return false;
            }
        }
    }
}

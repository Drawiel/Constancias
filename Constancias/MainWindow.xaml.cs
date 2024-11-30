using Constancias.Adicionales;
using DataAccess;
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
            string contrasenia = passwordBoxContrasenia.Password.ToString();
            if (!EsAdministrativo(numeroPersonal) && !ValidadorCampos.EstanVacios(numeroPersonal, contrasenia)) {

                AcademicoDAO academicoDAO = new AcademicoDAO();
                int resultado = academicoDAO.BuscarAcademicoPorNumeroPersonal(numeroPersonal);
                if (resultado == 0)
                {
                    if (ValidadorCampos.EsContraseniaValida(contrasenia))
                    {
                        int resultadoActualizacion = academicoDAO.AgregarAcademicoBasico(numeroPersonal, contrasenia);
                        if (resultadoActualizacion == 1)
                        {
                            SesionDeUsuario sesionDeUsuario = new SesionDeUsuario()
                            {
                                NombreUsuario = "Académico",
                                NumeroPersonal = numeroPersonal,
                            };

                            ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                            var solicitarConstancia = new SolicitarConstancia();
                            solicitarConstancia.Show();
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Error al iniciar sesión, verifique los datos ingresados");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ingrese una contraseña válida");
                    }
                }
                else if (resultado == 1) 
                {
                    int idAcademico = (int) academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal);
                    bool resultadoValidacion = academicoDAO.ValidarContraseña(idAcademico, contrasenia);
                    string nombreAcademico = academicoDAO.ObtenerNombreAcademicoPorNumeroPersonal(numeroPersonal);
                    if(resultadoValidacion == true)
                    {
                        if(nombreAcademico != null)
                        {
                            SesionDeUsuario sesionDeUsuario = new SesionDeUsuario()
                            {
                                NombreUsuario = nombreAcademico,
                                NumeroPersonal = numeroPersonal,
                            };

                            ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                            var solicitarConstancia = new SolicitarConstancia();
                            solicitarConstancia.Show();
                            this.Close();

                        } else
                        {
                            SesionDeUsuario sesionDeUsuario = new SesionDeUsuario()
                            {
                                NombreUsuario = "Académico",
                                NumeroPersonal = numeroPersonal,
                            };

                            ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                            var solicitarConstancia = new SolicitarConstancia();
                            solicitarConstancia.Show();
                            this.Close();

                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("La contraseña ingresada es incorrecta, por favor verífiquela");
                    }
                }
                else if (resultado == 2) 
                {
                    int idAcademico = (int) academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal);
                    if (ValidadorCampos.EsContraseniaValida(contrasenia))
                    {
                        int resultadoActualizacion = academicoDAO.ActualizarContraseñaYNombreAcademico(idAcademico, contrasenia);
                        string nombreAcademico = academicoDAO.ObtenerNombreAcademicoPorNumeroPersonal(numeroPersonal);
                        if (resultadoActualizacion == 1)
                        {
                            if (nombreAcademico != null)
                            {
                                SesionDeUsuario sesionDeUsuario = new SesionDeUsuario()
                                {
                                    NombreUsuario = nombreAcademico,
                                    NumeroPersonal = numeroPersonal,
                                };

                                ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                                var solicitarConstancia = new SolicitarConstancia();
                                solicitarConstancia.Show();
                                this.Close();

                            }
                            else
                            {
                                SesionDeUsuario sesionDeUsuario = new SesionDeUsuario()
                                {
                                    NombreUsuario = "Académico",
                                    NumeroPersonal = numeroPersonal,
                                };

                                ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                                var solicitarConstancia = new SolicitarConstancia();
                                solicitarConstancia.Show();
                                this.Close();

                            }

                        }
                        else
                        {
                            MessageBox.Show("Error al iniciar sesión, verifique los datos ingresados");
                        }

                    } else
                    {
                        MessageBox.Show("Ingrese una contraseña válida");
                    }
                    

                    
                } else
                {

                    MessageBox.Show("No se puede iniciar sesión, hay campos vacíos");
                }


                

            } else if (EsAdministrativo(numeroPersonal)) {

                SesionDeUsuario sesionDeUsuario = new SesionDeUsuario() {
                    NombreUsuario = "Jorge Octavio",
                    NumeroPersonal = numeroPersonal,
                };

                ManejadorDeSesion.GetInstancia().IniciarSesion(sesionDeUsuario);

                var inicioAdministrativo = new RegistrarProfesor();
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

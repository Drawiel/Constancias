using Constancias.Adicionales;
using Logic.Clases;
using Logic.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
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

        // Helper method to toggle visibility and collapse others
        private void ToggleVisibility(StackPanel panel)
        {
            // Collapse all panels first
            stackPannelProgramasEducativosActualizados.Visibility = Visibility.Collapsed;
            stackPanelCertificacion.Visibility = Visibility.Collapsed;
            stackPanelProductoAcademico.Visibility = Visibility.Collapsed;
            stackPanelProyectoCampo.Visibility = Visibility.Collapsed;
            stackPanelTrabajoRecepcional.Visibility = Visibility.Collapsed;

            // Toggle visibility of the selected panel
            panel.Visibility = panel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                var partTextBox = (DatePickerTextBox)datePicker.Template.FindName("PART_TextBox", datePicker);
                if (partTextBox != null)
                {
                    partTextBox.PreviewKeyDown += PartTextBox_PreviewKeyDown;
                    partTextBox.TextChanged += PartTextBox_TextChanged;
                }

                var popup = (Popup)datePicker.Template.FindName("PART_Popup", datePicker);
                if (popup != null && popup.Child is Calendar calendar)
                {
                    calendar.DisplayMode = CalendarMode.Decade;
                    calendar.DisplayModeChanged += (s, a) =>
                    {
                        if (calendar.DisplayMode != CalendarMode.Decade)
                            calendar.DisplayMode = CalendarMode.Decade;
                    };
                }
            }
        }

        private void PartTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void PartTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && DateTime.TryParse(textBox.Text, out DateTime newDate))
            {
                textBox.Text = newDate.ToString("yyyy");
            }
        }

        private void RegistrarAcademico(object sender, RoutedEventArgs e)
        {
            string nombreAcademico = textBoxNombreAcademico.Text;
            string tipoContratacion = comboBoxTipoContratacion.SelectedItem.ToString();
            string areaAcademica = comboBoxArea.SelectedItem.ToString();
            string fechaContratacion = datePickerFechaContratacion.SelectedDate.ToString();
            
            if(!ValidadorCampos.EstanVacios(nombreAcademico, tipoContratacion, areaAcademica, fechaContratacion))
            {
                DataContext = this;
                AcademicoDTO academicoDTO = new AcademicoDTO();
                academicoDTO.AreaAcademica = areaAcademica;
                academicoDTO.Nombre = nombreAcademico;  
                academicoDTO.TipoContratacion = tipoContratacion;

                ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();
                var idProgramaEducativo = programaEducativoDAO.ObtenerIdProgramaPorNombre(comboBoxPrograma.SelectedItem.ToString());

                academicoDTO.IdPrograma = idProgramaEducativo;
                AcademicoDAO academicoDAO = new AcademicoDAO();

                

            }




        }


        }
    }

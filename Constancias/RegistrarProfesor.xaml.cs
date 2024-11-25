using System;
using System.Collections.Generic;
using System.Linq;
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
            ToggleVisibility(EducationPanel);
        }

        private void ToggleCertificationVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(CertificationPanel);
        }

        private void ToggleProductVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(ProductPanel);
        }

        private void ToggleProjectVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(ProjectPanel);
        }

        private void ToggleThesisVisibility(object sender, RoutedEventArgs e)
        {
            ToggleVisibility(ThesisPanel);
        }

        // Helper method to toggle visibility and collapse others
        private void ToggleVisibility(StackPanel panel)
        {
            // Collapse all panels first
            EducationPanel.Visibility = Visibility.Collapsed;
            CertificationPanel.Visibility = Visibility.Collapsed;
            ProductPanel.Visibility = Visibility.Collapsed;
            ProjectPanel.Visibility = Visibility.Collapsed;
            ThesisPanel.Visibility = Visibility.Collapsed;

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
    }

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

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo2Ferretera.Views
{
    /// <summary>
    /// Lógica de interacción para Productos2view.xaml
    /// </summary>
    public partial class Productos2view : Window
    {
        public Productos2view()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Estadisticas_Click(object sender, RoutedEventArgs e)
        {
            EstadisticasView.Visibility = Visibility.Visible;
        }
    }
}

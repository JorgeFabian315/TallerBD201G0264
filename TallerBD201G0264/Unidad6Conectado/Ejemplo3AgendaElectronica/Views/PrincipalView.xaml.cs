using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.ViewModel;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.Views
{
    /// <summary>
    /// Lógica de interacción para PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : Window
    {
        public PrincipalView()
        {
            InitializeComponent();
            btnPortadaVisible.Visibility = Visibility.Hidden;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ConfirmarEliminar.Visibility = Visibility.Visible;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            ConfirmarEliminar.Visibility = Visibility.Hidden;
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            AEVM.BuscarCommand.Execute(txtBuscar.Text);

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AEVM.CambiarVistaCommand.Execute("Materias");
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            Portada1.Visibility = Visibility.Hidden;
            Portada2.Visibility = Visibility.Hidden;
            btnPortadaVisible.Visibility = Visibility.Visible;
        }

        private void btnPortadaVisible_Click(object sender, RoutedEventArgs e)
        {
            Portada1.Visibility = Visibility.Visible;
            Portada2.Visibility = Visibility.Visible;
            btnPortadaVisible.Visibility = Visibility.Hidden;


        }
    }
}

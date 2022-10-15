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

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo2Ferretera.Views
{
    /// <summary>
    /// Lógica de interacción para EstadisticasView.xaml
    /// </summary>
    public partial class EstadisticasView : UserControl
    {
        public EstadisticasView()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            EstadisticasView2.Visibility = Visibility.Hidden;
        }
    }
}

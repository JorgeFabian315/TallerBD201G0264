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

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.Views
{
    /// <summary>
    /// Lógica de interacción para AgregarView.xaml
    /// </summary>
    public partial class AgregarView : UserControl
    {
        public AgregarView()
        {
            InitializeComponent();
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }
        }
    }
}

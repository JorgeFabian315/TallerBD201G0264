using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.Helpers
{
    public class ConvertidorNumControl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nc = (string)value;
            string? url = nc == ""?"":$"https://intertec.tec-carbonifera.edu.mx/fotos/al/{nc.Substring(0, 2)}/{nc}.jpg";

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || nc.Length != 8)
            {
                return "/Assets/amigologo.png";
            }
            else
            {
                return url;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}

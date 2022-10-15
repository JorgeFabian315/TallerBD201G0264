using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo2Ferretera.Models
{
    public class Producto
    {

        public uint Sku { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Seccion { get; set; }

        public override string ToString()
        {
            return $"{(char)45} {Nombre}"; 
        }

    }
}

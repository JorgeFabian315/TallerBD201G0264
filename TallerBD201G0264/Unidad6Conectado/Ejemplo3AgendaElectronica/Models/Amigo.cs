using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.ViewModel;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.Models
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;
        public string Telefono { get; set; } = "";
        public string? CorreoElectronico { get; set; }
    }
}

using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.Models;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.ViewModel
{
    public class AgendaElectronicaVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Agenda Agenda { get; set; }
        public Amigo? Amigo { get; set; }
        public string Vista { get; set; } = "Ver";
        public string Error { get; set; } = "";
        public int IndiceAmigo { get; set; } = 0;
        public string Mostrar { get; set; } = "No";
        public ICommand CreateCommand { get; set; }
        public ICommand MostarCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand BuscarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public AgendaElectronicaVM()
        {
            Agenda = new Agenda();
            CreateCommand = new RelayCommand(Create);
            DeleteCommand = new RelayCommand(Delete);
            UpdateCommand = new RelayCommand(Update);
            MostarCommand = new RelayCommand<string>(MostrarMetodo);
            BuscarCommand = new RelayCommand<string>(Buscar);
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void MostrarMetodo(string mostrar)
        {
            Mostrar = mostrar;
            Evento();
        }

        private void Buscar(string buscar)
        {
            Agenda.Buscar(buscar);
            indice = 0;
            Evento();
        }

        private void Update()
        {
            if (Amigo != null)
            {
                Agenda.Update(Amigo);
                Agenda.ListaAmigos[indice] = Amigo;
                IndiceAmigo = indice;
                CambiarVista("Ver");
                Evento();
            }
        }

        private void Delete()
        {
            if (Amigo != null)
            {
                Agenda.Delete(Amigo);
                CambiarVista("Ver");
                Mostrar = "No";
                Evento();
            }
        }

        private void Cancelar()
        {
            CambiarVista("Ver");
            Amigo = null;
        }

        private void Create()
        {
            if (Amigo != null)
            {
                Agenda.Create(Amigo);
                CambiarVista("Ver");
                Evento();
            }

        }
        int indice;
        public void CambiarVista(string vista)
        {
            Vista = vista;
            if (Vista == "Agregar")
            {
                Amigo = new Amigo();
            }
            if (Vista == "Editar")
            {
                if (Amigo != null)
                {
                    var clon = new Amigo()
                    {
                        Id = Amigo.Id,
                        CorreoElectronico = Amigo.CorreoElectronico,
                        Nombre = Amigo.Nombre,
                        Telefono = Amigo.Telefono,
                        FechaNacimiento = Amigo.FechaNacimiento
                    };
                    indice = Agenda.ListaAmigos.IndexOf(Amigo);
                    Amigo = clon;
                }
                else
                    Amigo = Agenda.ListaAmigos[indice];
            }
            Evento();
        }
        public void Evento(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}

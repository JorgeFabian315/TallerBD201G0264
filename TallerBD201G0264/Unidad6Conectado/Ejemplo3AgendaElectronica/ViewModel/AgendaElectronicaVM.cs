using GalaSoft.MvvmLight.Command;
using Google.Protobuf.WellKnownTypes;
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
        private Amigo? amigo;

        public Amigo? Amigo
        {
            get { return amigo; }
            set
            {
                amigo = value;
                OnPropertyChanged("Amigo");
            }
        }

        //public Amigo? Amigo { get; set; }
        public string Vista { get; set; } = "Ver";
        public string Error { get; set; } = "";
        public int IndiceAmigo { get; set; } = 0;
        public long TotalAmigos { get; set; } 
        public string Mostrar { get; set; } = "No";
        public bool BloquearAgregar { get; set; } = false;
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
            TotalAmigos = Agenda.ListaAmigos.Count;
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
            OnPropertyChanged();
        }

        private void Buscar(string buscar)
        {

            Agenda.Buscar(buscar);
            if (Agenda.ListaAmigos.Count > 0)
                Amigo = Agenda.ListaAmigos[0];
            else
                Amigo = null;
            TotalAmigos = Agenda.ListaAmigos.Count;
            OnPropertyChanged();
        }

        private void Update()
        {
            if (Amigo != null)
            {
                if (getValidar(Amigo))
                {
                    BloquearAgregar = false;
                    Agenda.Update(Amigo);
                    Agenda.ListaAmigos[indice] = Amigo;
                    IndiceAmigo = indice;
                    CambiarVista("Ver");
                }
            }
        }
        private void Delete()
        {
            if (Amigo != null)
            {
                Agenda.Delete(Amigo);
                Mostrar = "No";
                TotalAmigos = Agenda.ListaAmigos.Count;

                CambiarVista("Ver");
            }
        }

        private void Cancelar()
        {
            Error = "";
            BloquearAgregar = false;
            CambiarVista("Ver");
            Amigo = null;
        }

        private void Create()
        {
            if (Amigo != null)
            {
                if (getValidar(Amigo))
                {
                    Agenda.Create(Amigo);
                    TotalAmigos = Agenda.ListaAmigos.Count;
                    BloquearAgregar = false;
                    CambiarVista("Ver");
                    OnPropertyChanged();
                }
            }

        }
        int indice;
        public void CambiarVista(string vista)
        {
            Vista = vista;
            if (Vista == "Agregar")
            {
                Amigo = new Amigo();
                BloquearAgregar = true;
            }
            if (Vista == "Editar")
            {
                BloquearAgregar = true;
                if (Amigo != null)
                {
                    var clon = new Amigo()
                    {
                        Id = Amigo.Id,
                        NumeroControl = Amigo.NumeroControl,
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
            OnPropertyChanged();
        }


        public bool getValidar(Amigo a)
        {
            if (a != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(a.NumeroControl))
                {
                    Error = "Escriba el número de control por favor";
                }
                else if (a.NumeroControl.Length != 8)
                {
                    Error = "El número de control debe de llevar 8 caracteres";
                }
                else if (Agenda.ListaAmigos.Any(x => x.NumeroControl == a.NumeroControl && x.Id != a.Id))
                {
                    Error = "El número de control ya existe";
                }
                else if (string.IsNullOrWhiteSpace(a.Nombre))
                {
                    Error = "Escriba el nombre por favor";
                }
                else if (a.Nombre.Length > 100)
                {
                    Error = "Ha excedido el tamaño del nombre permitido";
                }
                else if (string.IsNullOrWhiteSpace(a.Telefono))
                {
                    Error = "Escriba el número de teléfono por favor";
                }
                else if (a.Telefono.Length != 10)
                {
                    Error = "El número de teléfono debe de ser de 10 dígitos";
                }
                else if (Agenda.ListaAmigos.Any(x => x.Telefono == a.Telefono && x.Id != a.Id))
                {
                    Error = "El número de teléfono ya existe";
                }
                else if (a.FechaNacimiento == DateTime.Today)
                {
                    Error = "La fecha de nacimiento no puede ser la de hoy";
                }
                else if (a.FechaNacimiento > DateTime.Now)
                {
                    Error = "La fecha de nacimiento no puede ser superior a la de hoy";
                }
                else if (a.CorreoElectronico != null)
                {
                    if (a.CorreoElectronico.Length > 100)
                        Error = "Ha excedido el tamaño del correo electrónico";
                }
                if (Error == "")
                {
                    return true;
                }
                else
                {
                    OnPropertyChanged("Error");
                    return false;
                }
            }
            else
                return false;
        }

        public void OnPropertyChanged(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }
    }
}

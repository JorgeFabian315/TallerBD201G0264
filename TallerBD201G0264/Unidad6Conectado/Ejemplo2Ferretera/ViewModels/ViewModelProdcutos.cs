using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TallerBD201G0264.Unidad6Conectado.Ejemplo2Ferretera.Models;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo2Ferretera.ViewModels
{
    public class ViewModelProdcutos:INotifyPropertyChanged
    {
        public ControlProductos Productos { get; set; }
        public bool visible { get; set; } = false;
        public long Total { get; set; }
        public string ProductoMasCaro { get; set; }
        public string ProductoMasBarato { get; set; }
        public long TotalProducctosSeccion { get; set; }
        public string MarcaMasVendida { get; set; }
        public string MarcaMenosVendida { get; set; }
        public decimal PromedioPrecio { get; set; }
        public decimal PrecioMayor { get; set; }
        public decimal PrecioMenor { get; set; }
        public ICommand ListaProductosseccion { get; set; }
        public ICommand CargarTodosElementos { get; set; }
        public ViewModelProdcutos()
        {
            Productos = new ControlProductos();
            PromedioPrecio = Productos.PromedioPrecio;
            Total = Productos.Total;
            MarcaMenosVendida = Productos.MarcaMenosVendida;
            MarcaMasVendida = Productos.MarcaMasVendida;
            ProductoMasCaro = Productos.ProductoMasCaro;
            ProductoMasBarato = Productos.ProductoMasBarato;
            PrecioMayor = Productos.PrecioMayor;
            PrecioMenor = Productos.PrecioMenor;
            ListaProductosseccion = new RelayCommand<Seccion>(ListaProductosSecciones);
            CargarTodosElementos = new RelayCommand(CargarTodos);
        }

        private void CargarTodos()
        {
            visible = false;
            Productos.CargarTodosElementos();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ListaProductosSecciones(Seccion seccion)
        {
            visible = true;
            Productos.BuscarPorSeccion(seccion);
            TotalProducctosSeccion = Productos.TotalProductosSeccion;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

        }
    }
}

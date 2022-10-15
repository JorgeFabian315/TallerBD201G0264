using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo2Ferretera.Models
{
    public class ControlProductos
    {
        public List<Producto> ProductosLista { get; set; } = new List<Producto>();
        public List<Seccion> SeccionesLista { get; set; } = new List<Seccion>();
        MySqlConnection conexion;
        MySqlCommand comandosql;
        MySqlDataReader lector;
        public long Total { get; set; }
        public long TotalProductosSeccion { get; set; }
        public decimal PrecioMayor { get; set; }
        public decimal PrecioMenor { get; set; }
        public decimal PromedioPrecio { get; set; }
        public string ProductoMasCaro { get; set; }
        public string ProductoMasBarato { get; set; }
        public string MarcaMasVendida { get; set; }
        public string MarcaMenosVendida { get; set; }

        public ulong TotalProductos
        {
            get { return (ulong)ProductosLista.Count; }
        }
        //public decimal PrecioPromedio
        //{
        //    get { return ProductosLista.Average(x => x.Precio)}
        //}

        public ControlProductos()
        {
            conexion = new MySqlConnection("server = localhost;user = root; database = Ferretera; password = root");
            Calculos();
            CargarTodosElementos();
            CargarSecciones();

        }
        public void Calculos()
        {
            if(conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            comandosql = new("select count(*) as total from productos", conexion);
            Total = (long)comandosql.ExecuteScalar();
            comandosql = new ("select max(precio) from productos", conexion);
            PrecioMayor = (decimal)comandosql.ExecuteScalar();
            comandosql = new("select min(precio) from productos", conexion);
            PrecioMenor = (decimal)comandosql.ExecuteScalar();
            comandosql = new MySqlCommand("select avg(precio) from productos", conexion);
            PromedioPrecio = (decimal)comandosql.ExecuteScalar();
            comandosql = new MySqlCommand("select nombre from productos where precio = (select max(precio) from productos)", conexion);
            ProductoMasCaro = (string)comandosql.ExecuteScalar();
            comandosql = new MySqlCommand("select nombre from productos where precio = (select min(precio) from productos)", conexion);
            ProductoMasBarato = (string)comandosql.ExecuteScalar();
            comandosql = new MySqlCommand("select marca from productos group by marca having marca != '' order by count(marca)  desc  limit 1", conexion);
            MarcaMasVendida = (string)comandosql.ExecuteScalar();
            comandosql = new MySqlCommand("select marca from productos group by marca having marca != '' order by count(marca)  asc  limit 1", conexion);
            MarcaMenosVendida = (string)comandosql.ExecuteScalar();

        }
        public void CargarTodosElementos()
        {
            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            comandosql = new MySqlCommand("select p.sku,p.marca,p.nombre,p.descripcion,p.precio,s.nombre " +
                "as seccion from productos as p inner join secciones as s on s.id = p.idseccion order by p.sku", conexion);
            lector = comandosql.ExecuteReader();
            ProductosLista = new List<Producto>();
            while (lector.Read() == true)
            {

                Producto p = new Producto()
                {
                    Sku = uint.Parse(lector.GetString(0)),
                    Marca = string.IsNullOrWhiteSpace((string)lector["Marca"]) ? "SIN MARCA" : (string)lector["Marca"],
                    Nombre = (string)lector["Nombre"],
                    //Descripcion = Convert.IsDBNull(lector["Descripcion"])?"No disponible": (string)lector["Descripcion"],
                    Precio = (decimal)lector["Precio"],
                    Seccion = (string)lector["Seccion"]

                };
                ProductosLista.Add(p);

            }
            lector.Close();

        }

        public void CargarSecciones()
        {
            if(conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            comandosql = new ("select nombre as secciones from secciones group by secciones order by nombre",conexion);
            lector = comandosql.ExecuteReader();
            while (lector.Read() == true)
            {
                Seccion s = new Seccion()
                {
                    NombreSeccion = (string)lector["secciones"]
                };
                SeccionesLista.Add(s);
            }
            lector.Close();

        }

        public void BuscarPorSeccion(Seccion seccion)
        {
            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            comandosql = new( "select p.sku,p.marca, p.nombre, p.precio,s.nombre as seccion from productos as p inner join " +
                "secciones as s on p.idseccion =s.id where s.id in (select id from secciones where nombre = '" + seccion.NombreSeccion + "')",conexion);
            lector = comandosql.ExecuteReader();
            ProductosLista = new List<Producto>();
            while (lector.Read() == true)
            {
                Producto s = new Producto()
                {
                    Sku = (uint)lector["Sku"],
                    Marca = string.IsNullOrWhiteSpace((string)lector["Marca"]) ? "SIN MARCA" : (string)lector["Marca"],
                    Nombre = (string)lector["Nombre"],
                    Precio = (decimal)lector["Precio"],
                    Seccion = (string)lector["seccion"]

                };
                ProductosLista.Add(s);
            }
            lector.Close();

            comandosql = new("select count(*) from productos as p inner join " +
               "secciones as s on p.idseccion =s.id where s.id in (select id from secciones where nombre = '" + seccion.NombreSeccion + "')", conexion);
            TotalProductosSeccion = (long)comandosql.ExecuteScalar();
        }

        ~ControlProductos()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }


    }
}

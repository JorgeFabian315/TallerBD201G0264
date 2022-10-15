using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo1Paises.Models
{
    public class ControlPaises
    {
        public List<Pais> Paises { get; set; } = new List<Pais>();
        MySqlConnection conexion;
        MySqlCommand comandosql;
        MySqlDataReader lector;

        public ControlPaises()
        {
            // Paso 1 establecer la conexion
            // paso 2 abrir conexion
            // paso 4 leer los datos
            // paso 5 cerrar la conexion
            conexion = new MySqlConnection("server = localhost;user = root; database = world; password = root");
            //conexion.ConnectionString = "server = localhost;user = root; database = world; password = root";
            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            // paso 3 ejecutar el comando
            comandosql = new MySqlCommand();
            comandosql.Connection = conexion;
            comandosql.CommandText = "select * from paises order by Name";
            //comandosql.ExecuteScalar(); solo una fila por ejemplo count cuentame o sum sumame 
            //comandosql.ExecuteNonQuery(); 
            lector = comandosql.ExecuteReader();
            while (lector.Read() == true)
            {
                Pais p = new Pais()
                {
                    Code = (string)lector["Code"],
                    Name = (string)lector["Name"],
                    Continent = (string)lector["Continent"],
                    Region = (string)lector["Region"],
                    SurfaceArea = (float)lector["SurfaceArea"],
                    IndepYear = Convert.IsDBNull(lector["IndepYear"])?(short)0:(short) lector["IndepYear"],
                    Population = (int)lector["Population"],
                    LifeExpectancy = Convert.IsDBNull(lector["LifeExpectancy"])?(float)0:(float)lector["LifeExpectancy"],
                    LocalName = (string)lector["LocalName"],
                    GovernmentForm = (string)lector["GovernmentForm"],
                    HeadOfState = Convert.IsDBNull(lector["HeadOfState"])?"No hay":(string)lector["HeadOfState"]
                };
                Paises.Add(p);
            }
            lector.Close();
        }

        ~ControlPaises()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}

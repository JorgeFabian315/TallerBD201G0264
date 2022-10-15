using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerBD201G0264.Unidad6Conectado.Ejemplo3AgendaElectronica.Models
{
    public class Agenda
    {
        // Instanciar los objetos de las clases para hacer la conexion,lectura y comandos
        MySqlConnection conexion = new MySqlConnection(); // sql server
        MySqlCommand comandosql;
        MySqlDataReader lector;
        public ObservableCollection<Amigo> ListaAmigos { get; set; } = new ObservableCollection<Amigo>();
        public Agenda()
        {
            conexion.ConnectionString = "server = localhost; user = root; database = agenda; password = root";
            Conectar();
            comandosql = new MySqlCommand("select * from amigos order by Nombre", conexion);
            lector = comandosql.ExecuteReader();
            while (lector.Read())
            {
                Amigo a = new Amigo
                {
                    Id = (int)lector["Id"],
                    Nombre = (string)lector["Nombre"],
                    CorreoelEctronico = Convert.IsDBNull(lector["CorreoelEctronico"]) ? "" : (string)lector["CorreoelEctronico"],
                    Telefono = (string)lector["Telefono"],
                    FechaNacimiento = (DateTime)lector["FechaNacimiento"]
                };
                //Insertar en la coleccion
                ListaAmigos.Add(a);
            }

        }
        private void Conectar()
        {
            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            else
                conexion.Close();
        }
        // validar a la defensiva  html xaml viewmodel model base de datos
        public void Create(Amigo a)
        {
            //1.- Verificar si es null
            if (a is not null)
            {
                if (a.CorreoelEctronico is null)
                {
                    comandosql.CommandText = $"insert into amigos(Nombre,Telefono,FechaNacimiento) values('{a.Nombre}','{a.Telefono}',{a.FechaNacimiento.ToString("yyyy-MM-dd")})";
                }
                else
                {
                    comandosql.CommandText = $"insert into amigos(Nombre,Telefono,FechaNacimiento,CorreoelEctronico) values('{a.Nombre}','{a.Telefono}',{a.FechaNacimiento.ToString("yyyy-MM-dd")}" +
                        $",'{a.CorreoelEctronico}')";
                }
                comandosql.Connection = conexion;
                if (comandosql.ExecuteNonQuery() != 0) // me regresa el numero de filas de filas afectadas 
                {
                    comandosql.CommandText = "select max(id) from amigos";
                    comandosql.Connection = conexion;
                    var id = comandosql.ExecuteScalar();
                    a.Id =(id is null)?0:(int)id;
                    ListaAmigos.Add(a);
                }
            }
        }
        public void Dlete(Amigo a)
        {
            if (a is not null)
            {
                comandosql = new MySqlCommand($"delete from amigos where id = {a.Id}");
                if (comandosql.ExecuteNonQuery() != 0)
                {
                    var amigo = ListaAmigos.FirstOrDefault(x => x.Id == a.Id);
                    if(amigo != null)
                    ListaAmigos.Remove(a);
                }
            }
        }
        public void Update(Amigo a)
        {
            if (a is not null)
            {
                // preguntar si el correo es nulo
                comandosql = new MySqlCommand($"");
                if (comandosql.ExecuteNonQuery() != 0)
                {
                    var amigo = ListaAmigos.FirstOrDefault(x => x.Id == a.Id);
                    if (amigo != null)
                        ListaAmigos.Remove(a);
                    // buscar el objeto que recibo a en la coleccion y sustituir los datos
                }
            }
        }
        ~Agenda()
        {
            Conectar();
        }
    }
}

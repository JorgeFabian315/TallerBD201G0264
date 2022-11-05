using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            conexion.ConnectionString = "server = localhost; user = root; database = agenda2; password = root";
            Conectar();
            comandosql = new MySqlCommand("select * from amigos order by Nombre", conexion);
            lector = comandosql.ExecuteReader();
            ListaAmigos = new ObservableCollection<Amigo>();
            while (lector.Read())
            {
                Amigo a = new Amigo
                {
                    Id = (int)lector["Id"],
                    Nombre = (string)lector["Nombre"],
                    NumeroControl = (string)lector["NumeroControl"],
                    CorreoElectronico = Convert.IsDBNull(lector["CorreoElectronico"]) ? "" : (string)lector["CorreoElectronico"],
                    Telefono = (string)lector["Telefono"],
                    FechaNacimiento = (DateTime)lector["FechaNacimiento"]
                };
                //Insertar en la coleccion
                ListaAmigos.Add(a);
            }
            lector.Close();
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
                if (a.CorreoElectronico is null)
                {
                    comandosql = new MySqlCommand($"insert into amigos(Nombre,Telefono,FechaNacimiento,NumeroControl) " +
                        $"values('{a.Nombre}','{a.Telefono}','{a.FechaNacimiento.ToString("yyyy-MM-dd")}','{a.NumeroControl}')", conexion);
                }
                else
                {
                    comandosql = new($"insert into amigos(Nombre,Telefono,FechaNacimiento,CorreoElectronico,NumeroControl) " +
                        $"values('{a.Nombre}','{a.Telefono}','{a.FechaNacimiento.ToString("yyyy-MM-dd")}'" +
                        $",'{a.CorreoElectronico}','{a.NumeroControl}')", conexion);
                }
                if (comandosql.ExecuteNonQuery() != 0) // me regresa el numero de filas de filas afectadas 
                {
                    comandosql = new("select max(id) from amigos", conexion);
                    var id = comandosql.ExecuteScalar();
                    a.Id = (id is null) ? 0 : (int)id;
                    ListaAmigos.Add(a);

                }
            }
        }
        public void Delete(Amigo a)
        {
            if (a is not null)
            {
                comandosql = new MySqlCommand($"delete from amigos where id = {a.Id}", conexion);
                if (comandosql.ExecuteNonQuery() != 0)
                {
                    var amigo = ListaAmigos.FirstOrDefault(x => x.Id == a.Id);
                    if (amigo != null)
                        ListaAmigos.Remove(a);
                }
            }
        }
        public void Update(Amigo a)
        {
                if (a.CorreoElectronico is not null)
                {
                    comandosql = new MySqlCommand($"update amigos set Nombre = '{a.Nombre}',NumeroControl = '{a.NumeroControl}',CorreoElectronico = '{a.CorreoElectronico}'" +
                            $", Telefono = '{a.Telefono}', FechaNacimiento ='{a.FechaNacimiento.ToString("yyyy-MM-dd")}' where Id = {a.Id};", conexion);
                }
                else
                {
                    comandosql = new MySqlCommand($"update amigos set Nombre = '{a.Nombre}', NumeroControl = '{a.NumeroControl}'" +
                        $", Telefono = '{a.Telefono}', FechaNacimiento ='{a.FechaNacimiento.ToString("yyyy-MM-dd")}' where Id = {a.Id};", conexion);
                }
            comandosql.ExecuteNonQuery();

            ////if (comandosql.ExecuteNonQuery() != 0)
            ////{
            ////    var temporal = ListaAmigos.FirstOrDefault(x => x.Id == a.Id);
            ////    if (temporal != null)
            ////    {
            ////        temporal.Nombre = a.Nombre;
            ////        temporal.Telefono = a.Telefono;
            ////        temporal.FechaNacimiento = a.FechaNacimiento;
            ////        if (a.CorreoElectronico != null)
            ////            temporal.CorreoElectronico = a.CorreoElectronico;
            ////    }
            
            ////    }

            }
        
      
        
        public void Buscar(string buscar)
        {
            comandosql = new MySqlCommand($"select * from amigos where nombre like '%{buscar}%'",conexion);
            lector = comandosql.ExecuteReader();

            ListaAmigos = new ObservableCollection<Amigo>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    Amigo a = new Amigo
                    {
                        Id = (int)lector["Id"],
                        Nombre = (string)lector["Nombre"],
                        NumeroControl = (string)lector["NumeroControl"],
                        CorreoElectronico = Convert.IsDBNull(lector["CorreoElectronico"]) ? "" : (string)lector["CorreoElectronico"],
                        Telefono = (string)lector["Telefono"],
                        FechaNacimiento = (DateTime)lector["FechaNacimiento"]
                    };
                    ListaAmigos.Add(a);
                }

            }
            lector.Close();

        }
        
        
        ~Agenda()
        {
            Conectar();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using Prog2.AgendaElectronica.Core;

namespace Prog2.AgendaElectronica.Data
{
    public class ContactoRepository
    {
        // Cambia "NOMBreservidor" por el nombre de tu instancia (ej. .\SQLEXPRESS)
        private readonly string _cadenaConexion = 
            @"Server=localhost\SQLEXPRESS;Database=AgendaDB;Integrated Security=True;";

        public void Insertar(Contacto c)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = @"INSERT INTO Contactos 
                (Nombre, Apellido, FechaNacimiento, Direccion, Genero, 
                EstadoCivil, Movil, Telefono, CorreoElectronico) 
                VALUES (@Nom, @Ape, @Fec, @Dir, @Gen, @Est, @Mov, @Tel, @Cor)";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Nom", c.Nombre);
                cmd.Parameters.AddWithValue("@Ape", c.Apellido);
                cmd.Parameters.AddWithValue("@Fec", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Dir", c.Direccion);
                cmd.Parameters.AddWithValue("@Gen", c.Genero);
                cmd.Parameters.AddWithValue("@Est", c.EstadoCivil);
                cmd.Parameters.AddWithValue("@Mov", c.Movil);
                cmd.Parameters.AddWithValue("@Tel", c.Telefono);
                cmd.Parameters.AddWithValue("@Cor", c.CorreoElectronico);

                conexion.Open();
                cmd.ExecuteNonQuery();
            } 
            
        }

        public void Modificar(Contacto c)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = @"UPDATE Contactos SET 
                Nombre = @Nom, 
                Apellido = @Ape, 
                FechaNacimiento = @Fec, 
                Direccion = @Dir, 
                Genero = @Gen, 
                EstadoCivil = @Est, 
                Movil = @Mov, 
                Telefono = @Tel, 
                CorreoElectronico = @Cor 
                WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Id", c.Id);
                cmd.Parameters.AddWithValue("@Nom", c.Nombre);
                cmd.Parameters.AddWithValue("@Ape", c.Apellido);
                cmd.Parameters.AddWithValue("@Fec", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Dir", c.Direccion);
                cmd.Parameters.AddWithValue("@Gen", c.Genero);
                cmd.Parameters.AddWithValue("@Est", c.EstadoCivil);
                cmd.Parameters.AddWithValue("@Mov", c.Movil);
                cmd.Parameters.AddWithValue("@Tel", c.Telefono);
                cmd.Parameters.AddWithValue("@Cor", c.CorreoElectronico);

                conexion.Open();
                cmd.ExecuteNonQuery();

            } 
         
        }

        public DataTable Buscar(string filtroNombre)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT * FROM Contactos WHERE Nombre LIKE @Filtro";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Filtro", "%" + filtroNombre + "%");
                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                return tabla;
            } 
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                string query = "DELETE FROM Contactos WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
           
        }
    }

}


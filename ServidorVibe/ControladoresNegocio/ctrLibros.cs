using ServidorVibe.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ServidorVibe.ControladoresNegocio
{
    public class ctrLibros
    {
        private string administradorBD = ConfigurationManager.ConnectionStrings["NombreConexionBD"].ConnectionString;
        public List<Libros> Obtener()
        {
            var respuesta = new List<Libros>();
            try
            {
                using (var connection = new SqlConnection(administradorBD))
                {
                    connection.Open();

                    var query = "SELECT * FROM Libros";
                    var command = new SqlCommand(query, connection);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var serie = new Libros
                            {
                                LibroId = Convert.ToInt32(reader["LibroId"]),
                                CodigoLibro = reader["CodigoLibro"].ToString(),
                                Titulo = reader["Titulo"].ToString(),
                                Caratula = reader["Caratula"].ToString(),
                                Autor = reader["Autor"].ToString(),
                                Tema = reader["Tema"].ToString(),
                                AñoPublicacion = reader["AñoPublicacion"].ToString(),
                                Editorial = reader["Editorial"].ToString(),
                                Estatus = Convert.ToInt32(reader["Estatus"])
                            };

                            respuesta.Add(serie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Add(new Libros{ CodigoLibro = "Error: " + ex.ToString()});
            }
            return respuesta;
        }

        public bool Actualizar(Libros objeto)
        {
            var respuesta = new bool();
            try
            {
                using (var connection = new SqlConnection(administradorBD))
                {
                    connection.Open();

                    var query = @"
                        UPDATE Libros
                        SET CodigoLibro = @CodigoLibro,
                            Titulo = @Titulo,
                            Caratula = @Caratula,
                            Autor = @Autor,
                            Tema = @Tema,
                            AñoPublicacion = @AñoPublicacion,
                            Editorial = @Editorial,
                            Estatus = @Estatus
                        WHERE LibroId = @LibroId
                    ";
                    var command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@LibroId", objeto.LibroId);
                    command.Parameters.AddWithValue("@CodigoLibro", objeto.CodigoLibro);
                    command.Parameters.AddWithValue("@Titulo", objeto.Titulo);
                    command.Parameters.AddWithValue("@Caratula", objeto.Caratula);
                    command.Parameters.AddWithValue("@Autor", objeto.Autor);
                    command.Parameters.AddWithValue("@Tema", objeto.Tema);
                    command.Parameters.AddWithValue("@AñoPublicacion", objeto.AñoPublicacion);
                    command.Parameters.AddWithValue("@Editorial", objeto.Editorial);
                    command.Parameters.AddWithValue("@Estatus", objeto.Estatus);

                    command.ExecuteNonQuery();
                }
                return respuesta = true;
            }
            catch (Exception ex)
            {
                return respuesta = false;
            }
        }
    }
}
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
                respuesta.Add(new Libros { CodigoLibro = "Error: " + ex.ToString() });
            }
            return true;
        }
    }
}
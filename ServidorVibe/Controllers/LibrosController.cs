using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using ServidorVibe.ControladoresNegocio;
using ServidorVibe.Entidades;

namespace ServidorVibe.Controllers
{
    public class LibrosController : ApiController
    {
        [HttpPost]
        [ActionName("Obtener")]
        public List<Libros> Obtener()
        {
            var controlador = new ctrLibros();
            var respuesta = controlador.Obtener();
            return respuesta;
        }

        [HttpPut]
        [ActionName("Actualizar")]
        public bool Actualizar(Libros objeto)
        {
            var controlador = new ctrLibros();
            var respuesta = controlador.Actualizar(objeto);
            return respuesta;
        }
    }
} 
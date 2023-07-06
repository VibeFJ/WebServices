using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using ServidorVibe.Entidades;

namespace ServidorVibe.Controllers
{
    public class GraficosController : ApiController
    {
        [HttpPost]
        [ActionName("Obtener")]
        public double Obtener(Graficos objeto)
        {
            for (int i = 0; i < objeto.contador; i++)
            {
                objeto.Exy += (i + 1) * objeto.Datos[i];
                objeto.Ex += (i + 1);
                objeto.Ey += objeto.Datos[i];
                objeto.Ex2 += (i + 1) * (i + 1);
                objeto._Ex_ = objeto.Ex * objeto.Ex;
            }

            if (objeto.Operacion)
            {
                objeto.Resultado = ((objeto.contador * objeto.Exy) - (objeto.Ex * objeto.Ey)) / ((objeto.contador * objeto.Ex2) - objeto._Ex_);
            }
            else
            {
                objeto.Resultado = ((objeto.Ey * objeto.Ex2) - (objeto.Ex * objeto.Exy)) / ((objeto.contador * objeto.Ex2) - objeto._Ex_);
            }

            return objeto.Resultado;
        }
    }
} 
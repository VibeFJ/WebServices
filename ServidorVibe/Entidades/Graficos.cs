using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorVibe.Entidades
{
    public class Graficos
    {
        public int contador { get; set; }
        public double Exy { get;set; }
        public double Ex { get; set; }
        public double Ey { get; set; }
        public double Ex2 { get; set; }
        public double _Ex_ { get; set; }
        public List<int> Datos { get; set; }
        public double Resultado { get; set; }
        public bool Operacion { get; set; }
    }
}

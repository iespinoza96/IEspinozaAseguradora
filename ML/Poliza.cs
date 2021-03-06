using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Poliza
    {
        public int IdPoliza { get; set;}
        public string Nombre { get; set; }
        public ML.SubPoliza SubPolzia { get; set; }
        public string NumeroPolzia { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public ML.Vigencia Vigencia { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<object> Polizas { get; set; }
    }
}

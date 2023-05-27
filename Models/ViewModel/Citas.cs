using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMORD.Models.ViewModel
{
    public class Citas
    {
        public int ID_SERVICIO { get; set; }
        public int ID_USUARIO { get; set; }
        public string CODIGO_BENF { get; set; }
        public byte[] DOCSER { get; set; }
        public int HORA { get; set; } //COMO OBTENER LA HORA, SOLO LA HORA
        public DateTime FECHA { get; set; }
        public string ESTADO_CITA { get; set; }
    }
}
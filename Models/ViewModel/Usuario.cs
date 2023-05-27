using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMORD.Models.ViewModel
{
    public class Usuario
    {   
        public int ID_USUARIO { get; set; }
        public string COD_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string CONTRASENA { get; set; }
        public string CORREO { get; set; }
        public int COD_ROL { get; set; }
        public string CODIGO_INSTITUCION { get; set; }
        public string ConfirmarClave { get; set; }
        public string TOKEN_RECOVERY { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMORD.Models.ViewModel
{
    public class DatosBeneficiario
    {
        public int CODIGO_BENF { get; set; }
        public string NOMBRE { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public string ESCOLARIDAD { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public string DIRECCION { get; set; }
        public string NOMBRE_MADRE { get; set; }
        public string ESCOLARIDAD_M { get; set; }
        public string OCUPACION_M { get; set; }
        public int TELEFONO_M { get; set; }
        public string NOMBRE_PADRE { get; set; }
        public string ESCOLARIDAD_P { get; set; }
        public string OCUPACION_P { get; set; }
        public int TELEFONO_P { get; set; }
        public int NUM_HERMANOS { get; set; }
        public string LUGAR_QUE_OCUPA { get; set; }
        public string MOTIVO_DE_CONSULTA { get; set; }
        public string ENFERMEDADES_PADECIENTES { get; set; }
        public string MEDICAMENTOS { get; set; }
        public string ESQUEMA_VACUNAS { get; set; }
        public string PRUEBAS_AUDITIVAS { get; set; }
        public string PRUEBAS_OFTALMOLOGICAS { get; set; }
        public string APARATO_AUDITIVO { get; set; }
        public string LENTES { get; set; }
        public string OTRAS { get; set; }
        public string CIRUGIAS { get; set; }
        public string EMBARAZO_TERMINO { get; set; }
        public string OBSERVACIONES_TE { get; set; }
        public string PARTO_NORMAL { get; set; }
        public string OBSERVACIONES_PARTO { get; set; }
        public string COMPLICACIONES_EMBARAZO { get; set; }
        public string NINO_LLORO { get; set; }
        public string COLORACION { get; set; }
        public string INCUBADORA { get; set; }
        public string COLOR_NACIMIENTO { get; set; }
        public string TRATAMIENTO_POSTPARTO { get; set; }
        public string INFECCIONES { get; set; }
        public string FIEBRES { get; set; }
        public string CONVULCIONES { get; set; }
        public string TIENE_LENGUAJE { get; set; }
        public string CAMINA { get; set; }
        public string OBSERVACIONES { get; set; }
        public byte[] ARCHIVO { get; set; }
        public double TAMANIO { get; set; }
        public string EXTENSION { get; set; }
        public string RUTA { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public string FORMATO { get; set; }

        //public FileBytes files { get; set; }

        public HttpPostedFileBase[] inputSubirArchivos { get; set; }



    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AMORD.Models.ViewModel;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Configuration;


namespace AMORD.Controllers


//CON EL CODGIO QUE SE INGRSE EN UN LABEL PODER HACER LA BUSQUEDA EN EL SELECT Y QUE EL DATO
//LO INGRESE EN EL QUERY DE LA TABLA CITAS


{
    public class PrincipalController : Controller
    {


        static string cadena = "Data Source=.;Initial Catalog=AMORD;Integrated Security=true";

        // GET: Principal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.ViewModel.Usuario datosU)
        {
            string cdm;

            cdm = "select * from USUARIO WHERE CORREO = '" + datosU.CORREO + "' AND CONTRASENA ='" + datosU.CONTRASENA + "'";
           

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand command = new SqlCommand(cdm, cn);

                

                cn.Open();

                command.CommandType = CommandType.Text;

                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    Console.WriteLine("Hola");
                }

                //string correo1 = "";
                //string pass = "";

                //datosU.CORREO = correo1;
                //datosU.CONTRASENA = pass;

                //if (correo1 !=)

                using (SqlConnection de = new SqlConnection(cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_VALIDARUSUARIO", de);
                    cmd.Parameters.AddWithValue("@CORREO", datosU.CORREO);
                    cmd.Parameters.AddWithValue("@CONTRASENA", datosU.CONTRASENA);
                    cmd.CommandType = CommandType.StoredProcedure;

                    de.Open();

                    
                        datosU.ID_USUARIO = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    

                    

                    if (datosU.ID_USUARIO != 0)
                    {
                        //Session["usuario"] = datosU;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "usuario no encontrado";
                        return Login();

                    }

                }
            }
        } 

        public ActionResult Crear_Usuario()
        {
            return Index();
        }

        [HttpPost]
        public ActionResult Crear_Usuario(Models.ViewModel.Usuario datosU)
        {
            string cdm = "select * from USUARIO WHERE COD_USUARIO = @cod_usuario";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand command = new SqlCommand(cdm, cn);
                command.Parameters.AddWithValue("@cod_usuario", datosU.COD_USUARIO);

                cn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //ViewData["Mensaje"] = "usuario no encontrado";
                    ModelState.AddModelError("cod_usuario", "ESTE CODIGO YA EXISTE");
                    reader.Close();
                }
                else
                {
                    reader.Close();

                    SqlCommand prn = new SqlCommand("INSERT INTO USUARIO (COD_USUARIO,NOMBRE,CORREO,CONTRASENA,COD_ROL,CODIGO_INSTITUCION) " +
                        "VALUES(@COD_USUARIO, @NOMBRE, @CORREO, @CONTRASENA, @COD_ROL, @CODIGO_INSTITUCION)", cn);

                    prn.Parameters.AddWithValue("@COD_USUARIO", datosU.COD_USUARIO);
                    prn.Parameters.AddWithValue("@NOMBRE", datosU.NOMBRE);
                    prn.Parameters.AddWithValue("@CORREO", datosU.CORREO);
                    prn.Parameters.AddWithValue("@CONTRASENA", datosU.CONTRASENA);
                    prn.Parameters.AddWithValue("@COD_ROL", datosU.COD_ROL);
                    prn.Parameters.AddWithValue("@CODIGO_INSTITUCION", datosU.CODIGO_INSTITUCION);

                    prn.ExecuteNonQuery();

                    ModelState.AddModelError("cod_usuario", "DATOS INGRESADOS CORRECTAMENTE");

                }
            }

            return Index();
        }

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }


        [HttpGet]
        public ActionResult DatosBeneficiario(HttpPostedFileBase[] archivoPDF)
        {
            return View();
        }
    
        [HttpPost]
        public ActionResult DatosBeneficiario(HttpPostedFileBase archivoPDF, Models.ViewModel.DatosBeneficiario datos)
        {
            //string Extension = Path.GetExtension(datos.files.FileName);

            //MemoryStream dp = new MemoryStream();
            //datos.files.InputStream.CopyTo(dp);
            //byte[] data = dp.ToArray();

            //string Extension2 = Path.GetExtension(datos.files.FileName);

            //MemoryStream dp2 = new MemoryStream();
            //datos.files.InputStream.CopyTo(dp2);
            //byte[] archivoBytes = dp2.ToArray();

            //obtener(datos, archivoBytes);

            //ViewBag.ArchivoPDF = archivoPDF;

            //if (archivoPDF != null && archivoPDF.ContentLength > 0)
            //{

            //    string nombrearch = Path.GetFileName(archivoPDF.FileName);
            //    string ruta = Path.Combine(Server.MapPath("~/Content/ArchivosPDF"));
            //    archivoPDF.SaveAs(ruta);

            //    datos.RUTA = "~/Content/ArchivosPDF/" + nombrearch;
            //}

            string cdm = "select * from BENEFICIARIO WHERE CODIGO_BENF = @codigo_benf";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand command = new SqlCommand(cdm, cn);
                command.Parameters.AddWithValue("@codigo_benf", datos.CODIGO_BENF);

                cn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //ViewData["Mensaje"] = "usuario no encontrado";
                    ModelState.AddModelError("codigo", "ESTE CODIGO YA EXISTE");
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    SqlCommand cmd = new SqlCommand("INSERT INTO BENEFICIARIO (CODIGO_BENF,NOMBRE,FECHA_NACIMIENTO,ESCOLARIDAD,FECHA_INGRESO,DIRECCION,NOMBRE_MADRE," +
                        "ESCOLARIDAD_M,OCUPACION_M,TELEFONO_M,NOMBRE_PADRE,ESCOLARIDAD_P,OCUPACION_P,TELEFONO_P,NUM_HERMANOS,LUGAR_QUE_OCUPA,MOTIVO_DE_CONSULTA" +
                        ")VALUES(@codigo_benf,@nombre,@fecha_nacimiento,@escolaridad,@fecha_ingreso,@direccion,@nombre_madre,@escolaridad_m,@ocupacion_m," +
                        "@telefono_m,@nombre_padre,@escolaridad_p,@ocupacion_p,@telefono_p,@num_hermanos,@lugar_que_ocupa,@motivo_de_consulta)", cn);

                    cmd.Parameters.AddWithValue("@codigo_benf", datos.CODIGO_BENF);
                    cmd.Parameters.AddWithValue("@nombre", datos.NOMBRE);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", datos.FECHA_NACIMIENTO);
                    cmd.Parameters.AddWithValue("@escolaridad", datos.ESCOLARIDAD);
                    cmd.Parameters.AddWithValue("@fecha_ingreso", datos.FECHA_INGRESO);
                    cmd.Parameters.AddWithValue("@direccion", datos.DIRECCION);
                    cmd.Parameters.AddWithValue("@nombre_madre", datos.NOMBRE_MADRE);
                    cmd.Parameters.AddWithValue("@escolaridad_m", datos.ESCOLARIDAD_M);
                    cmd.Parameters.AddWithValue("@ocupacion_m", datos.OCUPACION_M);
                    cmd.Parameters.AddWithValue("@telefono_m", datos.TELEFONO_M);
                    cmd.Parameters.AddWithValue("@nombre_padre", datos.NOMBRE_PADRE);
                    cmd.Parameters.AddWithValue("@escolaridad_p", datos.ESCOLARIDAD_P);
                    cmd.Parameters.AddWithValue("@ocupacion_p", datos.OCUPACION_P);
                    cmd.Parameters.AddWithValue("@telefono_p", datos.TELEFONO_P);
                    cmd.Parameters.AddWithValue("@num_hermanos", datos.NUM_HERMANOS);
                    cmd.Parameters.AddWithValue("@lugar_que_ocupa", datos.LUGAR_QUE_OCUPA);
                    cmd.Parameters.AddWithValue("@motivo_de_consulta", datos.MOTIVO_DE_CONSULTA);
                    
                    cmd.ExecuteNonQuery();


                    SqlCommand hc = new SqlCommand("INSERT INTO HISTORIAL_CLINICO (ENFERMEDADES_PADECIENTES,MEDICAMENTOS,ESQUEMA_VACUNAS,PRUEBAS_AUDITIVAS,PRUEBAS_OFTALMOLOGICAS,APARATO_AUDITIVO," +
                        "LENTES,OTRAS,CIRUGIAS,CODIGO_BENF) VALUES(@enfermedades_padecientes,@medicamentos,@esquema_vacunas,@pruebas_auditivas,@pruebas_oftalmologicas,@aparato_auditivo,@lentes,@otras," +
                        "@cirugias,@codigo_benf)", cn);

                    hc.Parameters.AddWithValue("@enfermedades_padecientes", datos.ENFERMEDADES_PADECIENTES);
                    hc.Parameters.AddWithValue("@medicamentos", datos.MEDICAMENTOS);
                    hc.Parameters.AddWithValue("@esquema_vacunas", datos.ESQUEMA_VACUNAS);
                    hc.Parameters.AddWithValue("@pruebas_auditivas", datos.PRUEBAS_AUDITIVAS);
                    hc.Parameters.AddWithValue("@pruebas_oftalmologicas", datos.PRUEBAS_OFTALMOLOGICAS);
                    hc.Parameters.AddWithValue("@aparato_auditivo", datos.APARATO_AUDITIVO);
                    hc.Parameters.AddWithValue("@lentes", datos.LENTES);
                    hc.Parameters.AddWithValue("@otras", datos.OTRAS);
                    hc.Parameters.AddWithValue("@cirugias", datos.CIRUGIAS);
                    hc.Parameters.AddWithValue("@codigo_benf", datos.CODIGO_BENF);

                    hc.ExecuteNonQuery();


                    //Terapia de Estimulación Temprana
                    //Terapia de integración Sensorial
                    //Terapia Física/ Hidroterapia
                    //Terapia de Comunicación y lenguaje
                    //Terapia pre ocupacional
                    //Terapia Ocupacional
                    //Psicología
                    //Servicio Medico
                    //Servicio Odontológico

                    SqlCommand ap = new SqlCommand("INSERT INTO ANTECEDENTES_PRENATALES (CODIGO_BENF,EMBARAZO_TERMINO,OBSERVACIONES_TE,PARTO_NORMAL,OBSERVACIONES_PARTO,COMPLICACIONES_EMBARAZO)" +
                        "VALUES(@codigo_benf,@embarazo_termino,@observaciones_te,@parto_normal,@observaciones_parto,@complicaciones_embarazo)", cn);

                    ap.Parameters.AddWithValue("@codigo_benf", datos.CODIGO_BENF);
                    ap.Parameters.AddWithValue("@embarazo_termino", datos.EMBARAZO_TERMINO);
                    ap.Parameters.AddWithValue("@observaciones_te", datos.OBSERVACIONES_TE);
                    ap.Parameters.AddWithValue("@parto_normal", datos.PARTO_NORMAL);
                    ap.Parameters.AddWithValue("@observaciones_parto", datos.OBSERVACIONES_PARTO);
                    ap.Parameters.AddWithValue("@complicaciones_embarazo", datos.COMPLICACIONES_EMBARAZO);

                    ap.ExecuteNonQuery();



                    SqlCommand prn = new SqlCommand("INSERT INTO ANTECEDENTES_PERINATALES (CODIGO_BENF,NINO_LLORO,COLORACION,INCUBADORA,COLOR_NACIMIENTO) " +
                        "VALUES(@codigo_benf,@nino_lloro,@coloracion,@incubadora,@color_nacimiento)", cn);

                    prn.Parameters.AddWithValue("@codigo_benf", datos.CODIGO_BENF);
                    prn.Parameters.AddWithValue("@nino_lloro", datos.NINO_LLORO);
                    prn.Parameters.AddWithValue("@coloracion", datos.COLORACION);
                    prn.Parameters.AddWithValue("@incubadora", datos.INCUBADORA);
                    prn.Parameters.AddWithValue("@color_nacimiento", datos.COLOR_NACIMIENTO);

                    prn.ExecuteNonQuery();


                    SqlCommand ptn = new SqlCommand("INSERT INTO ANTECEDENTES_POSTNATALES (CODIGO_BENF,TRATAMIENTO_POSTPARTO,INFECCIONES,FIEBRES,CONVULCIONES,TIENE_LENGUAJE," +
                        "CAMINA,OBSERVACIONES) VALUES (@codigo_benf,@tratamiento_postparto,@infecciones,@fiebres,@convulciones,@tiene_lenguaje," +
                        "@camina,@observaciones)", cn);

                    ptn.Parameters.AddWithValue("@codigo_benf", datos.CODIGO_BENF);
                    ptn.Parameters.AddWithValue("@tratamiento_postparto", datos.TRATAMIENTO_POSTPARTO);
                    ptn.Parameters.AddWithValue("@infecciones", datos.INFECCIONES);
                    ptn.Parameters.AddWithValue("@fiebres", datos.FIEBRES);
                    ptn.Parameters.AddWithValue("@convulciones", datos.CONVULCIONES);
                    ptn.Parameters.AddWithValue("@tiene_lenguaje", datos.TIENE_LENGUAJE);
                    ptn.Parameters.AddWithValue("@camina", datos.CAMINA);
                    ptn.Parameters.AddWithValue("@observaciones", datos.OBSERVACIONES);

                    ptn.ExecuteNonQuery();


                    ModelState.AddModelError("codigo", "DATOS INGRESADOS CORRECTAMENTE");
                }


            }

            //Respuesta_Json respuesta = new Respuesta_Json();
            //try
            //{


            //    for (int i = 0; i < archivos.Length; i++)
            //    {
            //        DatosBeneficiario archivo = new DatosBeneficiario();
            //        archivo.EXTENSION = Path.GetExtension(archivos[i].FileName);
            //        archivo.NOMBRE_ARCHIVO = Path.GetFileNameWithoutExtension(archivos[i].FileName);
            //        archivo.FORMATO = MimeMapping.GetMimeMapping(archivos[i].FileName);

            //        double tamanio = archivos[i].ContentLength;
            //        tamanio = tamanio / 1000000.0;
            //        archivo.TAMANIO = Math.Round(tamanio, 2);

            //        //convertir
            //        //archivo a un byte array
            //        Stream fs = archivos[i].InputStream;
            //        BinaryReader br = new BinaryReader(fs);
            //        archivo.ARCHIVO = br.ReadBytes((Int32)fs.Length);


            //        //como insertarlo a la base de datos
            //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
            //        {
            //            try
            //            {
            //                connection.Open();
            //                string sql = "insert into DOCUMENTOS(CODIGO_BENF,NOMBRE_ARCHIVO, EXTENSION, FORMATO, ARCHIVO, TAMANIO) values" +
            //                    "(@codigo_benf, @NOMBRE_ARCHIVO, @EXTENSION, @FORMATO, @ARCHIVO,@TAMANIO)";
            //                using (SqlCommand cmdp = new SqlCommand(sql, connection))
            //                {
            //                    cmdp.Parameters.Add("@codigo_benf", SqlDbType.VarChar, 10).Value = datos.CODIGO_BENF;
            //                    cmdp.Parameters.Add("@NOMBRE_ARCHIVO", SqlDbType.VarChar, 200).Value = archivo.NOMBRE_ARCHIVO;
            //                    cmdp.Parameters.Add("@EXTENSION", SqlDbType.VarChar, 10).Value = archivo.EXTENSION;
            //                    cmdp.Parameters.Add("@FORMATO", SqlDbType.VarChar, 200).Value = archivo.FORMATO;
            //                    cmdp.Parameters.Add("@ARCHIVO", SqlDbType.Image).Value = archivo.ARCHIVO;
            //                    cmdp.Parameters.Add("@TAMANIO", SqlDbType.Float).Value = archivo.TAMANIO;
            //                    cmdp.ExecuteNonQuery();
            //                }
            //                connection.Close();

            //                ViewData["Mensaje"] = "Documento insertado correctamente";
            //            }
            //            catch (Exception)
            //            {
            //                ViewData["Mensaje"] = "Documento no insertado";
            //            }

            //        }

            //    }
            //    respuesta.Codigo = 1;
            //    respuesta.Mensaje_Respuesta = "Se insertaron correctamente los archivos en la DB";
            //}
            //catch (Exception ex)
            //{
            //    respuesta.Codigo = 0;
            //    respuesta.Mensaje_Respuesta = ex.ToString();
            //    return Json(respuesta);
            //}
            //return Json(respuesta);


            return View();

        }

        [HttpGet]
        public ActionResult InsertarArchivos(HttpPostedFileBase[] archivos)
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertarArchivos(HttpPostedFileBase[] inputSubirArchivos, Models.ViewModel.DatosBeneficiario datos)
        {
            Respuesta_Json respuesta = new Respuesta_Json();
            try
            {
                //HttpPostedFileBase[] contenido = Request.Form["inputSubirArchivos"];

                

                for (int i = 0; i < datos.inputSubirArchivos.Length; i++)
                {
                    DatosBeneficiario archivo = new DatosBeneficiario();
                    archivo.EXTENSION = Path.GetExtension(datos.inputSubirArchivos[i].FileName);
                    archivo.NOMBRE_ARCHIVO = Path.GetFileNameWithoutExtension(datos.inputSubirArchivos[i].FileName);
                    archivo.FORMATO = MimeMapping.GetMimeMapping(datos.inputSubirArchivos[i].FileName);

                    double tamanio = datos.inputSubirArchivos[i].ContentLength;
                    tamanio = tamanio / 1000000.0;
                    archivo.TAMANIO = Math.Round(tamanio, 2);

                    //convertir
                    //archivo a un byte array
                    Stream fs = datos.inputSubirArchivos[i].InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    archivo.ARCHIVO = br.ReadBytes((Int32)fs.Length);



                    //como insertarlo a la base de datos
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                            string sql = "insert into DOCUMENTOS(CODIGO_BENF,NOMBRE_ARCHIVO, EXTENSION, FORMATO, ARCHIVO, TAMANIO) values" +
                                "(@codigo_benf, @NOMBRE_ARCHIVO, @EXTENSION, @FORMATO, @ARCHIVO,@TAMANIO)";
                            using (SqlCommand cmdp = new SqlCommand(sql, connection))
                            {
                                cmdp.Parameters.Add("@codigo_benf", SqlDbType.VarChar, 10).Value = datos.CODIGO_BENF;
                                cmdp.Parameters.Add("@NOMBRE_ARCHIVO", SqlDbType.VarChar, 200).Value = archivo.NOMBRE_ARCHIVO;
                                cmdp.Parameters.Add("@EXTENSION", SqlDbType.VarChar, 10).Value = archivo.EXTENSION;
                                cmdp.Parameters.Add("@FORMATO", SqlDbType.VarChar, 200).Value = archivo.FORMATO;
                                cmdp.Parameters.Add("@ARCHIVO", SqlDbType.Image).Value = archivo.ARCHIVO;
                                cmdp.Parameters.Add("@TAMANIO", SqlDbType.Float).Value = archivo.TAMANIO;
                                cmdp.ExecuteNonQuery();
                            }
                            connection.Close();

                            ViewData["Mensaje"] = "Documento insertado correctamente";
                        }
                        catch (Exception)
                        {
                            ViewData["Mensaje"] = "Documento no insertado";
                        }

                    }

                }
                respuesta.Codigo = 1;
                respuesta.Mensaje_Respuesta = "Se insertaron correctamente los archivos en la DB";
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 0;
                respuesta.Mensaje_Respuesta = ex.ToString();
                return Json(respuesta);
            }
            return Json(respuesta);
        }


        //[HttpPost]
        //public JsonResult InsertarEvaluaciones(HttpPostedFileBase[] archivos)
        //{

        //    Respuesta_Json respuesta = new Respuesta_Json();
        //    try
        //    {
        //        for (int i = 0; i < archivos.Length; i++)
        //        {
        //            ARCHIVOS archivo = new ARCHIVOS();
        //            archivo.Fecha_Entrada = DateTime.Now;
        //            archivo.Nombre_Archivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
        //            archivo.Extension = Path.GetExtension(archivos[i].FileName);
        //            archivo.Formato = MimeMapping.GetMimeMapping(archivos[i].FileName);

        //            double tamanio = archivos[i].ContentLength;
        //            tamanio = tamanio / 1000000.0;
        //            archivo.Tamanio = Math.Round(tamanio, 2);

        //            //convertir archivo a un byte array
        //            Stream fs = archivos[i].InputStream;
        //            BinaryReader br = new BinaryReader(fs);
        //            archivo.Archivo = br.ReadBytes((Int32)fs.Length);

        //            //como insertarlo a la base de datos
        //            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString))
        //            {
        //                connection.Open();
        //                string sql = "insert into Archivos(Nombre_Archivo, Extension, Formato, Fecha_Entrada, Archivo, Tamanio) values" +
        //                    "(@nombreArchivo,@extension, @formato, @fechaEntrada,@archivo, @tamanio )";
        //                using (SqlCommand cmd = new SqlCommand(sql, connection))
        //                {
        //                    cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar, 100).Value = archivo.Nombre_Archivo;
        //                    cmd.Parameters.Add("@extension", SqlDbType.VarChar, 5).Value = archivo.Extension;
        //                    cmd.Parameters.Add("@formato", SqlDbType.VarChar, 200).Value = archivo.Formato;
        //                    cmd.Parameters.Add("@fechaEntrada", SqlDbType.DateTime).Value = archivo.Fecha_Entrada;
        //                    cmd.Parameters.Add("@archivo", SqlDbType.Image).Value = archivo.Archivo;
        //                    cmd.Parameters.Add("@tamanio", SqlDbType.Float).Value = archivo.Tamanio;
        //                    cmd.ExecuteNonQuery();
        //                }
        //                connection.Close();
        //            }

        //        }
        //        respuesta.Codigo = 1;
        //        respuesta.Mensaje_Respuesta = "Se insertaron correctamente los archivos en la DB";
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Codigo = 0;
        //        respuesta.Mensaje_Respuesta = ex.ToString();
        //        return Json(respuesta);
        //    }
        //    return Json(respuesta);
        //}


        //agg vista




        //[HttpGet]
        //public ActionResult Formulario1()
        //{
        //    return View();
        //}

        
        //public ActionResult Formulario1(HttpPostedFileBase[] archivoPDF, Models.ViewModel.DatosBeneficiario datos)
        //{
        //    if (archivoPDF != null && archivoPDF.ContentLength > 0)
        //    {

        //        string nombrearch = Path.GetFileName(archivoPDF.Filename);
        //        datos.files.InputStream.Read(archivoBytes, 0, datos.files.ContentLength);
        //    }

        //    return View();
        //}

        //add vista 




        [HttpGet]
        public ActionResult Citas()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Citas()
        //{
        //    return View();
        //}


        [HttpGet]
        public ActionResult Servicios()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Servicios()
        //{
        //    return View();
        //}


        public ActionResult MostrarPersona_Get()
        {
            try
            {


                #region
                using (SqlConnection cn = new SqlConnection(cadena))
                {

                    string sentencia = "select CODIGO_BENF, NOMBRE, NOMBRE_MADRE ,NOMBRE_PADRE from beneficiario";
                    SqlDataAdapter da = new SqlDataAdapter(sentencia, cn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    #endregion

                    TempData["MSG"] = "Estos son los registros de la tabla persona";
                    return View(dt);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                DataTable dt = new DataTable();
                return View(dt);
            }



        }
    }
}

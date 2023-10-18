using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;
using Newtonsoft.Json;

namespace WebMatricula.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult BuscarNombre(string dni)
        {
            List<Alumno> listaAlumno = new List<Alumno>();
            
            listaAlumno = new CN_Matricula().buscarAlumno(dni);

            Alumno alumno = new Alumno();

            foreach (var v in listaAlumno)
            {
                alumno.IdAlumno = v.IdAlumno;
                alumno.Nombre = v.Nombre;
            }


            return Json(new { Nombre = alumno.Nombre , IdAlumno = alumno.IdAlumno}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarVacantes()
        {
            List<Vacantes> oLista = new List<Vacantes>();
            oLista = new CN_Matricula().listarVacantes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCurso()
        {
            List<Curso> oLista = new List<Curso>();
            oLista = new CN_Matricula().listarCursos();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarMatricula(string objeto)
        {
            string mensaje = string.Empty;
            bool operacion_exitosa = true;            

            Matricula objmatricula = new Matricula();
            objmatricula = JsonConvert.DeserializeObject<Matricula>(objeto);

            int resultado = new CN_Matricula().RegistrarMatricula(objmatricula);
            

            return Json(new { operacionExitosa = operacion_exitosa, idGenerado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
     }
}
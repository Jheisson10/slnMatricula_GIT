using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{    

    public class CN_Matricula
    {

        private CD_Matricula objCapaDato = new CD_Matricula();

        public List<Alumno> buscarAlumno(string dni)
        {            
            return objCapaDato.buscarAlumno(dni);
        }

        public List<Vacantes> listarVacantes()
        {
            return objCapaDato.listarVacantes();
        }

        public List<Curso> listarCursos()
        {
            return objCapaDato.listarCursos();
        }

        public int RegistrarMatricula(Matricula obj)
        {
            return objCapaDato.RegistrarMatricula(obj);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Matricula
    {
        public int id_matricula { get; set; }
        public int id_alumno { get; set; }
        public int id_modalidad { get; set; }
        public int id_vacante { get; set; }
        public DateTime fechaMatricula { get; set; }
        public string fechaAnulacion { get; set; }

    }
}

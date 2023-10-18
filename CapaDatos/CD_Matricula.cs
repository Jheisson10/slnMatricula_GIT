using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Matricula
    {
        public List<Alumno> buscarAlumno(string dni)
        {
            List<Alumno> listaAlumno = new List<Alumno>();

            using (OracleConnection cn = new OracleConnection(Conexion.cn))
            {
                                
                string queryString = "select id_alumno, nombres || ' ' || apellidos as Nombre from ALUMNO where DNI = '"+ dni +"'";
                using (OracleConnection connection = new OracleConnection(Conexion.cn))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);                    
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            listaAlumno.Add(new Alumno()
                            {
                                Nombre = reader["Nombre"].ToString(),
                                IdAlumno =  Convert.ToInt32(reader["id_alumno"].ToString())
                            });
                        }
                    }
                    finally
                    {
                        // always call Close when done reading.
                        reader.Close();
                    }
                }

            }

            return listaAlumno;

        }

        public List<Vacantes> listarVacantes()
        {

            List<Vacantes> listaVacante = new List<Vacantes>();

            using (OracleConnection cn = new OracleConnection(Conexion.cn))
            {

                string queryString = "select v.id_vacante, c.nombre as Curso,c.creditos,nvl(s.nombre,' ') as Seccion,nvl(to_char(v.cantidad_vacante),' ') as vacante from vacante v left join curso c on v.id_curso = c.id_curso left join seccion s on v.id_seccion = s.id_seccion";
                using (OracleConnection connection = new OracleConnection(Conexion.cn))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            listaVacante.Add(new Vacantes()
                            {
                                IdVacantes = Convert.ToInt32(reader["ID_VACANTE"].ToString()),
                                Curso = reader["CURSO"].ToString(),
                                Creditos = reader["CREDITOS"].ToString(),                                                             
                                Seccion = (reader["SECCION"].ToString()),
                                Vacante = (reader["VACANTE"].ToString())
                            }); ; ;
                        }
                    }
                    finally
                    {
                        // always call Close when done reading.
                        reader.Close();
                    }
                }

            }

            return listaVacante;
        }

        public List<Curso> listarCursos()
        {

            List<Curso> listaCurso = new List<Curso>();

            using (OracleConnection cn = new OracleConnection(Conexion.cn))
            {

                string queryString = "select id_curso,nombre from curso";
                using (OracleConnection connection = new OracleConnection(Conexion.cn))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            listaCurso.Add(new Curso()
                            {
                                IdCurso = Convert.ToInt32(reader["ID_CURSO"].ToString()),
                                Nombre = reader["NOMBRE"].ToString()
                            }); ; ;
                        }
                    }
                    finally
                    {
                        // always call Close when done reading.
                        reader.Close();
                    }
                }

            }

            return listaCurso;
        }

        public int RegistrarMatricula(Matricula obj)
        {
            int idautogenerado = 0;
            
            try
            {

                using (OracleConnection connection = new OracleConnection(Conexion.cn))
                {
                    connection.Open();                 

                    using (OracleCommand command = new OracleCommand("InsertarMatricula", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.Add("id_alumno", OracleType.Int32).Value = obj.id_alumno;
                        command.Parameters.Add("id_modalidad", OracleType.Int32).Value = obj.id_modalidad;
                        command.Parameters.Add("pid_vacante", OracleType.Number).Value = obj.id_vacante;
                        command.Parameters.Add("fechaMatricula", OracleType.VarChar).Value = obj.fechaMatricula.ToString("dd-MM-yyyy");                        

                        // Parámetro de salida para obtener el resultado (si es necesario)
                        command.Parameters.Add("p_resultado", OracleType.Number).Direction = ParameterDirection.Output;

                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();

                        // Obtener el valor de salida si es necesario
                        int resultado = Convert.ToInt32(command.Parameters["p_resultado"].Value);
                        
                    }

                    connection.Close();
                }



               
            }
            catch (Exception ex)
            {
                idautogenerado = -1;                
            }
            return idautogenerado;
        }

    }
}

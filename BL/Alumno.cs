using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context=new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumno in context.Alumnos
                                 select new
                                 {
                                     IdAlumno=tablaAlumno.IdAlumno,
                                     Nombre=tablaAlumno.Nombre,
                                     Genero=tablaAlumno.Genero,
                                     Edad=tablaAlumno.Edad,
                                 });
                    result.Objects = new List<object>();
                    if (query!=null && query.Count()>0)
                    {
                        foreach (var row in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = row.IdAlumno;
                            alumno.Nombre=row.Nombre;
                            alumno.Genero = bool.Parse(row.Genero.ToString());
                            alumno.Edad = int.Parse(row.Edad.ToString());
                            result.Objects.Add(alumno);
                            result.Correct = true;

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "Error no hay registros por mostrar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context=new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumno in context.Alumnos
                                 where tablaAlumno.IdAlumno == IdAlumno
                                 select new
                                 {
                                     IdAlumno=tablaAlumno.IdAlumno,
                                     Nombre=tablaAlumno.Nombre,
                                     Genero=tablaAlumno.Genero,
                                     Edad=tablaAlumno.Edad,
                                 });
                    if (query!=null && query.Count()>0)
                    {
                        ML.Alumno alumno = new ML.Alumno();
                        var queryDatos = query.ToList().Single();
                        alumno.IdAlumno = queryDatos.IdAlumno;
                        alumno.Nombre = queryDatos.Nombre;
                        alumno.Genero = bool.Parse(queryDatos.Genero.ToString());
                        alumno.Edad = int.Parse(queryDatos.Edad.ToString());

                        result.Object=alumno;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se puede mostrar el alumno";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context=new DL.MsandovalAlfaSolucionesContext())
                {
                    DL.Alumno nuevoAlumno = new DL.Alumno();
                    nuevoAlumno.Nombre= alumno.Nombre;
                    nuevoAlumno.Genero = alumno.Genero;
                    nuevoAlumno.Edad = alumno.Edad;
                    context.Alumnos.Add(nuevoAlumno);
                    context.SaveChanges();
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context=new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumno in context.Alumnos
                                 where tablaAlumno.IdAlumno == alumno.IdAlumno
                                 select tablaAlumno).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = alumno.Nombre;
                        query.Genero = alumno.Genero;
                        query.Edad = alumno.Edad;
                        context.SaveChanges();
                        result.Correct=true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MsandovalAlfaSolucionesContext context=new DL.MsandovalAlfaSolucionesContext())
                {
                    var query = (from tablaAlumno in context.Alumnos
                                 where tablaAlumno.IdAlumno == IdAlumno
                                 select tablaAlumno).First();
                    context.Alumnos.Remove(query);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
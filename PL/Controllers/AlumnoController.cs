using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Alumno alumno=new ML.Alumno();
            alumno.Alumnos = new List<object>();
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var responseTask = client.GetAsync("Alumno");
                responseTask.Wait();

                var resultService=responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var itemAlumno in readTask.Result.Objects)
                    {
                        ML.Alumno alumnoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(itemAlumno.ToString());
                        alumno.Alumnos.Add(alumnoResult);
                    }
                }
            }        
            
            return View(alumno);
        }
        [HttpGet]
        public IActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = new List<object>();
            if (IdAlumno!=null)
            {
                using (var client =new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5280/api/");
                    var responseTask = client.GetAsync("Alumno/" + IdAlumno);
                    responseTask.Wait();

                    var resultService = responseTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Alumno alumnoResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());
                        alumno = alumnoResult;

                    }

                }
            }
            else
            {

            }
            return View(alumno);
        }
        [HttpPost]
        public IActionResult Form(ML.Alumno alumno)
        {
            if (alumno.IdAlumno==0)//Add
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5280/api/Alumno");
                    var postTask = client.PostAsJsonAsync(client.BaseAddress, alumno);
                    postTask.Wait();

                    var resultPost=postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se agrego correctamente el alumno";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo agrega correctamente el alumno";
                    }
                }
            }
            else//Update
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5280/api/");
                    var putTask=client.PutAsJsonAsync("Alumno/"+alumno.IdAlumno,alumno);
                    putTask.Wait();
                    var resultPut = putTask.Result;
                    if (resultPut.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se actualizo correctamente el alumno";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo actualizar correctamente el alumno";
                    }
                }
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public IActionResult Delete(int IdAlumno)
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5280/api/");
                var deleteTask = client.DeleteAsync("Alumno/" + IdAlumno);
                deleteTask.Wait();
                var resultDelete=deleteTask.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se elimino correctamente el alumno";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo borrar el alumno";
                }
            }
            return PartialView("Modal");
        }
    }
}

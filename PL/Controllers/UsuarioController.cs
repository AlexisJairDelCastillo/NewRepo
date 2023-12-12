using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public UsuarioController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string contrasenia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    var responseTask = client.GetAsync("usuario/loginusuario/" + email);
                    responseTask.Wait();

                    var resultLogin = responseTask.Result;

                    if (resultLogin.IsSuccessStatusCode)
                    {
                        var readTask = resultLogin.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Usuario usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());

                        if (contrasenia == usuario.Contrasenia)
                        {

                            return View("../Home/Index");
                        }
                        else
                        {
                            ViewBag.Message = "La contrasenia es incorrecta.";
                            return PartialView("Modal");
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result resultUsuario = new ML.Result();
            resultUsuario.Objects = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("usuario/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuario.Objects.Add(resultItemList);
                    }
                }

                usuario.Usuarios = resultUsuario.Objects;
            }

            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultAcceso = BL.Acceso.GetAll(); ;
            usuario.Acceso = new ML.Acceso();
            usuario.Acceso.Accesos = resultAcceso.Objects;

            if (IdUsuario == null)
            {
                ViewBag.Titulo = "Registrar";
                ViewBag.Action = "Agregar";

                return View(usuario);
            }
            else
            {
                ViewBag.Titulo = "Modificar";
                ViewBag.Action = "Actualizar";

                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

                if (result.Object != null)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Acceso.Accesos = resultAcceso.Objects;

                    return View(usuario);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = result.Message;

                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == 0)
            {
                ML.Result result = new ML.Result();

                using(HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Usuario>("usuario/add", usuario);
                    postTask.Wait();

                    HttpResponseMessage resultTask = postTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                        ViewBag.Titulo = "El registro se inserto correctamente.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "Ocurrio un error al insertar el registro.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                }
            }
            else
            {
                ML.Result result = new ML.Result();

                using(HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PutAsJsonAsync<ML.Usuario>("usuario/update/" + usuario.IdUsuario, usuario);
                    postTask.Wait();

                    HttpResponseMessage resultTask = postTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                        ViewBag.Titulo = "El registro se actualizo correctamente.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "Ocurrio un error al actualizar el registro.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result resultUsuario = new ML.Result();
            int idUsuario = usuario.IdUsuario;

            using(HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.DeleteAsync("usuario/delete/" + idUsuario);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "El registro se elimino correctamente.";

                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al eliminar el registro.";

                    return View("Modal");
                }
            }
        }
    }
}

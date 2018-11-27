using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuienesQuien.Models;
using QuienEsQuien.Models;

namespace QuienEsQuien.Controllers
{
    public class BackOfficeController : Controller
    {
        // GET: backOffice

        Conexion bd = new Conexion();

        public ActionResult Index()
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //ABM PREGUNTAS
        public ActionResult Preguntas()
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                List<Preguntas> Preguntas = new List<Preguntas>();

                Preguntas = bd.ListarPreguntas();

                ViewBag.Lista = Preguntas;
                return View("ABM_Preguntas");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult EdicionPregunta(string Accion, int ID = 0)
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                ViewBag.Accion = Accion;
                if (Accion == "Insertar")
                {
                    Preguntas P = bd.ObtenerPregunta(ID);
                    ViewBag.Id = ID;
                    ViewBag.Categorias = bd.ListarCategorias();
                    ViewBag.PreguntaNow_cate = P.IdCategoria;
                    ViewBag.CategoriaNow = null;
                    ViewBag.Contador = 0;
                    return View("FinPregunta", P);
                }
                if (Accion == "Modificar")
                {
                    Preguntas P = bd.ObtenerPregunta(ID);
                    ViewBag.Id = ID;
                    ViewBag.Categorias = bd.ListarCategorias();
                    ViewBag.PreguntaNow_cate = P.IdCategoria;
                    return View("FinPregunta", P);
                }
                if (Accion == "Ver")
                {
                    Preguntas P = bd.ObtenerPregunta(ID);
                    Categorias c = bd.ObtenerCategoria(P.IdCategoria);
                    ViewBag.Categoria = c.Nombre;
                    return View("FinPregunta", P);
                }
                if (Accion == "Eliminar")
                {
                    bool x = true;
                    List<Personaje_pregunta> lista = bd.ListarPersonajes_Pregunta();
                    foreach (Personaje_pregunta miPersonaje_pregunta in lista)
                    {
                        if (miPersonaje_pregunta.IdPregunta == ID)
                        {
                            ViewBag.Error = "No se puede eliminar la pregunta seleccionada porque está relacionada con algún personaje";
                            x = false;
                        }
                    }
                    if (x == true)
                    {
                        bd.EliminarPregunta(ID);
                    }
                    List<Preguntas> Categoria = new List<Preguntas>();
                    Categoria = bd.ListarPreguntas();
                    ViewBag.Lista = Categoria;
                    return View("ABM_Preguntas");
                }
                return View("ABM_Preguntas");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult CosasAPregunta(Preguntas x, string Accion, int tCate, int Id = 0)
        {
            int r;
            x.IdPregunta = Id;
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                if (Accion == "Insertar")
                {
                    if (x.Texto == null && tCate == -1)
                    {
                        ViewBag.Error = "Ingrese datos correctos en los campos";
                        ViewBag.Accion = Accion;
                        ViewBag.Categorias = bd.ListarCategorias();
                        return View("FinPregunta", x);
                    }
                    else if (x.Texto == null)
                    {
                        ViewBag.Error = "Ingrese texto en la pregunta";
                        ViewBag.Accion = Accion;
                        ViewBag.Categorias = bd.ListarCategorias();

                        //hacer que la categoria no vuelva a -1
                        ViewBag.CategoriaNow = bd.ObtenerCategoria(tCate);
                        ViewBag.Contador = 0;
                        //fin

                        return View("FinPregunta", x);
                    }
                    else if (tCate == -1)
                    {
                        ViewBag.Error = "Ingrese una categoría";
                        ViewBag.Accion = Accion;
                        ViewBag.Categorias = bd.ListarCategorias();
                        return View("FinPregunta", x);
                    }
                    x.IdCategoria = tCate;
                    List<Preguntas> listaPreg = bd.ListarPreguntas();
                    foreach (Preguntas preg in listaPreg)
                    {
                        if (preg.Texto == x.Texto)
                        {
                            if (preg.IdCategoria == x.IdCategoria)
                            {
                                ViewBag.Accion = Accion;
                                ViewBag.Categorias = bd.ListarCategorias();
                                ViewBag.Error = "Esa pregunta ya existe en esa categoría";
                                return View("FinPregunta", x);
                            }
                        }
                    }
                    r = bd.InsertarPregunta(x);
                }
                else if (Accion == "Modificar")
                {
                    if (x.Texto == null)
                    {
                        ViewBag.Error = "Ingrese texto en la pregunta";
                        ViewBag.Accion = Accion;
                        ViewBag.Categorias = bd.ListarCategorias();

                        return View("FinPregunta", x);
                    }
                    x.IdCategoria = tCate;
                    List<Preguntas> listaPreg = bd.ListarPreguntas();
                    foreach (Preguntas preg in listaPreg)
                    {
                        if (preg.Texto == x.Texto)
                        {
                            if (preg.IdCategoria == x.IdCategoria)
                            {
                                ViewBag.Accion = Accion;
                                ViewBag.Categorias = bd.ListarCategorias();
                                ViewBag.Error = "Esa pregunta ya existe en esa categoría";
                                return View("FinPregunta", x);
                            }
                        }
                    }
                    r = bd.ModificarPregunta(x);
                }
                List<Preguntas> MiPregunta = new List<Preguntas>();
                MiPregunta = bd.ListarPreguntas();

                ViewBag.Lista = MiPregunta;
                return View("ABM_Preguntas");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        // ABM CATEGORIAS

        public ActionResult Categorias()
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                List<Categorias> Categoria = new List<Categorias>();
                Conexion MiConexion = new Conexion();

                Categoria = MiConexion.ListarCategorias();

                ViewBag.Lista = Categoria;
                return View("ABM_Categorias");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult EdicionCategoria(string Accion, int ID = 0)
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                bool x = true;
                ViewBag.Accion = Accion;
                if (Accion == "Modificar")
                {
                    Categorias C = bd.ObtenerCategoria(ID);
                    ViewBag.Id = ID;
                    return View("FormTrabajador", C);
                }
                if (Accion == "Ver")
                {
                    Categorias C = bd.ObtenerCategoria(ID);
                    return View("FormTrabajador", C);
                }
                if (Accion == "Eliminar")
                {
                    List<Personajes> lista = bd.ListarPersonajes();
                    foreach (Personajes miPersonaje in lista)
                    {
                        if (miPersonaje.IdCategoria == ID)
                        {
                            ViewBag.BajaCategoria = "No se puede eliminar la categoría seleccionada porque uno o más personajes pertenecen a ella";
                            x = false;
                            List<Categorias> Categoria = new List<Categorias>();
                            Categoria = bd.ListarCategorias();
                            ViewBag.Lista = Categoria;
                        }
                    }
                    if (x == true)
                    {
                        bd.EliminarTrabajador(ID);
                        List<Categorias> Categoria = new List<Categorias>();
                        Categoria = bd.ListarCategorias();
                        ViewBag.Lista = Categoria;
                    }
                    return View("ABM_Categorias");
                }
                if (Accion == "Insertar")
                {
                    return View("FormTrabajador");
                }

                return View("FormTrabajador");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult CosasATrabajador(Categorias x, string Accion, int Id = 0)
        {
            x.IdCategoria = Id;
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                Conexion MiConexion2 = new Conexion();
                List<Categorias> listaCategorias = new List<Categorias>();
                listaCategorias = MiConexion2.ListarCategorias();

                if (Accion == "Insertar")
                {
                    List<Categorias> MiCategoria = new List<Categorias>();
                    MiCategoria = MiConexion2.ListarCategorias();
                    foreach (Categorias MiCAtegoria in MiCategoria)
                    {
                        if (MiCAtegoria.Nombre == x.Nombre)
                        {
                            ViewBag.Categorias = listaCategorias;
                            ViewBag.Baja = "Ya existe una categoria con ese nombre";
                            ViewBag.Accion = Accion;
                            return View("FormTrabajador");
                        }
                    }
                    MiConexion2.InsertarCategoria(x);
                }
                if (Accion == "Modificar")
                {
                    MiConexion2.ModificarCategoria(x);
                }
                List<Categorias> MiCateforia = new List<Categorias>();
                MiCateforia = MiConexion2.ListarCategorias();

                ViewBag.Lista = MiCateforia;
                return View("ABM_Categorias");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //ABM REGUNTAS_PERSONAJES

        public ActionResult Personajes_Pregunta(int ID, string Nombre,int IdCategoria)
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                List<Personaje_pregunta> ListaPersonajes_Pregunta = new List<Personaje_pregunta>();
                List<Preguntas> ListaPreguntas = new List<Preguntas>();
                List<Preguntas> ListaPregsPersonaje = new List<Preguntas>();
                Conexion MiConexion = new Conexion();
                ListaPreguntas = MiConexion.ListarPreguntas();
                ListaPersonajes_Pregunta = MiConexion.ListarPersonajes_Pregunta();
                ListaPregsPersonaje = MiConexion.ListarPreguntasxPersonaje(ID);
                List<int> ListaIdsPregPers = new List<int>();
                foreach (Preguntas P in ListaPregsPersonaje)
                {
                    ListaIdsPregPers.Add(P.IdPregunta);
                }

                ViewBag.Lista = ListaPersonajes_Pregunta;
                ViewBag.IdPersonaje = ID;
                ViewBag.IdCategoria = IdCategoria;
                ViewBag.ListaPreguntas = ListaPreguntas;
                ViewBag.ListaPregsPersonaje = ListaIdsPregPers;
                ViewBag.NombrePersonaje = Nombre;
                return View("ABM_Personajes_Pregunta");

            }//ACA
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult EdicionPersonaje_Pregunta(int[] Box,int IdPersonaje)
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                Conexion MiConexion1 = new Conexion();
                List<Preguntas> ListaPregsPersonaje = new List<Preguntas>();
                ListaPregsPersonaje = MiConexion1.ListarPreguntasxPersonaje(IdPersonaje);
                    MiConexion1.EliminarPersonaje_Pregunta(IdPersonaje);
                if (Box != null)
                {
                    foreach (int i in Box)
                    {
                        MiConexion1.InsertarPersonaje_Pregunta(IdPersonaje, i);
                    }
                }
             
                return RedirectToAction("Personajes", "BackOffice");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //ABM PERSONAJES

        public ActionResult Personajes()
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                List<Personajes> ListaPersonajes = new List<Personajes>();
                Conexion MiConexion = new Conexion();

                ListaPersonajes = MiConexion.ListarPersonajes();

                ViewBag.Lista = ListaPersonajes;
                return View("ABM_Personajes");

            }//ACA
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EdicionPersonaje(string Accion, int ID = 0)
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                Conexion MiConexion1 = new Conexion();
                ViewBag.Accion = Accion;
                List<Categorias> listaCategorias = new List<Categorias>();
                listaCategorias = MiConexion1.ListarCategorias();
                ViewBag.Categorias = listaCategorias;


                if (Accion == "Modificar")
                {

                    Personajes C = MiConexion1.ObtenerPersonaje(ID);
                    ViewBag.Id = ID;
                    ViewBag.Imagen = C.Imagen;
                    return View("FormPersonaje", C);
                }
                if (Accion == "Ver")
                {
                    Personajes C = MiConexion1.ObtenerPersonaje(ID);
                    ViewBag.Imagen = C.Imagen;
                    return View("FormPersonaje", C);
                }
                if (Accion == "Eliminar")
                {


                    MiConexion1.EliminarPersonaje(ID);
                    List<Personajes> Personaje = new List<Personajes>();
                    Personaje = MiConexion1.ListarPersonajes();
                    ViewBag.Lista = Personaje;

                    return View("ABM_Personajes");
                }
                if (Accion == "Insertar")
                {
                    return View("FormPersonaje");
                }

                return View("FormPersonaje");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult CosasAPersonaje(Personajes x, string Accion, int Id = 0)

        {
            string path = null;
            string fileName = null;
            x.IdPersonaje = Id;
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                Conexion MiConexion2 = new Conexion();
                List<Categorias> listaCategorias = new List<Categorias>();
                listaCategorias = MiConexion2.ListarCategorias();
                if (Accion == "Insertar")
                {
                    List<Personajes> MiPersonaje = new List<Personajes>();
                    MiPersonaje = MiConexion2.ListarPersonajes();
                    foreach (Personajes Personaje in MiPersonaje)
                    {
                        if (Personaje.Nombre == x.Nombre)
                        {
                            ViewBag.Categorias = listaCategorias;
                            ViewBag.Baja = "Ya existe un personaje con ese nombre";
                            ViewBag.Accion = Accion;
                            return View("FormPersonaje");
                        }
                    }


                    MiConexion2.InsertarPersonaje(x);
                    if (x.Archivo != null)
                    {
                        path = Server.MapPath("~/Content/");
                        fileName = Path.GetFileName(x.Archivo.FileName);
                        string filename = x.Archivo.FileName;
                        x.Archivo.SaveAs(path + fileName);
                        path = path + fileName;
                        x.Imagen = x.Archivo.FileName;
                    }
                }
                if (Accion == "Modificar")
                {


                    if (x.Archivo == null)
                    {
                        string Imagen1 = MiConexion2.ObtenerImagen(Id);
                        x.Imagen = Imagen1;

                    }
                    else
                    {
                        path = Server.MapPath("~/Content/");
                        fileName = Path.GetFileName(x.Archivo.FileName);
                        string filename = x.Archivo.FileName;
                        x.Archivo.SaveAs(path + fileName);
                        path = path + fileName;
                        x.Imagen = x.Archivo.FileName;
                    }


                    MiConexion2.ModificarPersonaje(x);
                }
                List<Personajes> MiCateforia = new List<Personajes>();
                MiCateforia = MiConexion2.ListarPersonajes();

                ViewBag.Lista = MiCateforia;
                return View("ABM_Personajes");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }








    }
}


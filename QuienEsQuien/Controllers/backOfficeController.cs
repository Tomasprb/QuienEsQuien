using System;
using System.Collections.Generic;
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
                if (Accion == "Modificar")
                {
                    Preguntas P = bd.ObtenerPregunta(ID);
                    ViewBag.Id = ID;
                    return View("FinPregunta", P);
                }
                if (Accion == "Ver")
                {
                    Preguntas P = bd.ObtenerPregunta(ID);
                    return View("FinPregunta", P);
                }
                if (Accion == "Eliminar")
                {
                    bd.EliminarPregunta(ID);
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

        public ActionResult CosasATrabajador(Categorias x, string Accion, int Id = 0)
        {
            x.IdCategoria = Id;
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                Conexion MiConexion2 = new Conexion();
                if (Accion == "Insertar")
                {
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

        public ActionResult Personajes_Pregunta()
        {
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                List<Personajes> ListaPersonajes = new List<Personajes>();
                Conexion MiConexion = new Conexion();

                ListaPersonajes = MiConexion.ListarPersonajes();

                ViewBag.Lista = ListaPersonajes;
                return View("ABMM_Personajes_Pregunta");

            }//ACA
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //------------------------------------------------------------

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
                bool x = true;
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
                    List<Personajes> lista = MiConexion1.ListarPersonajes();
                    foreach (Personajes miPersonaje in lista)
                    {
                        if (miPersonaje.IdPersonaje == ID)
                        {
                            ViewBag.BajaCategoria = "No se puede eliminar la categoría seleccionada porque uno o más personajes pertenecen a ella";
                            x = false;
                            List<Personajes> Personaje = new List<Personajes>();
                            Personaje = MiConexion1.ListarPersonajes();
                            ViewBag.Lista = Personaje;
                        }
                    }
                    if (x == true)
                    {
                        MiConexion1.EliminarPersonaje(ID);
                        List<Personajes> Personaje = new List<Personajes>();
                        Personaje = MiConexion1.ListarPersonajes();
                        ViewBag.Lista = Personaje;
                    }
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

        public ActionResult CosasAPersonaje(string Imagen, Personajes x, string Accion, int Id = 0)
        {
            x.IdCategoria = Id;
            if (Convert.ToBoolean(Session["AdminNow"]) == true)
            {
                Conexion MiConexion2 = new Conexion();
                if (Accion == "Insertar")
                {
                    MiConexion2.InsertarPersonaje(x);
                }
                if (Accion == "Modificar")
                {
                    x.Imagen = Imagen;
                    if (Imagen != x.Imagen)
                    MiConexion2.ModificarPersonaje(x);
                }
                List<Personajes> MiCateforia = new List<Personajes>();
                MiCateforia = MiConexion2.ListarPersonajes();

                ViewBag.Lista = MiCateforia;
                return View("ABM_Categorias");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }








    }
}


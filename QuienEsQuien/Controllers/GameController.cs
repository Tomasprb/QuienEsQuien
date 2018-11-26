using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuienesQuien.Models;
using QuienEsQuien.Models;

namespace QuienEsQuien.Controllers
{
    public class GameController : Controller
    {
        Conexion BD = new Conexion();
        // GET: Game
        public ActionResult Index()
        {
            Session["Primera"] = true;
            Session["BitcoinsARestar"] = 0;
            ViewBag.Categorias = BD.ListarCategorias();
            return View();
        }
        public ActionResult Categoria(int tCate)
        {
            if (tCate == -1)
            {
                ViewBag.Categorias = BD.ListarCategorias();
                ViewBag.Error = "Seleccione una categoría";
                return View("Index");
            }
            if (tCate != 0)
            {
                Personajes MiPersonaje = new Personajes();
                Conexion MiConexion2 = new Conexion();
                List<Personajes> MisPersonajes = new List<Personajes>();
                MisPersonajes = MiConexion2.PersonajesPorCategoria(tCate);
                int Num = MisPersonajes.Count();
                int n = new Random().Next(1, Num);
                MiPersonaje = MisPersonajes[n - 1];
                Session["PersonajeAzar"] = MiPersonaje;
                List<Personajes> ListaPersonajes = new List<Personajes>();
                Conexion MiConexion = new Conexion();
                ListaPersonajes = MiConexion.PersonajesPorCategoria(tCate);
                ViewBag.Lista = ListaPersonajes;
                Session["ListaPersonajes"] = ListaPersonajes;
            }
            else
            {
                Personajes MiPersonaje2 = new Personajes();
                Conexion MiConexion3 = new Conexion();
                List<Personajes> MisPersonajes1 = new List<Personajes>();
                MisPersonajes1 = MiConexion3.Personajes();
                int Num = MisPersonajes1.Count();
                int n = new Random().Next(1, Num);
                MiPersonaje2 = MisPersonajes1[n];
                Session["PersonajeAzar"] = MiPersonaje2;
                List<Personajes> ListaPersonajes = new List<Personajes>();
                Conexion MiConexion = new Conexion();
                ListaPersonajes = MiConexion.Personajes();
                ViewBag.Lista = ListaPersonajes;
                Session["ListaPersonajes"] = ListaPersonajes;
            }
            Session["Categoría"] = tCate;
            return RedirectToAction("Mostrar_personajes");
        }
        public ActionResult Mostrar_personajes()
        {
            if ((int)Session["BitcoinsARestar"] != 0 && (bool)Session["Primera"] == false)
            {
                Session["BitcoinsARestar"] = (int)Session["BitcoinsARestar"] - 5000;
            }
            return View();
        }

        public ActionResult Preguntas()
        {
            if ((int)Session["Categoría"] == 0)
            {
                Session["ListaPreguntas"] = BD.ListarPreguntas();
            }
            else
            {
                Session["ListaPreguntas"] = BD.ListarPreguntasCate((int)Session["Categoría"]);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Respuesta(string Pregunta0)
        {
            int Pregunta = Convert.ToInt32(Pregunta0);
            //BORRO LA PREGUNTA DEL SESSION
            List<Preguntas> ListaPreguntas = (List<Preguntas>)Session["ListaPreguntas"];
            int a = ListaPreguntas.Count();
            List<Preguntas> Pregs = BD.ListarPreguntas();
            foreach (Preguntas x in ListaPreguntas)
            {
                foreach (Preguntas preguntas in Pregs)
                {
                    if (x.IdPregunta == Pregunta)
                    {
                        ListaPreguntas.Remove(preguntas);
                    }
                }
            }
            while (a > 0)
            {
                if (ListaPreguntas[a - 1].IdPregunta == Pregunta)
                {
                    ListaPreguntas.RemoveAt(a - 1);
                    a = 0;
                }
                else
                {
                    a--;
                }
            }
            Session["ListaPreguntas"] = ListaPreguntas;
            //RESTO BITCOINS
            Session["BitcoinsARestar"] = (int)Session["BitcoinsARestar"] - 500;
            //ME FIJO SI LA PREGUNTA ES CORRECTA
            Personajes p = (Personajes)Session["PersonajeAzar"];
            int respuesta = BD.Respuesta(Pregunta, p.IdPersonaje);
            if (respuesta == -1)
            {
                //SACAR PERSONAJES DE Session["ListaPersonajes"]
                List<Personajes> Lista = (List<Personajes>)Session["ListaPersonajes"];
                List<int> personaje = BD.Personaje_pregunta(Pregunta);
                foreach (Personajes x in Lista)
                {
                    foreach (int i in personaje)
                    {
                        if (x.IdPersonaje == i)
                        {
                            Personajes P = BD.ObtenerPersonaje(i);
                            Lista.Remove(P);
                        }
                    }
                }
                Session["ListaPersonajes"] = Lista;
                ViewBag.Respuesta = false;
            }
            else
            {
                //SACAR PERSONAJES DE Session["ListaPersonajes"]
                List<Personajes> Lista = (List<Personajes>)Session["ListaPersonajes"];
                List<int> personaje = BD.Personaje_pregunta(Pregunta);
                foreach (Personajes x in Lista)
                {
                    foreach (int i in personaje)
                    {
                        if (x.IdPersonaje == i)
                        {
                        }
                        else
                        {
                            Personajes P = BD.ObtenerPersonaje(i);
                            Lista.Remove(P);
                        }
                    }
                }
                Session["ListaPersonajes"] = Lista;
                ViewBag.Respuesta = true;
            }
            Session["Primera"] = false;
            return View();
        }
        public ActionResult Arriesgar(int Personaje)
        {
            if (Personaje == ((Personajes)Session["PersonajeAzar"]).IdPersonaje)
            {
                BD.RestarBitcoins((int)Session["BitcoinsARestar"], (int)Session["NombreNow"]);
                return View("Fin");
            }
            else
            {
                Session["BitcoinsARestar"] = (int)Session["BitcoinsARestar"] - 50000;
                ViewBag.Arriesgar = false;
                return View("Respuesta");
            }
        }
        public ActionResult Arriesgar_personaje()
        {
            return View("Arriesgar_personaje");
        }
    }
}
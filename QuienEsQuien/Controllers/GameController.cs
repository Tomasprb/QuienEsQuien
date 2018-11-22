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
            ViewBag.Categorias = BD.ListarCategorias();
            return View();
        }
        public ActionResult Mostrar_personajes(int tCate)
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

          




            return View();
        }
        public ActionResult Preguntas()
        {
            ViewBag.Preguntas = BD.ListarPreguntas();
            return View();
        }
    }
}
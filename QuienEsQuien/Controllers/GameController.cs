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
            return View();
        }
        public ActionResult Preguntas()
        {
            ViewBag.Preguntas = BD.ListarPreguntas();
            return View();
        }
    }
}
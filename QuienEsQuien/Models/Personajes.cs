using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Personajes
    {
        private int _IdPersonaje;
        private string _Nombre;
        private string _Imagen;
        private int _IdCategoria;

        public Personajes(int IdPersonaje, string Nombre, string Imagen, int IdCategoria)
        {
            _IdPersonaje = IdPersonaje;
            _Nombre = Nombre;
            _Imagen = Imagen;
            _IdCategoria = IdCategoria;
        }

        public Personajes()
        {

        }

        public int IdPersonaje
        {
            get
            {
                return _IdPersonaje;
            }

            set
            {
                _IdPersonaje = value;
            }
        }
        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }
        public string Imagen
        {
            get
            {
                return _Imagen;
            }

            set
            {
                _Imagen = value;
            }
        }
        public int IdCategoria
        {
            get
            {
                return _IdCategoria;
            }

            set
            {
                _IdCategoria = value;
            }
        }
    }
}
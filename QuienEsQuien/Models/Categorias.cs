using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Categorias
    {
        private int _IdCategoria;
        private string _Nombre;

        public Categorias(int _IdCategoria, string _Nombre)
        {
            this._IdCategoria = _IdCategoria;
            this._Nombre = _Nombre;
        }
        public Categorias()
        {
            IdCategoria = _IdCategoria;
            Nombre = _Nombre;
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
    }
}
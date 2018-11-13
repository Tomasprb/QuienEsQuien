using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Preguntas
    {
        private int _IdPregunta;
        private string _Texto;
        private int _IdCategoria;

        public Preguntas(int _IdPregunta, string _Texto, int _IdCategoria)
        {
            this._IdPregunta = _IdPregunta;
            this._Texto = _Texto;
            this._IdCategoria = _IdCategoria;
        }
        public Preguntas()
        {

        }

        public int IdPregunta
        {
            get
            {
                return _IdPregunta;
            }

            set
            {
                _IdPregunta = value;
            }
        }

        public string Texto
        {
            get
            {
                return _Texto;
            }

            set
            {
                _Texto = value;
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
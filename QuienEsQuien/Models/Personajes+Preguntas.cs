using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Personajes_Preguntas
    {
        private int _IdPersonaje;
        private int _IdPregunta;

        public Personajes_Preguntas(int _IdPersonaje, int _IdPregunta)
        {
            this._IdPersonaje = _IdPersonaje;
            this._IdPregunta = _IdPregunta;
        }
        public Personajes_Preguntas()
        {
            IdPersonaje = _IdPersonaje;
            IdPregunta = _IdPregunta;
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
    }
}
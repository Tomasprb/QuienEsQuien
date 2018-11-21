using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Personaje_pregunta
    {
        private int _IdPersonaje_pregunta;
        private int _IdPersonaje;
        private int _IdPregunta;

        public Personaje_pregunta(int perpre, int per, int pre)
        {
            _IdPersonaje_pregunta = perpre;
            _IdPersonaje = per;
            _IdPregunta = pre;
        }

        public Personaje_pregunta()
        {

        }

        public int IdPersonaje_pregunta
        {
            get
            {
                return _IdPersonaje_pregunta;
            }

            set
            {
                _IdPersonaje_pregunta = value;
            }
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
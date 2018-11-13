using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Partida
    {
        private int _IdPartida;
        private int _IdJugador1;
        private int _IdJugador2;
        private double _Puntaje;

        public Partida(int _IdPartida, int _IdJugador1, int _IdJugador2, double _Puntaje)
        {
            this._IdPartida = _IdPartida;
            this._IdJugador1 = _IdJugador1;
            this._IdJugador2 = _IdJugador2;
            this._Puntaje = _Puntaje;
        }
        public Partida()
        {
            IdPartida = _IdPartida;
            IdJugador1 = _IdJugador1;
            IdJugador2 = _IdJugador2;
            Puntaje = _Puntaje;
        }

        public int IdPartida
        {
            get
            {
                return _IdPartida;
            }

            set
            {
                _IdPartida = value;
            }
        }

        public int IdJugador1
        {
            get
            {
                return _IdJugador1;
            }

            set
            {
                _IdJugador1 = value;
            }
        }

        public int IdJugador2
        {
            get
            {
                return _IdJugador2;
            }

            set
            {
                _IdJugador2 = value;
            }
        }

        public double Puntaje
        {
            get
            {
                return _Puntaje;
            }

            set
            {
                _Puntaje = value;
            }
        }
    }
}
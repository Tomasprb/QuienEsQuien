using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Usuarios
    {
        private int _IdUsuario;
        private string _Nombre;
        private string _Contraseña;
        private double _Puntaje;
        private bool _Admin;
        private int _Bitcoins;
        private int _MaxBitcoins;
        private int _PartidasJugadas;
        private int _PartidasGanadas;

        public Usuarios(int _IdUsuario, string _Nombre, string _Contraseña, double _Puntaje, bool _Admin, int _Bitcoins, int _MaxBitcoins, int _PartidasJugadas, int _PartidasGanadas)
        {
            this._IdUsuario = _IdUsuario;
            this._Nombre = _Nombre;
            this._Contraseña = _Contraseña;
            this._Puntaje = _Puntaje;
            this._Admin = _Admin;
            this._Bitcoins = _Bitcoins;
            this._MaxBitcoins = _MaxBitcoins;
            this._PartidasJugadas = _PartidasJugadas;
            this._PartidasGanadas = _PartidasGanadas;
        }

        public Usuarios()
        {

        }
        
        public int IdUsuario
        {
            get
            {
                return _IdUsuario;
            }

            set
            {
                _IdUsuario = value;
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

        public string Contraseña
        {
            get
            {
                return _Contraseña;
            }

            set
            {
                _Contraseña = value;
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

        public bool Admin
        {
            get
            {
                return _Admin;
            }

            set
            {
                _Admin = value;
            }
        }

        public int Bitcoins
        {
            get
            {
                return _Bitcoins;
            }

            set
            {
                _Bitcoins = value;
            }
        }

        public int MaxBitcoins
        {
            get
            {
                return _MaxBitcoins;
            }

            set
            {
                _MaxBitcoins = value;
            }
        }

        public int PartidasJugadas
        {
            get
            {
                return _PartidasJugadas;
            }

            set
            {
                _PartidasJugadas = value;
            }
        }

        public int PartidasGanadas
        {
            get
            {
                return _PartidasGanadas;
            }

            set
            {
                _PartidasGanadas = value;
            }
        }
    }
}
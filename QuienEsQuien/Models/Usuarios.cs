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

        public Usuarios(int _IdUsuario, string _Nombre, string _Contraseña, double _Puntaje, bool _Admin)
        {
            this._IdUsuario = _IdUsuario;
            this._Nombre = _Nombre;
            this._Contraseña = _Contraseña;
            this._Puntaje = _Puntaje;
            this._Admin = _Admin;
        }
        public Usuarios()
        {
            IdUsuario = _IdUsuario;
            Nombre = _Nombre;
            Contraseña = _Contraseña;
            Puntaje = _Puntaje;
            Admin = _Admin;
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
    }
}
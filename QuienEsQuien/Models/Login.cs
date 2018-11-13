using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Login
    {
        private string _Nombre;
        private bool _Admin;
        private string _Contraseña;
     

        public Login(string Nombre, bool Admin, string Contraseña)
        {
            _Nombre = Nombre;
            _Admin = Admin;
            _Contraseña = Contraseña;
        }

        public Login()
        {

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
    }
}
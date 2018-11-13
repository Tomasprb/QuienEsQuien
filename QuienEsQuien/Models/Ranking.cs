using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Ranking
    {
        private int _IdUser;
        private int _Puesto;

        public Ranking(int _IdUser, int _Puesto)
        {
            this._IdUser = _IdUser;
            this._Puesto = _Puesto;
        }
        public Ranking()
        {
            IdUser = _IdUser;
            Puesto = _Puesto;
        }
        public int IdUser
        {
            get
            {
                return _IdUser;
            }

            set
            {
                _IdUser = value;
            }
        }

        public int Puesto
        {
            get
            {
                return _Puesto;
            }

            set
            {
                _Puesto = value;
            }
        }
    }
}
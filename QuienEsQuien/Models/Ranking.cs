using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Ranking
    {
        private int _IdUser;
        private string _User;
        private int _Bitcoins;

        public Ranking(int IdUser, string User, int Bitcoins)
        {
            _IdUser = IdUser;
            _User = User;
            _Bitcoins = Bitcoins;
        }

        public Ranking()
        {

        }

        public string User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = value;
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
    }
}
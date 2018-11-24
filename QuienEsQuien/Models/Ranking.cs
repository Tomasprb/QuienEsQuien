using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienEsQuien.Models
{
    public class Ranking
    {
        private string _User;
        private int _Bitcoins;

        public Ranking(string User, int Bitcoins)
        {
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
    }
}
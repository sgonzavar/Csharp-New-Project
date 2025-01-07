using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleAppBank
{
    internal class People
    {
        private string _name;
        private string _card;
        private string _pssword;
        private int _balance;

        public People(string name, string card, string pssword, int balance)
        {
            _name = name;
            _card = card;
            _pssword = pssword;
            _balance = balance;
        }

        public string Name { get => _name; set => _name = value; }
        public string Card { get => _card; set => _card = value; }
        public string Pssword { get => _pssword; set => _pssword = value; }
        public int Balance { get => _balance; set => _balance = value; }
    }
}

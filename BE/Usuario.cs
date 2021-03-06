﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario
    {
        int _id;
        int _dvh;
        string _password;
        string _username;
        bool _eliminado;
        List<Rol> roles;


        public int Id { get => _id; set => _id = value; }
        public int Dvh { get => _dvh; set => _dvh = value; }
        public string Password { get => _password; set => _password = value; }
        public string Username { get => _username; set => _username = value; }
        public bool Eliminado { get => _eliminado; set => _eliminado = value; }
        public List<Rol> Roles { get => roles; set => roles = value; }

        public override string ToString()
        {
            return _username;
        }
    }
}

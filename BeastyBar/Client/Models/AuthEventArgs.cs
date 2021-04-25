using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Models
{
    public class AuthEventArgs : EventArgs
    {
        public AuthEventArgs(int id, string name, string token)
        {
            this.Id = id;
            this.Name = name;
            this.Token = token;
        }
        public int Id { get; }

        public string Name { get; }

        public string Token { get; }
    }
}

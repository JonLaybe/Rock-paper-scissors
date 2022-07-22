using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    public class User
    {
        public string Name { get; private set; }
        public TcpClient Client { get; private set; }
        static User _user;
        private User(string name, TcpClient client) {
            Name = name;
            Client = client;
        }
        static public User GetObject(string name, TcpClient client)
        {
            if (_user == null)
                _user = new User(name, client);
            return _user;
        }
        static public User GetObject()
        {
            return _user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    public class IOClient
    {
        static private IOClient _ioclient;
        private User _user;
        private IOClient()
        {
            _user = User.GetObject();
        }
        static public IOClient GetObject()
        {
            if (_ioclient == null)
                _ioclient = new IOClient();
            return _ioclient;
        }
        public string ListenMessage()
        {
            NetworkStream stream;
            byte[] buffer = new byte[1000];

            stream = _user.Client.GetStream();
            stream.Read(buffer, 0, 1000);

            string message = Encoding.UTF8.GetString(buffer);

            return message.Remove(message.IndexOf('\0'));
        }
        public void SendMessage(string message)
        {
            NetworkStream stream;
            byte[] buffer = new byte[message.Length];

            stream = _user.Client.GetStream();

            buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, message.Length);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    public class TcpConnact
    {
        public TcpClient client { get; private set; }
        public TcpConnact()
        {
            client = new TcpClient();
        }
        public bool Connacted(string ip, int port)
        {
            try
            {
                client.Connect(ip, port);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        static List<User> users;
        static TcpListener listen;
        static bool game = false;
        static User user1;
        static void Listen(User user)
        {
            user.Name = ListenMessage(user.client);
            while (user.client.Connected)
            {
                string message = ListenMessage(user.client);
                user.Item = message;
                CheakReady(user);

                Console.WriteLine(message);
            }
        }

        static string ListenMessage(TcpClient client)
        {
            NetworkStream stream;
            byte[] buffer = new byte[1000];

            stream = client.GetStream();
            stream.Read(buffer, 0, 1000);

            string message = Encoding.UTF8.GetString(buffer);
            
            return message.Remove(message.IndexOf('\0'));
        }
        static bool SendMessage(string message, TcpClient client)
        {
            NetworkStream stream;
            byte[] buffer = new byte[message.Length];

            stream = client.GetStream();

            buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, message.Length);

            return true;
        }
        static void Wait()
        {
            listen.Start();
            while (true)
            {
                if (users.Count() <= 2)
                {
                    Console.WriteLine("[Server]Сonnection waiting");
                    TcpClient client = listen.AcceptTcpClient();
                    User user = new User();
                    user.client = client;
                    users.Add(user);
                    Task task = new Task(() => Listen(user));
                    task.Start();
                    Console.WriteLine("[Server]Connact client " + users.Count());
                }
            }
        }
        static void CheakReady(User user)
        {
            if (game)
            {
                string message = Game(user);

                SendMessage(users[1].Item + "|" + message, users[0].client);
                SendMessage(users[0].Item + "|" + message, users[1].client);

                game = false;
            }
            else
            {
                user1 = user;
                game = true;
            }
        }
        static string Game(User user)
        {
            if (user.Item == user1.Item)
                return "draw";
            else if (user.Item == "stone" && user1.Item == "scissors")
                return user.Name;
            else if (user.Item == "stone" && user1.Item == "paper")
                return user1.Name;
            else if (user.Item == "scissors" && user1.Item == "stone")
                return user1.Name;
            else if (user.Item == "scissorse" && user1.Item == "paper")
                return user.Name;
            else if (user.Item == "paper" && user1.Item == "stone")
                return user.Name;
            else if (user.Item == "paper" && user1.Item == "scissors")
                return user1.Name;
            return null;
        }
        static void Main(string[] args)
        {
            users = new List<User>();
            listen = new TcpListener(System.Net.IPAddress.Any, 1300);
            Wait();
        }
    }
}

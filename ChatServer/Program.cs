using BLL.ChatServer;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    internal class Program
    {
        static TcpListener listener;
        static List<Client> clients;
        static void Main(string[] args)
        {
            clients = new List<Client>();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            listener.Start();

            while (true)
            {
                var client = new Client(listener.AcceptTcpClient());
                clients.Add(client);

                /* connect users */

            }
        }
    }
}
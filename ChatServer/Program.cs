using BLL.ChatServer;
using System.Net;
using System.Net.Sockets;
using wpfreg.Net.IO;

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
                BroadcastConnection();
            }
        }

        static void BroadcastConnection()
        {
            foreach (var notifiedClient in clients)
            {
                foreach (var client in clients)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(client.Username);
                    notifiedClient.ClientSocket.Client.Send(broadcastPacket.GetPackageBytes());
                }
            }
        }
    }
}
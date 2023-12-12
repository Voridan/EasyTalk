using System.Net;
using System.Net.Sockets;
using wpfreg.Net.IO;

namespace ChatServer
{
    public class Program
    {
        static TcpListener listener;
        static List<Client> clients;
        static void Main(string[] args)
        {
            clients = new List<Client>();
            listener = new TcpListener(IPAddress.Parse("172.16.200.215"), 7891);
            listener.Start();

            while (true)
            {
                var client = new Client(listener.AcceptTcpClient());
                if(IsClientConnected(client.Username) == false)
                {
                    clients.Add(client);
                }

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
                    broadcastPacket.WriteOpCode((int)OpCode.BROADCAST);
                    broadcastPacket.WriteMessage(client.Username);
                    notifiedClient.ClientSocket.Client.Send(broadcastPacket.GetPackageBytes());
                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            foreach(var client in clients)
            {
                var packet = new PacketBuilder();
                packet.WriteOpCode(5);
                packet.WriteMessage(message);
                client.ClientSocket.Client.Send(packet.GetPackageBytes());
            }
        }

        public static void BroadcastDisconnect(string username)
        {
            var client = clients.Where((client) => client.Username == username).FirstOrDefault();
            if (client != null)
            {
                clients.Remove(client);

                foreach (var clnt in clients)
                {
                    var packet = new PacketBuilder();
                    packet.WriteOpCode(10);
                    packet.WriteMessage(client.Username);
                    client.ClientSocket.Client.Send(packet.GetPackageBytes());
                }

                BroadcastMessage($"{client.Username} disconnected.");
            }
        }

        static bool IsClientConnected(string username) => clients.Any(client => client.Username == username);
    }
}
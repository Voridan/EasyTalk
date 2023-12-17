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
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            listener.Start();

            while (true)
            {
                var client = new Client(listener.AcceptTcpClient());
                if(IsClientConnected(client.User.NickName) == false)
                {
                    clients.Add(client);
                }

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
                    broadcastPacket.WriteMessage(client.User.NickName);
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
                packet.WriteMessage(message + "  " + Guid.NewGuid().ToString());
                client.ClientSocket.Client.Send(packet.GetPackageBytes());
            }
        }

        public static void BroadcastDisconnect(string username)
        {
            var client = clients.Where((client) => client.User.NickName == username).FirstOrDefault();
            if (client != null)
            {
                clients.Remove(client);

                foreach (var clnt in clients)
                {
                    var packet = new PacketBuilder();
                    packet.WriteOpCode(10);
                    packet.WriteMessage(client.User.NickName);
                    client.ClientSocket.Client.Send(packet.GetPackageBytes());
                }

                BroadcastMessage($"{client.User.NickName} disconnected.");
            }
        }

        static bool IsClientConnected(string username) => clients.Any(client => client.User.NickName == username);
    }
}
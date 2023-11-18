using ChatServer.Net.IO;
using System.Net.Sockets;

namespace BLL.ChatServer
{
    public class Client
    {
        private PacketReader packetReader;
        
        public string Username { get; set; }

        public TcpClient ClientSocket { get; set; }
        
        public Client(TcpClient client)
        {
            ClientSocket = client;
            packetReader = new PacketReader(ClientSocket.GetStream());

            // if opcode != 0 drop the connection
            var opCode = packetReader.ReadByte();
            Username = packetReader.ReadMessage();
            Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: {Username}");
        }
    }
}

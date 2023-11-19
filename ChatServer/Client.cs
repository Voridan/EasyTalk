using ChatServer.Net.IO;
using System.Net.Sockets;

namespace ChatServer
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

            var opCode = packetReader.ReadByte();
            if(opCode != 0)
            {
                Console.WriteLine($"[{DateTime.Now}]: Connection dropped. Opcode is not 0.");
                client.Close();
                return;
            }

            Username = packetReader.ReadMessage();
            Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: {Username}");

            Task.Run(() => Procces());
        }
        
        void Procces()
        {
            while (true)
            {
                try
                {
                    var opcode = packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 5:
                            var msg = packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}] Message received. {msg}");
                            Program.BroadcastMessage($"[{DateTime.Now}] {Username}: {msg}");
                            break;
                        
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{Username} disconnected");
                    Program.BroadcastDisconnect(Username);
                    ClientSocket.Close();
                    break;
                }
            }
        }
    }
}

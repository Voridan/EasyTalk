using BLL.Models;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using wpfreg.Net.IO;

namespace wpfreg.Net
{
     public class Server
    {
        TcpClient _client;
        PacketBuilder _packetBuilder;
        public PacketReader PacketReader;
        public event Action connectedEvent;
        public event Action msgRecieveEvent;
        public event Action userDisconectEvent;

        public Server()
        {
            _client = new TcpClient();
        }

        public void ConnectToServer(UserModel user)
        {
            if(!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7891);
                PacketReader = new PacketReader(_client.GetStream());
                if(user != null)
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(UserModel.Serialize(user));
                    _client.Client.Send(connectPacket.GetPackageBytes());
                }
                ReadPackets();
            }
        }

        private void ReadPackets()
        {
            Task.Run(() => 
            { 
                while(true)
                {
                    var opcode = PacketReader.ReadByte();
                    switch(opcode)
                    {
                        case 1:
                            connectedEvent?.Invoke();
                            break;
                        case 5:
                            msgRecieveEvent?.Invoke();
                            break;
                        case 10:
                            userDisconectEvent?.Invoke();
                            break;
                        default:
                            Console.WriteLine("ah yes...");
                            break;
                    }
                }
            });
        }

        public void SendMessageToServer(string message)
        {
            var messagePacket = new PacketBuilder();
            messagePacket.WriteOpCode(5);
            messagePacket.WriteString(message);
            _client.Client.Send(messagePacket.GetPackageBytes());
        }
    }
}

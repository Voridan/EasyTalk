using System.IO;
using System.Net.Sockets;
using System.Text;

namespace wpfreg.Net.IO
{
    public class PacketReader : BinaryReader
    {
        private NetworkStream stream;

        public PacketReader(NetworkStream ns)
            : base(ns)
        {
            stream = ns;
        }

        public string ReadMessage()
        {
            var length = ReadInt32();
            byte[] msgBuffer = new byte[length];
            stream.Read(msgBuffer, 0, length);
            var msg = Encoding.UTF8.GetString(msgBuffer);

            return msg;
        }
    }
}

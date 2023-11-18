using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace wpfreg.Net.IO
{
    class PacketReader : BinaryReader
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
            var msg = Encoding.ASCII.GetString(msgBuffer);

            return msg;
        }
    }
}

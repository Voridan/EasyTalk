using System;
using System.IO;
using System.Text;

namespace wpfreg.Net.IO
{
     public class PacketBuilder
    {
        private readonly MemoryStream _ms;

        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }

        public void WriteString(string msg)
        {
            var msgLength = msg.Length;
            _ms.Write(BitConverter.GetBytes(msgLength));
            _ms.Write(Encoding.UTF8.GetBytes(msg));
        }

        public byte[] GetPackageBytes()
        {
            return _ms.ToArray();
        }
    }
}

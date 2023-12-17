using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfreg.Net.IO
{
     public class PacketBuilder
    {
        MemoryStream _ms;
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
            bool b = VerifyMessage(msg);
        }
        public byte[] GetPackageBytes()
        {
            return _ms.ToArray();
        }

        public bool VerifyMessage(string msg)
        {
            var expectedLength = msg.Length;
            var buffer = _ms.ToArray();

            // Assuming the first 4 bytes represent the length of the string
            var actualLength = BitConverter.ToInt32(buffer, 0);

            // Compare the expected length with the actual length
            return expectedLength == actualLength;
        }
    }
}

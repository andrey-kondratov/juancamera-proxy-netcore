using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace JuanIpCamera
{
    public sealed class Demuxer
    {
        public void Demux(NetworkStream input, Stream videoOutput,
            Stream audioOutput, CancellationToken cancellationToken = default)
        {
            Span<byte> h264 = stackalloc byte[] { 0, 0, 0, 1 };
            Span<byte> g711 = stackalloc byte[] { 0xaa, 0, 0, 0 };

            int index = 0;
            const int size = 4;

            Span<byte> buffer = stackalloc byte[size];
            input.Read(buffer);

            Stream current = Stream.Null;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (buffer.SequenceEqual(h264))
                {
                    current = videoOutput;
                }
                else if (buffer.SequenceEqual(g711))
                {
                    current = audioOutput;
                }

                current.WriteByte(buffer[index]);

                int next = input.ReadByte();
                if (next == -1)
                {
                    break;
                }

                buffer[index] = (byte)next;
                index = (index + 1) % size;
            }
        }
    }
}

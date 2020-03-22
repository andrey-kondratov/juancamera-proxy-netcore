using System;
using System.Net.Sockets;

namespace JuanIpCamera
{
    public sealed class Client : IDisposable
    {
        private readonly TcpClient _client;

        public Client(string hostname, int port)
        {
            _client = new TcpClient(hostname, port);
        }

        public NetworkStream GetStream()
        {
            var stream = _client.GetStream();

            // additional handshake before data starts coming
            stream.Write(Commands.GetBubbleLive);
            stream.Read(new byte[1142]);
            stream.Write(Commands.Auth);
            stream.Read(new byte[54]);
            stream.Write(Commands.RequestStream);

            return stream;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
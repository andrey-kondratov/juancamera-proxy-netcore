using System.Text;

namespace JuanIpCamera
{
    internal static class Commands
    {
        public static readonly byte[] GetBubbleLive = Encoding.UTF8.GetBytes("GET /bubble/live?ch=0&stream=0 HTTP/1.1\r\n\r\n");

        public static readonly byte[] Auth =
        {
            0xaa, 0, 0, 0, 0x35, 0, 0x0e, 0x16, 0xc2, 0x71, 0, 0, 0, 0x2c, 0, 0, 0, 0,
            0x61, 0x64, 0x6d, 0x69, 0x6e, // admin
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        public static readonly byte[] RequestStream =
        {
            0xaa, 0, 0, 0, 0x15, 0x0a, 0x0e, 0x16, 0xc2, 0xdf,
            0, 0, 0, 0, 0, 0, 0, 0, 0x01, 0, 0, 0, 0, 0, 0, 0
        };
    }
}
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JuanIpCamera.ConsoleApp
{
    internal static class Program
    {
        private static string After(this string[] args, string value) =>
            args.SkipWhile(v => v != value).ElementAtOrDefault(1);

        private static int? AsInt(this string value) =>
            int.TryParse(value, out int result) ? result : default(int?);

        public static async Task Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Usage: juanipc <hostname>[:<port=80>] \n" +
                    "  [-f <filename>] [--demux]");
                return;
            }

            // capture Ctrl+C
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, args) =>
            {
                cts.Cancel();
                args.Cancel = true;
            };

            // configuration
            string[] address = args.First().Split(':');
            string hostname = address[0];
            int port = address.ElementAtOrDefault(1).AsInt() ?? 80;
            bool demux = args.Contains("--demux");
            string filename = args.After("-f");

            // connect to camera
            using var client = new Client(hostname, port);
            using var stream = client.GetStream();

            try
            {
                if (demux)
                {
                    // initialize demuxer
                    var demuxer = new Demuxer();
                    if (filename == null)
                    {
                        Stream stdout = Console.OpenStandardOutput();
                        demuxer.Demux(stream, stdout, Stream.Null,
                            cts.Token);
                        return;
                    }

                    using FileStream video = File.Open($"{filename}.h264",
                        FileMode.Create, FileAccess.Write);
                    using FileStream audio = File.Open($"{filename}.g711",
                        FileMode.Create, FileAccess.Write);

                    demuxer.Demux(stream, video, audio, cts.Token);
                    return;
                }

                // forward raw stream
                Stream output = filename != null
                    ? File.Open(filename, FileMode.Create, FileAccess.Write,
                        FileShare.Read)
                    : Console.OpenStandardOutput();

                await stream.CopyToAsync(output, cts.Token);
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
        }
    }
}

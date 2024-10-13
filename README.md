> This project is now archived as I no longer use the camera. Feel free to create a fork and maintain your version.

# juancamera-proxy-netcore
A proxy for streaming video from a Juan IP camera without its custom handshaking

## Usage

Make sure you have .NET Core 2.1+ SDK installed, then run:

```sh
dotnet run JuanIpCamera.ConsoleApp -- <camera_ip> --demux -f output
```

You should see the files `output.h264` (video) and `output.g711` (audio) appear in the working directory. Press `Ctrl+C` to stop recording.

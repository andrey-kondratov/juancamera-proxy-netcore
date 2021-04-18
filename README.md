# juancamera-proxy-netcore
A proxy for streaming video from a Juan IP camera without its custom handshaking

## Build

Make sure you have a Linux with .NET Core 2.1 SDK installed, then run:

```sh
dotnet tool restore
dotnet cake --target=publish
```

## Usage

Navigate to the `./out` directory, and run:

```sh
./juanipc <camera_ip> --demux --filename output
```

You should see the files `output.h264` (video) and `output.g711` (audio) appear in the `./out` folder. Press `Ctrl+C` to stop recording.

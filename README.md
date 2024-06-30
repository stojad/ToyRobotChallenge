# ToyRobotChallenge
This is an implementation of the Toy Robot Challenge as an ASP.NET WebAPI. The commands to control and report the robot's state are implemented as REST endpoints.
This implementation includes a Swagger UI in order to provide a convenient front-end interface to use the API, but any REST client such as Postman or `curl` can be used to issue commands using the API.

The intention behind using a REST API as opposed to a simple CLI program is for it to simulate how a physical robot using an internet-connected microcontroller like a Raspberry Pi would expose an API to allow users to control it remotely over a network. Using a very lightweight cross-platform REST API implementation like a ASP.NET WebAPI service could feasibly run on the Linux OS used by an inexpensive microcontroller in a real-time system.

## Dependencies
The .NET 8.0 SDK is all that is required to build and run this solution. This solution was developed in a Windows environment, but the code can be built and run on Windows, MacOS and Linux environments. You can download the SDK [here](https://dotnet.microsoft.com/en-us/download).

## Getting Started
1. In order to run the service locally, open a terminal like PowerShell and navigate to the `ToyRobotChallenge.Service` subdirectory of the repository root and run the command `dotnet run`:
```
PS C:\Users\David> cd .\source\repos\ToyRobotChallenge\ToyRobotChallenge.Service\
PS C:\Users\David\source\repos\ToyRobotChallenge\ToyRobotChallenge.Service> dotnet run
```
2. Once the service successfully builds and starts, the following log output will indicate where the Swagger UI will be available:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5291
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\David\source\repos\ToyRobotChallenge\ToyRobotChallenge.Service
```
3. In any web browser, visit the path `/swagger` to the local listening URL (e.g. [http://localhost:5291/swagger](http://localhost:5291/swagger)) to access the Swagger UI.
4. Expand any of the actions and use 'Try it out' to issue commands.

## Configuration
By default, the grid on which the robot can move is a 5x5 grid, but this can be configured to be any positive integer value, with distinct X and Y sizes to allow for rectangular grids. If you wish to modify this configuration, you can find it in the file `appsettings.json` in the `ToyRobotChallenge.Service` subdirectory of the repository root. Modify the `Grid` section of the configuration file to define the width and height of the grid:
```json
"Grid": {
  "GridSizeX": 5,
  "GridSizeY": 5
}
```

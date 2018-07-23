### Dockerized WCF Self-Host

This project is an attempt to dockerize a WCF self-hosted (Console-based) application written in C# .NET, as a Windows image to be deployed as a Docker container in a Windows host.
The project hosts a single WCF service (HelloWorldService) with a single operation (SayHello) which return the input string parameter as a the output appended with additional string.
The WCF service is exposed with 2 endpoints, HTTP on port 8080 using basicHttp binding and TCP on port 8000 using net.tcp binding including a mex metadata endpoint.
The same executable also serves as a WCF client when executed without any command-line arguments (--server).

#### Files

The following are 2 important files of interest describing how WCF services are being hosted, bundled as Docker image and exposed to the external world.

* App.config
* Dockerfile

**App.config**

This file shows how the WCF services are hosted (server) and consumed (client) using both HTTP and TCP endpoint. Few important things to note is that the endpoints are bound to all interfaces within the container using the wildcard in the baseAddress config and the security is set to "none" for net.tcp binding using netTcpBinding configuration.

**Dockerfile**

This file shows how the console application is built as a Docker image. The image start with a base image of windowsservercore from Microsoft which is a basic layer for a Windows container to be running on a Windows host.
WCF-HTTP-Activation for .NET v4.5 is enabled using PowerShell. The executable file DockerizedWcfSelfHost.exe and the configuration file DockerizedWcfSelfHost.exe.config are copied onto the image app (C:\app) location and the entrypoint or startup is set to DockerizedWcfSelfHost.exe with a command-line argument "--server".
Also, TCP ports 8000 and 8080 as exposed to enable HTTP and TCP WCP endpoints.

**Building image**

This command build an image wcf tagged latest using the Dockerfile.

```docker
docker build -t wcf:latest .
```

**Running as a container**

This command runs the image wcf:latest as a named container "wcf" with ports 8000 and 8080 exposed on host in interactive mode (-i), allocate pseudo-TTY (-t) and detached (-d) so that the container lives as a long running process due to Console.ReadLine().

```docker
docker run --name wcf -p 8000:8000 -p 8080:8080 -itd wcf:latest
```

Happy coding!
FROM microsoft/windowsservercore
RUN powershell -Command Add-WindowsFeature NET-WCF-HTTP-Activation45
WORKDIR app
EXPOSE 8000
EXPOSE 8080
COPY . .
ENTRYPOINT ["DockerizedWcfSelfHost.exe", "--server"]
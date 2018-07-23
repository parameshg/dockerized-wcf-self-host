using System;
using System.ServiceModel;

namespace DockerizedWcfSelfHost
{
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string SayHello(string name);
    }

    public class HelloWorldService : IHelloWorldService
    {
        public string SayHello(string name)
        {
            Console.WriteLine($"Hello, {name}");

            return string.Format("Hello, {0}", name);
        }
    }
}
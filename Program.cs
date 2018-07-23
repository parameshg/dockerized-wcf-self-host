using System;
using System.ServiceModel;

namespace DockerizedWcfSelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = args.Length.Equals(1) && args[0].Equals("--server");

            try
            {
                if (server)
                {
                    using (var host = new ServiceHost(typeof(HelloWorldService)))
                    {
                        host.Open();

                        foreach (var i in host.BaseAddresses)
                            Console.WriteLine("The service is ready at {0}", i.ToString());

                        Console.WriteLine("Press <Enter> to stop the service.");
                        Console.ReadLine();

                        host.Close();
                    }
                }
                else
                {
                    Console.WriteLine($"HttpEndpoint: {DateTime.Now.ToString()} => {new External.HelloWorldServiceClient("HttpEndpoint").SayHello(DateTime.Now.ToString())}");
                    Console.WriteLine($"TcpEndpoint: {DateTime.Now.ToString()} => {new External.HelloWorldServiceClient("TcpEndpoint").SayHello(DateTime.Now.ToString())}");
                    Console.WriteLine("Press <Enter> to exit.");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }
    }
}
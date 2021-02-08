using Service_Project.Services;
using System;
using System.Reflection;
using Topshelf;

namespace Service_Project
{
    class Program
    {
        private static string SERVICE_NAME = "HeartbeatService";

        static void Main(string[] args)
        {
            try
            {
                Type service_type = Type.GetType($"Service_Project.Services.{SERVICE_NAME}");
                MethodInfo get_host_factory = service_type.GetMethod(
                    "GetHostFactory", BindingFlags.Static | BindingFlags.Public);

                TopshelfExitCode exit_code = (TopshelfExitCode)get_host_factory.Invoke(null, null);
            
                int exit_code_value = (int)Convert.ChangeType(exit_code, exit_code.GetTypeCode());
                Environment.ExitCode = exit_code_value;
            }
            catch(Exception exception)
            {
                Console.WriteLine($"{exception.GetType()}: {exception.Message}");
                Console.ReadKey();
            }
        }
    }
}

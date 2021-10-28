using System;
using aspnetcore_azure_tables.Data;

namespace aspnetcore_azure_tables
{
    class Program
    {
        static void Main(string[] args)
        {
            // Common.CreateTableAsync("employee").GetAwaiter().GetResult();
            Operations ops = new Operations();
            ops.TriggerOperations().Wait();

            Console.ReadKey();
        }
    }
}

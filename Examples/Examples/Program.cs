using System;
using System.Diagnostics;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = new EnumManagerExample();
            Console.WriteLine("Get Results: " + example.GetResults(args));
            Console.WriteLine("Get More Results: " + example.GetMoreResults(args));
            Debugger.Break();
        }
    }
}

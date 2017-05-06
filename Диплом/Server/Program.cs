using System;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine("Hello World!");

                Function.execute(new CommandLine().get());

                
            }
        }
    }
}
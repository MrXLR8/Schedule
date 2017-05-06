using System;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Hello World!");
            if(Console.ReadLine()== "start")
            {
                Server.Initialize(11000);
            }
        }
    }
}
using System;

namespace SvrDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int port = 8080;
            if(args != null && args.Length > 0)
            {               
               int.TryParse(args[0], out port);  
            }

            new TimeServer().Bind(port);
        }
    }
}

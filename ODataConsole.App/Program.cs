using System;
using System.Linq;
using Default;

namespace ODataConsole.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceRoot = "https://localhost:44307/odata";
            var context = new Container(new Uri(serviceRoot));
            var products = context.Products.ExecuteAsync().Result;
            products.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Name);
            });
            Console.ReadLine();
        }
    }
}

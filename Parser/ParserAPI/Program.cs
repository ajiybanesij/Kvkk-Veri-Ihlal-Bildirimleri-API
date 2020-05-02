using ParserAPI.Controller;
using ParserAPI.Models;
using System;
using System.Collections.Generic;

namespace ParserAPI
{
    class Program
    {

        static void Main(string[] args)
        {
            
            Controller.Parser controller = new Controller.Parser();
            List<Summary> _sumList=controller.ParseData();
            foreach (var item in _sumList)
            {
                Console.WriteLine(item.title);
                Console.WriteLine(item.url);
                Console.WriteLine(item.image);
                Console.WriteLine(item.date);
                Console.WriteLine("#####################################");
            }
            Console.ReadKey();
        }
    }
}

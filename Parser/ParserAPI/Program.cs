using Newtonsoft.Json;
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
            Result<Summary> _sumList =controller.ParseData();
            string json = JsonConvert.SerializeObject(_sumList, Formatting.Indented);
            Console.WriteLine(json);

            /*
            foreach (var item in _sumList.Content)
            {
                Console.WriteLine(item.title);
                Console.WriteLine(item.url);
                Console.WriteLine(item.image);
                Console.WriteLine(item.date);
                Console.WriteLine("#####################################");
            }
            */

            Console.ReadKey();
        }
        ///Image/CropImage?w=420&h=205&f=/SharedFolderServer/ContentImages/82e37767-f1df-4f0d-8218-4366ddf1f4ea.jpeg
        ///Image/CropImage?w=420&amp;h=205&amp;f=/SharedFolderServer/ContentImages/82e37767-f1df-4f0d-8218-4366ddf1f4ea.jpeg
    }
}

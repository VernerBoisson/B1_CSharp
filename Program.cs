using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace bottwitter
{
    class Program
    {
        static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json");
            Configuration = builder.Build();
            string path = Configuration["path"];
            int number = int.Parse(Configuration["searchNumber"]);
            string[] keywords = Configuration["keywords"].Split('|');
            string[] users = Configuration["users"].Split('|');
            string[] concourskeyword = Configuration["concours"].Split('|');


            Console.WriteLine("Hello World!");
            Console.WriteLine(Configuration["path"]);
            Tools.Authentification(Configuration["credentials:consumerKey"],
                                    Configuration["credentials:consumerSecret"],
                                    Configuration["credentials:accessToken"],
                                    Configuration["credentials:accessTokenSecret"]);

            Console.WriteLine("Mode de lancement: (U)sers / (M)ot Clés / (C)oncours");
            var key = Console.ReadKey();
            while (true)
            {
                if (key.Key == ConsoleKey.U)
                {
                    ImgUser imgUser = new ImgUser(path, users);
                    imgUser.Run();
                    break;
                }
                else if (key.Key == ConsoleKey.M)
                {
                    ImgKeyWords t1 = new ImgKeyWords(path, keywords, number);
                    t1.Run();
                    break;
                }
                else if (key.Key == ConsoleKey.C)
                {
                    while (true) {
                        Concours concours = new Concours(concourskeyword);
                        concours.Run();
                    }
                }
                else
                {
                    Console.WriteLine("Veuillez taper une lettre valide !");
					key = Console.ReadKey();
                    continue;
                }
            }
            Console.WriteLine("Good Bye!");

        }



    }
}

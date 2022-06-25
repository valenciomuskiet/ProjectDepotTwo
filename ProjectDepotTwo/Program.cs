using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
    class Program // class program
    {
        static void Main()
        {

            int component = 0;
            while (component != -1)
            {
                ApplicatieComponentBezoeker bezoeker = new ApplicatieComponentBezoeker();
                ApplicatieComponentGids gids = new ApplicatieComponentGids();

                switch (component)                                                 
                {
                    case 0:
                        component = StartSchermComponent();
                        break;
                    case 1:
                        component = bezoeker.BezoekersComponent();
                        break;
                    case 2:
                        component = gids.GidsComponent();
                        break;
                }
            }
        }


        private static int StartSchermComponent()                                     
        {
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write("Depot Boijmans Van Beuningen\n\n[1] Bezoeker\n[2] Gids\n\n\n\nSelecteer uw optie :");

                    string invoergebruiker = Console.ReadLine();
                    switch (invoergebruiker)
                    {
                        case "1":
                            return 1;
                        case "2":
                            return 2;
                        default:
                            Console.WriteLine("Invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }
    }
}
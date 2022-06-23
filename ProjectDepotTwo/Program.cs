using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
    class Program
    {
        static void Main()
        {
            int Pagina = 0;
            while (Pagina != -1)
            {
                ApplicatieComponentBezoeker bezoeker = new ApplicatieComponentBezoeker();
                ApplicatieComponentGids gids = new ApplicatieComponentGids();

                switch (Pagina)                                                 
                {
                    case 0:
                        Pagina = StartMenuDepot();
                        break;
                    case 1:
                        Pagina = bezoeker.BezoekersMenu();
                        break;
                    case 2:
                        Pagina = gids.GidsMenu();
                        break;
                }
            }
        }





        private static int StartMenuDepot()                                     
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Depot Boijmans Van Beuningen\n\n[1] Bezoeker\n[2] Gids\n\nSelecteer uw optie :");

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

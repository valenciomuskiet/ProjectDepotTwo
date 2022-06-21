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
                Bezoeker bezoeker = new Bezoeker();
                Gids gids = new Gids();

                switch (Pagina)                                                 /// Switch statement voor de menu's
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


        private static int StartMenuDepot()                                     /// Startmenu 
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Het Depot Boijmans Van Beuningen\n\n[1] Bezoeker\n[2] Gids");

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



/*

                }
                else if (Reservering.tijd == reservering12uur)
                {
                    --bezoekerslimit12uur;
                }
                else if (Reservering.tijd == reservering13uur)
                {
                    --bezoekerslimit13uur;
                }
                else if (Reservering.tijd == reservering14uur)
                {
                    --bezoekerslimit14uur;
                }
                else if (Reservering.tijd == reservering15uur)
                {
                    --bezoekerslimit15uur;
                }

                else if (Reservering.tijd == reservering16uur)
                {
                    --bezoekerslimit16uur;
                }
                else if (Reservering.tijd == reservering17uur)
                {
                    --bezoekerslimit17uur;
                }

            }
            return false;


*/
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
<<<<<<< Updated upstream

            int component = 0;
            while (component != -1)
            {
                ApplicatieComponentBezoeker bezoeker = new ApplicatieComponentBezoeker();
                ApplicatieComponentGids gids = new ApplicatieComponentGids();

                switch (component)                                                 
=======
            ApplicatieComponentBezoeker bezoeker = new ApplicatieComponentBezoeker();  
            ApplicatieComponentGids gids = new ApplicatieComponentGids();              
            int component = 0;
            while (component != -1)
            {
                switch (component)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream


        private static int StartSchermComponent()                                     
=======
        private static int StartSchermComponent()                       
>>>>>>> Stashed changes
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
                            Console.WriteLine("Invoer onjuist. Toets [Enter] om het opnieuw te proberen");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }
    }
}
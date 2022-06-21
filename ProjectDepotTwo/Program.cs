using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Bezoeker bezoeker = new Bezoeker();
            Gids gids = new Gids();

            bezoeker.BezoekersMenu();
            //gids.GidsMenu();
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
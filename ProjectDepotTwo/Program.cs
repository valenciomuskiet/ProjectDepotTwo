using System;
using System.Collections.Generic;
using System.IO;
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
                switch (Pagina)                                                 /// Switch statement voor de menu's
                {
                    case 0:
                        Pagina = StartMenuDepot();
                        break;
                    case 1:
                        Pagina = BezoekerMenu();
                        break;
                    case 2:
                        Pagina = GidsMenu();
                        break;
                }
            }
        }

        private static int StartMenuDepot()                                     /// Startmenu 
        {
            while (true)
            {
                LeegPagina();
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

        public static int BezoekerMenu()                                       /// bezoekersmenu
        {
            while (true)
            {
                LeegPagina();
                Console.WriteLine("Bezoeker\n\n[1] om een reservering te maken \n[0] om terug te gaan ");

                string invoerbezoeker = Console.ReadLine();
                switch (invoerbezoeker)
                {

                    case "0":
                        return 0;

                    case "1":
                        LeegPagina();
                        WeergaveRondleidingen();

                        List<Reservering> LijstVanReserveringen = new List<Reservering>();

                        Console.Write("\n\nSelecteer (getal) de Rondleiding naar keuze: ");
                        int num1 = Convert.ToInt32(Console.ReadLine());

                        LeegPagina();
                        Console.WriteLine("Door u geselecteerd : " + GeselecteerdeTijd(num1));
                        Console.WriteLine("\nVoer uw unieke ticket code in: ");

                        int Acode = Convert.ToInt32(Console.ReadLine());
                        int returnvalue = (CodeControle(Acode));

                        // annuleer indien deze lijst verwijderd moet worden en speel opnieuw af
                        var LijstReserveringenJson = File.ReadAllText(@"reservering1.Json");
                        LijstVanReserveringen = JsonConvert.DeserializeObject<List<Reservering>>(LijstReserveringenJson);


                        if (returnvalue == 0)
                        {
                            LijstVanReserveringen.Add(new Reservering(Acode, GeselecteerdeTijd(num1)));

                            string NieuweLijstVanReserveringen = JsonConvert.SerializeObject(LijstVanReserveringen);
                            File.WriteAllText(@"reservering1.Json", NieuweLijstVanReserveringen);
                            LeegPagina();

                            Console.WriteLine("Opgeslagen, toets [Enter] om terug te gaan.");
                            Console.ReadLine();

                        }
                        else
                        {
                            Console.WriteLine("Code onjuist, toets [Enter] om terug te gaan !");
                            Console.ReadLine();
                        }
                        break;

                    default:
                        Console.WriteLine("Invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
                        Console.ReadLine();
                        break;

                }
            }
        }

        private static int GidsMenu()                                           /// Gidsmenu 
        {
            while (true)
            {
                LeegPagina();
                Console.WriteLine("Gids\n\n[0] om terug te gaan naar het start menu\n[1] om een overzicht te zien van de reserveringen");
                string invoergids = Console.ReadLine();

                switch (invoergids)
                {
                    case "1":
                        LeegPagina();

                        Console.ReadLine();

                        break;
                    case "0":
                        return 0;
                    default:
                        Console.WriteLine("Invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void LeegPagina()                                                /// static method voor pagina legen
        {
            Console.Clear();
        }

        static int CodeControle(int code)
        {
            int answer = code % 17;

            if (answer == 0)
            {
                return 0;
            }
            else
                return answer;
        }

        static DateTime GeselecteerdeTijd(int tijdOptie)                        /// static method voor de juiste geselecteerde tijd ophalen
        {
            DateTime Tijdvak;

            var Vandaag = DateTime.Today;
            var reservering11uur = Vandaag.AddHours(11);
            var reservering12uur = Vandaag.AddHours(12);
            var reservering13uur = Vandaag.AddHours(13);
            var reservering14uur = Vandaag.AddHours(14);
            var reservering15uur = Vandaag.AddHours(15);
            var reservering16uur = Vandaag.AddHours(16);
            var reservering17uur = Vandaag.AddHours(17);

            switch (tijdOptie)
            {
                case 1:
                    Tijdvak = reservering11uur;
                    break;
                case 2:
                    Tijdvak = reservering12uur;
                    break;
                case 3:
                    Tijdvak = reservering13uur;
                    break;
                case 4:
                    Tijdvak = reservering14uur;
                    break;
                case 5:
                    Tijdvak = reservering15uur;
                    break;
                case 6:
                    Tijdvak = reservering16uur;
                    break;
                case 7:
                    Tijdvak = reservering17uur;
                    break;
                default:
                    Tijdvak = Vandaag;
                    break;
            }
            return Tijdvak;
        }

        static void WeergaveRondleidingen()                                     /// static methode voor het ophalen van de rondleidingen
        {
            var dateNu = DateTime.Now;
            Console.WriteLine("Huidige datum: " + dateNu);

            var huidigelijst = File.ReadAllText(@"reservering1.Json");
            var Reserveringenvoorcount = JsonConvert.DeserializeObject<List<Reservering>>(huidigelijst);

            var bezoekerslimit = 13;
            var bezoekerslimit11uur = 13;
            var bezoekerslimit12uur = 13;
            var bezoekerslimit13uur = 13;
            var bezoekerslimit14uur = 13;
            var bezoekerslimit15uur = 13;
            var bezoekerslimit16uur = 13;
            var bezoekerslimit17uur = 13;

            var Vandaag = DateTime.Today;
            var reservering11uur = Vandaag.AddHours(11);
            var reservering12uur = Vandaag.AddHours(12);
            var reservering13uur = Vandaag.AddHours(13);
            var reservering14uur = Vandaag.AddHours(14);
            var reservering15uur = Vandaag.AddHours(15);
            var reservering16uur = Vandaag.AddHours(16);
            var reservering17uur = Vandaag.AddHours(17);


            foreach (var Reservering in Reserveringenvoorcount)
            {
                if (Reservering.tijd == reservering11uur)
                {
                    bezoekerslimit11uur--;
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

            //Console.WriteLine("plekken vrij voor vandaag: " + (13 - Reserveringenvoorcount.Count));
            Console.WriteLine("\n\n[1] 11:00 - 11:20" + "|| plekken vrij: " + bezoekerslimit11uur);
            Console.WriteLine("\n[2] 12:00 - 12:20" + "|| plekken vrij: " + bezoekerslimit12uur);
            Console.WriteLine("\n[3] 13:00 - 12:20" + "|| plekken vrij: " + bezoekerslimit13uur);
            Console.WriteLine("\n[4] 14:00 - 11:20" + "|| plekken vrij: " + bezoekerslimit14uur);
            Console.WriteLine("\n[5] 15:00 - 12:20" + "|| plekken vrij: " + bezoekerslimit15uur);
            Console.WriteLine("\n[6] 16:00 - 12:20" + "|| plekken vrij: " + bezoekerslimit16uur);
            Console.WriteLine("\n[7] 17:00 - 12:20" + "|| plekken vrij: " + bezoekerslimit17uur);

        }
    }
}



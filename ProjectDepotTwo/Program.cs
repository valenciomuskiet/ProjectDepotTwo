using System;
using System.IO;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Het Depot\nTyp 'bezoeker' of 'gids' om verder te gaan: ");
            string gebruiker = Console.ReadLine();

            if (gebruiker == "bezoeker")
            {
                LeegPagina();
                SelecteerRondleidingPagina();
                int rondleidingnummer = Convert.ToInt32(Console.ReadLine());

                LeegPagina();
                Console.WriteLine("Huidige datum : " + DateTime.Now + "\n" + "Door u geselecteerd : " + Tijdvak(rondleidingnummer));
                Console.WriteLine("\n\nvoer jouw unieke ticket code in : ");

                int code = Convert.ToInt32(Console.ReadLine());
                int codecontroleresultaat = (CodeControle(code));

                if (codecontroleresultaat == 0)
                {
                    Reservering reservering1 = new Reservering()
                    {
                        Code = code,
                        Tijd = Tijdvak(rondleidingnummer),
                    };

                    string strReservering1 = JsonConvert.SerializeObject(reservering1);
                    Console.WriteLine(strReservering1);
                    File.WriteAllText(@"reservering1.Json", strReservering1);
                    LeegPagina();
                    Console.WriteLine("Opgeslagen !");
                }
                else
                {
                    LeegPagina();
                    Console.WriteLine(" code is ongeldig");
                }

            }
            else if (gebruiker == "gids")
            {
                LeegPagina();
                Console.WriteLine("Deze optie is per volgende week beschikbaar !");
            }

            else
            {
                LeegPagina();
                Console.Clear();
                Console.WriteLine("Input niet valid, probeer het opnieuw");
            }

            static void LeegPagina()
            {
                Console.Clear();
            }


            static void SelecteerRondleidingPagina()
            {
                var huidigeDatum = DateTime.Now;
                Console.WriteLine("Huidige datum: " + huidigeDatum);
                Console.WriteLine("Plekken vrij: volgende week bescikbaar");
                Console.WriteLine("Rondleidingen:\n\n[1] 11:00 - 12:00\n[2] 12:00 - 13:00\n[3] 13:00 - 14:00\n[4] 14:00 - 15:00\n[5] 15:00 - 16:00\n[6] 16:00 - 17:00\n");
                Console.Write("\n\nSelecteer (getal) de Rondleiding naar keuze: ");
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

            static string Tijdvak(int rondleidingnummer)
            {
                string Tijdvak;

                switch (rondleidingnummer)
                {
                    case 1:
                        Tijdvak = "11:00-12:00";
                        break;
                    case 2:
                        Tijdvak = "12:00-13:00";
                        break;
                    case 3:
                        Tijdvak = "13:00-14:00";
                        break;
                    case 4:
                        Tijdvak = "14:00-15:00";
                        break;
                    case 5:
                        Tijdvak = "15:00-16:00";
                        break;
                    case 6:
                        Tijdvak = "16:00-17:00";
                        break;
                    default:
                        Tijdvak = "onjuiste keuze";
                        break;
                }
                return Tijdvak;
            }
        }
    }
}


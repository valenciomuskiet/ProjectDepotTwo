using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
	public class Gids
	{
		Bezoeker bezoeker = new Bezoeker();

		public void GidsMenu()
        {
			using (StreamReader r = new StreamReader(@"Rondleidingen.json"))
			{
				string json = r.ReadToEnd();
				bezoeker.LijstRondleidingen = JsonConvert.DeserializeObject<List<Rondleiding>>(json);

			}
			using (StreamReader r = new StreamReader(@"Reserveringen.json"))
			{
				string json = r.ReadToEnd();
				bezoeker.LijstReserveringen = JsonConvert.DeserializeObject<List<Reservering>>(json);
			}
			while (true)
            {
				Console.Clear();
				Console.WriteLine("Gids\n\n[0] om terug te gaan naar het startmenu\n[1] om een rondleiding te starten \n[10] om een overzicht te zien van de reserveringen");
				string invoergids = Console.ReadLine();
				switch (invoergids)
				{
					case "1":
						Console.Clear();
						var check = bezoeker.LijstRondleidingen.Find(x => x.tijd.Hour == DateTime.Now.Hour);

						if (check != null)
						{
							while (true)
							{
								Console.Clear();
								Console.WriteLine($"Rondleiding van {check.tijd} starten..");
								Console.WriteLine("Vul de codes in voor deze rondleiding: \nOm uit dit menu ");
								string input = Console.ReadLine();
								bool succesvolParsed = int.TryParse(input, out int e);
								while (succesvolParsed != true)
								{
									Console.WriteLine("Uw invoer is niet correct, vul afstublieft een code in bestaand uit cijfers");
									input = Console.ReadLine();
									succesvolParsed = int.TryParse(input, out e);
								}

								if (e == 1)
								{
									break;
								}

								else if (bezoeker.LijstReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour) != null)
								{
									Console.WriteLine("Je mag een labjas\n Druk op [ENTER] voor de volgende");
									Console.ReadLine();
								}

								else
								{
									Console.WriteLine($"\nOnjuist code, code bestaat niet\nDruk op enter om een andere code te proberen");
								}

								Console.ReadLine();

							}

						}
						else
						{
							Console.WriteLine("Op dit moment is er geen beschikbare rondleiding om te starten. \nKijk op het overzicht\n Druk enter om door te gaan");
							Console.ReadLine();
						}
						break;

					case "2":
						Console.Clear();
						break;

					case "10":
						Console.Clear();
						int nummer = 1;
						foreach (Rondleiding rondleiding in bezoeker.LijstRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
						{
							int reserveringenoprondleiding = bezoeker.LijstReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
							Console.WriteLine($"[{nummer}] Rondleiding van {rondleiding.tijd} || plekken vrij: {rondleiding.capaciteit - reserveringenoprondleiding}");
							nummer++;

						}
						Console.WriteLine("klik enter om verder te gaan");
						Console.ReadLine();
						break;

					case "0":
						break;
					default:
						Console.WriteLine("invoer onjuist, selecteer een van bovenstaande opties a.u.b.");

						break;
				
				}
								
			}

		}
		
	}
}


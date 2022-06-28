using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
	public class ApplicatieComponentGids : Rondleiding
	{
		public ApplicatieComponentBezoeker bezoeker = new ApplicatieComponentBezoeker();


		public int GidsComponent()
		{
			using (StreamReader rondleidingenJson = new StreamReader(@"Rondleidingen.json"))
			{
				string stringRondleidingen = rondleidingenJson.ReadToEnd();
				bezoeker.LijstVanRondleidingen = JsonConvert.DeserializeObject<List<Rondleiding>>(stringRondleidingen);
			}
			using (StreamReader r = new StreamReader(@"Reserveringen.json"))
			{
				string json = r.ReadToEnd();
				bezoeker.LijstVanReserveringen = JsonConvert.DeserializeObject<List<Reservering>>(json);
			}
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Gids\n\n[1] hoofdpagina \n[2] Start Rondleiding");
				string invoergids = Console.ReadLine();
				switch (invoergids)
				{
					case "0":
						return 0;

					case "1":
						Console.Clear();
						var check = bezoeker.LijstVanRondleidingen.Find(x => x.tijd.Hour == DateTime.Now.Hour);

						if (check != null)
						{
							while (true)
							{
								Console.Clear();
								Console.WriteLine($"Rondleiding van {check.tijd} gestart\nVul de codes in voor deze rondleiding: \nToets [1] Om uit dit menu");
								string input = Console.ReadLine();
								bool succesvolParsed = int.TryParse(input, out int e);

								while (succesvolParsed != true)
								{
									Console.WriteLine("Uw invoer is niet correct. Vul afstublieft een code in bestaand uit cijfers");
									input = Console.ReadLine();
									succesvolParsed = int.TryParse(input, out e);
								}

								if (e == 1)
								{
									break;
								}
								else if (bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == false) != null)
								{
									bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == false).rondleidinggestart = true;
									Console.WriteLine("U mag een lab jas\n Druk op [Enter] en geef mij door aan de volgende");
									serializeJson();
								}
								else if (bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == true) != null)
								{
									Console.WriteLine("U bent al gestart met deze rondleiding. Druk op [Enter] en geef mij door aan de volgende");
								}
								else if (bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour != DateTime.Now.Hour && x.rondleidinggestart == false) != null)
								{
									var checkCodeTijd = bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Today);

									Console.WriteLine($"Met deze code is gereserveerd voor { checkCodeTijd.tijd}. Vul alleen code's in voor deze rondleiding a.u.b.");
								}
								else
								{
									Console.WriteLine("\nDruk op enter om een andere code te proberen");
								}
								Console.ReadLine();
							}
						}
						else
						{
							Console.WriteLine("Op dit moment is er geen beschikbare rondleiding om te starten. \nKijk op het overzicht wanneer de eerst volgende rondleiding plaatsvind\nDruk enter om af te sluiten");
							Console.ReadLine();
						}
						break;

					default:
						Console.WriteLine("Invoer onjuist. Selecteer een van bovenstaande opties a.u.b.");
						Console.ReadLine();
						break;
				}
			}
		}

		/// methods 

		public void serializeJson()
		{
			using (StreamWriter file = File.CreateText(@"Rondleidingen.json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, bezoeker.LijstVanRondleidingen);
			}

			using (StreamWriter file = File.CreateText(@"Reserveringen.json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, bezoeker.LijstVanReserveringen);
			}
		}
	}
}

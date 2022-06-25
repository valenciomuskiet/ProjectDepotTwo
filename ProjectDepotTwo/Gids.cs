using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace ProjectDepotTwo
{
	public class ApplicatieComponentGids //: ApplicatieComponentBezoeker
	{
		ApplicatieComponentBezoeker bezoeker = new ApplicatieComponentBezoeker();  

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
				Console.WriteLine("Gids\n\n[0] hoofdpagina \n[1] Start Rondleiding \n[2] Overzicht aankomende rondleidingen");
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
								Console.WriteLine($"Rondleiding van {check.tijd} gestart");
								Console.WriteLine("Vul de codes in voor deze rondleiding: \ntoets [1] Om uit dit menu ");

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

								else if (bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == false) != null)
								{
									bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == false).rondleidinggestart = true;
									Console.WriteLine("Je mag een lab jas\n Druk op [ENTER] voor de volgende");
									serialize();
								}
								else if (bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == true) != null)
								{

									Console.WriteLine("U bent al gestart met de rondleiding. Druk op [enter] om een andere code te proberen");
								}

								else
								{
									Console.WriteLine($"\nDruk op enter om een andere code te proberen");
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

					case "2":
						Console.Clear();
						int nummer = 1;
						foreach (Rondleiding rondleiding in bezoeker.LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
						{
							int reserveringenoprondleiding = bezoeker.LijstVanReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
							Console.WriteLine($"[{nummer}] Rondleiding van {rondleiding.tijd} || plekken vrij: {rondleiding.capaciteit - reserveringenoprondleiding}");
							nummer++;

						}
						Console.WriteLine("klik enter om verder te gaan");
						Console.ReadLine();
						break;

					default:
						Console.WriteLine("invoer onjuist, selecteer een van bovenstaande opties a.u.b.");
						Console.ReadLine();
						break;
				}
			}
		}

		public void serialize()
        {
			using (StreamWriter file = File.CreateText(@"rondleidingen.json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, bezoeker.LijstVanRondleidingen);
			}

			using (StreamWriter file = File.CreateText(@"reserveringen.json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, bezoeker.LijstVanReserveringen);
			}
		}
	}
}


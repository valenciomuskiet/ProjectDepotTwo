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

				var check = bezoeker.LijstVanRondleidingen.Find(x => x.tijd.Hour == DateTime.Now.Hour);
				Console.Clear();
				Console.WriteLine($"Huidige tijd: " + DateTime.Now);
				Console.Write($"Toets [1] om de eerstvolgende  te starten van bovenstaand uurvak : ");
				string invoergids = Console.ReadLine();
				switch (invoergids)
				{
					case "1":
						Console.Clear();

						if (check != null)
						{
							while (true)
							{
								Console.Clear();
								Console.Write($"gestart: Rondleiding van {check.tijd}\nToets [1] om af te sluiten.\n\nVul de codes in voor deze rondleiding: ");
								string input = Console.ReadLine();
								bool succesvolParsed = int.TryParse(input, out int e);

								while (succesvolParsed != true)
								{
									Console.Clear();
									Console.Write($"gestart: Rondleiding van {check.tijd}\nToets [1] om af te sluiten");
									Console.Write("\n\nUw invoer is niet correct.\nToets een code in bestaand uit cijfers a.u.b : ");
									input = Console.ReadLine();
									succesvolParsed = int.TryParse(input, out e);
								}
								if (e == 1)
								{
									break;
								}


								else if (bezoeker.LijstVanReserveringen.Find
									(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == false) != null)
								{
									bezoeker.LijstVanReserveringen.Find
									(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour == DateTime.Now.Hour && x.rondleidinggestart == false).rondleidinggestart = true;
									Console.Write("\nU mag een lab jas\ntoets [Enter] en geef mij door aan de volgende.");
									serializeJson();
								}


								else if (bezoeker.LijstVanReserveringen.Find
									(x => x.code == e && x.datum == DateTime.Now.Date
									&& x.tijd.Hour == DateTime.Now.Hour
									&& x.rondleidinggestart == true) != null)
								{
									Console.Write("\nU bent al gestart met deze rondleiding.\nToets [Enter] en geef mij door aan de volgende.");
								}



								else if (bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Now.Date && x.tijd.Hour != DateTime.Now.Hour && x.rondleidinggestart == false) != null)
								{
									var checkCodeTijd = bezoeker.LijstVanReserveringen.Find(x => x.code == e && x.datum == DateTime.Today);

									Console.Write($"\nMet deze code is al gereserveerd voor { checkCodeTijd.tijd}. U mag geen labjas\nToets [Enter] en toets uw code voor deze rondleiding in. ");
								}
								else
								{
									Console.Write("\nCode onjuist.U mag geen labjas\nToets [Enter] en toets uw code juist in a.u.b.");
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
						Console.WriteLine("Invoer onjuist. Toets [Enter] en probeer het opnieuw");
						Console.ReadLine();
						break;
				}
			}
		}
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

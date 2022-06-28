using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
	public class ApplicatieComponentBezoeker : Reservering
	{
		public List<Rondleiding> LijstVanRondleidingen { get; set; }
		public List<Reservering> LijstVanReserveringen { get; set; }


		public int BezoekersComponent()
		{
			using (StreamReader RondleidingenJson = new StreamReader(@"Rondleidingen.json"))
			{
				string stringRondleidingen = RondleidingenJson.ReadToEnd();
				LijstVanRondleidingen = JsonConvert.DeserializeObject<List<Rondleiding>>(stringRondleidingen);
			}
			using (StreamReader ReserveringenJson = new StreamReader(@"Reserveringen.json"))
			{
				string stringReserveringen = ReserveringenJson.ReadToEnd();
				LijstVanReserveringen = JsonConvert.DeserializeObject<List<Reservering>>(stringReserveringen);
			}
			while (true)
			{
				Console.Clear();
				Console.Write("Huidige datum: " + DateTime.Now + "\n");

				int indexVanRondleiding = 1;
				int alleRondleidingenVanVandaag = 0;
				foreach (Rondleiding rondleiding in LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
				{
					int aantalReserveringenInRondleiding = LijstVanReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
					Console.WriteLine($"[{indexVanRondleiding}] Rondleiding van {rondleiding.tijd} || plaatsen beschikbaar: {rondleiding.capaciteit - aantalReserveringenInRondleiding}");
					indexVanRondleiding++;
					alleRondleidingenVanVandaag++;
				}

				Console.Write("\n\nSelecteer een rondleiding naar keuze: ");
				string gekozeRondleiding = Console.ReadLine();
				bool succesvolParsedStringNaarInt = int.TryParse(gekozeRondleiding, out int rondleidingkeuzeVanBezoeker);
				if (succesvolParsedStringNaarInt != true | rondleidingkeuzeVanBezoeker > alleRondleidingenVanVandaag | rondleidingkeuzeVanBezoeker < 1)
				{
					Console.Write("[] Keuze is niet geldig. Selecteer een van bovenstaande opties a.u.b: ");
					gekozeRondleiding = Console.ReadLine();
					return 1;
				}

				Console.Clear();
				Console.Write("Door u geselecteerd: " + Check(rondleidingkeuzeVanBezoeker - 1).tijd + " \n\nVoer uw unieke ticket code in: ");
				string Acode1 = Console.ReadLine();
				bool succesvolParsedb = int.TryParse(Acode1, out int Acode);
				while (succesvolParsedb != true)
				{
					Console.Clear();
					Console.Write("Door u geselecteerd: " + Check(rondleidingkeuzeVanBezoeker - 1).tijd + "\n\nUw invoer is niet correct. Typ een code bestaand uit cijfers a.u.b. :  ");
					Acode1 = Console.ReadLine();
					succesvolParsedb = int.TryParse(Acode1, out Acode);
				}
				bool aRondleidinggestart = false;
				(int codegetal, bool returnvalue) = CodeValidatie(Acode);
				var checkCodeLijst = LijstVanReserveringen.Find(x => x.code == Acode && x.datum == DateTime.Today);
				if (returnvalue == true)
				{
					if (checkCodeLijst == null)
					{
						if (Check(rondleidingkeuzeVanBezoeker - 1).capaciteit > LijstVanReserveringen.Where(x => x.tijd == Check(rondleidingkeuzeVanBezoeker - 1).tijd && x.datum == DateTime.Now.Date).Count())
						{
							Reservering reserveringdepot = (new Reservering(Acode, DateTime.Today, Check(rondleidingkeuzeVanBezoeker - 1).tijd, aRondleidinggestart));
							LijstVanReserveringen.Add(reserveringdepot);
							Console.Clear();
							Console.Write("Opgeslagen. Toets [Enter] om terug te gaan.");
							Console.ReadLine();
							//return 1;
						}
						else
						{
							Console.Write("Deze rondleiding zit helaas vol. Toets [Enter] om terug te gaan. ");
							Console.ReadLine();
							//return 1;
						}
					}
					else
					{
						Console.Write($"Met deze code is al gereserveerd voor: {checkCodeLijst.tijd}. Toets [Enter] om terug te gaan.");
						Console.ReadLine();
						//return 1;
					}
				}
				else
				{
					Console.WriteLine($"Code is niet geldig. Toets [Enter] om terug te gaan. ");
					Console.ReadLine();
					// return1;
				}
				using (StreamWriter file = File.CreateText(@"Rondleidingen.json"))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, LijstVanRondleidingen);
				}

				using (StreamWriter file = File.CreateText(@"Reserveringen.json"))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, LijstVanReserveringen);
				}
				return 0;
			}
		}



















		/// methods


		Rondleiding Check(int tijdoptie)
		{
			int q = LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date).Count();
			Rondleiding[] rondleidingen = new Rondleiding[q];

			int i = 0;
			int maxindex = 0;
			foreach (Rondleiding rondleiding in LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
			{
				rondleidingen[i] = rondleiding;
				i++;
				maxindex++;
			}
			return rondleidingen[tijdoptie];
		}

		public static new (int codegetal, bool returnvalue) CodeValidatie(int code)
		{
			if (code % 17 == 0)
			{
				return (code, true);
			}
			else
			{
				return (code, false);
			}
		}
	}
}

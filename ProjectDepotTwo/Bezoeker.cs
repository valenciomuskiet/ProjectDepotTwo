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

		public ApplicatieComponentBezoeker()
		{
			LijstVanRondleidingen = new List<Rondleiding>();
			LijstVanReserveringen = new List<Reservering>();
		}
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
				Console.WriteLine("Huidige datum: " + DateTime.Now);

				int nummer = 1;
				foreach (Rondleiding rondleiding in LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
				{
					int reserveringenC = LijstVanReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
					Console.WriteLine($"[{nummer}] Rondleiding van {rondleiding.tijd} || plaatsen beschikbaar: {rondleiding.capaciteit - reserveringenC}");
					nummer++;
				}

				Console.Write("\n\nSelecteer een rondleiding naar keuze: ");
				string Anum1 = Console.ReadLine();
				bool succesvolParsedc = int.TryParse(Anum1, out int num1);

				while (succesvolParsedc != true)
				{
					Console.Write("Vul een code in bestaand uit cijfers a.u.b: ");
					Anum1 = Console.ReadLine();
					succesvolParsedc = int.TryParse(Anum1, out num1);
				}

				Console.Clear();
				Console.Write("Door u geselecteerd: " + Check(num1 - 1).tijd + " \n\nVoer uw unieke ticket code in: ");

				string Acode1 = Console.ReadLine();
				bool succesvolParsedb = int.TryParse(Acode1, out int Acode);
				while (succesvolParsedb != true)
				{
					Console.Clear();
					Console.WriteLine("Door u geselecteerd: " + Check(num1 - 1).tijd);
					Console.Write("Uw invoer is niet correct, vul afstublieft een code in bestaand uit cijfers: ");
					Acode1 = Console.ReadLine();
					succesvolParsedb = int.TryParse(Acode1, out Acode);
				}


				bool aRondleidinggestart = false;

				(int codegetal, bool returnvalue) = Reservering.CodeValidatie(Acode);
			
				var checkCodeLijst = LijstVanReserveringen.Find(x => x.code == Acode && x.datum == DateTime.Today);

				if (returnvalue == true)
				{
					if (checkCodeLijst == null)
					{
						if (Check(num1 - 1).capaciteit > LijstVanReserveringen.Where(x => x.tijd == Check(num1 - 1).tijd && x.datum == DateTime.Now.Date).Count())
						{
							Reservering reserveringdepot = (new Reservering(Acode, DateTime.Today, Check(num1 - 1).tijd, aRondleidinggestart));
							LijstVanReserveringen.Add(reserveringdepot);

							Console.Clear();
							Console.WriteLine("Opgeslagen, toets [Enter] om terug te gaan");
							Console.ReadLine();
						}
						else
						{
							Console.WriteLine("Deze rondleiding zit helaas vol, toets enter om terug te gaan");
							Console.ReadLine();
						}
					}
					else
					{
						Console.WriteLine($"Met deze code is al gereserveerd voor: {checkCodeLijst.tijd}. Klik [Enter] om terug te gaan naar de startpagina ");
						Console.ReadLine();
					}
				}
				else
				{
					Console.WriteLine($"Code is niet geldig, toets [enter] om terug te gaan ");
					Console.ReadLine();
				}
				using (StreamWriter file = File.CreateText(@"rondleidingen.json"))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, LijstVanRondleidingen);
				}

				using (StreamWriter file = File.CreateText(@"reserveringen.json"))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, LijstVanReserveringen);
				}
				return 0;
			}
		}

		Rondleiding Check(int tijdoptie)
		{
			int q = LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date).Count();
			Rondleiding[] rondleidingen = new Rondleiding[q];

			int i = 0;
			foreach (Rondleiding rondleiding in LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
			{
				rondleidingen[i] = rondleiding;
				i++;
			}
			return rondleidingen[tijdoptie];
		}
	}
}
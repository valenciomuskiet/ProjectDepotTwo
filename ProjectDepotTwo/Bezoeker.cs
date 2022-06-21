using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
	public class Bezoeker
	{
		public List<Rondleiding> LijstRondleidingen { get; set; }
		public List<Reservering> LijstReserveringen { get; set; }

		public Bezoeker()
		{
			LijstRondleidingen = new List<Rondleiding>();
			LijstReserveringen = new List<Reservering>();
		}

		public int BezoekersMenu()
		{
			
			using (StreamReader r = new StreamReader(@"Rondleidingen.json"))
			{
				string json = r.ReadToEnd();
				LijstRondleidingen = JsonConvert.DeserializeObject<List<Rondleiding>>(json);

			}
			
			using (StreamReader r = new StreamReader(@"Reserveringen.json"))
			{
				string json = r.ReadToEnd();
				LijstReserveringen = JsonConvert.DeserializeObject<List<Reservering>>(json);
			}
			

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Bezoeker\n\n[1] om een reservering te maken.");

				string invoerbezoeker = Console.ReadLine();
				switch (invoerbezoeker)
				{
					case "1":
						Console.Clear();
						var dateNu = DateTime.Now;
						Console.WriteLine("Huidige datum: " + dateNu + "\n");

						int nummer = 1;

						foreach (Rondleiding rondleiding in LijstRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
						{
							int reserveringenC = LijstReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
							Console.WriteLine($"[{nummer}] Rondleiding van {rondleiding.tijd} || plekken vrij {rondleiding.capaciteit - reserveringenC}");
						}

						Console.WriteLine("Selecteer de rondleiding naar keuze ");
						int num1 = Convert.ToInt32(Console.ReadLine());

						Console.Clear();
						Console.WriteLine("Door u geselecteerd :" + Check(num1-1).tijd);
						Console.WriteLine("\nVoer uw unieke ticket code in");

						int Acode = Convert.ToInt32(Console.ReadLine());
						bool returnvalue = (CodeControle(Acode));

						var checkCodeLijst = LijstReserveringen.Find(x => x.code == Acode && x.datum == DateTime.Today);

						if (returnvalue == true)
						{
							if (checkCodeLijst == null)
							{
								if (Check(num1 - 1).capaciteit > LijstReserveringen.Where(x => x.tijd == Check(num1 - 1).tijd && x.datum == DateTime.Now.Date).Count())
								{
									Reservering reserveringdepot = (new Reservering(Acode, DateTime.Today, Check(num1 - 1).tijd));
									LijstReserveringen.Add(reserveringdepot);

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
								Console.WriteLine($"Met deze code is al gereserveerd voor: {checkCodeLijst.tijd}.");
								Console.ReadLine();
							}

						}
						else
						{
							Console.WriteLine($"Code is niet geldig, toets [enter] om terug te gaan ");
							Console.ReadLine();
						}
						break;

					default:
						Console.WriteLine("invoer onjuist , selecteer een van bovenstaande opties aub");
						break;
				}

	
				using (StreamWriter file = File.CreateText(@"rondleidingen.json"))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, LijstRondleidingen);
				}

				using (StreamWriter file = File.CreateText(@"reserveringen.json"))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, LijstReserveringen);
				}
				return 0;
			}
			
		}
		

		static bool CodeControle(int code)
		{
			bool answer = code % 17 == 0;
			return answer;
		}


		static void GeselecteerdeTijd()
		{
			var Vandaag = DateTime.Today;
			var reservering11uur = Vandaag.AddHours(11);
			var reservering12uur = Vandaag.AddHours(12);
			var reservering13uur = Vandaag.AddHours(13);
			var reservering14uur = Vandaag.AddHours(13);
			var reservering15uur = Vandaag.AddHours(13);
			var reservering16uur = Vandaag.AddHours(13);
			var reservering17uur = Vandaag.AddHours(13);
		}

		Rondleiding Check(int tijdoptie)
        {
			int q = LijstRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date).Count();
			Rondleiding[] rondleidingen = new Rondleiding[q];

			int i = 0;
			foreach (Rondleiding rondleiding in LijstRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
            {
				rondleidingen[i] = rondleiding;
				i++;
            }
			return rondleidingen[tijdoptie];
        }






	}
}


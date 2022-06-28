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

<<<<<<< Updated upstream
		public ApplicatieComponentBezoeker()
		{
			LijstVanRondleidingen = new List<Rondleiding>();
			LijstVanReserveringen = new List<Reservering>();
		}
=======
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Huidige datum: " + DateTime.Now);

<<<<<<< Updated upstream
				int nummer = 1;
				foreach (Rondleiding rondleiding in LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
				{
					int reserveringenC = LijstVanReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
					Console.WriteLine($"[{nummer}] Rondleiding van {rondleiding.tijd} || plaatsen beschikbaar: {rondleiding.capaciteit - reserveringenC}");
					nummer++;
=======
				int indexNummerVanRondleiding = 1;
				int aantalAankomendeRondleidingenVanVandaag = 0;

				foreach (Rondleiding rondleiding in LijstVanRondleidingen.Where(x => x.tijd.Hour > DateTime.Now.Hour && x.datum == DateTime.Now.Date))
				{
					int aantalReserveringenInRondleiding = LijstVanReserveringen.Where(x => x.tijd == rondleiding.tijd).Count();
					Console.WriteLine($"[{indexNummerVanRondleiding}] Rondleiding van {rondleiding.tijd} || plaatsen beschikbaar: {rondleiding.capaciteit - aantalReserveringenInRondleiding}");
					indexNummerVanRondleiding++;
					aantalAankomendeRondleidingenVanVandaag++;
>>>>>>> Stashed changes
				}



/// Rondleiding selecteren

				Console.Write("\n\nSelecteer een rondleiding naar keuze: ");
<<<<<<< Updated upstream
				string Anum1 = Console.ReadLine();
				bool succesvolParsedc = int.TryParse(Anum1, out int num1);

				while (succesvolParsedc != true)
				{
					Console.Write("Vul een code in bestaand uit cijfers a.u.b: ");
					Anum1 = Console.ReadLine();
					succesvolParsedc = int.TryParse(Anum1, out num1);
=======
				string keuzeRondleiding = Console.ReadLine();
				bool succesvolParsedStringNaarInt = int.TryParse(keuzeRondleiding, out int rondleidingkeuzeVanBezoeker);
				while (succesvolParsedStringNaarInt != true | rondleidingkeuzeVanBezoeker > aantalAankomendeRondleidingenVanVandaag | rondleidingkeuzeVanBezoeker < 1) 
				{
					Console.WriteLine("Uw Keuze is niet valid. Toets [Enter] om deze melding pop up te sluiten en probeer het opnieuw. ");
					Console.ReadLine();
					return 1;
>>>>>>> Stashed changes
				}




/// Ticket code invoeren

				Console.Clear();
<<<<<<< Updated upstream
				Console.Write("Door u geselecteerd: " + Check(num1 - 1).tijd + " \n\nVoer uw unieke ticket code in: ");

=======
				Console.Write("Door u geselecteerd: " + Geselecteerdetijd(rondleidingkeuzeVanBezoeker - 1).tijd + " \n\nVoer uw unieke ticket code in: ");
>>>>>>> Stashed changes
				string Acode1 = Console.ReadLine();
				bool succesvolParsedb = int.TryParse(Acode1, out int Acode);
				while (succesvolParsedb != true)
				{
					Console.Clear();
<<<<<<< Updated upstream
					Console.WriteLine("Door u geselecteerd: " + Check(num1 - 1).tijd);
					Console.Write("Uw invoer is niet correct, vul afstublieft een code in bestaand uit cijfers: ");
=======
					Console.Write("Door u geselecteerd: " + Geselecteerdetijd(rondleidingkeuzeVanBezoeker - 1).tijd + "\n\nUw invoer is niet correct. Typ een code bestaand uit cijfers a.u.b. :  ");
>>>>>>> Stashed changes
					Acode1 = Console.ReadLine();
					succesvolParsedb = int.TryParse(Acode1, out Acode);
				}

<<<<<<< Updated upstream

				bool aRondleidinggestart = false;

				(int codegetal, bool returnvalue) = Reservering.CodeValidatie(Acode);
			
=======
				bool aRondleidinggestart = false;
				(int codegetal, bool returnvalue) = Reservering.CodeValidatie(Acode);
>>>>>>> Stashed changes
				var checkCodeLijst = LijstVanReserveringen.Find(x => x.code == Acode && x.datum == DateTime.Today);

				if (returnvalue == true)
				{
					if (checkCodeLijst == null)
					{
<<<<<<< Updated upstream
						if (Check(num1 - 1).capaciteit > LijstVanReserveringen.Where(x => x.tijd == Check(num1 - 1).tijd && x.datum == DateTime.Now.Date).Count())
						{
							Reservering reserveringdepot = (new Reservering(Acode, DateTime.Today, Check(num1 - 1).tijd, aRondleidinggestart));
=======
						if (Geselecteerdetijd(rondleidingkeuzeVanBezoeker - 1).capaciteit > LijstVanReserveringen.Where(x => x.tijd == Geselecteerdetijd(rondleidingkeuzeVanBezoeker - 1).tijd && x.datum == DateTime.Now.Date).Count())
						{
							Reservering reserveringdepot = (new Reservering(Acode, DateTime.Today, Geselecteerdetijd(rondleidingkeuzeVanBezoeker - 1).tijd, aRondleidinggestart));
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
				using (StreamWriter file = File.CreateText(@"rondleidingen.json"))
=======

				using (StreamWriter file = File.CreateText(@"Rondleidingen.json"))
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
		Rondleiding Check(int tijdoptie)
=======
		Rondleiding Geselecteerdetijd(int tijdoptie)
>>>>>>> Stashed changes
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
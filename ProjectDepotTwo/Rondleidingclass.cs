using System;
namespace ProjectDepotTwo
{
	public class Rondleiding
	{
<<<<<<< Updated upstream:ProjectDepotTwo/Rondleidingclass.cs
		public Rondleiding() { }
=======
		public Rondleiding() {}
>>>>>>> Stashed changes:ProjectDepotTwo/RondleidingObject.cs
		public int capaciteit { get; set; }
		public DateTime datum { get; set; }
		public DateTime tijd { get; set; }

		public Rondleiding(
			int Capaciteit,
			DateTime Datum,
			DateTime Tijd)
		{
			capaciteit = Capaciteit;
			datum = Datum;
			tijd = Tijd;
		}
	}
}
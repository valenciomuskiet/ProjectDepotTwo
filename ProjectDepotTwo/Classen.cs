using System;

namespace ProjectDepotTwo
{
	public class Reservering
	{
		public Reservering() { }

		public int code { get; set; }
		public DateTime datum { get; set; }
		public DateTime tijd { get; set; }

		public Reservering(int aCode, DateTime aDatum, DateTime aTijd)
		{
			code = aCode; 
			datum = aDatum;
			tijd = aTijd;
		}
	}

	public class Rondleiding
    {
		public Rondleiding() { }

		public int capaciteit { get; set; }
		public DateTime datum { get; set; }
		public DateTime tijd { get; set; }

		public Rondleiding(int Capaciteit, DateTime Datum, DateTime Tijd)
        {
			capaciteit = Capaciteit;
			datum = Datum;
			tijd = Tijd;
        }
    }
}



using System;
namespace ProjectDepotTwo
{
	public class Reservering
	{

		public Reservering() { }

		public int code { get; set; }
		public DateTime datum { get; set; }
		public DateTime tijd { get; set; }
		public bool rondleidinggestart { get; set; }

		public static int reserveringID = 0;


		public Reservering(int aCode, DateTime aDatum, DateTime aTijd, bool aRondleidinggestart)
		{
			code = aCode;
			datum = aDatum;
			tijd = aTijd;
			reserveringID++;
			rondleidinggestart = aRondleidinggestart;
		}

		public static (int codegetal, bool returnvalue) CodeValidatie(int code) // tu[;
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


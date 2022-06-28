using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectDepotTwo
{
	public class Reservering
	{
		public Reservering() { }

		public int code { get; set; }
		public DateTime datum { get; set; }
		public DateTime tijd { get; set; }
		public bool rondleidinggestart { get; set; }

		public Reservering(
			int aCode,
			DateTime aDatum,
			DateTime aTijd,
			bool aRondleidinggestart)
		{
			code = aCode;
			datum = aDatum;
			tijd = aTijd;
			rondleidinggestart = aRondleidinggestart;
		}
		public static (int codegetal, bool returnvalue) CodeValidatie(int code)
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


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

		public Reservering(int aCode, DateTime aDatum, DateTime aTijd)
		{
			code = aCode;
			datum = aDatum;
			tijd = aTijd;
		}
	}
}


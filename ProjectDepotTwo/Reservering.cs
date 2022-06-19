using System;
namespace ProjectDepotTwo
{
	public class Reservering
	{
		public int code { get; set; }
		public DateTime tijd { get; set; }
		public static int plekkengereserveerd = 13;

		public Reservering(int aCode, DateTime num1)
		{
			code = aCode; 
			tijd = num1;
		}
	}
}



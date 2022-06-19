using System;
namespace ProjectDepotTwo
{
	public class Reservering
	{
		public int Code { get; set; }
		public string Tijd { get; set; }
		public static int plekkengereserveerd = 13;

		public Reservering()
		{
			plekkengereserveerd--;
		}


		public override string ToString()
		{
			return String.Format("Student Information:\n\tCode: {0}, Tijd: {1} ", Code, Tijd);
		}

	}
}



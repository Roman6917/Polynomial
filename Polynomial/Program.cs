using System;


namespace Polynomial
{
	public static class Program
	{
		public static void Main()
		{
			var mon = new Monomial(1, 5);
			var poli = new Polynomial(
				mon,
				mon,
				mon.Derivative(),
				mon.Derivative(2),
				mon.Derivative(3),
				mon.Derivative(4)
			);

			Console.Out.WriteLine(poli);
			Console.Out.WriteLine(poli.Derivative(0));
			Console.Out.WriteLine(poli.Derivative());
			Console.Out.WriteLine(poli.Derivative(2));
			Console.Out.WriteLine(poli.Derivative(3));
			Console.Out.WriteLine(poli.Derivative(4));
			Console.Out.WriteLine(poli.Derivative(5));
			Console.Out.WriteLine(poli.Derivative(6));
		}
	}
}
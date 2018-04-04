using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Polynomial
{
	public class Polynomial : IEnumerable
	{
		private readonly List<Monomial> _monomials;

		public Polynomial(params Monomial[] monomials)
		{
			_monomials = monomials.ToList();
		}

		public IEnumerator GetEnumerator()
		{
			return _monomials.GetEnumerator();
		}

		public override string ToString()
		{
			var result = "";
			var counter = 0;
			_monomials.ForEach(mon =>
			{
				counter++;
				result += mon.ToString();
				if (counter != _monomials.Count)
					result += " + ";
			});
			if (result == "")
				result = "0";
			return result;
		}

		private void Add(Monomial mon)
		{
			_monomials.Add(mon);
		}

		public Polynomial Derivative(uint iterations = 1)
		{
			var poli = new Polynomial();

			if (iterations == 0)
				return this;

			foreach (var mon in _monomials)
			{
				var derivative = mon.Derivative(iterations);
				if (Equals(derivative, Monomial.Zero)) continue;
				poli.Add(derivative);
			}

			return poli;
		}
	}
}
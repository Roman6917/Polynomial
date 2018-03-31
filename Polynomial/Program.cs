using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable AccessToModifiedClosure
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Polynomial
{
	public class Monomial: ICloneable
		{
			public Monomial(double coefficient = 0, int power = 0)
			{
				Power = power;
				Coefficient = coefficient;
			}

			private int Power { get; set; }
			private double Coefficient { get; set; }
			public static readonly Monomial Zero = new Monomial();

			public Monomial Derivative(uint iterations = 1)
			{
				Monomial result;
				if (Power == 0 || Coefficient == 0)
				{
					result = Zero;
				}
				else if (iterations == 0)
				{
					result = this;
				}
				else
				{
					result = new Monomial(Coefficient * Power, Power - 1);
				}

				if (iterations > 1)
					result = result.Derivative(iterations - 1);

				return result;
			}

			public override string ToString()
			{
				var coeff = "";
				if (Coefficient != 1.0)
					coeff = Coefficient + "";
				var pow = "";
				if (Power != 0.0 && Coefficient != 0)
				{
					pow = "x";
					if (Power != 1.0)
						pow += $"^{Power}";
				}

				var result = $"{coeff}{pow}";
				if (result == "")
					result = $"{Coefficient}";
				return result;
			}

			public object Clone()
			{
				return new Monomial(Coefficient, Power);
			}

			private bool Equals(Monomial other)
			{
				return Power.Equals(other.Power) && Coefficient.Equals(other.Coefficient);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				return obj.GetType() == GetType() && Equals((Monomial) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (Power * 397) ^ Coefficient.GetHashCode();
				}
			}

			public static bool operator ==(Monomial left, Monomial right)
			{
				return Equals(left, right);
			}

			public static bool operator !=(Monomial left, Monomial right)
			{
				return !Equals(left, right);
			}

			public Monomial Add(Monomial mon)
			{
				var newMon = (Monomial) Clone();
				if (mon.Coefficient == 0) 
					return newMon;
				if (newMon.Coefficient == 0)
					newMon.Power = mon.Power;
				if (mon.Power != newMon.Power)
					throw new ArgumentException("Power has to be the same");
				newMon.Coefficient += mon.Coefficient;
				return newMon;
			}
		}

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
/*

			private void Normalize()
			{
				var items = new List<Monomial>();
				var maxPow = int.MinValue;
				_monomials.ForEach(mon =>
				{
					if (mon.Power > maxPow)
					{
						maxPow = mon.Power;
						items.AddRange(Enumerable.Repeat(Monomial.Zero, maxPow - items.Count));
					}

					Console.Out.WriteLine("mon = {0}", mon);
					Console.Out.WriteLine("mon.Power - 1 = {0}", mon.Power - 1);
					Console.Out.WriteLine("items[mon.Power - 1] = {0}", items[mon.Power - 1]);
					items[mon.Power - 1] = items[mon.Power - 1].Add(mon);
				});
				_monomials = items;
			}
*/

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

//				if (iterations > 1)
//					poli = poli.Derivative(iterations - 1);
				return poli;
			}
		}

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
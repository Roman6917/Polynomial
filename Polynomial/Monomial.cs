using System;
using System.Globalization;

namespace Polynomial
{
	public class Monomial : ICloneable
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
				coeff = Coefficient.ToString(CultureInfo.InvariantCulture);
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
}
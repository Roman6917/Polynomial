using System;
using System.Collections.Generic;
using Xunit;


namespace Polynomial.Test
{
	public class UnitTests
	{
		private static Monomial monomial { get; } = new Monomial(1, 5);

		private static Polynomial polynomial { get; } = new Polynomial(monomial,
			monomial,
			monomial.Derivative(),
			monomial.Derivative(2),
			monomial.Derivative(3),
			monomial.Derivative(4));

		[Theory]
		[MemberData(nameof(MonomialData.Data), MemberType = typeof(MonomialData))]
		public void MonomialObjectTest(string source, string expected)
		{
			Assert.Equal(expected, source);
		}

		[Theory]
		[MemberData(nameof(MonomialData.DataDerivative), MemberType = typeof(MonomialData))]
		public void MonomialDerivativeObjectTest(string source, string expected)
		{
			Assert.Equal(expected, source);
		}

		[Theory]
		[MemberData(nameof(MonomialData.ThrowsData), MemberType = typeof(MonomialData))]
		public void TestAddMonomialThrows(Monomial mon)
		{
			Assert.Throws<ArgumentException>(() => monomial.Add(mon));
		}

		[Theory]
		[MemberData(nameof(PolynomialData.Data), MemberType = typeof(PolynomialData))]
		public void PolynomialObjectTest(string source, string expected)
		{
			Assert.Equal(expected, source);
		}

		[Theory]
		[MemberData(nameof(PolynomialData.DataDerivative), MemberType = typeof(PolynomialData))]
		public void PolynomialDerivativeObjectTest(string source, string expected)
		{
			Assert.Equal(expected, source);
		}


		private class MonomialData
		{
			public static IEnumerable<object[]> ThrowsData => new List<object[]>
			{
				new object[] {new Monomial(1, 6)},
				new object[] {new Monomial(2, 8)},
				new object[] {new Monomial(177, 7)},
			};

			public static IEnumerable<object[]> Data => new List<object[]>
			{
				new object[] {new Monomial(1, 2).ToString(), "x^2"},
				new object[] {new Monomial(3, 3).ToString(), "3x^3"},
				new object[] {new Monomial(3, 1).ToString(), "3x"},
				new object[] {new Monomial(0, 3).ToString(), "0"},
				new object[] {new Monomial(24, 75).ToString(), "24x^75"},
				new object[] {new Monomial(45, 7).ToString(), "45x^7"},
			};

			public static IEnumerable<object[]> DataDerivative => new List<object[]>
			{
				new object[] {new Monomial(1, 2).Derivative(1).ToString(), "2x"},
				new object[] {new Monomial(3, 3).Derivative(3).ToString(), "18"},
				new object[] {new Monomial(2, 6).Derivative(5).ToString(), "1440x"},
				new object[] {new Monomial(4, 1).Derivative(2).ToString(), "0"},
				new object[] {new Monomial(14, 5).Derivative(2).ToString(), "280x^3"},
			};
		};

		private class PolynomialData
		{
			public static IEnumerable<object[]> Data => new List<object[]>
			{
				new object[] {polynomial.ToString(), "x^5 + x^5 + 5x^4 + 20x^3 + 60x^2 + 120x"},
				new object[]
				{
					new Polynomial(new Monomial(2, 5),
						new Monomial(10, 3),
						new Monomial(6, 3).Derivative(),
						new Monomial(4, 1)).ToString(),
					"2x^5 + 10x^3 + 18x^2 + 4x"
				},
				new object[]
				{
					new Polynomial(
						new Monomial(11, 25).Derivative(10),
						new Monomial(4, 8).Derivative(3),
						new Monomial(1, 5).Derivative(1),
						new Monomial(27, 4),
						new Monomial(46, 4).Derivative(),
						new Monomial(257, 7).Derivative(5),
						new Monomial(160, 1).Derivative()).ToString(),
					"130478439168000x^15 + 1344x^5 + 5x^4 + 27x^4 + 184x^3 + 647640x^2 + 160"
				},
			};

			public static IEnumerable<object[]> DataDerivative => new List<object[]>
			{
				new object[] {polynomial.Derivative(1).ToString(), "5x^4 + 5x^4 + 20x^3 + 60x^2 + 120x + 120"},
				new object[] {polynomial.Derivative(2).ToString(), "20x^3 + 20x^3 + 60x^2 + 120x + 120"},
				new object[] {polynomial.Derivative(3).ToString(), "60x^2 + 60x^2 + 120x + 120"},
				new object[] {polynomial.Derivative(4).ToString(), "120x + 120x + 120"},
				new object[] {polynomial.Derivative(5).ToString(), "120 + 120"},
				new object[] {polynomial.Derivative(6).ToString(), "0"},
			};
		};
	}
}
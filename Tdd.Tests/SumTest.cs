using System;
using System.Linq.Expressions;
using Tdd.Domains;

namespace Tdd.Tests
{
	public class SumTest
	{
		public SumTest()
		{
		}

		[Fact]
		public void Plus_式の加算が行えること()
		{
            // Arrange
            IExpression fiveBucks = Money.Dollar(5);
            IExpression fiveFrans = Money.Franc(10);
			var bank = new Bank();
			bank.AddRate("CHF", "USD", 2);

			// Act
			var exp = new Sum(fiveBucks, fiveFrans).Plus(fiveBucks);

            // Assert
            Assert.Equal(Money.Dollar(15), bank.Reduce(exp, "USD"));
        }

		[Fact]
		public void Times_加算の式に対して乗算できること()
		{
            // Arrange
            IExpression fiveBucks = Money.Dollar(5);
            IExpression fiveFrans = Money.Franc(10);
            var bank = new Bank();
            bank.AddRate("CHF", "USD", 2);

            // Act
            var exp = new Sum(fiveBucks, fiveFrans).Times(2);

            // Assert
            Assert.Equal(Money.Dollar(20), bank.Reduce(exp, "USD"));
        }
	}
}


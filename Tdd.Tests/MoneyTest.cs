using System;
using System.Diagnostics;
using Tdd.Domains;
using Xunit.Sdk;

namespace Tdd.Tests;

public class MoneyTest
{
    [Fact]
    public void Equals_数量と通貨の種類が同じであるときオブジェクト同士は等しくなること()
    {
        // Act & Assert
        Assert.Equal(new Money(5, "CHF"), new Money(5, "CHF"));
        Assert.Equal(new Money(10, "USD"), new Money(10, "USD"));
    }

    [Fact]
    public void Equals_数量が同じでないときオブジェクト同士は等しくならない()
    {
        // Act & Assert
        Assert.NotEqual(Money.Franc(5), Money.Franc(6));
    }

    [Fact]
    public void Equals_通貨が異なるときオブジェクト同士は等しくならない()
    {
        // Act & Assert
        Assert.NotEqual(Money.Franc(5), Money.Dollar(5));
    }

    [Fact]
    public void Equals_同じ数量のドルとフランを比較したとき等しくならないこと()
    {
        // Act & Assert
        Assert.NotEqual(Money.Dollar(5), Money.Franc(5));
    }

    [Theory]
    [InlineData(10, 2)]
    [InlineData(15, 3)]
    public void Times_引数で指定した数と通貨の数量が乗算された通貨オブジェクトが返ること(int amount, int times)
    {
        // Arrange
        var five = Money.Dollar(5);
        var expected = Money.Dollar(amount);

        // Act
        var actual = five.Times(times);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Dollar_指定した引数の数量のドルオブジェクトが返ること()
    {
        // Arrange
        var expected = new Money(5, "USD");

        // Act
        var actual = Money.Dollar(5);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Franc_指定した引数の数量のフランオブジェクトが返ること()
    {
        // Arrange
        var expected = new Money(5, "CHF");


        // Act
        var actual = Money.Franc(5);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Currency_オブジェクトからその通貨を表す文字列が返ること()
    {
        // Arrange

        // Act

        // Assert
        Assert.Equal("USD", Money.Dollar(1).Currency);
        Assert.Equal("CHF", Money.Franc(1).Currency);
    }

    [Fact]
    public void Plus_同じ通貨同士の加算ができること()
    {
        // Arrange
        var expected = Money.Dollar(20);
        var bank = new Bank();

        // Act
        var result = Money.Dollar(10).Plus(Money.Dollar(10));

        // Assert
        var actual = bank.Reduce(result, "USD");
        Assert.Equal(expected, actual);
    }


    [Fact]
    public void Plus_Sumインスタンスが返ること()
    {
        // Arrange
        var five = Money.Dollar(5);
        var six = Money.Dollar(6);

        // Act
        var result = five.Plus(six);

        // Assert
        var sum = result as Sum;

        Assert.Equal(five, sum?.Augend);
        Assert.Equal(six, sum?.Addend);

    }
}

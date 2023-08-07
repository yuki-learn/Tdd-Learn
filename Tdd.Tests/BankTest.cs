using System;
using System.ComponentModel;
using Tdd.Domains;

namespace Tdd.Tests;


public class BankTest
{
    private readonly Bank bank = new();

    [Fact]
    public void Reduce_指定した通貨に換算されること()
    {
        // Arrange
        var five = Money.Dollar(5);
        var sum = five.Plus(five);
        var bank = new Bank();

        // Act
        Money reduced = bank.Reduce(sum, "USD");

        // Assert
        Assert.Equal(Money.Dollar(10), reduced);
    }

    [Fact]
    public void Reduce_足し算の式を処理して指定通貨の結果が返ること()
    {
        // Arrange
        var sum = new Sum(Money.Dollar(5), Money.Dollar(6));

        // Act
        var reduced = bank.Reduce(sum, "USD");

        // Assert
        Assert.Equal(Money.Dollar(11), reduced);
    }

    [Fact]
    public void Reduce_異なる通貨同士の加算が行えること()
    {
        // Arrange
        IExpression fiveBucks = Money.Dollar(5);
        IExpression fiveFrancs = Money.Franc(10);
        bank.AddRate("CHF", "USD", 2);

        // Act
        var actual = bank.Reduce(fiveBucks.Plus(fiveFrancs), "USD");

        // Assert
        Assert.Equal(Money.Dollar(10), actual);
    }

    [Fact]
    public void Reduce_Moneyオブジェクトのときそのまま返ってくること()
    {
        // Arrange
        var dollar = Money.Dollar(1);

        // Act
        var actual = bank.Reduce(dollar, "USD");

        // Assert
        Assert.Equal(Money.Dollar(1), actual);
    }

    [Fact]
    public void Reduce_レート通りに別通貨への換算が行われること()
    {
        // Arrange
        bank.AddRate("CHF", "USD", 2);

        // Act
        var actual = bank.Reduce(Money.Franc(2), "USD");

        // Assert
        Assert.Equal(Money.Dollar(1), actual);
    }

    [Fact]
    public void Rate_同じ通貨のとき1が返ること()
    {
        // Act
        var actual = bank.Rate("USD", "USD");

        // Assert
        Assert.Equal(1, actual);
    }
}


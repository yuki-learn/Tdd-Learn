namespace Tdd.Domains;

/// <summary>
/// 通貨の加算クラス
/// </summary>
public class Sum : IExpression
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="augend">足される通貨(式)</param>
    /// <param name="addend">足す通貨(式)</param>
    public Sum(IExpression augend, IExpression addend)
    {
        this.Augend = augend;
        this.Addend = addend;
    }

    /// <summary>
    /// 足される通貨(式)
    /// </summary>
    public IExpression Augend { get; init; }

    /// <summary>
    /// 足す通貨(式)
    /// </summary>
    public IExpression Addend { get; init; }

    /// <summary>
    /// 式を足す
    /// </summary>
    /// <param name="addend">足す通貨(式)</param>
    /// <returns>式</returns>
    public IExpression Plus(IExpression addend)
    {
        return new Sum(this, addend);
    }

    /// <summary>
    /// 指定した通貨に変換した通貨オブジェクトを返す
    /// </summary>
    /// <param name="bank">銀行</param>
    /// <param name="to">通貨の種類</param>
    /// <returns>通貨オブジェクト</returns>
    public Money Reduce(Bank bank, string to)
    {
        var augend = bank.Reduce(Augend, to);
        var addend = bank.Reduce(Addend, to);
        return new Money(augend.Amount + addend.Amount, to);
    }

    public IExpression Times(int multiplier)
    {
        return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
    }
}
namespace Tdd.Domains;

public interface IExpression
{
    /// <summary>
    /// 指定した通貨に変換した通貨オブジェクトを返す
    /// </summary>
    /// <param name="bank">銀行</param>
    /// <param name="to">通貨の種類</param>
    /// <returns>通貨オブジェクト</returns>
    Money Reduce(Bank bank, string to);

    /// <summary>
    /// 加算を行う
    /// </summary>
    /// <param name="addend">足す通貨(式)</param>
    /// <returns>式</returns>
    IExpression Plus(IExpression addend);

    /// <summary>
    /// 乗算を行う
    /// </summary>
    /// <param name="multiplier">かける数</param>
    /// <returns>式</returns>
    IExpression Times(int multiplier);
}


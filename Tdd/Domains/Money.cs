namespace Tdd.Domains;

/// <summary>
/// 通貨オブジェクト
/// </summary>
public class Money : IExpression, IEquatable<Money>
{
    /// <summary>
    /// 通貨の種類
    /// </summary>
    public string Currency { get; init; }

    /// <summary>
    /// 数量
    /// </summary>
    public int Amount { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="amount">数量</param>
    /// <param name="currency">通貨の種類</param>
    public Money(int amount, string currency)
	{
        this.Amount = amount;
        this.Currency = currency;
	}

    /// <summary>
    /// 指定した数量のドルを返す
    /// </summary>
    /// <param name="amount">数量</param>
    /// <returns>ドル</returns>
    public static Money Dollar(int amount) => new(amount, "USD");

    /// <summary>
    /// 指定した数量のフランを返す
    /// </summary>
    /// <param name="amount">数量</param>
    /// <returns>フラン</returns>
    public static Money Franc(int amount) => new(amount, "CHF");

    /// <summary>
    /// 指定した数と通貨の数量を乗算して返す。
    /// </summary>
    /// <param name="multiplier">乗算する値</param>
    /// <returns>乗算後の通貨</returns>
    public IExpression Times(int multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }

    /// <summary>
    /// 通貨同士を加算する
    /// </summary>
    /// <param name="addend">加算する通貨</param>
    /// <returns>加算後の通貨</returns>
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
        var rate = bank.Rate(Currency, to);
        return new Money(this.Amount / rate, to);
    }

    /// <summary>
    /// インスタンスと引数のイコール結果を返す
    /// </summary>
    /// <param name="other">オブジェクト</param>
    /// <returns>等価: true, 非等価: false</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Money);
    }

    /// <summary>
    /// インスタンスと引数のイコール結果を返す
    /// </summary>
    /// <param name="other">通貨オブジェクト</param>
    /// <returns>等価: true, 非等価: false</returns>
    public bool Equals(Money? other)
    {
        return other is not null &&
               Amount == other.Amount &&
               Currency == other.Currency;
    }

    /// <summary>
    /// ハッシュコードを返す
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Amount);
    }

    /// <summary>
    /// 通貨オブジェクトが等価であるか確認する
    /// </summary>
    /// <param name="left">通貨オブジェクト</param>
    /// <param name="right">通貨オブジェクト</param>
    /// <returns>等価:true, 非等価: false</returns>
    public static bool operator ==(Money? left, Money? right)
    {
        return EqualityComparer<Money>.Default.Equals(left, right);
    }

    /// <summary>
    /// 通貨オブジェクトが非等価であるか確認する
    /// </summary>
    /// <param name="left">通貨オブジェクト</param>
    /// <param name="right">通貨オブジェクト</param>
    /// <returns>非等価: true, 等価: false</returns>
    public static bool operator !=(Money? left, Money? right)
    {
        return !(left == right);
    }
}
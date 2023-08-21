namespace Tdd.Domains;

/// <summary>
/// 通貨レート使用するペアクラス
/// </summary>
public class Pair : IEquatable<Pair>
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="from">元の通貨</param>
    /// <param name="to">変換先の通貨</param>
	public Pair(string from, string to)
	{
		this.From = from;
		this.To = to;
    }

    /// <summary>
    /// 元の通貨
    /// </summary>
	public string From { get; init; }

    /// <summary>
    /// 変換先の通貨
    /// </summary>
	public string To { get; init; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Pair);
    }

    public bool Equals(Pair? other)
    {
        return other is not null &&
               From == other.From &&
               To == other.To;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    public static bool operator ==(Pair? left, Pair? right)
    {
        return EqualityComparer<Pair>.Default.Equals(left, right);
    }

    public static bool operator !=(Pair? left, Pair? right)
    {
        return !(left == right);
    }
}


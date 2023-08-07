using System;
using System.Reflection.Metadata.Ecma335;

namespace Tdd.Domains;

public class Bank
{
	/// <summary>
	/// 通貨レートを保持する辞書
	/// </summary>
	private readonly Dictionary<Pair, int> rates = new();

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public Bank()
	{
	}

	/// <summary>
	/// レートを追加する
	/// </summary>
	/// <param name="from">元の通貨</param>
	/// <param name="to">変換先の通貨</param>
	/// <param name="rate">レート</param>
	public void AddRate(string from, string to, int rate)
	{
		rates.Add(new Pair(from, to), rate);
	}

    /// <summary>
    /// 通貨レートを返す
    /// </summary>
    /// <param name="from">元の通貨</param>
    /// <param name="to">変換先の通貨</param>
    /// <returns>レート</returns>
    public int Rate(string from, string to)
    {
		if (from == to) return 1;

        if (rates.TryGetValue(new Pair(from, to), out var rate))
        {
            return rate;
        }

        throw new ArgumentException($"{nameof(from)}_{nameof(to)}");
    }

	/// <summary>
	/// 式を処理して通貨オブジェクトを返す
	/// </summary>
	/// <param name="source">式</param>
	/// <param name="to">変換先の通貨</param>
	/// <returns>通貨</returns>
    public Money Reduce(IExpression source, string to)
	{
		return source.Reduce(this, to);
	}
}
## アサートファースト
テストはテストの終わりにパスすべき**Assert(アサート)から書く**のが良い。

アサートファーストで書くと「機能の実装はどこに属するべきか」「機能の名前はどんな名前にするべきか」等々、実装のことを考えずに、終着点である「正しい結果はなにか」と「どうやって検証すべきか」だけに焦点をあててテストをシンプルにできる。

例: `MoneyTest.cs`の異なる通貨の足し算のテスト

「正しい結果は何か」: $5 + 10CHF = $10(ドルとフランのレートが2:1のとき)
```cs
[Fact]
public void testMixedAddtion()
{
    Assert.Equal(Money.Dollar(10), result);
}
```

「それをどう検証するか」: 足し算後ドル換算した`result`ができるまでを少しずつ書いていく。
```cs
[Fact]
public void testMixedAddtion()
{
    var result = bank.Reduce(sum, "USD"); // 足し算した結果をドル換算に
    Assert.Equal(Money.Dollar(10), sum);
}
```

```cs
[Fact]
public void testMixedAddtion()
{
    var sum = Money.Dollar(5).Plus(Money.Franc(5)); // ドルとフランを足したオブジェクトを作る
    var result = bank.Reduce(sum, "USD"); // 足し算した結果をドル換算に
    Assert.Equal(Money.Dollar(10), sum);
}
```
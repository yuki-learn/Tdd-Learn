# 「テスト駆動開発」のメモ


## テスト駆動開発を行うとどうなる
* 動作するコードが設計判断にフィードバックをもたらすので、有機的に設計を進められるようになる。
* まずテストを書くようになる。テストを書くことが日常になる。
* テストを書きやすくするために、凝集度が高く、結合度の低いたくさんの部品で構成されたような設計になる。

## テスト駆動開発の作業順序、ルール

### ルール
* プロダクトコードを書く前に、失敗する自動テストコードを書く。
* 重複を除去する。
### 作業順序
「レッド」「グリーン」「リファクタリング」がTDDの基本順序。

1. Red: **動作せず、コンパイルも通らないようなテストを書く。**
2. Green: **おかしなコードでも良いので、そのテストを動作させる。**
3. リファクタリング: **テストを通すために発生した重複するコードなどを排除する。**

#### 詳細
1. 小さいテストを1つ書く。
2. すべてのテストを実行し、1つ失敗することを確認する。
3. 小さい変更を行う。
4. 再度テストを実行し、すべて成功することを確認する。
5. リファクタリングをして重複を削除する。

## グリーンバーのパターン

レッドバーになってグリーンを目指すときの作業詳細。

### 仮実装
失敗するテストを書いたあとは、速やかにグリーンバーにさせたいので **まずベタ書きの値を使って、実装を進めるにつれて徐々に変数へ置き換えて行く**。 

仮実装は後に消してしまうが、仮実装でもテストが通れば次やることが明確になるので作業しやすくなるというような効果がある。


```csharp
public int Add(int augend, int addend)
{
    return 11; // ベタ書きの値を使ってひとまずグリーン目指す。
}
```


### 三角測量
仮実装をするとたまたまテストが通ったことになるので、コードを一般化させる必要がある。

そこで一般化したコードを実装するときに、三角測量を行う。
実装が正しいことを確認できるような **別のテストケースを増やしてからコードを一般化させてみて、正しく一般化できているか確認する** 

例
```csharp
public int Add(int augend, int addend)
{
    return 11;
}

public void 2つの数値を足した結果が返ること()
{
    Assert.Equal(11, Add(5, 6));

    // このテストケースを追加して、仮実装から一般化させる実装を行う。
    Assert.Equal(3, Add(1, 2));
}
```


```cs

public int Add(int augend, int addend)
{
    // Assert.Equal(3, Add(1, 2));は これだと通らなくなるので変更する。
    return augend + addend;
}
```

### 明白な実装

三角測量をするまでもなく、コードの一般化が簡単なときは、**ベタ書きの値を使ってテストを通さず、素直に実装してしまう。** 
(↑ `Add`の例は正直、三角測量するまでもない)

もし、レッドバーになったらすぐ仮実装からやり始める。

## レッドバーのパターン
「レッドにする」 = 「テストを書き始める」なので、TODOリストに書いた内容からまずはどのテストを書き始めるか選ぶところから始める。

何を選ぶかは難しいが、できるだけ明白で書けば動かせそうなテストを選ぶ。

### 学習用テスト
自分の使ったことない機能を使ったテストを書く際は、まずはその**使ったことのない機能**に対してテストを書いて、動作を確かめる。

例) `Linq`の`Count()`の機能を確かめるテスト(確かめるまでもないが)

```cs
public void Count_コレクションの中身の数が返ること()
{
    // Arrange
    var testData = new string[] { "A", "B", "C" };

    // Act & Assert
    Assert.Equal(3, testData.Count());
}
```


### 回帰テスト

バグなど不具合が出たら、**不具合を再現するような最小のテストを書く**。

その後、プロダクトコードを修正して、そのテストが通れば不具合が修正されたことになる。


## AAAパターン
テストを書くときは以下の基本パターンに沿って書く。

1. **Arrange (準備)**
2. **Act (実行)**
3. **Assert (検証)**

AAAパターンで書くと、**そのテストが何の振る舞いのテストをしているのか明確になり、あとからテスト見たとき注目すべきコードもすぐ分かるようになる。**

#### 例(実装した通貨のプログラムから)
```csharp
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
```


## テスティングのパターン


### 小さいテスト
あまりにも大きくなってしまったテストが失敗したときは、そのテストから問題箇所を抜き出して小さいテストを別に作るようにする。

レッド、グリーン、リファクタリングのリズムが大事なのでそもそも大きいテストを作らないようにする。

### 失敗したままのテスト
コーディングを終えるときは失敗したままのテストを作って作業を終えるのが良い。次に作業するとき、レッドバーになっているのでやるべきことが明確になる。


### Log Stringパターン
正しい順序でメソッドが実行できているかテストしたいときは、**記録用文字列**を作って、メソッド呼び出しのたびにその文字に追記するようにする。


Observerパターンを実装していて特定の順番で通知が行われているか検証するときにLog Stringパターンが役に立つ。
```cs
public void 正しい実行順にメソッドが呼ばれること()
{
    // Arrange
    var domain = new Product();
    
    // Act
    domain.Run();

    // Assert
    Assert.Equal("setUp TestMethod TearDown", domain.Log);
}

private void Setup()
{
    this.Log = "setUp";
}

private void TestMethod()
{
    this.Log = $"{this.Log} TestMethod";
}

private void TearDown()
{
    this.Log = $"{this.Log} TearDown";
}
```


## その他メモ

* **テストは他のテストに依存させない**: テスト実行順序によって失敗 or 成功したりするようなテストは良くないので、テストは独立させ、他のテストに絶対依存させるべきでない。
* **TODOリスト**: 何をテストすべきかどうか、実際にテストを書き始める前にTODOリストに必要になりそうなテストを書いておく。やるべきことがハッキリし、後々漏らすことが減る。
* **アサートファースト**: [別途メモ](./assert.md)
* **テストする対象**:
    * 条件分岐
    * ループ
    * 操作
    * ポリモフィズム

### 良くないテスト
* **前準備に必要なコードが多い**: アサーションを行うまでにかなり長い準備のコードがある場合はオブジェクトが大きすぎるのでテストを分割する。
* **テストの実行時間が長いテスト**: TDDにおいてテスト実行に時間がかかりすぎるとそのテストは実行する頻度が減る。実行頻度が減ると、たくさんのコードが修正された後に実行させると通らなくなっていることがあるので、実行時間が短いテストを書くようにする。



### 他のテスト

TDDによって作られるユニットテストは以下の代わりにはならない。
* パフォーマンステスト
* 負荷テスト
* ユーザビリティテスト
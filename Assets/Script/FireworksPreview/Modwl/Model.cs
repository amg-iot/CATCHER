using UniRx;

/*
 * 
 * Some code is borrowed from [Rx.NET](https://rx.codeplex.com/) and [mono/mcs](https://github.com/mono/mono).
 *
 */
public class Model {
	// タイミング
	public ReactiveProperty<int> _timing { get; private set; }

	/**
	* コンストラクタ.
	*/
	public Model()
	{
		_timing = new ReactiveProperty<int>();
	}

	/**
	* 値の設定.
	*/
	public void SetTiming(int timing)
	{
		_timing.Value = timing;
	}
}

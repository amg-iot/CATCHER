using UniRx;

public class Model {
	// タイミング
	public ReactiveProperty<int> _timing { get; private set; }
	//private ReactiveProperty<int> _timing;
	//public IReadOnlyReactiveProperty<int> Timing
	//{
	//	get { return _timing; }
	//}

	// コンストラクタ
	public Model()
	{
		_timing = new ReactiveProperty<int>();
	}

	// Timingの値を設定する
	public void SetTiming(int timing)
	{
		_timing.Value = timing;
	}
}

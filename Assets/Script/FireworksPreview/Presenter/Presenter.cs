using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * 
 * Some code is borrowed from [Rx.NET](https://rx.codeplex.com/) and [mono/mcs](https://github.com/mono/mono).
 *
 */
public class Presenter : MonoBehaviour {
	// View
	[SerializeField]
	private View _view;

	// Model
	[SerializeField]
	private Model _model;

	// 実行中フラグ
	private bool i_run = false;

	// 準備中キャンパス
	private CanvasGroup canvasWaitGroup = null;

	/**
	* 開始関数.
	*/
	void Start () {
		// 文字変更コールバック設定
		StartCoroutine( FuncCoroutine() );
	}

	/**
	* 花火準備中文字変更コールバック.
	*/
    IEnumerator FuncCoroutine() {
		Text waitText = GameObject.Find("WaitText").GetComponent<Text>();

		int count = 0;
        while(i_run == false){
			if (count == 0) {
				waitText.text = "花火打ち上げ準備中";
				count++;
			} else if (count == 1) {
				waitText.text = "花火打ち上げ準備中.";
				count++;
			} else if (count == 2) {
				waitText.text = "花火打ち上げ準備中..";
				count++;
			} else if (count == 3) {
				waitText.text = "花火打ち上げ準備中...";
				count++;
			} else if (count == 4) {
				waitText.text = "花火打ち上げ準備中....";
				count = 0;
			}
            yield return new WaitForSeconds(0.5f);
        }
    }

	/**
	* オブジェクト生成時に呼び出すSONデータ送信ボタン.
	*/
	public void Awake()
	{
		// ビューのオブジェクトを作成
		_view = new View ();

		// 各種ビューのコールバックを設定	
		SetEvents();
	}

	/**
	* Viewのイベントの設定を行う.
	*/
	private void SetEvents()
	{
        // var hoge = GameObject.Find("sum");
		// EventTrigger trigger = hoge.GetComponent<EventTrigger>();
		// EventTrigger.Entry entry = new EventTrigger.Entry();
		// entry.eventID = EventTriggerType.PointerDown;
		// entry.callback.AddListener(( data ) => { _view.OnSumButtonClicked( (PointerEventData)data ); });
		// trigger.triggers.Add(entry);

		// _view.OnSumButtonClickedListener = OnSumButtonChildClicked;
	}
	
	/**
	* 開始関数.
	*/
	public void StartHanabi (JsonData data) {

		if (i_run) {
			return;
		}

		i_run = true;
		canvasWaitGroup = GameObject.Find("WaitCanvas").GetComponent<CanvasGroup>();
		canvasWaitGroup.alpha = 0;

		int runCunt = 0;
		// //10秒毎に花火を上げる
		Observable.Interval(TimeSpan.FromMilliseconds(1000*3)).Subscribe(l => {
			_view.ViewFireworks(hanabiData[runCunt++]);
		}).AddTo(this);
	}

	// 「＋」ボタンが押された時に呼ばれるメソッド
	// public void OnSumButtonChildClicked(PointerEventData data)
	// {
	// 	Debug.Log ("Click");
	// }
}

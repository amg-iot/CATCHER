using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MiniJSON;

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
	* JSONデータのパースを行う.
	*/
	private JsonData readJsonData(string text) {
		JsonData jsonData = new JsonData();
		
		long count = 0;
		IList familyList = (IList)Json.Deserialize(text);
		PersonalData[] personalDataList = new PersonalData[familyList.Count];
		foreach(IDictionary json in familyList){
			personalDataList[count] = new PersonalData();

			if (json.Contains("author")) {
				personalDataList[count].author = (string) json["author"];
				Debug.Log((string) json["author"]);
			}
			if (json.Contains("sex")) {
				personalDataList[count].sex = (string) json["sex"];
				Debug.Log((string) json["sex"]);
			}
			if (json.Contains("name")) {
				personalDataList[count].name = (string) json["name"];
				Debug.Log((string) json["name"]);
			}
			if (json.Contains("y")) {
				personalDataList[count].y = (long) json["y"];
				Debug.Log((long) json["y"]);
			}
			if (json.Contains("speed")) {
				personalDataList[count].speed = (long) json["speed"];
				Debug.Log((long) json["speed"]);
			}

			long num = 0;
			IList elem = (IList)json["bulletArr"];
			BulletArr[] bulletArrList = new BulletArr[elem.Count];
			foreach (IDictionary item in elem)
			{
				bulletArrList[num] = new BulletArr();

				if (item.Contains("type")) {
					bulletArrList[num].type = (string) item["type"];
					Debug.Log((string) item["type"]);
				}
				if (item.Contains("speed")) {
					bulletArrList[num].speed = 0;
					// bulletArrList[num].speed = (long) item["speed"];
					// Debug.Log((long) item["speed"]);
				}
				if (item.Contains("color")) {
					bulletArrList[num].color = (long) item["color"];
					Debug.Log((long) item["color"]);
				}
				if (item.Contains("degree")) {
					bulletArrList[num].degree = (long) item["degree"];
					Debug.Log((long) item["degree"]);
				}
				if (item.Contains("rectWidth")) {
					bulletArrList[num].rectWidth = (long) item["rectWidth"];
					Debug.Log((long) item["rectWidth"]);
				}
				if (item.Contains("rectHeight")) {
					bulletArrList[num].rectHeight = (long) item["rectHeight"];
					Debug.Log((long) item["rectHeight"]);
				}
				num++;
			}
			personalDataList[count].bulletArr = bulletArrList;
			count++;
		}
		jsonData.party = personalDataList;

		return jsonData;
	}

	IEnumerator goHanabi(string data) {

		i_run = true;
		JsonData jsonData = new JsonData();

		bool b_ok = true;
		// ルームプロパティから花火のデータを取得
		try {

			string repText = data.Replace("\r\n", "");
			jsonData = readJsonData(repText);
		} catch {
			b_ok = false;
		}

		if (b_ok) {
			canvasWaitGroup = GameObject.Find("WaitCanvas").GetComponent<CanvasGroup>();
			canvasWaitGroup.alpha = 0;

			var collection = new ReactiveCollection<PersonalData>();

			collection
			.ObserveAdd()
			.Subscribe(x =>
			{
				_view.ViewFireworks((PersonalData) x.Value);
			});

			foreach(PersonalData party in  jsonData.party){
				collection.Add(party);
				yield return new WaitForSeconds(0.5f);
			}
		}

		yield return true;
	}

	/**
	* 開始関数.
	*/
	public void StartHanabi (string data) {

		if (i_run) {
			return;
		}
		StartCoroutine( goHanabi(data) );
	}
}

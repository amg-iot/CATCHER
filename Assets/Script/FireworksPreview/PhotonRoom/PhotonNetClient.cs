using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniJSON;

public class PhotonNetClient : Photon.PunBehaviour {
    
	/**
	* Use this for initialization.
	*/
	void Start () {
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
					bulletArrList[num].speed = (long) item["speed"];
					Debug.Log((long) item["speed"]);
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
	
	/**
	* サーバー側からのデータ送信時に呼び出される.
	*/
	public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable changedProperties ){
		object value = null;

		// ルームプロパティから花火のデータを取得
		if (changedProperties.TryGetValue ("jsonText", out value)) {
			try {
				string repText = (string)value;
				JsonData data = readJsonData(repText);

				// 同一オブジェクト内のソースのみ可能
				Presenter presenter = GetComponent<Presenter>();
				presenter.StartHanabi(data);
			} catch {
			}
		}
	}
}

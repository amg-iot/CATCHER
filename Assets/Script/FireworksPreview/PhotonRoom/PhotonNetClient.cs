using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniJSON;

public class PhotonNetClient : Photon.PunBehaviour {
	private string g_JsonStr = "";
    
	/**
	* Use this for initialization.
	*/
	void Start () {
	}

	// /**
	// * 各花火データを一つにまとめる.
	// */
	// private HanabiDataList[] convMainList(JsonData jsonData) {
	// 	int addCount = 0;
	// 	HanabiDataList[] result = new HanabiDataList[addCount];

	// 	foreach(PersonalData json in jsonData.party){
			
	// 		foreach(BulletArr bulletArr in json.bulletArr){
	// 			System.Array.Resize(ref result, ++addCount);
	// 			result[addCount-1] = new HanabiDataList();

	// 			result[addCount-1].author = json.author;
	// 			result[addCount-1].sex = json.sex;
	// 			result[addCount-1].name = json.name;
	// 			result[addCount-1].y = json.y;
	// 			result[addCount-1].speed_main = json.speed;
	// 			result[addCount-1].type = bulletArr.type;
	// 			result[addCount-1].speed = bulletArr.speed;
	// 			result[addCount-1].color = bulletArr.color;
	// 			result[addCount-1].degree = bulletArr.degree;
	// 			result[addCount-1].radius = bulletArr.radius;
	// 			result[addCount-1].rectWidth = bulletArr.rectWidth;
	// 			result[addCount-1].rectHeight = bulletArr.rectHeight;
	// 		}
	// 	}

	// 	return result;
	// }

	public HanabiDataList[] Shuffle(HanabiDataList[] array)
	{
		var length = array.Length;
		var result = new HanabiDataList[length];
		System.Array.Copy(array, result, length);

		var random = new System.Random();
		int n = length;
		while (1 < n)
		{
			n--;
			int k = random.Next(n + 1);
			var tmp = result[k];
			result[k] = result[n];
			result[n] = tmp;
		}
		return result;
	}

	/**
	* サーバー側からのデータ送信時に呼び出される.
	*/
	public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable changedProperties ){
		object value = null;

		if (changedProperties.TryGetValue ("startSendJson", out value)) {
			g_JsonStr = "";
		}
		else if (changedProperties.TryGetValue ("sendJson", out value)) {
			g_JsonStr += (string)value;
		}
		else if (changedProperties.TryGetValue ("endSendJson", out value)) {
			// 同一オブジェクト内のソースのみ可能
			Presenter presenter = GetComponent<Presenter>();
			presenter.StartHanabi(g_JsonStr);
		}
	}
}

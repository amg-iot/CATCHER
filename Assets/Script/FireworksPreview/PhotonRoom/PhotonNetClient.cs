using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniJSON;

public class PhotonNetClient : Photon.PunBehaviour {
	private string g_JsonStr = "";
	private int max_num = 0;
	private int load_count = 0;
    
	/**
	* Use this for initialization.
	*/
	void Start () {
	}

	/**
	* サーバー側からのデータ送信時に呼び出される.
	*/
	public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable changedProperties ) {
		object value = null;
		Text waitText = GameObject.Find("TextNext").GetComponent<Text>();

		if (changedProperties.TryGetValue ("startSendJson", out value)) {
			g_JsonStr = "";
			max_num = (int)value;
			waitText.text = "サーバーデータ受信 カウント" + (++load_count) + "/" + max_num;
		}
		else if (changedProperties.TryGetValue ("sendJson", out value)) {
			g_JsonStr += (string)value;
			waitText.text = "サーバーデータ受信 カウント" + (++load_count) + "/" + max_num;
		}
		else if (changedProperties.TryGetValue ("endSendJson", out value)) {
			waitText.text = "サーバーデータ受信 完了";
			// 同一オブジェクト内のソースのみ可能
			Presenter presenter = GetComponent<Presenter>();
			presenter.StartHanabi(g_JsonStr);
		}
	}
}

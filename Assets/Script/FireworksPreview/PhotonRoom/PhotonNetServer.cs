using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniJSON;

public class PhotonNetServer : Photon.PunBehaviour {

	/**
	* Use this for initialization.
	*/
	void Start () {
		// Room作成ボタンのコールバック追加
		Button SendButton = GameObject.Find("SendButton").GetComponent<Button>();
		SendButton.onClick.AddListener (OnClickSendButton);
	}
	
	/**
	* JSONデータ送信ボタン.
	*/
	public void OnClickSendButton() {
		InputField jsonInput = GameObject.Find("jsonInput").GetComponent<InputField>();

		if (jsonInput != null) {
			string repText = jsonInput.text.Replace("\r\n", "");
			var properties  = new ExitGames.Client.Photon.Hashtable();
			properties.Add( "jsonText", repText );
			PhotonNetwork.room.SetCustomProperties( properties );
		}

	}
}

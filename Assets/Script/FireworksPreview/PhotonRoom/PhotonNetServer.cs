using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonNetServer : Photon.PunBehaviour {
    
	// Use this for initialization
	void Start () {
		// Room作成ボタンのコールバック追加
		Button SendButton = GameObject.Find("SendButton").GetComponent<Button>();
		SendButton.onClick.AddListener (OnClickSendButton);
	}
	
	public void OnClickSendButton() {
		var properties  = new ExitGames.Client.Photon.Hashtable();
		properties.Add( "jsonText", "あいうえお下記久家湖" );
		PhotonNetwork.room.SetCustomProperties( properties );
	}

	public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable changedProperties ){
		object value = null;

		// ルームプロパティから床の色を取得
		if (changedProperties.TryGetValue ("jsonText", out value)) {
			string color = (string)value;

			Debug.Log("受信データ：" + value);
		}
	}
}

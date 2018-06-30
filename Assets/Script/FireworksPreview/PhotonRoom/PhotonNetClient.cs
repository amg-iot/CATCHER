using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonNetClient : Photon.PunBehaviour {
    
	// Use this for initialization
	void Start () {
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

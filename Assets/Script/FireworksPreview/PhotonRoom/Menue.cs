using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menue : Photon.PunBehaviour {
    public string ObjectName;

    // エディタのインスペクタで、この変数にヒエラルキーにある Canvas を割り当ててください。
    public Canvas canvasSub = null;

    private void changeSubWindowEnable(Canvas canvasSub, bool enable ) {
        if (canvasSub != null) canvasSub.enabled = enable;
    }

	// Use this for initialization
	void Start () {
		// Button StartButton = GameObject.Find("StartButton").GetComponent<Button>();
		// StartButton.onClick.AddListener (OnClickStartButton);
		// Photonネットワークの設定を行う
        
        canvasSub = GameObject.Find("CanvasSub").GetComponent<Canvas>();
        // ダイアログを表示するときまで、 Canvas を無効にしておく。
        changeSubWindowEnable(canvasSub, false);

		Button AdminButton = GameObject.Find("AdminButton").GetComponent<Button>();
		AdminButton.onClick.AddListener (OnClickAdminButton);

        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.sendRate = 30;
	}
		
    public void OnClickAdminButton() {
		SceneManager.LoadScene ("Fireworks Project");
        // ダイアログを表示するときまで、 Canvas を有効に変更
        changeSubWindowEnable(canvasSub, true);
	}

	// public void OnClickStartButton() {
	// 	SceneManager.LoadScene ("Fireworks Project");
	// }

    // 「ロビー」に接続した際に呼ばれるコールバック
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        //ラインダム接続
        //PhotonNetwork.JoinRandomRoom();
    }

    // いずれかの「ルーム」への接続に失敗した際のコールバック
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");

        // ルームを作成（今回の実装では、失敗＝マスタークライアントなし、として「ルーム」を作成）
        PhotonNetwork.CreateRoom(null);
    }

    // Photonサーバに接続した際のコールバック
    public override void OnConnectedToPhoton()
    {
        Debug.Log("OnConnectedToPhoton");
    }

    // マスタークライアントに接続した際のコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomRoom();
    }

	   // いずれかの「ルーム」に接続した際のコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        // 「ルーム」に接続したらCubeを生成する（動作確認用）
        GameObject cube = PhotonNetwork.Instantiate(ObjectName, Vector3.zero, Quaternion.identity, 0);
    }

    // 現在の接続状況を表示（デバッグ目的）
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}

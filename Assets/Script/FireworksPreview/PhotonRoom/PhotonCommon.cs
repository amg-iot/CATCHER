using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonCommon : Photon.PunBehaviour {
	// サーバー判定フラグ
    private bool    m_is_server  = false;
	private CanvasGroup canvasGroup = null;
	private CanvasGroup canvasLoadGroup = null;

	// クライアント用遷移シーン名
    private const string SCENE_NAME = "LaunchFireworks";
	// サーバー用遷移シーン名
    private const string SCENE_NAME_SERVER = "ServerSendScene";

	/**
	* Use this for initialization.
	*/
	void Start () {
		// Photonネットワークの設定を行う
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.sendRate = 30;

		// 適当にニックネームを設定
        PhotonNetwork.playerName = "Admin" + UnityEngine.Random.Range(1000, 9999);

		// Room作成ボタンのコールバック追加
		Button RoomAdd = GameObject.Find("RoomAdd").GetComponent<Button>();
		RoomAdd.onClick.AddListener (OnClickRoomAddButton);

		// Room作成ボタンのコールバック追加
		Button RoomLoad = GameObject.Find("RoomLoad").GetComponent<Button>();
		RoomLoad.onClick.AddListener (OnClickRoomLoadButton);

		canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		canvasLoadGroup = GameObject.Find("CanvasLoad").GetComponent<CanvasGroup>();
		canvasLoadGroup.alpha = 0;

		 // シーンの読み込みコールバックを登録.
        SceneManager.sceneLoaded += OnLoadedScene;
	}
	
	/**
	* シーンの読み込みを行う.
	*/
	private IEnumerator LoadScene( float i_time )
    {
		canvasGroup.alpha = 0;
		canvasLoadGroup.alpha = 1;

        // 一定時間経ってからシーンを読む.
        yield return new WaitForSeconds( i_time );
		// シーンの読み込み
        AsyncOperation _async = SceneManager.LoadSceneAsync( (m_is_server)? SCENE_NAME_SERVER:SCENE_NAME );

		Image progress = GameObject.Find("progress").GetComponent<Image>();
		Text loadText = GameObject.Find("LoadText").GetComponent<Text>();

		int count = 0;
		float progressNum = (float)0.01;
		//シーン移動を許可するかどうか
        _async.allowSceneActivation = false;
		 while (_async.progress < 0.9f) //0.9で止まる
        {
            Debug.Log(_async.progress);
			progress.fillAmount = (float)_async.progress;
			if (count == 0) {
				loadText.text = "NowLoading";
				count++;
			} else if (count == 1) {
				loadText.text = "NowLoading.";
				count++;
			} else if (count == 2) {
				loadText.text = "NowLoading..";
				count++;
			} else if (count == 3) {
				loadText.text = "NowLoading...";
				count++;
			} else if (count == 4) {
				loadText.text = "NowLoading....";
				count = 0;
			}
        }
        Debug.Log("ロード完了");
        _async.allowSceneActivation = true;

		yield return _async;
    }

	/**
	* シーンの読み込みを行う(遅延読み込み).
	*/
	private void OnLoadedScene( Scene i_scene, LoadSceneMode i_mode )
    {
        // シーンの遷移が完了したら自分用のオブジェクトを生成.
		if( PhotonNetwork.isMasterClient )
        {
			// カスタムプロパティーを追加
			var properties  = new ExitGames.Client.Photon.Hashtable();
			properties.Add( "jsonText", "test" );
			PhotonNetwork.room.SetCustomProperties( properties );
        }
    }

	/**
	* ROOMへ入る.
	*/
	public void OnClickRoomLoadButton() {
		InputField RoomName = GameObject.Find("RoomName").GetComponent<InputField>();

		var val = RoomName.text;
		if (val != null && val != "" ) {
			bool result = PhotonNetwork.JoinRoom(val);
		}
	}

	/**
	* ROOMの作成.
	*/
	public void OnClickRoomAddButton() {
		InputField RoomName = GameObject.Find("RoomName").GetComponent<InputField>();

		if (RoomName != null) {
			var val = RoomName.text;
			if (val != null && val != "" ) {
				//作成する部屋の設定
				RoomOptions roomOptions = new RoomOptions();
				//ロビーで見える部屋にする
				roomOptions.IsVisible = true;
				//他のプレイヤーの入室を許可する
				roomOptions.IsOpen = true;
				//入室可能人数を設定
				roomOptions.MaxPlayers = (byte)10;
				// ルームを作成
				bool result = PhotonNetwork.CreateRoom(val, roomOptions, null);
				// Serverですよフラグ
				m_is_server = true;
			}
		}
	}

	/**
	* Roomリストが更新されるときに呼ばれる.
	*/
	void OnReceivedRoomListUpdate() {
		GameObject dummy = (GameObject)Resources.Load ("Prefab/RoomLabel");
		RectTransform content = GameObject.Find("Canvas/RoomList/Viewport/Content").GetComponent<RectTransform>();
		float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
		float btnHeight = dummy.GetComponent<LayoutElement>().preferredHeight;
		content.sizeDelta = new Vector2(0, (btnHeight + btnSpace)  * 10);

		// 既存のRoomを取得.
		RoomInfo [] roomInfo = PhotonNetwork.GetRoomList();
		if (roomInfo == null || roomInfo.Length == 0) return;
 
 		try
        {
			GameObject[] clones = GameObject.FindGameObjectsWithTag("RoomLabel");
			if (clones != null) {
				foreach (GameObject clone in clones) {
					Destroy(clone);
				}
			}
		} catch (UnityException e) {}
		// 個々のRoomの名前を表示.
		for (int i = 0; i < roomInfo.Length; i++) {
			GameObject perefab = (GameObject)Resources.Load ("Prefab/RoomLabel");
			GameObject newGameObject = Instantiate (perefab, content.sizeDelta, Quaternion.identity);
			newGameObject.transform.SetParent(content, false);
			//ボタンのテキスト変更
			newGameObject.transform.GetComponentInChildren<Text>().text = "    " + roomInfo[i].name;
			string name = roomInfo[i].name;
			//ボタンのクリックイベント登録
			newGameObject.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(name));
		}
	}

	/**
	* 名前の情報をテキストエリアに反映.
	*/
	public void OnClick(string roomName)
	{
		InputField RoomName = GameObject.Find("RoomName").GetComponent<InputField>();
		RoomName.text = roomName;
	}

	// いずれかの「ルーム」に接続した際のコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
		// 少し時間をおいてからシーンを遷移させる.
		StartCoroutine( LoadScene( 0.5f ) );
    }

    // 「ロビー」に接続した際に呼ばれるコールバック
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
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
    }

    // いずれかの「ルーム」への接続に失敗した際のコールバック
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
    }
}

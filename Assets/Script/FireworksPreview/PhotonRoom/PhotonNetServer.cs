using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniJSON;
using System.IO;
using System.Text;

public class PhotonNetServer : Photon.PunBehaviour {
	private string g_JsonStr = "";

	private GameObject gameObject = null;

	/**
	* Use this for initialization.
	*/
	void Start () {
		gameObject = GameObject.Find("dummy");

		//記入したScriptがComponentとして登録されているGameObjectにNKTextManを追加(手動でも可)
		gameObject.AddComponent<NKTextMan> ();

		// 送信ボタンのコールバック追加
		Button SendButton = GameObject.Find("SendButton").GetComponent<Button>();
		SendButton.onClick.AddListener (OnClickSendButton);

		// JSON保存ボタンのコールバック追加
		Button jsonLoadButton = GameObject.Find("jsonLoadButton").GetComponent<Button>();
		jsonLoadButton.onClick.AddListener (OnClickJsonLoadButton);

		// JSON読み込みボタンのコールバック追加
		Button jsonSaveButton = GameObject.Find("jsonSaveButton").GetComponent<Button>();
		jsonSaveButton.onClick.AddListener (OnClickJsonSaveButton);

		// JSON読み込みボタンのコールバック追加
		Button jsonLocalLoadButton = GameObject.Find("jsonLocalLoadButton").GetComponent<Button>();
		jsonLocalLoadButton.onClick.AddListener (OnClickJsonLocalLoadButton);

		GameObject dummy = (GameObject)Resources.Load ("Prefab/FileLabel");
		RectTransform content = GameObject.Find("Canvas/FileList/Viewport/Content").GetComponent<RectTransform>();
		float btnSpace = content.GetComponent<VerticalLayoutGroup>().spacing;
		float btnHeight = dummy.GetComponent<LayoutElement>().preferredHeight;
		content.sizeDelta = new Vector2(0, (btnHeight + btnSpace)  * 10);

		try
        {
			GameObject[] clones = GameObject.FindGameObjectsWithTag("FileLabel");
			if (clones != null) {
				foreach (GameObject clone in clones) {
					Destroy(clone);
				}
			}
		} catch (UnityException e) {}
	}

	/**
	* 名前の情報をテキストエリアに反映.
	*/
	public void OnClick(string roomName)
	{
		InputField RoomName = GameObject.Find("FileNameInputField").GetComponent<InputField>();
		RoomName.text = roomName;
	}
	
	/**
	* JSONデータ読み込みボタン.
	*/
	public void OnClickJsonLoadButton() {
		InputField fileNameInputField = GameObject.Find("FileNameInputField").GetComponent<InputField>();
		NKTextMan textMan = gameObject.GetComponent<NKTextMan> ();

		string strStream = textMan.readText("/Resources/Save/" + fileNameInputField.text);
	}
	
	private IEnumerator LoadText(string textFileName){
		string txtBuffer = "";
		string path = "";
		TextReader txtReader = null;
		string description = "";
		
		Debug.Log( Application.streamingAssetsPath );
		path = Application.dataPath + "/StreamingAssets" + "/" + textFileName;
		Debug.Log("filepath:"+path);

		FileStream file = new FileStream(path,FileMode.Open,FileAccess.Read);
		if (file != null) {
			txtReader = new StreamReader(file);

			if (txtReader != null) {
				yield return new WaitForSeconds(0f);
				description = txtReader.ReadToEnd();

				string[] result = mbStrSplit(description, 1000);
				
				var properties  = new ExitGames.Client.Photon.Hashtable();
				properties.Add( "startSendJson", result.Length );
				PhotonNetwork.room.SetCustomProperties( properties );

				foreach ( var jsonData in result )
				{
					var properties2  = new ExitGames.Client.Photon.Hashtable();
					properties2.Add( "sendJson", jsonData );
					PhotonNetwork.room.SetCustomProperties( properties2 );
				}
				
				var properties3  = new ExitGames.Client.Photon.Hashtable();
				properties3.Add( "endSendJson", "" );
				PhotonNetwork.room.SetCustomProperties( properties3 );
			}
		}

		yield return description;
	}

	/**
	* JSONデータ読み込みボタン.
	*/
	public void OnClickJsonLocalLoadButton() {
		InputField fileNameInputField = GameObject.Find("FileNameInputField").GetComponent<InputField>();

		StartCoroutine( LoadText(fileNameInputField.text) );
	}

	/**
	* JSONデータ保存ボタン.
	*/
	public void OnClickJsonSaveButton() {
		InputField fileNameInputField = GameObject.Find("FileNameInputField").GetComponent<InputField>();
		InputField jsonInput = GameObject.Find("jsonInput").GetComponent<InputField>();
		NKTextMan textMan = gameObject.GetComponent<NKTextMan> ();

		//保存
		if(textMan.saveText("/Resources/Save/" + fileNameInputField.text, jsonInput.text)){
		　　//保存成功
		} else {
		　　//保存失敗
		}
	}
	
	/**
	* JSONデータ送信ボタン.
	*/
	public void OnClickSendButton() {
		// InputField jsonInput = GameObject.Find("jsonInput").GetComponent<InputField>();

		// if (jsonInput != null) {
		// 	string repText = jsonInput.text.Replace("\r\n", "");
		// 	var properties  = new ExitGames.Client.Photon.Hashtable();
		// 	properties.Add( "jsonText", repText );
		// 	PhotonNetwork.room.SetCustomProperties( properties );
		// }
	}

	//*********************************************************************
	/// <summary>文字列を指定した文字数単位で分割する(全角文字考慮)
	/// </summary>
	/// <param name="inStr">  分割前文字列</param>
	/// <param name="length"> 1行の長さ</param>
	/// <returns>             分割後文字列の配列</returns>
	//*********************************************************************
	private string[] mbStrSplit( string inStr, int length ) {
		List<string> outArray = new List<string>(); // 分割結果の保存領域
		string       outStr   = "";                 // 現在処理中の分割後文字列
		Encoding     enc      = Encoding.GetEncoding("UTF-8");
	
		// パラメータチェック
		if ( inStr == null || length < 1 ) {
			return outArray.ToArray();
		}
	
		//--------------------------------------
		// 全ての文字を処理するまで繰り返し
		//--------------------------------------
		for ( int offset = 0; offset < inStr.Length ; offset++ ) {
			//----------------------------------------------------------
			// 今回処理する文字と、その文字を含めた分割後文字列長を取得
			//----------------------------------------------------------
			string curStr         = inStr[offset].ToString();
			int    curTotalLength = enc.GetByteCount( outStr ) + enc.GetByteCount( curStr );
	
			//-------------------------------------
			// この文字が、分割点になるかチェック
			//-------------------------------------
			if ( curTotalLength == length ) {
				// 処理中の文字を含めると、ちょうどピッタリ
				outArray.Add( outStr + curStr );
				outStr = "";
			} else if ( curTotalLength > length ) {
				// 処理中の文字を含めると、あふれる
				outArray.Add( outStr );
				outStr = curStr;
			} else {
				// 処理中の文字を含めてもまだ余裕あり
				outStr += curStr;
			}
		}
	
		// 最後の行の文を追加する
		if ( !outStr.Equals( "" ) ) {
			outArray.Add( outStr );
		}
	
		// 分割後データを配列に変換して返す
		return outArray.ToArray();
	}
}

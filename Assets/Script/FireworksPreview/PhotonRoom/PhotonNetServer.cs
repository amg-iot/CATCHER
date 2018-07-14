using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniJSON;
using System.IO;

public class PhotonNetServer : Photon.PunBehaviour {

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

        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Resources/Save/" );
        FileInfo[] info = dir.GetFiles("*.txt");
		foreach(FileInfo f in info){
			Debug.Log( f.Name );
			GameObject perefab = (GameObject)Resources.Load ("Prefab/FileLabel");
			GameObject newGameObject = Instantiate (perefab, content.sizeDelta, Quaternion.identity);
			newGameObject.transform.SetParent(content, false);
			//ボタンのテキスト変更
			newGameObject.transform.GetComponentInChildren<Text>().text = "    " + f.Name;
			string name = f.Name;
			//ボタンのクリックイベント登録
			newGameObject.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(name));
		}
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

		string repText = strStream.Replace("\r\n", "");
		var properties  = new ExitGames.Client.Photon.Hashtable();
		properties.Add( "jsonText", repText );
		// PhotonNetwork.room.SetCustomProperties( properties );
	}
	
	private IEnumerator LoadText(string textFileName){
		string txtBuffer = "";
		string path = "";
		TextReader txtReader = null;
		string description = "";
		
#if UNITY_EDITOR
		Debug.Log( Application.streamingAssetsPath );
		path = Application.streamingAssetsPath + "/" + textFileName;
		Debug.Log("filepath:"+path);
		FileStream file = new FileStream(path,FileMode.Open,FileAccess.Read);
		txtReader = new StreamReader(file);
		yield return new WaitForSeconds(0f);
#elif UNITY_ANDROID
		path = "jar:file://" + Application.dataPath + "!/assets" + "/" + textFileName;
		WWW www = new WWW(path);
		yield return www;
		txtReader = new StringReader(www.text);
#endif
		// while((txtBuffer = txtReader.ReadLine()) != null){
		// 	description = description + txtBuffer + "\r\n";
		// }
		description = txtReader.ReadToEnd();
		Debug.Log("description:"+description);
		sendClient (description);

		yield return description;
	}

	/**
	* JSONデータ読み込みボタン.
	*/
	public void OnClickJsonLocalLoadButton() {
		InputField fileNameInputField = GameObject.Find("FileNameInputField").GetComponent<InputField>();

		StartCoroutine( LoadText(fileNameInputField.text) );
	}

	private void sendClient(string data) {

		string repText = data.Replace("\r\n", "");
		var properties  = new ExitGames.Client.Photon.Hashtable();
		properties.Add( "jsonText", repText );
		PhotonNetwork.room.SetCustomProperties( properties );
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
}

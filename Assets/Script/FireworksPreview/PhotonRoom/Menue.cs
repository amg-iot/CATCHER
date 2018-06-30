using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menue : MonoBehaviour {
    public string ObjectName;

	// Use this for initialization
	void Start () {

		// 管理者ボタンのコールバック追加
		Button AdminButton = GameObject.Find("AdminButton").GetComponent<Button>();
		AdminButton.onClick.AddListener (OnClickAdminButton);
	}
    public void OnClickAdminButton() {
		// Photon接続設定画面に遷移する
		SceneManager.LoadScene ("photonSetting");
	}
}

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

		// 親子モノづくり体験ボタンのコールバック追加
		Button StartButton = GameObject.Find("StartButton").GetComponent<Button>();
		StartButton.onClick.AddListener (OnClickStartButton);
	}

	public void OnClickStartButton() {
		SceneManager.LoadScene ("photonSetting");
	}

    public void OnClickAdminButton() {
		SceneManager.LoadScene ("photonSetting");
	}
}

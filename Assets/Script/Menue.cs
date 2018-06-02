using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button StartButton = GameObject.Find("StartButton").GetComponent<Button>();
		StartButton.onClick.AddListener (OnClickStartButton);
	}
	
	public void OnClickStartButton() {
		SceneManager.LoadScene ("Fireworks Project");
	}
}

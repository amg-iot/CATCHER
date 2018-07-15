using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/*
 * 
 * Some code is borrowed from [Rx.NET](https://rx.codeplex.com/) and [mono/mcs](https://github.com/mono/mono).
 *
 */
public class View : MonoBehaviour {

	public int m_run_count = 0;

	[SerializeField]
	public delegate void OnSumButtonChildClicked(PointerEventData data); // delegate 型の宣言

	[SerializeField]
	public OnSumButtonChildClicked OnSumButtonClickedListener;

	/**
	* 花火を表示する.
	*/
	public bool ViewFireworks(PersonalData party)
	{
		BulletArr[] bulletArr = party.bulletArr;

		Debug.Log ("bulletLength:" + bulletArr.Length);

		if (bulletArr.Length == 0) {
			/** 球型 （テスト用）*/
			createTestSphereFireworks ();
		}
		// １重の円周型
		else if (bulletArr.Length == 18) {
			createCircumferenceMonoFireworks ();
		} 
		// 2重の円周型・扇の下
		else if (bulletArr.Length == 30) {

			// 扇型の向き取得
			int direction = checkDirectionType2(bulletArr);

			if (direction != 0) {
				// 花火の生成
				newFanningFireworks(direction);
			} else {
				createCircumferenceDoubleFireworks ();
			}
		}
		// 3重の円周型
		else if (bulletArr.Length == 39) {
			createCircumferenceTripleFireworks ();
		}
		// 扇型
		else if (bulletArr.Length == 25) {

			// 扇型の向き取得
			int direction = checkDirectionType(bulletArr);

			// 花火の生成
			newFanningFireworks(direction);
		}
		// 「火」
		else if (bulletArr.Length == 21) {
			createFireworksWithMeshObject ("Prefab/09-KanjiFireSeedObject", 5);

		}
		// ミッキー型
		else if (bulletArr.Length == 26) {
			createMichyOutlineFireworks ();
		}
		// ミッキー型(顔あり)
		else if (bulletArr.Length == 33) {
			createFireworksWithMeshObject ("Prefab/11-MichyOutlineWithFaceSeedObject", 6);

		}
		// ハート形
		else if (bulletArr.Length == 38) {
			createFireworksWithMeshObject ("Prefab/12-HeartSeedObject", 1);
		}
		// 花形2?
		else if (bulletArr.Length == 32) {

		} 
		// 花形3?
		else if (bulletArr.Length == 48) {
			/** 球型 */
			createTestSphereFireworks ();
		}  
		// すごく開くやつ
		else if (bulletArr.Length == 90) {
		}   
		// 半笑い
		else if (bulletArr.Length == 31) {
			createSmaileFaceFireworks ();
		}    
		// アイスクリーム
		else if (bulletArr.Length == 24) {
			createIceCreamFireworks ();
		}     
		// モンスターボール
		else if (bulletArr.Length == 102) {
			createMonstarBallFireworks ();
		}      
		// ミッキー丸３つ
		else if (bulletArr.Length == 3) {
			createMichy3CirlesFireworks ();
		}       
		// 手裏剣
		else if (bulletArr.Length == 48) {

		}        
		// 手裏剣(カラフル)
		else if (bulletArr.Length == 64) {

		}
		// 螺旋型
		else if (bulletArr.Length == 76) {
			createSpiralFireworks ();
			// デモ用
		} else if (bulletArr.Length == 9999) {
			createDemoFireworks ();

		} else {
			Debug.Log ("No fireworks found");
		}

		return true;
	}

	/// <summary>
	/// Creates the test sphere fireworks.
	/// </summary>
	private void createTestSphereFireworks() {
		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/15-DefaultSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));
		}

	}

	/// <summary>
	/// 1重の円周型の花火作成
	/// </summary>
	private void createCircumferenceMonoFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/01-CircumferenceMonoSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild (0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, -90));

		}
	}

	/// <summary>
	/// 2重の円周型の花火作成
	/// </summary>
	private void createCircumferenceDoubleFireworks(){

		Transform player = GameObject.Find ("Player").transform;

		Debug.Log ("02-CircumferenceDoubleSeedObject");

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/02-CircumferenceDoubleSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild (0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, -90));

			// 重力方向に向きを修正
			GameObject childObject2 = newGameObject.transform.GetChild (1).gameObject;
			childObject2.transform.Rotate (new Vector3 (0, -90, -90));

		}
	}

	/// <summary>
	/// 3重の円周型の花火作成
	/// </summary>
	private void createCircumferenceTripleFireworks(){
		Transform player = GameObject.Find ("Player").transform;

		Debug.Log ("03-CircumferenceTripleSeedObject");

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/03-CircumferenceTripleSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild (0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, -90));

			// 重力方向に向きを修正
			GameObject childObject2 = newGameObject.transform.GetChild (1).gameObject;
			childObject2.transform.Rotate (new Vector3 (0, -90, -90));

			// 重力方向に向きを修正
			GameObject childObject3 = newGameObject.transform.GetChild (2).gameObject;
			childObject3.transform.Rotate (new Vector3 (0, -90, -90));
		}

	}

	/// <summary>
	/// 扇型の花火を生成する
	/// </summary>
	/// <param name="direction">扇型の向き(上:0、左:1, 下:2, 右:3)</param>
	private void newFanningFireworks(int direction) {

		Transform player = GameObject.Find ("Player").transform;
		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/04-07-FanningSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;

			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			if (prefab.activeSelf) {
				// 子要素のFireworksObjectの角度を調整
				GameObject childObject = newGameObject.transform.GetChild(0).gameObject;
				childObject.transform.Rotate (new Vector3 (0, 90, 225+(90 * direction) /*向き調整*/));
			}

		}
	}

	/// <summary>
	/// 「火」の花火を作成
	/// </summary>
	private void createKanjiHiFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/09-KanjiFireSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;
			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

		}
	}

	/// <summary>
	/// ミッキー（輪郭のみ）花火作成
	/// </summary>
	private void createMichyOutlineFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/10-MichyOutlineSeedObject");

			pointList.z -= 1800;
			pointList.y += 500;
			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild (0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, -90));

			// 重力方向に向きを修正
			GameObject childObject2 = newGameObject.transform.GetChild (1).gameObject;
			childObject2.transform.Rotate (new Vector3 (0, -90, -90));

			// 重力方向に向きを修正
			GameObject childObject3 = newGameObject.transform.GetChild (2).gameObject;
			childObject3.transform.Rotate (new Vector3 (0, -90, -90));
		}

	}

	/// <summary>
	/// ミッキー（輪郭のみ）花火作成
	/// </summary>
	private void createMichyOutlineWithFaceFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/11-MickyWithFaceSeedObject");

			pointList.y += 350;
			pointList.z -= 1800;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild(0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, 0));

		}
	}

	/// <summary>
	/// 半笑い花火作成
	/// </summary>
	private void createSmaileFaceFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/16-FaceSeedObject");

			pointList.x += 1200;
			pointList.y += 100;
			pointList.z += 0;

			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 140));
		}
	}

	/// <summary>
	/// ミッキー（円３つ）型の花火作成
	/// </summary>
	private void createMichy3CirlesFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/19-MickySeedObject");

			pointList.y += 350;
			pointList.z -= 1800;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild (0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, 0));
		}
	}

	/// <summary>
	/// アイスクリーム型の花火作成
	/// </summary>
	private void createIceCreamFireworks() {
		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/17-IceCreamObject");

			pointList.y += 10;
			pointList.z += 100;

			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));
		}
	}

	/// <summary>
	/// モンスターボール型の花火作成
	/// </summary>
	private void createMonstarBallFireworks() {
		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/18-MonstarBallObject");

			pointList.x += 1200;
			pointList.y += 100;
			pointList.z += 0;

			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 140));
		}
	}

	/// <summary>
	/// 螺旋型の花火作成
	/// Creates the spiral fireworks.
	/// </summary>
	private void createSpiralFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/22-SpiralSeedObject");

			pointList.z -= 1800;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			// 重力方向に向きを修正
			GameObject childObject = newGameObject.transform.GetChild(0).gameObject;
			childObject.transform.Rotate (new Vector3 (0, -90, -90));

		}
	}

	/// <summary>
	/// デモ用の花火作成
	/// Creates the demo fireworks.
	/// </summary>
	private void createDemoFireworks() {

		Transform player = GameObject.Find ("Player").transform;

		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

			//--------------------------------------
			// 球型１作成
			Vector3 pointList = mainPlayer.transform.position;
			GameObject perefab = (GameObject)Resources.Load ("Prefab/99-DemoSeedObject");

			pointList.x -= 1800;
			pointList.z -= 2200;
			pointList.y += 350;
			GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3(-90, 0, 0));

			//--------------------------------------
			// 球型２作成
			Vector3 pointList2 = mainPlayer.transform.position;
			GameObject perefab2 = (GameObject)Resources.Load ("Prefab/99-DemoSeedObject");

			pointList2.x -= 1800;
			pointList2.z -= 1600;
			pointList2.y += 400;
			GameObject newGameObject2 = Instantiate (perefab2, pointList2, Quaternion.identity);
			newGameObject2.transform.Rotate (new Vector3(-90, 0, 0));

		}
	}

	/// <summary>
	/// メッシュで作成された花火を実体化する
	/// </summary>
	private void createFireworksWithMeshObject(string prehabPath, int childObjectNum) {

		Transform player = GameObject.Find ("Player").transform;
		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;
			Vector3 pointList = mainPlayer.transform.position;
			GameObject prefab = (GameObject)Resources.Load (prehabPath);
			pointList.z -= 1800;
			pointList.y += 500;
			GameObject seedObject = Instantiate (prefab, pointList, Quaternion.identity);
			seedObject.transform.Rotate (new Vector3 (-90, 0, 0));
			for (int i=0; i<childObjectNum; i++) {
				// 重力方向に向きを修正
				GameObject childObject = seedObject.transform.GetChild (i).gameObject;
				childObject.transform.Rotate (new Vector3 (0, -90, -90));
			}
		}
	}


	/// <summary>
	/// 扇の取得慮タイプ判定
	/// Creates the demo fireworks.
	/// </summary>
	public int checkDirectionType(BulletArr[] bulletArrList) {
		int result = 0;

		int checkNum = 0;
		foreach(BulletArr bulletArr in bulletArrList){
			checkNum += (int) bulletArr.degree;
		}

		if (checkNum >= 6750) {
			// 上向き
			result = 0;
		} else if (checkNum < 6750 && checkNum >= 4500) {
			// 左向き
			result = 1;
		} else {
			// 右向き
			result = 3;
		}

		return result;
	}

	/// <summary>
	/// 扇の取得慮タイプ判定(下向き扇)
	/// Creates the demo fireworks.
	/// </summary>
	public int checkDirectionType2(BulletArr[] bulletArrList) {
		int result = 0;

		int checkNum = 0;
		foreach(BulletArr bulletArr in bulletArrList){
			checkNum += (int) bulletArr.degree;
		}

		if (checkNum >= 2710) {
			// 二重丸
			result = 0;
		} else {
			// した向き
			result = 2;
		}

		return result;
	}
}

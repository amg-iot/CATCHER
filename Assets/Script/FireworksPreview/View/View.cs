using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

	[SerializeField]
	public List<GameObject> g_DeletePrefab = new List<GameObject>();

	// 花火の打ち上げ地点の数
	private const int LAUNCH_POINT_NUM = 3;

	/**
	* 花火を表示する.
	*/
	public bool ViewFireworks(PersonalData party)
	{

		BulletArr[] bulletArr = party.bulletArr;

		if (bulletArr.Length == 0) {
			/** 球型 （テスト用）*/
			createFireworksWithMeshObject ("Prefab/15-DefaultSeedObject", 1);
		}
		// １重の円周型
		else if (bulletArr.Length == 18) {
			createFireworksWithMeshObject ("Prefab/01-CircumferenceMonoSeedObject", 1);
		} 
		// 2重の円周型・扇の下
		else if (bulletArr.Length == 30) {

			// 扇型の向き取得
			int direction = checkDirectionType2(bulletArr);

			if (direction != 0) {
				// 花火の生成
				newFanningFireworks(direction);
			} else {
				createFireworksWithMeshObject ("Prefab/02-CircumferenceDoubleSeedObject", 2);
			}
		}
		// 3重の円周型
		else if (bulletArr.Length == 39) {
			createFireworksWithMeshObject ("Prefab/03-CircumferenceTripleSeedObject", 3);
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
			createFireworksWithMeshObject ("Prefab/10-MichyOutlineSeedObject", 3);

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
			createFireworksWithMeshObject ("Prefab/15-DefaultSeedObject", 1);
		}  
		// すごく開くやつ
		else if (bulletArr.Length == 90) {
		}   
		// 半笑い
		else if (bulletArr.Length == 31) {
			createFireworksWithMeshObject ("Prefab/16-FaceSeedObject", 4);

		}    
		// アイスクリーム
		else if (bulletArr.Length == 24) {
			createFireworksWithMeshObject ("Prefab/17-IceCreamObject", 3);

		}     
		// モンスターボール
		else if (bulletArr.Length == 102) {
			createFireworksWithMeshObject ("Prefab/18-MonstarBallObject", 4);
		}      
		// ミッキー丸３つ
		else if (bulletArr.Length == 3) {
			createFireworksWithMeshObject ("Prefab/19-MickySeedObject", 1);

		}       
		// 手裏剣
		else if (bulletArr.Length == 48) {

		}        
		// 手裏剣(カラフル)
		else if (bulletArr.Length == 64) {

		}
		// 螺旋型
		else if (bulletArr.Length == 76) {
			createFireworksWithMeshObject ("Prefab/22-SpiralSeedObject", 1);

			// デモ用
		} else if (bulletArr.Length == 9999) {
			createDemoFireworks ();

		} else {
			Debug.Log ("No fireworks found");
		}

		return true;
	}
		
	/// <summary>
	/// 扇型の花火を生成する
	/// </summary>
	/// <param name="direction">扇型の向き(上:0、左:1, 下:2, 右:3)</param>
	private void newFanningFireworks(int direction) {

		Transform player = GameObject.Find ("Player").transform;
		if (player != null) {
			GameObject mainPlayer = player.Find ("MainPlayer").gameObject;
			GameObject prefab = (GameObject)Resources.Load ("Prefab/04-07-FanningSeedObject");

			// 花火の打ち上げ地点を設定
			Vector3 pointList = calcLaunchPoint(mainPlayer);

			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));
			g_DeletePrefab.Add(newGameObject);

			if (prefab.activeSelf) {
				// 子要素のFireworksObjectの角度を調整
				GameObject childObject = newGameObject.transform.GetChild(0).gameObject;
				childObject.transform.Rotate (new Vector3 (0, 90, 225+(90 * direction) /*向き調整*/));
			}

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
			GameObject prefab = (GameObject)Resources.Load (prehabPath);

			// 花火の打ち上げ地点を設定
			Vector3 pointList = calcLaunchPoint(mainPlayer);

			// 親オブジェクトの向き修正
			GameObject seedObject = Instantiate (prefab, pointList, Quaternion.identity);
			seedObject.transform.Rotate (new Vector3 (-90, 0, 0));
			g_DeletePrefab.Add(seedObject);

			// 子オブジェクトの向きを修正
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

	/// <summary>
	/// 打ち上げ地点の座標を計算する
	/// </summary>
	/// <returns>The launch point.</returns>
	private Vector3 calcLaunchPoint(GameObject player) {

		// 座標の初期値を取得
		Vector3 pointList = player.transform.position;

		// 花火の打ち上げ地点を設定
		System.Random rnd = new System.Random();
		int r = rnd.Next(LAUNCH_POINT_NUM);
		pointList.z -= 700 + 2000 * r;
		pointList.y += 1200;

		return pointList;
	}

}

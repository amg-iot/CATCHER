using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


public class View : MonoBehaviour {
	
	[SerializeField]
	public delegate void OnSumButtonChildClicked(PointerEventData data); // delegate 型の宣言
	[SerializeField]
	// 「加算」ボタンが押された時のイベントリスナー
	public OnSumButtonChildClicked OnSumButtonClickedListener;


	// 花火を表示する
	public void ViewFireworks(int type)
	{
		Debug.Log ("type = " + type);

		if (type == 0) {
			/** 球型 （テスト用）*/
			Transform player = GameObject.Find ("Player").transform;

			if (player != null) {
				GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

				Vector3 pointList = mainPlayer.transform.position;
				GameObject perefab = (GameObject)Resources.Load ("Prefab/00-DefaultSeedObject");

//				pointList.y += 10;
				pointList.z -= 1800;
				pointList.y += 500;
				GameObject newGameObject = Instantiate (perefab, pointList, Quaternion.identity);
				newGameObject.transform.Rotate (new Vector3 (0, 0, 0));
			}
		} else if (type == 1) {
			/** １重の円周型 */
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
		} else if (type == 2) {
			/** 2重の円周型 */
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

		} else if (4 <= type && type <= 7) {
			/* 扇型 */

			// 扇型の向き
			int direction = 0;

			switch (type) {
			case 4: // 上
				direction = 0;
				break;
			case 5: // 左
				direction = 1;
				break;
			case 6: // 右
				direction = 3;
				break;
			case 7: // 下
				direction = 2;
				break;

			}

			// 花火の生成
			newFanningFireworks(direction);

		} else if (type == 9) {
			/* 「火」 */
			// TODO: ※実装中　particleのsub emittingの座標変えられるのか？

			Transform player = GameObject.Find ("Player").transform;

			if (player != null) {
				GameObject mainPlayer = player.Find ("MainPlayer").gameObject;

				Vector3 pointList = mainPlayer.transform.position;
				GameObject prefab = (GameObject)Resources.Load ("Prefab/09-KanjiHiSeedObject");

				pointList.y += 10;
				pointList.z += 150;
				GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
				newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

				if (prefab.activeSelf) {
					// 子要素のFireworksObjectの角度を調整
					GameObject childObject0 = newGameObject.transform.GetChild(0).gameObject;
					childObject0.transform.Rotate (new Vector3 (0, 0, 0));
					Vector3 pos0 = childObject0.transform.position;
					pos0.x += 150;
					pos0.y += 150;
					pos0.z += 150;
					childObject0.transform.position = new Vector3 (pos0.x+100, pos0.y+100, pos0.z+100);

					GameObject childObject1 = newGameObject.transform.GetChild(1).gameObject;
					childObject1.transform.Rotate (new Vector3 (-30, 0, 0));

					GameObject childObject2 = newGameObject.transform.GetChild(2).gameObject;
					childObject2.transform.Rotate (new Vector3 (30, 0, 0));

				}

			}

		} else if (type == 19) {
			/* ミッキー型 */

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
				GameObject childObject = newGameObject.transform.GetChild(0).gameObject;
				childObject.transform.Rotate (new Vector3 (0, -90, 0));

			}

		}else if (type == 22) {
			/* 螺旋型 */
		
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

		} else if (type == 99) {
			/* デモ画像撮影用 */
		
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

			pointList.y += 10;
			pointList.z += 100;

			GameObject newGameObject = Instantiate (prefab, pointList, Quaternion.identity);
			newGameObject.transform.Rotate (new Vector3 (-90, 0, 0));

			if (prefab.activeSelf) {
				// 子要素のFireworksObjectの角度を調整
				GameObject childObject = newGameObject.transform.GetChild(0).gameObject;
				childObject.transform.Rotate (new Vector3 (-90, 0, 225+(90 * direction) /*向き調整*/));
			}
	
		}
	}
		
	// 「＋」ボタンが押された時に呼ばれるメソッド
/*	public void OnSumButtonClicked(PointerEventData data)
	{
		if (OnSumButtonClickedListener != null)
		{
			OnSumButtonClickedListener(data);
		}
	}
	*/
}

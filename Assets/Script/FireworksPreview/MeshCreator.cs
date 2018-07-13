using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// meshをプログラムから生成
/// 参考URL https://qiita.com/divideby_zero/items/eef7604e306300fd7833
/// </summary>
public class MeshCreator : MonoBehaviour
{
	// 花火の形
	public int type = 0;

	private const int MESH_TYPE_CIRCLE_BIG = 1;
	private const int MESH_TYPE_CIRCLE_MEDIUM = 2;
	private const int MESH_TYPE_CIRCLE_SMALL = 3;
	private const int MESH_TYPE_MICHY = 25;
	private const int MESH_TYPE_SPIRAL = 22;

	[SerializeField]
	private MeshFilter meshFilter;

	private Mesh mesh;
	private List<Vector3> vertextList = new List<Vector3>();
	//private List<Vector2> uvList = new List<Vector2>(); テクスチャをMeshに貼るときに必要になるUV座標
	private List<int> indexList = new List<int>();

	void Start ()
	{
		mesh = CreateMeshByType(type);
		meshFilter.mesh = mesh;
	}

	/// <summary>
	/// 花火の形パターンと対応するMeshを作成
	/// </summary>
	/// <returns>The mesh by pattern.</returns>
	/// <param name="pattern">pattern 花火の形(type)とは一致しないので注意！</param>
	private Mesh CreateMeshByType(int pattern) {

		if (pattern == MESH_TYPE_CIRCLE_BIG) {
			/* １重の円周（大） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 100, new CirclePoints ());
		
		} else if (pattern == MESH_TYPE_CIRCLE_MEDIUM) {
			/* 1重の円周　(中) */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 75, new CirclePoints ());			
			 
		} else if (pattern == MESH_TYPE_CIRCLE_SMALL) {
			/* 1重の円周　(小) */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 50, new CirclePoints ());
		
		} else if (pattern >= 8 && pattern <= 19) {
			/* 放射状（中心は空洞） */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 100, 0), 
				new Vector3 ( 2, 100, 0), 
				new Vector3 ( 2,  40, 0), 
				new Vector3 (-2,  40, 0),
				30*(pattern - 8)
			);
	
		} else if (pattern == 20) {
			/* 「火」の真ん中の棒 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 40, 0), 
				new Vector3 ( 2, 40, 0), 
				new Vector3 ( 2,  0, 0), 
				new Vector3 (-2,  0, 0),
				0
			);

		} else if (pattern == 21) {
			/* 「火」の左払い */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 40, 0), 
				new Vector3 ( 2, 40, 0), 
				new Vector3 ( 2,  0, 0), 
				new Vector3 (-2,  0, 0),
				-135
			);
		
		} else if (pattern == 22) {
			/* 「火」の右払い */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 40, 0), 
				new Vector3 ( 2, 40, 0), 
				new Vector3 ( 2,  0, 0), 
				new Vector3 (-2,  0, 0),
				135
			);
		
		} else if (pattern == 23) {
			/* 「火」の左の点 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 35, 0), 
				new Vector3 ( 2, 35, 0), 
				new Vector3 ( 2, 30, 0), 
				new Vector3 (-2, 30, 0),
				-45
			);

		} else if (pattern == 24) {
			/* 「火」の右の点 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 35, 0), 
				new Vector3 ( 2, 35, 0), 
				new Vector3 ( 2, 30, 0), 
				new Vector3 (-2, 30, 0),
				45
			);

		} else if (pattern == MESH_TYPE_MICHY) {
			/* ミッキー型 */
			return CreateMickyMesh ();
		
		} else if (pattern == 26) {
			/* ミッキー(顔あり)型 左目*/
			return  CreatePlaneMeshWithRotation (
				new Vector3 (-2, 35, 0), 
				new Vector3 ( 2, 35, 0), 
				new Vector3 ( 2, 30, 0), 
				new Vector3 (-2, 30, 0),
				-45
			);

		} else if (pattern == 27) {
			/* ミッキー(顔あり)型 右目*/
			return  CreatePlaneMeshWithRotation (
				new Vector3 (-2, 35, 0), 
				new Vector3 ( 2, 35, 0), 
				new Vector3 ( 2, 30, 0), 
				new Vector3 (-2, 30, 0),
				45
			);

		} else if (pattern == 28) {
			/* ミッキー(顔あり)型 口*/
			return  CreatePlaneMeshWithRotation (
				new Vector3 (-2, 40, 0), 
				new Vector3 ( 2, 40, 0), 
				new Vector3 ( 2, 30, 0), 
				new Vector3 (-2, 30, 0),
				180
			);

		} else if (pattern == 29) {
			/* ミッキー（輪郭のみ）左耳　*/
			return CreateCircumferenceMesh (new Vector3 (-87, 87, 0), 50, new CirclePoints ());

		} else if (pattern == 30) {
			/* ミッキー（輪郭のみ）右耳　*/
			return CreateCircumferenceMesh (new Vector3 (-87, -87, 0), 50, new CirclePoints ());

		} else if (pattern == 31) {
			/* ミッキー（輪郭のみ）頭　*/
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 80, new CirclePoints ());

		} else if (pattern == 32) {
			/* ミッキー（輪郭のみ）左目 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-2, 35, 0), 
				new Vector3 ( 2, 35, 0), 
				new Vector3 ( 2, 30, 0), 
				new Vector3 (-2, 30, 0),
				-45
			);

		} else if (pattern == 160) {
			/* 顔：１重の円周（小） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 10, new CirclePoints ());

		} else if (pattern == 161) {
			/* 顔：１重の円周（小） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 10, new CirclePoints ());

		} else if (pattern == 162) {
			/* 顔：１重の円周（大） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 100, new CirclePoints ());

		} else if (pattern == 163) {
			/* 顔：口の線 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-1, 180, 90), 
				new Vector3 ( 1, 180, 90), 
				new Vector3 ( 1,  0, 0), 
				new Vector3 (-1,  0, 0),
				40
			);

		} else if (pattern == 170) {
			/* アイス：１重の円周（小） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 50, new CirclePoints ());

		} else if (pattern == 171) {
			/* アイス：左の線 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-1, 120, 0), 
				new Vector3 ( 1, 120, 0), 
				new Vector3 ( 1,  0, 0), 
				new Vector3 (-1,  0, 0),
				40
			);

		} else if (pattern == 172) {
			/* アイス：右の線 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-1, 90, 0), 
				new Vector3 ( 1, 90, 0), 
				new Vector3 ( 1,  0, 0), 
				new Vector3 (-1,  0, 0),
				40
			);

		} else if (pattern == 180) {
			/* モンスターボール：１重の円周（小） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 35, new CirclePoints ());

		} else if (pattern == 181) {
			/* モンスターボール：１重の円周（大） */
			return CreateCircumferenceMesh (new Vector3 (0, 0, 0), 100, new CirclePoints ());

		} else if (pattern == 182) {
			/* モンスターボール：真ん中の線 */
			return CreatePlaneMeshWithRotation (
				new Vector3 (-1, 180, 0), 
				new Vector3 ( 1, 180, 0), 
				new Vector3 ( 1,  0, 0), 
				new Vector3 (-1,  0, 0),
				40
			);

		} else if (pattern == MESH_TYPE_SPIRAL) {
			/* 螺旋型 */
			return CreateSpiralMesh(new Vector3(0, 0, 0));

		}

		return new Mesh ();
	}
			
	/// <summary>
	/// 四角形のメッシュを作成
	/// </summary>
	/// <returns>The plane mesh.</returns>
	private Mesh CreatePlaneMeshWithRotation(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, int angle)
	{
		var mesh = new Mesh();

		var meshSize = 100;

		var radian = Math.PI * angle / 180.0;

		// 座標に回転を加える
		vertextList.Add(rotateVector3(point0, angle)); //0番頂点
		vertextList.Add(rotateVector3(point1, angle)); //1番頂点
		vertextList.Add(rotateVector3(point2, angle)); //2番頂点
		vertextList.Add(rotateVector3(point3, angle)); //3番頂点

		indexList.AddRange(new []{
			0,2,1,
			1,2,3
		}); //0-2-1の頂点で1三角形。 1-2-3の頂点で1三角形。

		mesh.RecalculateNormals (); // 法線方向を(0,0,1) 固定にしない
		mesh.SetVertices(vertextList); //meshに頂点群をセット
		mesh.SetIndices(indexList.ToArray(),MeshTopology.Triangles, 0); //メッシュにどの頂点の順番で面を作るかセット
		return mesh;
	}

	/// <summary>
	/// 直角二等辺三角形のメッシュを作成
	/// </summary>
	/// <returns>The triangle mesh.</returns>
	private Mesh CreateTriangleMesh(Vector3 point0, Vector3 point1, Vector3 point2)
	{
		var mesh = new Mesh();

		var meshSize = 100;

		vertextList.Add(point0); //0番頂点
		vertextList.Add(point1); //1番頂点
		vertextList.Add(point2); //2番頂点

		indexList.AddRange(new []{
			1,2,3
		});//0-2-1の頂点で1三角形。 1-2-3の頂点で1三角形。

		mesh.RecalculateNormals (); // 法線方向を(0,0,1) 固定にしない
		mesh.SetVertices(vertextList); //meshに頂点群をセット
		mesh.SetIndices(indexList.ToArray(),MeshTopology.Triangles, 0); //メッシュにどの頂点の順番で面を作るかセット
		return mesh;
	}

	/// <summary>
	/// 螺旋型のMeshを作成
	/// </summary>
	/// <returns>The spiral mesh.</returns>
	private Mesh CreateSpiralMesh(Vector3 theOrigin)
	{
		var mesh = new Mesh();
		int spiral_num = 3;
		int density = 2;

		// 対数螺旋の座標算出
		for (int i=0 ; i < 360 * spiral_num * density; i++) {

			var theta = i * (Math.PI / 180) / density;
			var r = i * 100 / (360 * spiral_num * density);
			var x = r * Math.Cos(theta);
			var y = r * Math.Sin(theta);

			vertextList.Add(new Vector3((float)x + theOrigin.x, (float)y + theOrigin.y,  + theOrigin.z));

		}
			
		// 三角形の座標の組み合わせを設定
		for (int i=0; i < 360 * spiral_num * density; i++) {
			indexList.Add(i);
		}
			
		mesh.RecalculateNormals (); // 法線方向を(0,0,1) 固定にしない
		mesh.SetVertices(vertextList);//meshに頂点群をセット
		mesh.SetIndices(indexList.ToArray(),MeshTopology.Triangles, 0); //メッシュにどの頂点の順番で面を作るかセット
		return mesh;
	}

	/// <summary>
	/// ミッキー型の花火用Meshを作成
	/// </summary>
	/// <returns>The micky mesh.</returns>
	private Mesh CreateMickyMesh() {

		Mesh mesh = new Mesh ();

		// 構造体の初期化
		CirclePoints cpoints = new CirclePoints();
		cpoints.vertextList = new List<Vector3>();
		cpoints.indexList = new List<int>();

		// 頭部
		cpoints = CreateCirclePoints(new Vector3(0, 0, 0), 100, cpoints);

		// 左耳
		cpoints = CreateCirclePoints (new Vector3 (100, 100, 0), 70, cpoints);

		// 右耳
		cpoints = CreateCirclePoints (new Vector3 (-100, 100, 0), 70, cpoints);

		// Meshの作成
		mesh.RecalculateNormals (); // 法線方向を(0,0,1) 固定にしない
		mesh.SetVertices(cpoints.vertextList);//meshに頂点群をセット
		mesh.SetIndices(cpoints.indexList.ToArray(), MeshTopology.Triangles, 0); //メッシュにどの頂点の順番で面を作るかセット

		return mesh;

	}
			
	/// <summary>
	/// 円を近似する三角形の頂点の座標と、組み合わせを返す
	/// </summary>
	/// 計算方法は、ピザ（三角形）を一切れずつ集めて一枚の丸いピザにするイメージ
	/// <returns>The circle.</returns>
	/// <param name="theOrigin">The origin.</param>
	/// <param name="CircleSize">Circle size.</param>
	/// <param name="cpoints">Cpoints.</param>
	private CirclePoints CreateCirclePoints(Vector3 theOrigin, int CircleSize, CirclePoints cpoints)
	{

		var unit_angle = 5;
		var triangle_num = 360 / unit_angle;

		// 原点
		cpoints.vertextList.Add(theOrigin);

		// 近似のため、円上の座標を取得
		for (int i=0; i < triangle_num; i++) {
			var sin = Math.Sin(unit_angle * i * (Math.PI / 180));
			var cos = Math.Cos(unit_angle * i * (Math.PI / 180));
			cpoints.vertextList.Add(
				new Vector3((float)cos*CircleSize+theOrigin.x, (float)sin*CircleSize+theOrigin.y, theOrigin.z));

		}

		// 原点の番号を算出
		int initTriangleNumSize = cpoints.indexList.Count / 3 + cpoints.indexList.Count / 3 / triangle_num;

		// 座標と三角形の頂点を対応させる
		for (int i=0; i < triangle_num; i++) {

			// 第０頂点（原点）
			cpoints.indexList.Add(initTriangleNumSize);

			// 第1頂点
			cpoints.indexList.Add(i+1 + initTriangleNumSize);

			// 第2頂点
			if (i*3 + 2 < triangle_num * 3-1) {
				cpoints.indexList.Add(i+2 + initTriangleNumSize);
			} else {
				// 最後の三角形は最初の頂点に戻る
				cpoints.indexList.Add(1 + initTriangleNumSize);
			}

		}
			
		return cpoints;
	}

	/// <summary>
	/// 円周形のMeshを返す
	/// </summary>
	/// <returns>The circumference mesh.</returns>
	private Mesh CreateCircumferenceMesh(Vector3 theOrigin, int CircleSize, CirclePoints cpoints) {

		Mesh mesh = new Mesh ();

		// 構造体の初期化
		cpoints.vertextList = new List<Vector3>();
		cpoints.indexList = new List<int>();

		cpoints = CreateCircumferencePoints(theOrigin, CircleSize, cpoints);

		// Meshの作成
		mesh.RecalculateNormals (); // 法線方向を(0,0,1) 固定にしない
		mesh.SetVertices(cpoints.vertextList);//meshに頂点群をセット
		mesh.SetIndices(cpoints.indexList.ToArray(), MeshTopology.Triangles, 0); //メッシュにどの頂点の順番で面を作るかセット

		return mesh;

	}

	/// <summary>
	/// 円周Mesh作成用の座標を返す
	/// </summary>
	/// <returns>The circumference points.</returns>
	/// <param name="theOrigin">The origin.</param>
	/// <param name="CircleSize">Circle size.</param>
	/// <param name="cpoints">Cpoints.</param>
	private CirclePoints CreateCircumferencePoints(Vector3 theOrigin, int CircleSize, CirclePoints cpoints)
	{

		var unit_angle = 1;
		var triangle_num = 360 / unit_angle;

		// 原点
		//cpoints.vertextList.Add(theOrigin);

		// 近似のため、円上の座標を取得
		for (int i=0; i < triangle_num; i++) {
			var sin = Math.Sin(unit_angle * i * (Math.PI / 180));
			var cos = Math.Cos(unit_angle * i * (Math.PI / 180));
			cpoints.vertextList.Add(
				new Vector3((float)cos*CircleSize+theOrigin.x, (float)sin*CircleSize+theOrigin.y, theOrigin.z));

		}

		// 原点の番号を算出
		int initTriangleNumSize = cpoints.indexList.Count / 3 + cpoints.indexList.Count / 3 / triangle_num;

		// 座標と三角形の頂点を対応させる
		for (int i=0; i < triangle_num; i++) {
			cpoints.indexList.Add(i);
		}

		return cpoints;
	}

	/// <summary>
	/// 回転を加えた点の座標を返す
	/// </summary>
	/// <returns>The vector3.</returns>
	private Vector3 rotateVector3(Vector3 v3, int angle) {

		float x1 = v3.x;
		float y1 = v3.y;

		double radian = Math.PI * angle / 180;

		double x2 = x1 * Math.Cos(radian) - y1 * Math.Sin(radian);
		double y2 = x1 * Math.Sin(radian) + y1 * Math.Cos(radian);

		v3.x = (float) x2;
		v3.y = (float) y2;

		return v3;

	}

}

/// <summary>
/// 円を近似する三角形の頂点をあらわすクラス
/// </summary>
class CirclePoints
{
	// 各三角形の頂点の座標
	public List<Vector3> vertextList;

	// 各三角形の頂点の組み合わせ
	public List<int> indexList;

	public override string ToString()
	{
		return "(" + vertextList + ", " + indexList + ")";
	}
}
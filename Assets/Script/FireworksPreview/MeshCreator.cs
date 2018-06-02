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
	/// <returns>The mesh by type.</returns>
	/// <param name="type">Type.</param>
	private Mesh CreateMeshByType(int type) {

		if (type == 1) {
			/* １重の円周 */
			return CreateCircumferenceMesh();
			 
		} else if (type == 19) {
			/* ミッキー型 */
			return CreateMickyMesh ();
		} else if (type == 22) {
			/* 螺旋型 */
			return CreateSpiralMesh(new Vector3(0,  300, 0));
		}

		return new Mesh ();
	}
			
	/// <summary>
	/// 四角形のメッシュを作成
	/// </summary>
	/// <returns>The plane mesh.</returns>
	private Mesh CreatePlaneMesh()
	{
		var mesh = new Mesh();

		var meshSize = 100;

		vertextList.Add(new Vector3(-meshSize, -meshSize, 0)); //0番頂点
		vertextList.Add(new Vector3( meshSize, -meshSize, 0)); //1番頂点
		vertextList.Add(new Vector3(-meshSize,  meshSize, 0)); //2番頂点
		vertextList.Add(new Vector3( meshSize,  meshSize, 0)); //3番頂点

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
	private Mesh CreateTriangleMesh()
	{
		var mesh = new Mesh();

		var meshSize = 100;

		vertextList.Add(new Vector3(-meshSize, -meshSize, 0)); //0番頂点
		vertextList.Add(new Vector3( meshSize, -meshSize, 0)); //1番頂点
		vertextList.Add(new Vector3(-meshSize,  meshSize, 0)); //2番頂点

		indexList.AddRange(new []{
			0,2,1
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
	private Mesh CreateCircumferenceMesh() {

		Mesh mesh = new Mesh ();

		// 構造体の初期化
		CirclePoints cpoints = new CirclePoints();
		cpoints.vertextList = new List<Vector3>();
		cpoints.indexList = new List<int>();

		cpoints = CreateCircumferencePoints(new Vector3(0, 0, 0), 100, cpoints);

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
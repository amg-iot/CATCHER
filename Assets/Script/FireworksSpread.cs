using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksSpread : MonoBehaviour {

	public Vector3 defaultScale = Vector3.zero;
	private ParticleSystem parent_particle;
	private float lifetime=0;
	private float velo_size = 0.1f;

	// Use this for initialization
	void Start () {
		defaultScale = transform.lossyScale;

		Vector3 localScale = transform.localScale;
		transform.localScale = new Vector3(
			localScale.x / 10,
			localScale.y / 10,
			localScale.z / 10
		);

		parent_particle = this.transform.parent.GetComponent<ParticleSystem> ();

	}

	// Update is called once per frame
	void Update () {
		Vector3 lossScale = transform.lossyScale;
		Vector3 localScale = transform.localScale;

		if (this.gameObject != null && this.gameObject.activeSelf && !parent_particle.isPlaying) {

			lifetime += (float) 0.1;

			//『速度の計算』
			// 　衝撃波は時間tの(t^-3/5)という関数で表されるらしいが、
			//   c#のMath.Powでは計算できないので、1/tとして近似する。
			float velocity = (float) velo_size / lifetime; //(Mathf.Pow (i, -3/5));

			this.transform.localScale += new Vector3 (velocity,velocity,velocity/20);//指定数だけ拡大させる

			this.gameObject.transform.parent.Translate (0f, 0f, 0f);//-velocity * 750f);
		}
	}
}

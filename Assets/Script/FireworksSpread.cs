using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksSpread : MonoBehaviour {

	public Vector3 defaultScale = Vector3.zero;
	private ParticleSystem parent_particle;
	private Rigidbody _rigidbody;
	private int delta = 0;
	// Use this for initialization
	void Start () {
		defaultScale = transform.lossyScale;

		Vector3 localScale = transform.localScale;
		transform.localScale = new Vector3(
			localScale.x / 10,
			localScale.y / 10,
			localScale.z / 10
		);

		_rigidbody = this.GetComponent<Rigidbody> ();
		parent_particle = this.transform.parent.GetComponent<ParticleSystem> ();

	}

	// Update is called once per frame
	void Update () {
		Vector3 lossScale = transform.lossyScale;
		Vector3 localScale = transform.localScale;
					
		if (this.gameObject != null && this.gameObject.activeSelf && !parent_particle.isPlaying) {
						
			delta += 1;
			float velocity = 0.3f;

			if (localScale.x >= 4 || localScale.y >= 4) {
				velocity = 0.03f;
			}


			this.transform.localScale += new Vector3 (velocity,velocity,velocity/20);//指定数だけ拡大させる

			this.gameObject.transform.parent.Translate (0f, 0f, 0f);//-velocity * 750f);

			Debug.Log ("x:" + localScale.x + "y:" + localScale.y + "z:" + localScale.z);
//				Debug.Log ("parent_y:" + this.gameObject.transform.parent.gameObject.transform.position.y);
			}
//		}

			
//		transform.localScale = new Vector3(
//			localScale.x / lossScale.x * defaultScale.x + 1f,
//			localScale.y / lossScale.y * defaultScale.y + 1f,
//			localScale.z / lossScale.z * defaultScale.z + 1f
//		);
//		transform.localScale = new Vector3(
//			localScale.x + 1f,
//			localScale.y + 1f,
//			localScale.z + 1f
//		);

	}
}

using UnityEngine;
using System.Collections;

public class Decay : MonoBehaviour {

	public float decayTime = 1f;
	public bool isThrust = false;
	public Transform landEffect;

	private bool hit = false;

	// Use this for initialization
	void Start () {
		Destroy(gameObject,1);
	}

	void Update() {
		if (isThrust)
			rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime*2);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Planet" && isThrust && !hit) {
			hit = true;
			Transform land = (Transform)Instantiate(landEffect, new Vector3(transform.position.x,transform.position.y,transform.position.z-10), Quaternion.identity);
			land.parent = transform;
		}
	}
}

using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	public bool inBounds = false;
	private bool start = true;

	// Use this for initialization
	void Start () {
		int modifier = 0, randomizer = Random.Range (0,10);
		if (randomizer < 5)
			modifier = Random.Range(-2,12);
		if (randomizer > 5 && randomizer < 8)
			modifier = Random.Range(2,16);
		if (randomizer > 8)
			modifier = Random.Range(8,22);
		transform.localScale = new Vector3(transform.localScale.x+modifier,transform.localScale.y+modifier,transform.localScale.z);
		transform.rotation = Quaternion.Euler(0f,0f,Random.Range(0,360));
		StartCoroutine(VerifyInBounds(0.2f));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Border")
			inBounds = true;
		if (other.tag == "Atmosphere")
			Destroy (other.gameObject.transform.parent.gameObject);
		if (other.tag == "Player" && start)
			Destroy (gameObject);
	}

	IEnumerator VerifyInBounds(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		if (!inBounds)
			Destroy (gameObject);
		start = false;
		rigidbody2D.isKinematic = true;
	}
}

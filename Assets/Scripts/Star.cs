using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Star")
			Destroy (other.gameObject.transform.parent.gameObject);
	}
}

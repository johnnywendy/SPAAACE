using UnityEngine;
using System.Collections;

public class RotateTarget : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
			transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
			transform.rotation = Quaternion.Euler (Vector3.Lerp (transform.rotation.eulerAngles, target.rotation.eulerAngles, Time.deltaTime * 3f));
	}

	public void setInitialRotation() {
		transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").transform.rotation;
	}
}

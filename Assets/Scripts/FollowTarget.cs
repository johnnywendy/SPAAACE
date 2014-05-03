using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;
	public float Xoffset;
	public float Yoffset;

	void Start() {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Creatures"), true);
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x+Xoffset,target.position.y+Yoffset,transform.position.z);
	}


}

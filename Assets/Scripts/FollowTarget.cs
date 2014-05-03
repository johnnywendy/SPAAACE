using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;
	public float Xoffset;
	public float Yoffset;

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x+Xoffset,target.position.y+Yoffset,transform.position.z);
	}


}

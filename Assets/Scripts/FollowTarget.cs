using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	public Transform target;
	public bool relativeToScreen;
	public float Xoffset; // (-14, 6.8) (-14,7.4)
	public float Yoffset;

	void Start() {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Creatures"), true);
	}

	// Update is called once per frame
	void Update () {
		if (!relativeToScreen)
			transform.position = new Vector3(target.position.x+Xoffset,target.position.y+Yoffset,transform.position.z);
		if (relativeToScreen) {
			Vector2 dimensions = CalculateScreenSizeInWorldCoords();
			transform.position = new Vector3(target.position.x-(dimensions.x/2)+Xoffset,target.position.y+(dimensions.y/2)+Yoffset,transform.position.z);
		}
	}

	Vector2 CalculateScreenSizeInWorldCoords() {
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0,0,Camera.main.nearClipPlane));
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0,Camera.main.nearClipPlane));
		Vector3 p3 = Camera.main.ViewportToWorldPoint(new Vector3(1,1,Camera.main.nearClipPlane));
		
		float width = (p2 - p1).magnitude;
		float height = (p3 - p2).magnitude;

		Vector2 dimensions = new Vector2(width,height);
		
		return dimensions;
	}
}

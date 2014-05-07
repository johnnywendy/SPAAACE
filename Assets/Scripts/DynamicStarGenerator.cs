using UnityEngine;
using System.Collections;

public class DynamicStarGenerator : MonoBehaviour {

	public GameObject star;
	public GameObject StarsObject;
	private Vector3 lastPos;
	private Vector2 dimensions;

	// Use this for initialization
	void Start () {
		dimensions = GetComponent<FollowTarget>().CalculateScreenSizeInWorldCoords();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastPos != transform.position && Vector3.Distance(lastPos,transform.position) > 1f) {
			Vector3 pos = new Vector3(transform.position.x+dimensions.x/4-(transform.position.x-lastPos.x),transform.position.y+dimensions.y/4-(transform.position.y-lastPos.y),5f);
			GameObject sta = (GameObject)Instantiate(star,pos,Quaternion.identity);
			sta.transform.parent = StarsObject.transform;
			lastPos = transform.position;
		}
	}
}

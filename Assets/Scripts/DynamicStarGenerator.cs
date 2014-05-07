using UnityEngine;
using System.Collections;

public class DynamicStarGenerator : MonoBehaviour {

	public GameObject star;
	public GameObject StarsObject;
	public bool vertical;

	private Vector3 lastPos;
	private Vector2 dimensions;
	private ArrayList points;
	private float length;

	// Use this for initialization
	void Start () {
		dimensions = GetComponent<FollowTarget>().CalculateScreenSizeInWorldCoords();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastPos != transform.position && Vector3.Distance(lastPos,transform.position) > 1f) {
			if (vertical) {
				if (transform.position.y-lastPos.y > 0.25f) {
					for (int i = 0; i < dimensions.x; i++) {
						if (Random.Range(0,7) > 5) {
							float xoff = Random.Range(-0.6f,0.6f);
							float yoff = Random.Range(-0.6f,0.6f);
							Vector3 pos = new Vector3(transform.position.x-(dimensions.x/2)+i+xoff,transform.position.y+(dimensions.y/2)+yoff,5f);
							GameObject sta = (GameObject)Instantiate(star,pos,Quaternion.identity);
							sta.transform.parent = StarsObject.transform;
							lastPos = transform.position;
						}
					}
				}
				if (transform.position.y-lastPos.y < -0.25f) {
					for (int i = 0; i < dimensions.x; i++) {
						if (Random.Range(0,7) > 5) {
							float xoff = Random.Range(-0.6f,0.6f);
							float yoff = Random.Range(-0.6f,0.6f);
							Vector3 pos = new Vector3(transform.position.x-(dimensions.x/2)+i+xoff,transform.position.y-(dimensions.y/2)+yoff,5f);
							GameObject sta = (GameObject)Instantiate(star,pos,Quaternion.identity);
							sta.transform.parent = StarsObject.transform;
							lastPos = transform.position;
						}
					}
				}
			}
			else {
				if (transform.position.x-lastPos.x < -0.25f) {
					for (int i = 0; i < dimensions.y; i++) {
						if (Random.Range(0,7) > 5) {
							float xoff = Random.Range(-0.6f,0.6f);
							float yoff = Random.Range(-0.6f,0.6f);
							Vector3 pos = new Vector3(transform.position.x-(dimensions.x/2)+xoff,transform.position.y-(dimensions.y/2)+i+yoff,5f);
							GameObject sta = (GameObject)Instantiate(star,pos,Quaternion.identity);
							sta.transform.parent = StarsObject.transform;
							lastPos = transform.position;
						}
					}
				}
				if (transform.position.x-lastPos.x > 0.25f) {
					for (int i = 0; i < dimensions.y; i++) {
						if (Random.Range(0,7) > 5) {
							float xoff = Random.Range(-0.6f,0.6f);
							float yoff = Random.Range(-0.6f,0.6f);
							Vector3 pos = new Vector3(transform.position.x+(dimensions.x/2)+xoff,transform.position.y-(dimensions.y/2)+i+yoff,5f);
							GameObject sta = (GameObject)Instantiate(star,pos,Quaternion.identity);
							sta.transform.parent = StarsObject.transform;
							lastPos = transform.position;
						}
					}
				}
			}
			lastPos = transform.position;
		}
	}
}

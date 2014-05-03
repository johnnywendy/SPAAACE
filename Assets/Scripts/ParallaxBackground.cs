using UnityEngine;
using System.Collections;

public class ParallaxBackground : MonoBehaviour
{
	public float ParallaxFactor = 10;
	private Transform player;
	private Vector3 last;
	public bool invert = true;
	
	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void FixedUpdate() {
		Vector3 now = player.position;
		if (invert) {
			float xpos = transform.position.x-((now.x - last.x)/ParallaxFactor);
			float ypos = transform.position.y-((now.y - last.y)/ParallaxFactor);
			transform.position = new Vector3(xpos,ypos,transform.position.z);
		}
		else {
			float xpos = transform.position.x+((now.x - last.x)/ParallaxFactor);
			float ypos = transform.position.y+((now.y - last.y)/ParallaxFactor);
			transform.position = new Vector3(xpos,ypos,transform.position.z);
		}
		last = now;
	}
}
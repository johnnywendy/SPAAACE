using UnityEngine;
using System.Collections;

public class CreatureFly : MonoBehaviour {

	private float CameraDist, angle;
	private Vector3 mousePos, direction, pos;
	private bool doGravity;
	private bool innerGravity;
	private ArrayList gravityCenters;
	private bool grounded;
	private Transform currentPlanet;
	private float currentGravity;
	
	public Transform landEffect;
	
	void Start() {
		CameraDist = Camera.main.transform.position.y - transform.position.y;
		gravityCenters = new ArrayList();
		float temp = Random.Range(0, 4);
		transform.position = new Vector2 (0, 0);
		if (temp == 0)
			rigidbody2D.AddForce (transform.up * 1500f);
		if (temp == 1)
			rigidbody2D.AddForce (transform.up * -1500f);
		if (temp == 2)
			rigidbody2D.AddForce (transform.right * 1500f);
		if (temp == 3)
			rigidbody2D.AddForce (transform.right * -1500f);
	}
	
	void FixedUpdate() {
		if (grounded) {
			currentGravity = 5+currentPlanet.localScale.x;
			if (rigidbody2D.velocity.x > currentGravity/16)
				rigidbody2D.AddForce(1f * currentGravity/1.4f * transform.right);
			else
				rigidbody2D.AddForce(1f * currentGravity * transform.right);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Border")
			transform.position = new Vector3(-transform.position.x,-transform.position.y,transform.position.z);
		if (other.tag == "Atmosphere") {
			gravityCenters.Remove(other.transform);
			if (gravityCenters.Count <= 0)
				doGravity = false;
		}
		if (other.tag == "Inner")
			innerGravity = false;
		if (other.tag == "Planet")
			grounded = false;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Atmosphere") {
			rigidbody2D.velocity = Vector3.Lerp(rigidbody2D.velocity,Vector3.zero,Time.deltaTime*20);
			gravityCenters.Add(other.transform);
			doGravity = true;
		}
		if (other.tag == "Inner")
			innerGravity = true;
		if (other.tag == "Planet") {
			grounded = true;
			Transform land = (Transform)Instantiate(landEffect, new Vector3(transform.position.x,transform.position.y,transform.position.z-10), Quaternion.identity);
			land.parent = transform;
			currentPlanet = other.transform;
		}
		if (other.tag == "Player") {
			other.GetComponent<Propulsion>().setBurnRate();
			Destroy(this.gameObject);
		}
	}
	
	public bool IsGrounded() {
		return doGravity;
	}
	
	void ApplyGravity() {
		foreach (Transform center in gravityCenters) {
			mousePos = Camera.main.WorldToScreenPoint(transform.position);
			mousePos.z = CameraDist;
			pos = Camera.main.WorldToScreenPoint(center.position);
			mousePos.x = mousePos.x - pos.x;
			mousePos.y = mousePos.y - pos.y;
			angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+270));
			//rigidbody2D.AddForce((-transform.up) * (.8f*center.localScale.x));
		}
	}
}
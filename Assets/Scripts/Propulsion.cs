using UnityEngine;
using System.Collections;

public class Propulsion : MonoBehaviour {

	public float maxSpeed;
	public float walkSpeed;

	private float CameraDist, angle;
	private Vector3 mousePos, direction, pos;
	private bool doGravity;
	private bool innerGravity;
	private ArrayList gravityCenters;
	private bool grounded;
	private Transform currentPlanet;
	private float currentGravity;

	public Transform smokeEffect;
	public Transform fireEffect;
	public Transform landEffect;

	void Start() {
		CameraDist = Camera.main.transform.position.y - transform.position.y;
		gravityCenters = new ArrayList();
	}

	void FixedUpdate() {
		if (grounded && Input.GetButton("Horizontal")) {
			currentGravity = 5+currentPlanet.localScale.x;
			if (rigidbody2D.velocity.x > currentGravity/16)
				rigidbody2D.AddForce(Input.GetAxis("Horizontal") * currentGravity/1.4f * transform.right);
			else
				rigidbody2D.AddForce(Input.GetAxis("Horizontal") * currentGravity * transform.right);
			rigidbody2D.AddForce((currentGravity/2 * -transform.up));
		}
		/*	if (rigidbody2D.velocity.x > currentGravity/8)
				rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime);
		}
		else if (grounded || innerGravity) {
			rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime);
		}*/
	}

	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetButton("Horizontal")) {
			if (rigidbody2D.velocity.x > currentGravity/8)
				rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime);
		}
		else if (grounded || innerGravity) {
			rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime);
		}
		if (doGravity)
			ApplyGravity();
		if(Input.GetMouseButton(0)) {
			mousePos = Input.mousePosition;
			mousePos.z = CameraDist;
			pos = Camera.main.WorldToScreenPoint(transform.position);
			mousePos.x = mousePos.x - pos.x;
			mousePos.y = mousePos.y - pos.y;
			angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle-270));
			rigidbody2D.AddForce((transform.up) * 10f);
			Transform particles = Instantiate(smokeEffect, new Vector3(transform.position.x,transform.position.y,transform.position.z+1), Quaternion.identity) as Transform;
			particles.gameObject.rigidbody2D.velocity = rigidbody2D.velocity;
			particles.gameObject.rigidbody2D.AddForce((-transform.up) * 350f);
			Transform particles2 = Instantiate(fireEffect, new Vector3(transform.position.x,transform.position.y,transform.position.z+1), Quaternion.identity) as Transform;
			particles2.gameObject.rigidbody2D.velocity = rigidbody2D.velocity;
			particles2.gameObject.rigidbody2D.AddForce((-transform.up) * 350f);
		}
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed && Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed) {
			rigidbody2D.velocity = Vector3.Lerp (rigidbody2D.velocity,Vector3.zero,Time.deltaTime*2);
		}
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
		if (other.tag == "Planet") { // && (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed/5 || Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed/5))
			grounded = true;
			Transform land = (Transform)Instantiate(landEffect, new Vector3(transform.position.x,transform.position.y,transform.position.z-10), Quaternion.identity);
			land.parent = transform;
			currentPlanet = other.transform;
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
			rigidbody2D.AddForce((-transform.up) * (5f+center.localScale.x));
			if (innerGravity)
				rigidbody2D.AddForce((-transform.up) * (center.localScale.x/1.333f));
		}
	}
}

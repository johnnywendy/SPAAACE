using UnityEngine;
using System.Collections;

public class Propulsion : MonoBehaviour {

	public float maxSpeed;
	public float walkSpeed;
	private float burnSpeed = 0.14f;
	private float breathSpeed = 0.015f;

	private float fuel = 100;
	private float oxygen = 100;
	private float CameraDist, angle;
	private Vector3 mousePos, direction, pos;
	private bool doGravity;
	private bool innerGravity;
	private ArrayList gravityCenters;
	private bool grounded;
	private Transform currentPlanet;
	private float currentGravity;
	private float cFuel;
	private float cOxygen;
	private Transform FuelMeter;
	private Transform OxygenMeter;

	public Transform smokeEffect;
	public Transform fireEffect;
	public Transform landEffect;

	void Start() {
		CameraDist = Camera.main.transform.position.y - transform.position.y;
		gravityCenters = new ArrayList();
		cFuel = fuel;
		cOxygen = oxygen;
		FuelMeter = GameObject.FindGameObjectWithTag("Fuel").transform;
		OxygenMeter = GameObject.FindGameObjectWithTag("Oxygen").transform;
	}

	void FixedUpdate() {
		if (grounded && Input.GetButton("Horizontal")) {
			currentGravity = 5+currentPlanet.localScale.x;
			if (rigidbody2D.velocity.x > currentGravity/16)
				rigidbody2D.AddForce(Input.GetAxis("Horizontal") * currentGravity/1.4f * transform.right);
			else
				rigidbody2D.AddForce(Input.GetAxis("Horizontal") * currentGravity * transform.right);
		}
		FuelMeter.localScale = new Vector3(cFuel/fuel * 3f,FuelMeter.localScale.y,FuelMeter.localScale.z);
		OxygenMeter.localScale = new Vector3(cOxygen/oxygen * 3f,OxygenMeter.localScale.y,OxygenMeter.localScale.z);
	}

	// Update is called once per frame
	void Update () {
		if (cOxygen < 0) {
			Destroy(this.gameObject);
		}
		if (grounded && Input.GetButton("Horizontal")) {
			if (rigidbody2D.velocity.x > currentGravity/8)
				rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime);
		}
		else if (grounded || innerGravity) {
			rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity,Vector2.zero,Time.deltaTime);
		}
		if (doGravity) {
			ApplyGravity();
			if (cOxygen < oxygen) {
				cOxygen += 1;
				OxygenMeter.position = new Vector3(OxygenMeter.position.x+(1*0.015f),OxygenMeter.position.y,OxygenMeter.position.z);
			}
			if (cFuel < fuel) {
				cFuel += burnSpeed*2;
				FuelMeter.position = new Vector3(FuelMeter.position.x+(burnSpeed*2*0.015f),FuelMeter.position.y,FuelMeter.position.z);
			}
		}
		if (!doGravity) {
			cOxygen -= breathSpeed;
			OxygenMeter.position = new Vector3(OxygenMeter.position.x-(breathSpeed*0.015f),OxygenMeter.position.y,OxygenMeter.position.z);
		}
		if(Input.GetMouseButton(0) && cFuel > 0) {
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = CameraDist;
			pos = transform.position;
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
			cFuel -= burnSpeed;
			FuelMeter.position = new Vector3(FuelMeter.position.x-(burnSpeed*0.015f),FuelMeter.position.y,FuelMeter.position.z);
		}
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed && Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed && !doGravity) {
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
			land.renderer.material = other.renderer.material;
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

using UnityEngine;
using System.Collections;

public class CreatureFly : MonoBehaviour {
	
	public float flySpeed = 1500f;
	public float maxSpeed;
	
	private float CameraDist, angle;
	private Vector3 mousePos, direction, pos;
	private bool doGravity;
	private bool innerGravity;
	private ArrayList gravityCenters;
	private bool grounded;
	private Transform currentPlanet;
	private float currentGravity;
	private Vector3 dir;
	private string action;
	private bool check;
	
	public Transform landEffect;
	
	void Start() {
		maxSpeed = Random.Range (2f,8f);
		CameraDist = Camera.main.transform.position.y - transform.position.y;
		gravityCenters = new ArrayList();
		float temp = Random.Range(0, 4);
		transform.position = new Vector2 (Random.Range(-50, -500), Random.Range(50, 500));
		if (temp == 0)
			rigidbody2D.AddForce (transform.up * flySpeed);
		if (temp == 1)
			rigidbody2D.AddForce (transform.up * flySpeed);
		if (temp == 2)
			rigidbody2D.AddForce (transform.right * flySpeed);
		if (temp == 3)
			rigidbody2D.AddForce (transform.right * flySpeed);
	}
	
	IEnumerator ChangeDirection(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		int change = (int)Random.Range(0,4);
		if (change > 1) {
			action = "turn";
			if (change == 2)
				dir = transform.right;
			if (change == 3)
				dir = -transform.right;
		}
		else
			action = "straight";
		float temp = Random.Range(0,1);
		check = false;
		StartCoroutine(ChangeDirection(temp));
	}
	
	// Update is called once per frame
	void Update () {
		if (action == "turn")
			rigidbody2D.AddForce(dir*flySpeed/Random.Range(25,125));
		else if (!check) {
			check = true;
			StartCoroutine(ChangeDirection(Random.Range(1,6)));
		}
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed && Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed && !doGravity)
			rigidbody2D.velocity = Vector3.Lerp (rigidbody2D.velocity,Vector3.zero,Time.deltaTime*2);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Border")
			transform.position = new Vector3(-transform.position.x,-transform.position.y,transform.position.z);
	}
}
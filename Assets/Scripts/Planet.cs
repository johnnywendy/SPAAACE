﻿using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	public bool inBounds = false;
	private bool start = true;

	// Use this for initialization
	void Start () {
		int modifier = 0, randomizer = Random.Range (0,10);
		if (randomizer < 5)
			modifier = Random.Range(-2,12);
		if (randomizer > 5 && randomizer < 8)
			modifier = Random.Range(2,16);
		if (randomizer > 8)
			modifier = Random.Range(8,22);
		transform.localScale = new Vector3(transform.localScale.x+modifier,transform.localScale.y+modifier,transform.localScale.z);
		transform.rotation = Quaternion.Euler(0f,0f,Random.Range(0,360));
		int planetType = Random.Range(0,6);
		if (planetType == 0)
			SetupMoon1();
		if (planetType == 1)
			SetupMoon2();
		if (planetType == 2)
			SetupGrass1();
		if (planetType == 3)
			SetupGrass2();
		if (planetType == 4)
			SetupSand1();
		if (planetType == 5)
			SetupIce1();
		StartCoroutine(VerifyInBounds(0.2f));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Border")
			inBounds = true;
		if (other.tag == "Atmosphere")
			Destroy (other.gameObject.transform.parent.gameObject);
		if (other.tag == "Player" && start)
			Destroy (gameObject);
	}

	IEnumerator VerifyInBounds(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		if (!inBounds)
			Destroy (gameObject);
		start = false;
		rigidbody2D.isKinematic = true;
	}

	void SetupMoon1() {
		renderer.material = Resources.Load("moon_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("moon_1", typeof(Texture)) as Texture;
	}

	void SetupMoon2() {
		renderer.material = Resources.Load("moon_border_2", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("moon_2", typeof(Texture)) as Texture;
	}

	void SetupGrass1() {
		renderer.material = Resources.Load("grass_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("grass_1", typeof(Texture)) as Texture;
	}

	void SetupGrass2() {
		renderer.material = Resources.Load("grass_border_2", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("grass_2", typeof(Texture)) as Texture;
	}

	void SetupSand1() {
		renderer.material = Resources.Load("sand_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("sand_1", typeof(Texture)) as Texture;
	}

	void SetupIce1() {
		renderer.material = Resources.Load("ice_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("ice_1", typeof(Texture)) as Texture;
		GetComponents<CircleCollider2D>()[0].sharedMaterial = Resources.Load("IcySurface", typeof(PhysicsMaterial2D)) as PhysicsMaterial2D;
	}
}

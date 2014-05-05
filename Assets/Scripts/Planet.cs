using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	public bool inBounds = false;
	private bool start = true;
	private ArrayList radiusPoints;
	private ArrayList radiusPoints2;
	private ArrayList children = new ArrayList();
	private float numPoints;
	private float numPoints2;
	private GameObject ItemsObject;
	private GameObject ResObject;

	// Use this for initialization
	void Start () {
		ItemsObject = GameObject.Find ("PlanetObjects");
		ResObject = GameObject.Find ("PlanetResources");
		//transform.Find("Planet Map Icon").localScale += transform.localScale;
		//transform.Find("Planet Map Icon").parent = GameObject.Find("PlanetMapIcons").transform;
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

		float radius = GetComponents<CircleCollider2D>()[0].radius*transform.localScale.x+1f;
		numPoints = Mathf.Floor((radius*4)/2)*2;
		float step = (Mathf.PI*2) / numPoints;
		float current = 0;
		radiusPoints = new ArrayList();
		for (int i = 0; i < numPoints; i++) {
			radiusPoints.Add(new Vector2(Mathf.Sin(current) * radius,Mathf.Cos(current) * radius));
			current += step;
		}

		float radius2 = GetComponents<CircleCollider2D>()[0].radius*transform.localScale.x+1.2f;
		numPoints2 = Mathf.Floor((radius*4)/2)*2;
		float step2 = (Mathf.PI*2) / numPoints;
		float current2 = 0;
		radiusPoints2 = new ArrayList();
		for (int i = 0; i < numPoints; i++) {
			radiusPoints2.Add(new Vector2(Mathf.Sin(current) * radius,Mathf.Cos(current) * radius));
			current += step;
		}

		if (planetType == 0)
			SetupMoon1();
		if (planetType == 1)
			SetupMoon2();
		if (planetType == 2)
			SetupSlime1();
		if (planetType == 3)
			SetupGrass2();
		if (planetType == 4)
			SetupSand1();
		if (planetType == 5)
			SetupIce1();
		StartCoroutine(VerifyInBounds(0.2f));
	}

	public void DestroySelf() {
		if (children.Count > 0)
			foreach (GameObject child in children)
				Destroy(child);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Border")
			inBounds = true;
		if (other.tag == "Atmosphere")
			other.gameObject.transform.parent.GetComponent<Planet>().DestroySelf();
		if (other.tag == "Player" && start)
			DestroySelf();
	}

	IEnumerator VerifyInBounds(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		if (!inBounds)
			DestroySelf();
		start = false;
		rigidbody2D.isKinematic = true;
	}

	void SetRotation(GameObject thing) {
		Vector3 mousePos, pos;
		float angle;
		mousePos = thing.transform.position;
		pos = transform.position;
		mousePos.x = mousePos.x - pos.x;
		mousePos.y = mousePos.y - pos.y;
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		thing.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle+270));
	}

	void SetScale(GameObject thing) {
		thing.transform.localScale = new Vector3(thing.transform.localScale.x/transform.localScale.x*2,thing.transform.localScale.y/transform.localScale.y*2,thing.transform.localScale.z/transform.localScale.z);
	}
	
	void SetupMoon1() {
		renderer.material = Resources.Load("moon_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("moon_1", typeof(Texture)) as Texture;
		int numCraters = (int)Random.Range(0,numPoints/4);
		for (int i = 0; i < numCraters; i++) {
			int index = (int)Random.Range(0,numPoints);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject Crater = (GameObject)Instantiate(Resources.Load("moonCrater1", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,2f),Quaternion.identity);
			children.Add(Crater);
			SetRotation(Crater);
			Crater.transform.parent = ItemsObject.transform;
		}
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}

	void SetupMoon2() {
		renderer.material = Resources.Load("moon_border_2", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("moon_2", typeof(Texture)) as Texture;
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}

	void SetupGrass1() {
		renderer.material = Resources.Load("grass_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("grass_1", typeof(Texture)) as Texture;
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}

	void SetupGrass2() {
		renderer.material = Resources.Load("grass_border_2", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("grass_2", typeof(Texture)) as Texture;
		int numTrees = (int)Random.Range(numPoints/6,numPoints/2);
		for (int i = 0; i < numTrees; i++) {
			int index = (int)Random.Range(0,numPoints);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject tree = (GameObject)Instantiate(Resources.Load("grassTree1", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,2f),Quaternion.identity);
			children.Add(tree);
			SetRotation(tree);
			tree.transform.parent = ItemsObject.transform;
		}
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}

	void SetupSand1() {
		renderer.material = Resources.Load("sand_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("sand_1", typeof(Texture)) as Texture;
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}

	void SetupIce1() {
		renderer.material = Resources.Load("ice_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("ice_1", typeof(Texture)) as Texture;
		GetComponents<CircleCollider2D>()[0].sharedMaterial = Resources.Load("IcySurface", typeof(PhysicsMaterial2D)) as PhysicsMaterial2D;
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}

	void SetupSlime1() {
		renderer.material = Resources.Load("slime_border_1", typeof(Material)) as Material;
		transform.FindChild("Texture").gameObject.renderer.material.mainTexture = Resources.Load("slime_1", typeof(Texture)) as Texture;
		GetComponents<CircleCollider2D>()[0].sharedMaterial = Resources.Load("BouncySurface", typeof(PhysicsMaterial2D)) as PhysicsMaterial2D;
		int numRes = (int)Random.Range(0,numPoints2/6);
		for (int i = 0; i < numRes; i++) {
			int index = (int)Random.Range(0,numPoints2);
			Vector2 pos = (Vector2)radiusPoints[index];
			GameObject res = (GameObject)Instantiate(Resources.Load("Resource", typeof(GameObject)),new Vector3(transform.position.x+pos.x,transform.position.y+pos.y,-3f),Quaternion.identity);
			children.Add(res);
			SetRotation(res);
			res.transform.parent = ResObject.transform;
		}
	}
}

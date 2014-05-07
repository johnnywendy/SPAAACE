using UnityEngine;
using System.Collections;

public class GenerateStars : MonoBehaviour {
	
	public GameObject star;
	public int circumMod;
	public float percentFull;
	public GameObject StarsObject;

	private bool[] checks;
	private ArrayList ScrambledList;
	private int index;
	private float numOfPlanets;

	// Use this for initialization
	void Start () {
		GameObject border = GameObject.FindGameObjectWithTag("Border");
		float radius = border.GetComponent<CircleCollider2D>().radius;
		float numPoints = Mathf.Floor((radius/circumMod)/2)*2;
		float step = (Mathf.PI*2) / numPoints;
		float current = 0;
		ArrayList points1 = new ArrayList(),points2 = new ArrayList();
		for (int i = 0; i < numPoints; i++) {
			if (i < numPoints/2)
				points1.Add(new Vector2(Mathf.Sin(current) * radius,Mathf.Cos(current) * radius));
			else
				points2.Add(new Vector2(Mathf.Sin(current) * radius,Mathf.Cos(current) * radius));
			current += step;
		}
		ArrayList vertLines = new ArrayList(), horizLines = new ArrayList();
		float A, B, C;
		for (int i = 1, j = points2.Count-1; i < points1.Count; i++, j--) {
			Vector2 p1 = (Vector2)points1[i], p2 = (Vector2)points2[j];
			A = p1.y - p2.y;
			B = p2.x - p1.x;
			C = p1.x*p2.y - p2.x*p1.y;
			horizLines.Add(new Vector3(A,B,C));
			A = p1.x - p2.x;
			B = p2.y - p1.y;
			C = p1.y*p2.x - p2.y*p1.x;
			vertLines.Add(new Vector3(A,B,C));
		}
		ArrayList intersections = new ArrayList();
		for (int i = 0; i < vertLines.Count; i++) {
			Vector3 l1 = (Vector3)vertLines[i];
			for (int j = 0; j < horizLines.Count; j++) {
				Vector3 l2 = (Vector3)horizLines[j];
				float D, Dx, Dy;
				D = l1.x * l2.y-l1.y * l2.x;
				Dx = l1.z * l2.y-l1.y * l2.z;
				Dy = l1.x * l2.z-l1.z * l2.x;
				if (D != 0f)
					intersections.Add(new Vector2((Dx/D),(Dy/D)));
			}
		}
		checks = new bool[intersections.Count];
		ScrambledList = new ArrayList();
		while (intersections.Count > 0) {
			index = Random.Range(0,intersections.Count);
			ScrambledList.Add(intersections[index]);
			intersections.RemoveAt(index);
		}
		numOfPlanets = 1f;
		index = 0;
		StartCoroutine(GenerateStar(0.01f));
		border.GetComponent<CircleCollider2D>().radius += 75;
	}

	IEnumerator GenerateStar(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		if (Random.Range(1,8) == 1) {
			if (checks[index] != true) {
				Vector2 point = (Vector2)ScrambledList[index];
				GameObject sta = (GameObject)Instantiate(star,new Vector3(point.x+Random.Range(-10,10), point.y+Random.Range(-10,10), 30f),Quaternion.identity);
				sta.transform.parent = StarsObject.transform;
				numOfPlanets++;
				checks[index] = true;
			}
		}
		index++;
		if (index > ScrambledList.Count-1) index = 0;
		if (numOfPlanets/ScrambledList.Count < percentFull/100)
			StartCoroutine(GenerateStar(0.05f));
	}
}

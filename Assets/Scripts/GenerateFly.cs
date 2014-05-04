using UnityEngine;
using System.Collections;

public class GenerateFly : MonoBehaviour {

	public int num = 5;

	public GameObject creature;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < num; i++) {
			Instantiate(creature);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

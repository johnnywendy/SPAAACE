using UnityEngine;
using System.Collections;

public class CreatureScript : MonoBehaviour {

	public int typeInt;
	//if 0, defaults to random type

	// Use this for initialization
	void Start () {
		if (typeInt == 0) {
			typeInt = Random.Range(0, 6);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(typeInt == 1) {
			rigidbody2D.mass = 100;
		}
	}


}

using UnityEngine;
using System.Collections;

public class InventoryButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ShowHide(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (GetComponent<MeshRenderer>().enabled) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit)) {
					if (hit.collider.name != "Menu")
						if (hit.collider.name != "ItemBox")
							ShowHide(false);
				}			
				else
					ShowHide(false);
			}
		}
	}

	public void OnClick() {
		ShowHide(true);
	}

	void ShowHide(bool show) {
		GetComponent<MeshRenderer>().enabled = show;
		GetComponent<BoxCollider>().enabled = show;
		foreach (MeshRenderer child in transform.GetComponentsInChildren<MeshRenderer>())
			child.enabled = show;
		foreach (BoxCollider child in transform.GetComponentsInChildren<BoxCollider>())
			child.enabled = show;
	}
}

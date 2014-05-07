using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.tag == "Button") {
					if (hit.collider.name == "InventoryButton") {
						GameObject.Find("Menu").GetComponent<InventoryButton>().OnClick();
					}
				}
			}
		}
	}
}
